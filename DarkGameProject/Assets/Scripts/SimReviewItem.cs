using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;


public class SimReviewItem : MonoBehaviour
{
    private bool active = false;
    public float showT;

    private PatternReview patternReview;
    private GameEnding gameEnding;
    private List<PlayerReview> searchL;

    private PlayerReview self;

    private GameObject itemBg;
    private GameObject positivePic;
    private GameObject negativePic;
    private GameObject commentString;

    private GameObject refreshContent;
    public GameObject refreshBtn;

    private void Awake()
    {
        patternReview = GameObject.Find("Canvas").gameObject.GetComponent<PatternReview>();
        gameEnding = GameObject.Find("Canvas").gameObject.GetComponent<GameEnding>();

        itemBg = this.gameObject.transform.Find("Bg").gameObject;
        GameObject positiveContent = itemBg.transform.Find("PositiveContent").gameObject;
        GameObject pContent = positiveContent.transform.Find("Content").gameObject;
        positivePic = pContent.transform.Find("Positive").gameObject;
        negativePic = pContent.transform.Find("Negative").gameObject;
        GameObject commentContent = itemBg.transform.Find("CommentContent").gameObject;
        commentString = commentContent.transform.Find("CommentString").gameObject;

        refreshContent = this.gameObject.transform.Find("RefreshContent").gameObject;
        refreshBtn = refreshContent.transform.Find("RefreshBtn").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowAnime", showT);

        itemBg.GetComponent<Button>().onClick.AddListener(delegate
        {
            if(active == true)
            {
                RandomItem();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(active == true)
        {
            refreshContent.GetComponent<RectTransform>().sizeDelta = itemBg.GetComponent<RectTransform>().sizeDelta;
        }
    }

    void ShowAnime()
    {
        this.gameObject.transform.DOScale(new Vector3(1,1,1), 0.3f);
        RandomItem();
        active = true;
    }

    void RandomItem()
    {
        //随机一个review id
        //取哪个母集
        searchL = new List<PlayerReview>();
        if (patternReview.playerHelpfulReviewList.Count > 10)
        {
            //用helpful
            searchL = patternReview.playerHelpfulReviewList;
        }
        else
        {
            //用全量
            searchL = patternReview.playerReviewList;
        }
        //剔除已经显示的部分
        searchL = searchL.Except(gameEnding.hasShowReviewList).ToList();
        //随机
        if (searchL.Count != 0)
        {
            int randN = Random.Range(0, searchL.Count);
            PlayerReview item = searchL[randN];
            //剔除与显示
            if(gameEnding.hasShowReviewList.Contains(self) == true)
            {
                gameEnding.hasShowReviewList.Remove(self);
            }
            self = item;
            gameEnding.hasShowReviewList.Add(self);
        }
        //UI
        UIReset();
    }

    void UIReset()
    {
        if(self != null)
        {
            if(self.isPositive == true)
            {
                positivePic.transform.localScale = new Vector3(1, 1, 1);
                negativePic.transform.localScale = new Vector3(1, 0, 1);
            }
            else
            {
                positivePic.transform.localScale = new Vector3(1, 0, 1);
                negativePic.transform.localScale = new Vector3(1, 1, 1);
            }

            commentString.GetComponent<Text>().text = self.comment;
        }
    }
}
