using UnityEngine;


namespace BuildSpace
{
    public class TransformBuilding : MonoBehaviour
    {
        private Ray ray;
        private RaycastHit hit;
        private Touch touch;

        private Vector3 hitPosition;
        private bool firsTouchFlag;
        private bool checkPC;

        private void Start()
        {
            gameObject.transform.position = new Vector3(0f, 0.5f, 0f);
        }

        private void Update()
        {
            {
                if (Input.GetMouseButtonDown(0)) checkPC = true;
                if (Input.GetMouseButtonUp(0))
                {
                    checkPC = false;
                    firsTouchFlag = false;
                }
                if (!checkPC) return;
            }

            if (!Physics.Raycast(gameObject.transform.position + transform.up, -Vector3.up))
            {
                // gameObject.transform.position -= new Vector3(0.01f * hit.point.x, 0f , 0.01f * hit.point.z);
                return;
            }


            {
                Vector3 position = Input.mousePosition;
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
                            gameObject.transform.position = new Vector3(hit.point.x + gameObject.transform.position.x - hitPosition.x, 0.5f, hit.point.z + gameObject.transform.position.z - hitPosition.z);
                            hitPosition = hit.point;
                        }
                    }
                }
            }
        }
    }
}