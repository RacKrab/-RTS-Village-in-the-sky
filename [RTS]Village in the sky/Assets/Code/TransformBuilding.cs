using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BuildSpace
{
    public class TransformBuilding : MonoBehaviour
    {
        private Ray ray;
        private RaycastHit hit;
        private Touch touch;

        private bool firstTouchFlag;
        private bool stopReadingTouch;

        private Vector3 shiftPositionVector;
        private Vector3 secondFingerPosition;
        private Vector3 firstFingerPosition;

        private void Start()
        {
            gameObject.transform.position = new Vector3(0f,0.5f,0f);
        }

        private void Update()
        {
            if (Input.touchCount == 1)
            {
                touch = Input.GetTouch(0);
                ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.name == gameObject.name)
                    {
                        if (!firstTouchFlag)
                        {
                            firstFingerPosition = hit.point;
                            firstTouchFlag = true;
                        }
                        else
                        {
                            secondFingerPosition = hit.point;
                            shiftPositionVector = secondFingerPosition - firstFingerPosition;
                            shiftPositionVector = new Vector3(shiftPositionVector.x, 0f, shiftPositionVector.z);
                            gameObject.transform.localPosition += shiftPositionVector;
                            firstFingerPosition = hit.point;
                        }
                    }
                    else
                    {
                        firstTouchFlag = false;
                    }
                }
            }
        }
    }

}