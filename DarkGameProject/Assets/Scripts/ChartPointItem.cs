using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChartPointItem : MonoBehaviour
{
    public bool pointType = true;

    public float showNum = 0f;
    public float maxNum = 0f;
    public int order = 0;

    public GameObject panelObject;
    public GameObject chartObject;
    public GameObject chartContent;

    public GameObject preChartItem;

    private GameObject pointContent;
    public GameObject pointImage;
    public GameObject lineImage;
    private GameObject pointText;

    private void Awake()
    {
        pointContent = this.gameObject.transform.Find("PointContent").gameObject;
        pointImage = pointContent.transform.Find("PointImage").gameObject;
        lineImage = pointContent.transform.Find("LineImage").gameObject;
        pointText = this.gameObject.transform.Find("PointText").gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        //UISet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDetail(float n, float m, int oo, GameObject pp0, GameObject pp1, GameObject pp2, GameObject pci)
    {
        showNum = n;
        maxNum = m;
        order = oo;
        panelObject = pp0;
        chartObject = pp1;
        chartContent = pp2;
        preChartItem = pci;

        Invoke("UISet", 0.04f);
    }

    void UISet()
    {
        //文字
        pointText.GetComponent<Text>().text = (order + 1).ToString();

        //圆点位置
        float per = showNum / maxNum;
        float posY = per * chartContent.GetComponent<RectTransform>().sizeDelta.y;
        //float offsetV = chartObject.transform.localScale.x * panelObject.transform.localScale.x;
        float posYC = posY; //坐标修正

        if(pointType == true)
        {
            pointContent.transform.localPosition = new Vector3(0, posYC, 0);
        }
        else
        {
            pointContent.transform.localPosition = new Vector3(0, 0, 0);
            pointImage.transform.localPosition = new Vector3(0, 0, 0);
            pointImage.GetComponent<RectTransform>().sizeDelta = new Vector2(41, posYC);
        }
        

        Invoke("LineSet", 0.01f);
        
    }

    void LineSet()
    {
        //线
        if (preChartItem != null)
        {
            Vector3 direction = preChartItem.GetComponent<ChartPointItem>().pointImage.transform.position - pointImage.transform.position;
            //Debug.Log(preChartItem.transform.position + " - " + this.gameObject.transform.position + " - " + direction);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            lineImage.transform.localEulerAngles = new Vector3(0, 0, angle);

            float distance = Vector2.Distance(preChartItem.GetComponent<ChartPointItem>().pointImage.transform.position, pointImage.transform.position);
            float offsetV = chartObject.transform.localScale.x * panelObject.transform.localScale.x;
            lineImage.GetComponent<RectTransform>().sizeDelta = new Vector2(distance / offsetV, 7f);
            
        }
    }
}
