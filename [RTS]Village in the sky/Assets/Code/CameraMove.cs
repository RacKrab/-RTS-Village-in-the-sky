using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    private Camera cam;

    private float defaultRotaton;

    private float rotation;

    private float xRotation;
    private float zRotation;

    private float xPosition;
    private float zPosition;

    private float zDeltaPosition;
    private float xDeltaPosition;

    private bool IsMove;

   // private Vector2 currentPosition;

    private Vector2 deltaVector;

    // Use this for initialization
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();



        xRotation = cam.transform.rotation.eulerAngles.x;
        zRotation = cam.transform.rotation.eulerAngles.z;
        rotation = cam.transform.rotation.eulerAngles.y;
        defaultRotaton = cam.transform.rotation.eulerAngles.y;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsMove = true;

            zPosition = Input.mousePosition.x;
            xPosition = Input.mousePosition.y;

            //currentPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsMove = false;
        }

        if(IsMove)
        {
            zDeltaPosition = zPosition - Input.mousePosition.x;
            xDeltaPosition = xPosition - Input.mousePosition.y;

            zDeltaPosition = zDeltaPosition * 1/(1 - (defaultRotaton - zRotation));
            xDeltaPosition = xDeltaPosition * 1/(1 - (defaultRotaton - xRotation));



            //deltaVector = currentPosition - (Vector2)Input.mousePosition;
            Debug.Log(deltaVector);
            cam.transform.position = new Vector3(cam.transform.position.x + xDeltaPosition*0.1f,cam.transform.position.y, cam.transform.position.z + zDeltaPosition * 0.1f);
            //currentPosition = Input.mousePosition;
            zPosition = Input.mousePosition.x;
            xPosition = Input.mousePosition.y;


        }
    }

    public void Rotate(bool Side) // true - rotate to right // false - rotate to left
    {
        if (Side)
        {

            rotation += 30f;
            cam.transform.rotation = Quaternion.Euler(xRotation, rotation, zRotation);
            return;
        }
        if (!Side)
        {
            rotation -= 30f;
            cam.transform.rotation = Quaternion.Euler(xRotation, rotation, zRotation);
            return;
        }
    }
}
