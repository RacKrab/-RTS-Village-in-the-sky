using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BuildSpace
{
    public class TransformBuilding : MonoBehaviour
    {
        private Ray ray;
        private RaycastHit hit;
        private RaycastHit hitBottom;
        private Touch touch;
        // private Input touch;
        //Всю эту залупу нужно начисто переписывать, говнокод

        //Нужно привязывать позицию дома к позиции пальца, чтобы дом не мог выскользнуть из под мыши

        //Переписать скрипт без всяких трансформ позишон и прочьего говна
        //Всю эту залупу нужно начисто переписывать, говнокод



        private bool firstTouchFlag;
        private bool stopReadingTouch;
        private Vector3 position;

        private Vector3 shiftPositionVector;
        private Vector3 secondFingerPosition;
        private Vector3 firstFingerPosition;


        private bool checkPC;

        private void Start()
        {
            gameObject.transform.position = new Vector3(0f, 0.5f, 0f);
        }

        //private void Update()
        //{
        //    if (Input.touchCount == 1)
        //    {
        //        Debug.Log("111");
        //        touch = Input.GetTouch(0);
        //        ray = Camera.main.ScreenPointToRay(touch.position);

        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            if (hit.collider.name == gameObject.name)
        //            {
        //                if (!firstTouchFlag)
        //                {
        //                    firstFingerPosition = hit.point;
        //                    firstTouchFlag = true;
        //                }
        //                else
        //                {
        //                    secondFingerPosition = hit.point;
        //                    shiftPositionVector = secondFingerPosition - firstFingerPosition;
        //                    shiftPositionVector = new Vector3(shiftPositionVector.x, 0f, shiftPositionVector.z);
        //                    gameObject.transform.localPosition += shiftPositionVector;
        //                    firstFingerPosition = hit.point;
        //                }
        //            }
        //            else
        //            {
        //                firstTouchFlag = false;
        //            }
        //        }
        //    }
        //} // VERSION FOR MOBILE INSERT

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) checkPC = true;
            if (Input.GetMouseButtonUp(0)) checkPC = false;

            if (!checkPC) return;

            position = Input.mousePosition;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == gameObject.name) // Это что за залупа ? Нужно понять, как работает и нахуя здесь эта проверка
                {
                    position = gameObject.transform.position;
                    if (Physics.Raycast(position + transform.up, -Vector3.up, out hitBottom))
                    {
                        if (hitBottom.collider.name == "Island")
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
                                gameObject.transform.position += shiftPositionVector;
                                firstFingerPosition = hit.point;
                            }
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    firstTouchFlag = false;
                }

                //Переписать скрипт без всяких трансформ позишон и прочьего говна
                //Всю эту залупу нужно начисто переписывать, говнокод

            }
        } // VERSION FOR PC INSERT


        //private void Update()
        //{
        //    Vector3 position;

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Debug.Log("1");
        //        checkPC = true;
        //    }

        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        checkPC = false;
        //    }

        //    if (checkPC)
        //    {
        //        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    }
        //    else
        //    {
        //        return;
        //    }

        //    position = gameObject.transform.position;

        //    if (Physics.Raycast(ray, out hit) && Physics.Raycast(position + transform.up, -Vector3.up, out hitBottom))
        //    {
        //        Debug.Log("2");
        //        if (hit.collider.name != gameObject.name) firstTouchFlag = false; // Это что за залупа ? Нужно понять, как работает и нахуя здесь эта проверка
        //        if (hitBottom.collider.name == "Island")
        //        {
        //            if (!firstTouchFlag)
        //            {
        //                firstFingerPosition = hit.point;
        //                firstTouchFlag = true;
        //            }
        //            else
        //            {
        //                secondFingerPosition = hit.point;
        //                shiftPositionVector = secondFingerPosition - firstFingerPosition;
        //                shiftPositionVector = new Vector3(shiftPositionVector.x, 0f, shiftPositionVector.z);
        //                gameObject.transform.position += shiftPositionVector;
        //                firstFingerPosition = hit.point;
        //            }
        //        }

        //    }
        //} // VERSION FOR PC INSERT


        //private void Update()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        checkPC = true;
        //    }
        //    else if (Input.GetMouseButtonUp(0))
        //    {
        //        checkPC = false;
        //    }
        //    if (checkPC)
        //    {
        //        position = Input.mousePosition;
        //        //touch = Input.mousePosition;
        //        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            if (hit.collider.name == gameObject.name) // Это что за залупа ? Нужно понять, как работает и нахуя здесь эта проверка
        //            {
        //                position = gameObject.transform.position;
        //                if (Physics.Raycast(position + transform.up, -Vector3.up, out hitBottom))
        //                {
        //                    if (hitBottom.collider.name == "Island")
        //                    {
        //                        if (!firstTouchFlag)
        //                        {
        //                            firstFingerPosition = hit.point;
        //                            firstTouchFlag = true;
        //                        }
        //                        else
        //                        {
        //                            secondFingerPosition = hit.point;
        //                            shiftPositionVector = secondFingerPosition - firstFingerPosition;
        //                            shiftPositionVector = new Vector3(shiftPositionVector.x, 0f, shiftPositionVector.z);
        //                            gameObject.transform.position += shiftPositionVector;
        //                            firstFingerPosition = hit.point;
        //                        }
        //                    }
        //                }
        //                else
        //                {

        //                }
        //            }
        //            else
        //            {
        //                firstTouchFlag = false;
        //            }

        //            //Переписать скрипт без всяких трансформ позишон и прочьего говна
        //            //Всю эту залупу нужно начисто переписывать, говнокод

        //        }
        //    }
        //} // VERSION FOR PC INSERT
    }

}