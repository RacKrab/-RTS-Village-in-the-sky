using UnityEngine;

namespace BuildSpace
{
    public class LocalChangeMaterial : MonoBehaviour
    {
        private Material material;
        private bool[] stopAfterFirst;

        private void Start()
        {
            material = Resources.Load("ThatchRoof", typeof(Material)) as Material;
            stopAfterFirst = new bool[2];
        }

        void Update()
        {
            if (!stopAfterFirst[0] && (BuildingController.Status == 2 || BuildingController.Status == 3 || BuildingController.Status == 4 || BuildingController.Status == 5 || 
                BuildingController.Status == 6 || BuildingController.Status == 7 || BuildingController.Status == 8 || BuildingController.Status == 9))
            {
                gameObject.GetComponent<MeshRenderer>().material = material;
                BuildingController.Status++;
                stopAfterFirst[0] = true;
                return;
            }
            else if(stopAfterFirst[0] && !stopAfterFirst[1])
            {
                stopAfterFirst[1] = true;
                Invoke("StartNextStage" , 0.1f);
            }
        }

        private void StartNextStage()
        {
            BuildingController.Status = 10;
            gameObject.GetComponent<LocalChangeMaterial>().enabled = false;
        }
    }
}
