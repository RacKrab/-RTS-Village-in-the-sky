using UnityEngine;

public class UIControl : MonoBehaviour
{

    public GameObject DefaulfUI;
    public GameObject BuildUI;
    public GameObject BuildUI1;
    // Use this for initialization
    // Переписать функции через OnEnable

    void Start ()  {
        DefaulfUI.SetActive(true);
        BuildUI.SetActive(false);
        BuildUI1.SetActive(false);
    }

    public void UI_StartBuild()
    {
        DefaulfUI.SetActive(false);
        BuildUI.SetActive(true);
    }

    public void UI_1_StartBuild()
    {
        BuildUI.SetActive(false);
        BuildUI1.SetActive(true);
    }

    public void UI_1_Confirm_Cansel()
    {
        BuildUI.SetActive(true);
        BuildUI1.SetActive(false);
    }

    public void UI_EndBuild()
    {
        DefaulfUI.SetActive(true);
        BuildUI.SetActive(false);
        BuildUI1.SetActive(false);
    }
}
