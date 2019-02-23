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
        // private Input touch;
        //Всю эту залупу нужно начисто переписывать, говнокод

        //Нужно привязывать позицию дома к позиции пальца, чтобы дом не мог выскользнуть из под мыши

        //Переписать скрипт без всяких трансформ позишон и прочьего говна
        //Всю эту залупу нужно начисто переписывать, говнокод
        private Vector3 hitPosition;
        private Vector3 oldPosition;
        private bool firsTouchFlag;
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
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.name == gameObject.name)
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
                oldPosition = gameObject.transform.position;
            }



            //if(Physics.Raycast(ray, out hit))
            //{
            //    if (hit.collider.name != gameObject.name) // Это что за залупа ? Нужно понять, как работает и нахуя здесь эта проверка
            //    {
            //        Debug.Log("1");
            //        position = gameObject.transform.localPosition;
            //        firstTouchFlag = false;
            //        return;
            //    }
            //}

            //if (Physics.Raycast(position + transform.up, -Vector3.up, out hitBottom))
            //{
            //    Debug.Log(hitBottom.collider.name);
            //    if (hitBottom.collider.name != "Island") Debug.Log("3");

            //    if (!firstTouchFlag)
            //    {
            //        firstFingerPosition = hit.point;
            //        firstTouchFlag = true;
            //        position = gameObject.transform.localPosition;
            //        return;
            //    }
            //}

            //secondFingerPosition = hit.point;
            //shiftPositionVector = secondFingerPosition - firstFingerPosition;
            //shiftPositionVector = new Vector3(shiftPositionVector.x, 0f, shiftPositionVector.z);
            //gameObject.transform.position += shiftPositionVector;
            //firstFingerPosition = hit.point;
        } // VERSION FOR PC INSERT

    }

}