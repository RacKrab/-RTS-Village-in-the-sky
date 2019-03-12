using UnityEngine;

namespace BuildSpace
{
    public class BuildingController : MonoBehaviour
    {
        public static byte Rotate { get; set; } // Возможно стоит что-то сделать с этой переменной
        public static byte Status { get; set; }

        /// <summary>
        /// 0 - off
        /// 1 - first call function BuildingNumber
        /// 2 - end building and call funcrion EndBuild
        /// 3...9 - call ChangeMaterial
        /// 10 - call ReBakeNavmesh
        /// 
        /// 255 - end of all(if we call 255 , then all ends)
        /// </summary>

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
            currentObject = Instantiate(buildings[count], new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        }

        public void EndBuild()
        {
            if (Status == 0) return;
            Status = 2;
            currentObject.GetComponent<TransformBuilding>().enabled = false;
            currentObject = null;
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
            Status = 255;
        }
    }
}
