using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour {

    public GameObject gameObject;
    private RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray , out hit))
        {
            if(hit.collider.name == "Island" && hit.point.y >=0.5f)
            {
                gameObject.transform.position = hit.point;
            }
        }
	}
}
