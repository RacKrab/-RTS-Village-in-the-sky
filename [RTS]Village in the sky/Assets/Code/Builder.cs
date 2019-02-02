using UnityEngine;
using System.Collections;

namespace BuildSpace
{
    public class Builder : MonoBehaviour
    {
        public static bool StopFlag { get; set; } // Переменная отвечает за изменение материала после постройки здания.
        public bool IsOff { get; set; } // В том случае, если переменная false, в методе update начинается отслеживание положения пальца и перемещение соответствующего строения за ним. 
                                        //true - в методе update ничего не происходит
        public GameObject[] buildings; // Массив содержит все строения

        private GameObject currentBuildingObject; // текущий объет

        private Ray ray;
        private RaycastHit hit;
        private Touch touch;
        //private int СurrentBuildingNumber { get; set; }

        void Start()
        {
            StopFlag = false;
            //СurrentBuildingNumber = 0;
            IsOff = true;
        }

        void Update()
        {
            if (IsOff)
            {
                return;
            }
            else
            {
                if (Input.touchCount >= 1)
                {
                    touch = Input.GetTouch(0);
                }

                ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.name == "Island" && hit.point.y >= 0.5f)
                    {
                        currentBuildingObject.transform.position = hit.point;
                    }
                }
            }
        }

        public void BuildingNumber(int count)
        {
            //СurrentBuildingNumber = count;
            currentBuildingObject = Instantiate(buildings[count], new Vector3(0f, 0.5f, 0f), Quaternion.identity); // Выбор здания, которое необходимо построить
            //touch.phase = TouchPhase.Canceled;
            IsOff = false; // Update начинает отслеживать положение пальца
        }

        public void EndBuild()
        {
            IsOff = true; // Update перестает создавать лучи
            StopFlag = true; // Запускается смена материала
            currentBuildingObject = null; // На всякий случай зануляем ссылку на объект, на самом деле эту строку можно удалить


            Invoke("WaitChangeMaterials", 0.1f);
            //StartCoroutine(WaitChangeMaterials());
        }

        //IEnumerator WaitChangeMaterials()
        //{
        //    yield return new WaitForSeconds(0.1f);
        //    StopFlag = false;
        //}

            public void WaitChangeMaterials()
        {
            StopFlag = false;
        }

        private void BuildCreate()
        {
            currentBuildingObject.transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
            currentBuildingObject = null;
            touch.phase = TouchPhase.Canceled;
        }
    }
}
