using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCube : MonoBehaviour {


    private Ray ray;
    private Vector3 vec1;
	// Use this for initialization
	void Start () {
        vec1 = new Vector3(0.1f, 0f, 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += vec1;
        if (!Physics.Raycast(transform.position , -Vector3.up))
        {
            vec1 = -vec1;
        }
	}
}
