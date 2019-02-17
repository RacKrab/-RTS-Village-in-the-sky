using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour {

    public GameObject DefaulfUI;
    public GameObject BuildUI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartBuild()
    {
        DefaulfUI.SetActive(false);
        BuildUI.SetActive(true);
    }

    public void AcceptBuildChangesAndEnd()
    {
        DefaulfUI.SetActive(true);
        BuildUI.SetActive(false);
    }
}
