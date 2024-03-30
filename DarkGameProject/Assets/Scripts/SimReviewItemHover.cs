using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


public class SimReviewItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleMultiplier; // 放大的倍数
    private Vector3 originalScale; // 用于存储原始大小
    private GameObject parentItem;

    private void Awake()
    {
        scaleMultiplier = 1.5f;

        parentItem = this.gameObject.transform.parent.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 当鼠标悬停进入时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetOrder();
        // 将UI对象的大小设置为原始大小的scaleMultiplier倍
        //transform.localScale = originalScale * scaleMultiplier;
        parentItem.transform.DOScale(originalScale * scaleMultiplier, 0.3f);
        parentItem.GetComponent<SimReviewItem>().refreshBtn.transform.localScale = new Vector3(1, 1, 1);
    }

    // 当鼠标悬停离开时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        // 恢复UI对象的原始大小
        //transform.localScale = originalScale;
        parentItem.transform.DOScale(originalScale, 0.3f);
        parentItem.GetComponent<SimReviewItem>().refreshBtn.transform.localScale = new Vector3(1, 0, 1);
    }

    void SetOrder()
    {
        parentItem.transform.SetAsLastSibling();

    }
}
