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

    private bool isScrolling;
    private float sizeDelta;

    private Text newObject;
    private Text[] instObjects; //Объекты
    private Transform[] positionObjects;


    private Vector2 positionVector;
    private Vector2 scaleVector; // Размеры объектов
    private Transform savePosition;
    private Text saveText;
    private Color color;
    private RectTransform anchoredContentPosition;
    private Vector2 contentVector;

    private string[] CountriesName; // Тестовая переменная, скорее всего её нужно будет отправить в утиль // возможно стоит заменить на enum


    void Start()
    {
        CountriesName = new string[] { "Français", "English", "Русский", "Беларускі", "中文(BETA)" };

        color = new Color(0.7f, 0.7f, 0.7f, 0f);
        sizeDelta = text.GetComponent<RectTransform>().sizeDelta.y;
        anchoredContentPosition = GetComponent<RectTransform>();
        instObjects = new Text[CountriesName.Length];
        positionObjects = new Transform[CountriesName.Length];

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
                instObjects[0].transform.localPosition = new Vector2(instObjects[0].transform.localPosition.x, -sizeDelta * (CountriesName.Length / 2));
                positionObjects[0] = instObjects[0].transform;
                continue;
            }


            instObjects[i].transform.localPosition = new Vector2(instObjects[i].transform.localPosition.x, instObjects[i - 1].transform.localPosition.y + sizeDelta);
            positionObjects[i] = instObjects[i].transform;
        }
    }

    void Update()
    {
        SearchNearestPosition();
        ChangeColorObjects();
        if (!isScrolling) AnchoredPositionToNearest();
        LoopScroll();
    }

    private void LoopScroll()
    {
        if (nearestTextID == 0)
        {
            positionVector.x = positionObjects[CountriesName.Length - 1].localPosition.x;
            positionVector.y = positionObjects[0].localPosition.y - sizeDelta;
            positionObjects[CountriesName.Length - 1].localPosition = positionVector;

            savePosition = positionObjects[CountriesName.Length - 1];
            saveText = instObjects[CountriesName.Length - 1];

            for (int c = CountriesName.Length - 1; c > 0; c--)
            {
                positionObjects[c] = positionObjects[c - 1];
                instObjects[c] = instObjects[c - 1];
            }

            positionObjects[0] = savePosition;
            instObjects[0] = saveText;
        }
        else if (nearestTextID == CountriesName.Length - 1)
        {
            positionVector.x = positionObjects[0].localPosition.x;
            positionVector.y = positionObjects[CountriesName.Length - 1].localPosition.y + sizeDelta;
            positionObjects[0].localPosition = positionVector;

            savePosition = positionObjects[0];
            saveText = instObjects[0];

            for (int c = 0; c < CountriesName.Length - 1; c++)
            {
                positionObjects[c] = positionObjects[c + 1];
                instObjects[c] = instObjects[c + 1];
            }

            positionObjects[CountriesName.Length - 1] = savePosition;
            instObjects[CountriesName.Length - 1] = saveText;
        }
    }

    private void SearchNearestPosition()
    {
        float nearestPosition = float.MaxValue;

        for (int i = 0; i < CountriesName.Length; i++)
        {
            float currentPosition = Mathf.Abs(anchoredContentPosition.anchoredPosition.y + positionObjects[i].localPosition.y);

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

        scaleVector.x = Mathf.SmoothStep(positionObjects[i].localScale.x, scale, speedChangedSize * Time.deltaTime);
        scaleVector.y = Mathf.SmoothStep(positionObjects[i].localScale.y, scale, speedChangedSize * Time.deltaTime);

        positionObjects[i].localScale = scaleVector;
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
            instObjects[k].color = Color.Lerp(instObjects[k].color, color, speedChangedColor * Time.deltaTime);
        }
    }

    private void AnchoredPositionToNearest()
    {
        contentVector.y = Mathf.SmoothStep(anchoredContentPosition.anchoredPosition.y, -positionObjects[nearestTextID].localPosition.y, speedChangedSize * Time.deltaTime);
        anchoredContentPosition.anchoredPosition = contentVector;
    }

    public void ChangeIsScrolling(bool value)
    {
        isScrolling = value;
    }
}