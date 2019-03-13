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

    private float xLocalPosition;
    private float zLocalPosition;

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
            return;

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

            Debug.Log(zDeltaPosition + "///" + xDeltaPosition);

            zLocalPosition = zDeltaPosition;
            xLocalPosition = xDeltaPosition;



            //zDeltaPosition *=0.05f;
            zDeltaPosition = ((zLocalPosition * Mathf.Sin(360 - rotation)) + (xLocalPosition * Mathf.Cos(rotation))) * Time.deltaTime * 4f;
            xDeltaPosition = ((xLocalPosition * Mathf.Sin(rotation - 360)) + (zLocalPosition * Mathf.Cos(rotation))) * Time.deltaTime * 4f;
            //zDeltaPosition = ((xDeltaPosition * Mathf.Cos(rotation))) * Time.deltaTime;
            //xDeltaPosition = ((zDeltaPosition * Mathf.Cos(rotation)) + (xDeltaPosition * Mathf.Sin(rotation))) * Time.deltaTime;
            //xDeltaPosition = ((zDeltaPosition * Mathf.Cos(rotation))) * Time.deltaTime;

            //xDeltaPosition = ((zDeltaPosition * Mathf.Cos(rotation))) * Time.deltaTime;



            //deltaVector = currentPosition - (Vector2)Input.mousePosition;
            //Debug.Log(deltaVector);
            cam.transform.position = new Vector3(cam.transform.position.x + xDeltaPosition, cam.transform.position.y, cam.transform.position.z + zDeltaPosition);
            //currentPosition = Input.mousePosition;
            zPosition = Input.mousePosition.x;
            xPosition = Input.mousePosition.y;


        }
    }

    public void Rotate(bool Side) // true - rotate to right // false - rotate to left
    {
        if (Side)rotation += 30f;
        if (!Side)rotation -= 30f;
        if (rotation > 360) rotation -= 360;
        if (rotation < 0) rotation += 360;
        //Debug.Log(rotation);

        //rotation = rotation*100;
        //rotation = (int)rotation;
        //rotation = rotation / 100;

        Debug.Log(rotation);

        cam.transform.rotation = Quaternion.Euler(xRotation, rotation, zRotation);
    }
}
