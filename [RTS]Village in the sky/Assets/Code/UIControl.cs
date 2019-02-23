using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{

    public GameObject DefaulfUI;
    public GameObject BuildUI;
    public GameObject BuildScript;
	// Use this for initialization
    // Переписать функции через OnEnable

	void Start ()  {
        DefaulfUI.SetActive(true);
        BuildUI.SetActive(false);
        BuildScript.SetActive(false);
    }

    public void StartBuild()
    {
        DefaulfUI.SetActive(false);
        BuildUI.SetActive(true);
        BuildScript.SetActive(true);
    }

    public void EndBuilding()
    {
        DefaulfUI.SetActive(true);
        BuildUI.SetActive(false);
        BuildScript.SetActive(false);
    }
}
