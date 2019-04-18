using UnityEngine;

public class CameraMove : MonoBehaviour
{
    /// <summary>
    /// Переменная camState характеризует текущее состояние камеры
    /// 0 - камера не перемещается, находится в статичном положении
    /// 1 - камера двигается по оси x или z или x + z
    /// 2 - вращение камеры
    /// 3 - приближение / удаление камеры (перемещение по оси y)
    /// </summary>
    public static byte camState;

    public bool Rotate = false;

    private Touch[] touches;

    private Transform camPosition;

    private Vector3 transformCamPosition;

    private float first_x_Ratio, first_z_Ratio, second_x_Ratio, second_z_Ratio; //Скорость изменения координат

    private bool IsMove, FirstCheck;

    private float CurrentDistance, OldDistance;

    [Range(0f, 10f)]
    public float cam_Speed;

    void Start()
    {
        transformCamPosition = new Vector3();

        FirstCheck = true;

        camPosition = gameObject.GetComponent<Camera>().transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsMove = StartRay();

            camPosition = Camera.main.transform;

            first_z_Ratio = Input.mousePosition.y;
            first_x_Ratio = Input.mousePosition.x;

            return;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            camState = 0; // Камера прекратила своё перемещение
            //Возможно позже стоит добавить плавности камере, и camState нужно будет менять в другом месте

            IsMove = false;

            first_x_Ratio = 0; first_z_Ratio = 0;
            second_x_Ratio = 0; second_z_Ratio = 0;

        }
        else if (IsMove && Input.touchCount == 1)
        {
            RotateAroundObject();

            camState = 1; // Камера перемещается по осям x,z

            first_x_Ratio -= Input.mousePosition.x;
            first_z_Ratio -= Input.mousePosition.y;

            float rad = (camPosition.transform.rotation.y * 3.1415f) / 180f;

            float xChangePosition = ((first_x_Ratio * Mathf.Cos(rad)) + (first_z_Ratio * Mathf.Sin(rad))) * Time.deltaTime * cam_Speed;
            float zChangePosition = ((first_z_Ratio * Mathf.Cos(rad)) - (first_x_Ratio * Mathf.Sin(rad))) * Time.deltaTime * cam_Speed;

            transformCamPosition.x = camPosition.localPosition.x + xChangePosition;
            transformCamPosition.y = camPosition.localPosition.y;
            transformCamPosition.z = camPosition.localPosition.z + zChangePosition;

            camPosition.localPosition = transformCamPosition;

            first_x_Ratio = Input.mousePosition.x;
            first_z_Ratio = Input.mousePosition.y;
        }
        else if (/*IsMove && */Input.touchCount == 2)
        {
            //Scale();
            //RotateAroundObject();
        }
    }

    private void RotateAroundObject()
    {
        //float object_position_x, object_position_y;
        float radius;

        Vector2 centerPos = new Vector2(0 , 0);
        Vector2 currentPos = new Vector2(camPosition.transform.localPosition.x, camPosition.transform.localPosition.z);

        radius = Vector2.Distance(centerPos , currentPos);

        float zpos = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow((camPosition.transform.position.z + 0), 2)) + 0;
        float xpos = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow((camPosition.transform.position.x + 0), 2)) + 0;

        camPosition.transform.position = new Vector3(xpos + 0.01f , camPosition.transform.position.y , zpos + 0.01f);

        Debug.Log(radius);
    }

    //private void Scale()
    //{
    //    if (FirstCheck)
    //    {

    //        touches = Input.touches;

    //        OldDistance = Vector2.Distance(touches[0].position , touches[1].position);

    //        FirstCheck = false;

    //        return;
    //    }
    //    touches = Input.touches; // С этим нужно что-то делать.

    //    CurrentDistance = Vector2.Distance(touches[0].position, touches[1].position);

    //    float distance_Change = CurrentDistance - OldDistance;

    //    if(Mathf.Abs(distance_Change) > 20f)
    //    {
    //        if(distance_Change > 0f)
    //        {
    //            Debug.Log("+++");
    //            camPosition.transform.position = new Vector3(camPosition.transform.position.x,camPosition.transform.position.y - 1f,camPosition.transform.position.z);
    //        }
    //        if(distance_Change < 0f)
    //        {
    //            Debug.Log("---");
    //            camPosition.transform.position = new Vector3(camPosition.transform.position.x, camPosition.transform.position.y + 1f, camPosition.transform.position.z);
    //        }
    //    }
    //    if(CurrentDistance < 400f)
    //    {
    //        Debug.Log(camPosition.transform.rotation);
    //        camPosition.transform.eulerAngles = new Vector3(camPosition.transform.eulerAngles.x, camPosition.transform.eulerAngles.y + 1, camPosition.transform.eulerAngles.z);/*Quaternion.Euler(camPosition.transform.rotation.x , camPosition.transform.rotation.y + i, camPosition.transform.rotation.z);*/
    //    }

    //    OldDistance = CurrentDistance;

    //}

    //При детекте первого прикосновения, идет проверка, не бьет ли луч по текущему объекту (объекту, с которым мы работаем).В результате , при попадании по объекту, камера теряет управление.
    private bool StartRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "UI")
            {
                Debug.Log("UI");
                return false;
            }
            if (StateClass.current_Object != null)
            {
                if (hit.collider.name == StateClass.current_Object.name) return false;
            }
            return true;       
        }
        return false;
    }
}