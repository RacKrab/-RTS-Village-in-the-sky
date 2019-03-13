using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour {
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("NO, stop it");
    }
}
