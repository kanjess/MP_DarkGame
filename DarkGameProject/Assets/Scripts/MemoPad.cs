using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class MemoPad : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameEnding gameEnding;

    public float scaleMultiplier; // 放大的倍数
    private Vector3 originalScale; // 用于存储原始大小
    private GameObject parentItem;

    private GameObject ceoText;
    private GameObject ceoSign;
    private GameObject ceoBadge;

    private string ceoString;

    private int ceoStringOrder = 0;

    private void Awake()
    {
        scaleMultiplier = 1.5f;

        gameEnding = GameObject.Find("Canvas").gameObject.GetComponent<GameEnding>();
        parentItem = this.gameObject.transform.parent.gameObject;

        ceoText = this.gameObject.transform.Find("Text").gameObject;
        ceoSign = this.gameObject.transform.Find("Sign").gameObject;
        ceoBadge = this.gameObject.transform.Find("Badge").gameObject;


    }

    // Start is called before the first frame update
    void Start()
    {
        originalScale = new Vector3(1, 1, 1);
        ceoString = ceoText.GetComponent<Text>().text;
        ceoText.GetComponent<Text>().text = "";
        ceoSign.GetComponent<Image>().DOFade(0, 0);
        ceoBadge.GetComponent<Image>().DOFade(0, 0);
        ceoBadge.transform.localScale = new Vector3(5, 5, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 当鼠标悬停进入时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 将UI对象的大小设置为原始大小的scaleMultiplier倍
        //transform.localScale = originalScale * scaleMultiplier;
        parentItem.transform.DOScale(originalScale * scaleMultiplier, 0.3f);
        
    }

    // 当鼠标悬停离开时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        // 恢复UI对象的原始大小
        //transform.localScale = originalScale;
        parentItem.transform.DOScale(originalScale, 0.3f);
        
    }

    public void CEOStringAnime()
    {
        if(ceoStringOrder < ceoString.Length)
        {
            ceoStringOrder++;
            if(ceoStringOrder >= ceoString.Length)
            {
                ceoStringOrder = ceoString.Length;
            }
            //Debug.Log(1);
            string ccc = ceoString.Substring(0, ceoStringOrder);
            ceoText.GetComponent<Text>().text = ccc;

            Invoke("CEOStringAnime", 0.03f);
        }
        else
        {
            //名字动画
            SignAndBadge();
        }
    }
    void SignAndBadge()
    {
        ceoSign.GetComponent<Image>().DOFade(1f, 0.3f);
        
        ceoBadge.GetComponent<Image>().DOFade(1f, 0.3f);

        Tweener anime = ceoBadge.transform.DOScale(new Vector3(0.5f, 0.5f, 1), 0.5f);
        anime.OnComplete(() => gameEnding.ReviewItemShow());
    }


}
