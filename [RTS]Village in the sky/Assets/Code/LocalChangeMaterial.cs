using UnityEngine;

namespace BuildSpace
{
    public class LocalChangeMaterial : MonoBehaviour
    {
        private Material material;
        private bool stopWork;

        private void Start()
        {
            material = Resources.Load("ThatchRoof", typeof(Material)) as Material;
        }

        void Update()
        {
            if (BuildingController.StopFlag && !stopWork)
            {
                gameObject.GetComponent<MeshRenderer>().material = material;
                stopWork = true;
            }
        }
    }
}
