using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfoBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string infoText;

    public GameObject infoContent;
    public GameObject infoTextShow;

    // Start is called before the first frame update
    void Start()
    {
        infoTextShow.GetComponent<Text>().text = infoText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 当鼠标悬停进入时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        infoContent.transform.localScale = new Vector3(1, 1, 1);
    }

    // 当鼠标悬停离开时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        infoContent.transform.localScale = new Vector3(1, 0, 1);
    }
}
