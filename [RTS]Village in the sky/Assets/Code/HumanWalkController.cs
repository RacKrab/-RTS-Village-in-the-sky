using UnityEngine;
using UnityEngine.AI;

public class HumanWalkController : MonoBehaviour {

    public NavMeshAgent agent;
    public GameObject house, house2 , house3;

    public void Start()
    {
        house3 = house;
    }

    void Update ()
    {
                //ray = cam.ScreenPointToRay(touch.position);
                //ray = cam.ScreenPointToRay(house.transform.position);
                agent.SetDestination(house3.transform.position);
                if (agent.transform.position.x == house3.transform.position.x)
                {
                    if (house3 == house)
                    {
                        Debug.Log("1");
                        house3 = house2;
                    }
                    else
                    {
                        Debug.Log("2");
                        house3 = house;
                    }
            }
	}
}
