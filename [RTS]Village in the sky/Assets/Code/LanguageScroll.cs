using UnityEngine;
using UnityEngine.UI;

public class LanguageScroll : MonoBehaviour
{
    //Нужно добавить условий, которые бы тормозили выполнение программы в некоторых местах, если нет никаких изменений.
    // Разгрести всю эту кучу переменных и структурировать их

    public Text text;

    private readonly float rangeScaleChanges = 130f; // Отвечает за то, в какой области текст начинает увеличиваться.
    private readonly float speedChangedSize = 15f; // Отвечает за то, как быстро изменяется размер при входе в область scaleUnknown
    private readonly float speedChangedColor = 5f; // Отвечает за скорость затухания цвета

    private int nearestTextID; // Номер центрального элемента

    public static string CurrentLanguage;//!!!!!Удалить , это мусор

    private bool isScrolling;

    private Text[] instObjects; //Объекты
    private Vector2[] scaleObjects; // Размеры объектов


    private Text localSaveText;
    private Color color; //Цвет
    private RectTransform anchoredContentPosition;
    private Vector2 contentVector;

    private string[] CountriesName; // Тестовая переменная, скорее всего её нужно будет отправить в утиль // возможно стоит заменить на enum


    void Start()
    {
        CountriesName = new string[] { "Français", "English", "Русский", "Беларускі" , "中文(BETA)" };

        color = new Color(0.7f, 0.7f, 0.7f, 0f);

        anchoredContentPosition = GetComponent<RectTransform>();
        instObjects = new Text[CountriesName.Length];
        scaleObjects = new Vector2[CountriesName.Length];

        StartInitObjects();
    }

    private void StartInitObjects()
    {
        for (int i = 0; i < CountriesName.Length; i++)
        {
            instObjects[i] = Instantiate(text, transform, false);
            instObjects[i].text = CountriesName[i];
            instObjects[i].color = color;

            if (i == 0)
            {
                instObjects[0].transform.localPosition = new Vector2(instObjects[0].transform.localPosition.x, -text.GetComponent<RectTransform>().sizeDelta.y * (CountriesName.Length / 2));
                continue;
            }


            instObjects[i].transform.localPosition = new Vector2(instObjects[i].transform.localPosition.x, // При инициализации объекта изменяем позицию
                instObjects[i - 1].transform.localPosition.y + text.GetComponent<RectTransform>().sizeDelta.y);
        }
    }

    void Update()
    {
        SearchNearestPosition();
        ChangeColorObjects();
        if (!isScrolling) AnchoredPositionToNearest();
        LoopScroll();
        CurrentLanguage = instObjects[nearestTextID].text;//!!!!!Удалить , это мусор
        Debug.Log(CurrentLanguage);//!!!!!Удалить , это мусор
    }

    private void LoopScroll()
    {
        if (nearestTextID == 0)
        {
            instObjects[CountriesName.Length - 1].transform.localPosition = new Vector2(instObjects[CountriesName.Length - 1].transform.localPosition.x,
                instObjects[0].transform.localPosition.y - text.GetComponent<RectTransform>().sizeDelta.y); //* (CountriesName.Length / 2) если всего три переменные(объекта Text) , то последнюю переменную следует домножить на хуету в скобках, будет плавнее

            localSaveText = instObjects[CountriesName.Length - 1];

            for (int c = CountriesName.Length - 1; c > 0; c--)
            {
                instObjects[c] = instObjects[c - 1];
            }

            instObjects[0] = localSaveText;
        }
        else if (nearestTextID == CountriesName.Length - 1)
        {
            instObjects[0].transform.localPosition = new Vector2(instObjects[0].transform.localPosition.x,
                 instObjects[CountriesName.Length - 1].transform.localPosition.y + text.GetComponent<RectTransform>().sizeDelta.y); //* (CountriesName.Length / 2) если всего три переменные(объекта Text) , то последнюю переменную следует домножить на хуету в скобках, будет плавнее
            localSaveText = instObjects[0];

            for (int c = 0; c < CountriesName.Length - 1; c++)
            {
                instObjects[c] = instObjects[c + 1];
            }

            instObjects[CountriesName.Length - 1] = localSaveText;
        }
    }

    private void SearchNearestPosition()
    {
        float nearestPosition = float.MaxValue;

        for (int i = 0; i < CountriesName.Length; i++)
        {
            float currentPosition = Mathf.Abs(anchoredContentPosition.anchoredPosition.y + instObjects[i].transform.localPosition.y);

            if (nearestPosition > currentPosition)
            {
                nearestPosition = currentPosition;
                nearestTextID = i;
            }
            ChangeSizeObjects(i, currentPosition);
        }
    }

    private void ChangeSizeObjects(int i, float currentPosition)
    {
        float scale = Mathf.Clamp(1 / (currentPosition) * rangeScaleChanges, 0.5f, 1f);
        scaleObjects[i].x = Mathf.SmoothStep(instObjects[i].transform.localScale.x, scale, speedChangedSize * Time.deltaTime);
        scaleObjects[i].y = Mathf.SmoothStep(instObjects[i].transform.localScale.y, scale, speedChangedSize * Time.deltaTime);
        instObjects[i].transform.localScale = scaleObjects[i];
    }

    private void ChangeColorObjects()
    {
        for (int k = 0; k < CountriesName.Length; k++)
        {
            float scaleUp = Mathf.Clamp((1 / instObjects[k].color.a) * (rangeScaleChanges / 2), 0f, 1f);

            if (k == nearestTextID)
            {
                color.a = scaleUp;
                instObjects[k].color = Color.Lerp(instObjects[k].color, color, speedChangedColor * Time.deltaTime);
                continue;
            }

            if ((k == nearestTextID + 1 || k == nearestTextID - 1) && instObjects[k].color.a < 0.5f)
            {
                color.a = scaleUp;
                instObjects[k].color = Color.Lerp(instObjects[k].color, color, speedChangedColor * Time.deltaTime);
                continue;
            }
            else if ((k == nearestTextID + 1 || k == nearestTextID - 1) && instObjects[k].color.a > 0.5f)
            {
                color.a = 0.5f;
                instObjects[k].color = Color.Lerp(instObjects[k].color, color, speedChangedColor * Time.deltaTime);
                continue;
            }
            else if (k == nearestTextID + 1 || k == nearestTextID - 1)
            {
                continue;
            }

            color.a = 0f;
            instObjects[k].color = Color.Lerp(instObjects[k].color, color, speedChangedColor * Time.deltaTime); // Нужно подумать над скоростью, возможно стоит вынести в отдельную переменную 
        }
    }

    private void AnchoredPositionToNearest()
    {
        contentVector.y = Mathf.SmoothStep(anchoredContentPosition.anchoredPosition.y, -instObjects[nearestTextID].transform.localPosition.y, speedChangedSize * Time.deltaTime);
        anchoredContentPosition.anchoredPosition = contentVector;
    }

    public void ChangeIsScrolling(bool value)
    {
        isScrolling = value;
    }
}
