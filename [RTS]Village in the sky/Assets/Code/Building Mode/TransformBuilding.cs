using UnityEngine;


namespace BuildSpace
{
    public class TransformBuilding : MonoBehaviour
    {
        private Ray ray;
        private RaycastHit hit;
        private Vector3 hitPosition;
        private Transform savePosition;

        private bool firsTouchFlag;
        private bool checkPC;
        private float rotation;

        private void Start()
        {
            gameObject.transform.position = new Vector3(0f, 0.5f, 0f);
            savePosition = gameObject.transform;
            rotation = 0f;
        }

        private void Update()
        {

            {
                if (BuildingController.Rotate == 1)
                {
                    rotation -= 30f; // коэффициент поворота
                    savePosition.localRotation = Quaternion.Euler(0f, rotation, 0f);
                    BuildingController.Rotate = 0;
                    checkPC = false;
                    return;
                }

                if (BuildingController.Rotate == 2)
                {
                    rotation += 30f; // коэффициент поворота
                    savePosition.localRotation = Quaternion.Euler(0f, rotation, 0f);
                    BuildingController.Rotate = 0;
                    checkPC = false;
                    return;
                }
            }

            {
                if (Input.GetMouseButtonDown(0)) checkPC = true;
                if (Input.GetMouseButtonUp(0))
                {
                    checkPC = false;
                    firsTouchFlag = false;
                }
                if (!checkPC) return;
            }

            {
                if (!Physics.Raycast(savePosition.position + transform.up, -Vector3.up))
                {
                    savePosition.position -= new Vector3(0.2f * hit.point.x, 0f, 0.2f * hit.point.z);
                    return;
                }
            }


            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.name == gameObject.name)
                    {
                        if (!firsTouchFlag)
                        {
                            hitPosition = hit.point;
                            firsTouchFlag = true;
                        }
                        else
                        {
                            savePosition.position = new Vector3(hit.point.x + savePosition.position.x - hitPosition.x, 0.5f, hit.point.z + savePosition.position.z - hitPosition.z);
                            hitPosition = hit.point;
                        }
                    }
                }
            }
        }
    }
}