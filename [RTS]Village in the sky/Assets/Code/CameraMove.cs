using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform camPosition;

    private Vector3 transformCamPosition;

    private float xRotation, yRotation, zRotation;
    private float xChangePosition, zChangePosition; // Координаты, перенесенные в систему координат
    private float xRatio, zRatio; //Скорость изменения координат

    private bool IsMove;

    void Start()
    {
        transformCamPosition = new Vector3();

        camPosition = gameObject.GetComponent<Camera>().transform;

        xRotation = gameObject.GetComponent<Camera>().transform.rotation.eulerAngles.x;
        yRotation = gameObject.GetComponent<Camera>().transform.rotation.eulerAngles.y;
        zRotation = gameObject.GetComponent<Camera>().transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsMove = true;

            zRatio = Input.mousePosition.y;
            xRatio = Input.mousePosition.x;

            return;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            IsMove = false;
        }
        else if (IsMove)
        {
            xRatio -= Input.mousePosition.x;
            zRatio -= Input.mousePosition.y;

            float rad = (yRotation * 3.1415f) / 180f;

            xChangePosition = ((xRatio * Mathf.Cos(rad)) + (zRatio * Mathf.Sin(rad))) * Time.deltaTime;
            zChangePosition = ((zRatio * Mathf.Cos(rad)) - (xRatio * Mathf.Sin(rad))) * Time.deltaTime;

            transformCamPosition.x = camPosition.localPosition.x + xChangePosition;
            transformCamPosition.y = camPosition.localPosition.y;
            transformCamPosition.z = camPosition.localPosition.z + zChangePosition;

            camPosition.localPosition = transformCamPosition;

            xRatio = Input.mousePosition.x;
            zRatio = Input.mousePosition.y;
        }
    }

    public void Rotate(bool Side) // true - rotate to right // false - rotate to left
    {
        /*Этот скрипт нужно будет переписать, пока это просто заглушка*/
        if (Side) yRotation += 45f;
        if (!Side) yRotation -= 45f;

        camPosition.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
    }
}