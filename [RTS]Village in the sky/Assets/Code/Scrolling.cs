using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrolling : MonoBehaviour {

    [Range(0,50)]
    [Header("Controllers")]
    public int panelCount;
    [Range(0,500)]
    public int panelOffet;

    [Range(0f,20f)]
    public float snapSpeed;

    [Range(0f,10f)]
    public float scaleOffset;

    [Range(1f, 20f)]
    public float scaleSpeed;

    public ScrollRect scrollRect;

    [Header("Other Objects")]

    public GameObject prefabPanel;

    private bool isScrolling;
    private Vector2[] posPanels;
    private Vector2[] pansScale;
    private Vector2 contentVector;
    private GameObject[] instantiateObjects;
    private RectTransform contentRect;
    private int minPosId;

	void Start () {
        contentRect = GetComponent<RectTransform>();
        posPanels = new Vector2[panelCount];
        pansScale = new Vector2[panelCount];
        instantiateObjects = new GameObject[panelCount];

		for(int i = 0; i<panelCount;i++)
        {
           instantiateObjects[i] =  Instantiate(prefabPanel, transform, false);

            if (i == 0) continue;

            instantiateObjects[i].transform.localPosition = new Vector2(instantiateObjects[i-1].transform.localPosition.x + prefabPanel.GetComponent<RectTransform>().sizeDelta.x + panelOffet , 
                instantiateObjects[i].transform.localPosition.y);

            posPanels[i] = -instantiateObjects[i].transform.localPosition;
        }
	}

    private void FixedUpdate()
    {
        if(contentRect.anchoredPosition.x >= posPanels[0].x && !isScrolling || contentRect.anchoredPosition.x <= posPanels[posPanels.Length - 1].x && !isScrolling)
        {
            scrollRect.inertia = false;
        }
        float nearestPos = float.MaxValue;

        for(int i = 0; i < panelCount; i++)
        {
            float distance = Mathf.Abs( contentRect.anchoredPosition.x - posPanels[i].x);
            if(distance < nearestPos)
            {
                nearestPos = distance;
                minPosId = i;
            }
            float scale = Mathf.Clamp(1 / (distance / panelOffet) * scaleOffset,0.5f , 1f);
            pansScale[i].x = Mathf.SmoothStep(instantiateObjects[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instantiateObjects[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            instantiateObjects[i].transform.localScale = pansScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);

        if (scrollVelocity < 200 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 200) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, posPanels[minPosId].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;

    }


    public void ChangeScrollingBool(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}
