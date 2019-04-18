using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {

    public int current_Triggered;

    public void Start()
    {
        current_Triggered = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        current_Triggered++;
    }

    public void OnTriggerExit(Collider other)
    {
        current_Triggered--;
    }

    //Этот скрипт не отключается после строительства, нужно пофиксить
}
