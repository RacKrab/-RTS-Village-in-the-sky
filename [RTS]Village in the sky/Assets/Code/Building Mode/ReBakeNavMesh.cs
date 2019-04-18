using UnityEngine;
using UnityEngine.AI;



namespace BuildSpace
{
    public class ReBakeNavMesh : MonoBehaviour
    {
        private NavMeshSurface _navmeshsurface;
        public static bool ReBake { get; set; }

        void Start()
        {
            _navmeshsurface = gameObject.GetComponent<NavMeshSurface>();
            _navmeshsurface.RemoveData();
            _navmeshsurface.BuildNavMesh();
        }

        void Update()
        {
            if (BuildingController.Status == 10)
            {
                _navmeshsurface.RemoveData();
                _navmeshsurface.BuildNavMesh();
                BuildingController.Status = 255;
                ReBake = true;
            }
        }
    }

}