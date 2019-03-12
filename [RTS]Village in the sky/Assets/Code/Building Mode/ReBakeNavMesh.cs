using UnityEngine;
using UnityEngine.AI;



namespace BuildSpace
{
    public class ReBakeNavMesh : MonoBehaviour
    {
        private NavMeshSurface _navmeshsurface;

        void Start()
        {
            _navmeshsurface = gameObject.GetComponent<NavMeshSurface>();
        }

        void Update()
        {
            if (BuildingController.Status == 10)
            {
                _navmeshsurface.RemoveData();
                _navmeshsurface.BuildNavMesh();
                BuildingController.Status = 255;
            }
        }
    }

}