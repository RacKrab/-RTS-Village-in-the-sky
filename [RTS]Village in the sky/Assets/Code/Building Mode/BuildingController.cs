using UnityEngine;

namespace BuildSpace
{
    public class BuildingController : MonoBehaviour
    {
        public static byte Rotate { get; set; } // Возможно стоит что-то сделать с этой переменной

        /// <summary>
        /// Status характеризует текущее состояние "Строительства".
        /// При создании любого стандартного здания необходимо исполнить некоторый алгоритм действий, который состоит из нескольких стандартных действий.
        /// 0 - off
        /// 1 - first call function BuildingNumber
        /// 2 - end building and call funcrion EndBuild
        /// 3...9 - call ChangeMaterial
        /// 10 - call ReBakeNavmesh
        /// 
        /// 255 - end of all(if we call 255 , then all ends)
        /// </summary>
        public static byte Status { get; set; }//Нужно переименовать статус в стате
        private GameObject currentObject;

        public GameObject[] buildings; // Массив всех строений

        public void Update()
        {
            if(Status == 255)
            {
                currentObject = null;
                Status = 0;
            }
        }

        public void BuildingNumber(int count)
        {
            if (Status !=0) return;
            Status = 1;
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.point);
                    currentObject = Instantiate(buildings[count], hit.point, Quaternion.identity);
                    StateClass.current_Object = currentObject;
                    return;
                }
                //В этом месте происходит троллинг. При выходе за границы карты интерфейс переключается, но объект не создается, в итоге хуета.
            }
        }

        public void EndBuild()
        {
            if (Status == 0) return;
            Status = 2;
            currentObject.GetComponent<TransformBuilding>().enabled = false;
            currentObject = null;
            StateClass.current_Object = null;
        }

        public void ChangeRotate(bool side)
        {
            if (side)
            {

                Rotate = 1;
            }
            else
            {
                Rotate = 2;
            }
        }

        public void CanselBuilding()
        {
            if (currentObject == null) return;
            Destroy(currentObject);
            currentObject = null;
            StateClass.current_Object = null;
            Status = 255;
        }
    }
}
