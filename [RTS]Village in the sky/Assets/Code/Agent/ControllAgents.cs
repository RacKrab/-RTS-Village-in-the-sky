using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace BuildSpace
{
    public class ControllAgents : MonoBehaviour
    {

        public NavMeshAgent[] agent;
        private Ray ray;
        private RaycastHit hit;
        private Vector2 saveVector;

        // Use this for initialization
        void Start()
        {

        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.name == "UI")
                    {
                        Debug.Log("It's UI");
                        return;
                    }
                    saveVector = hit.point;
                    for (int i = 0; i < agent.Length; i++)
                    {
                        agent[i].SetDestination(hit.point);
                    }
                }
            }
            if (ReBakeNavMesh.ReBake)
            {
                for(int i = 0; i < agent.Length; i++)
                {
                    agent[i].SetDestination(saveVector);
                }
                ReBakeNavMesh.ReBake = false;
            }
        }
    }

}