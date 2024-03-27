using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReviewItem : MonoBehaviour
{
    private PatternReview patternReview;
    private GameMode gameMode;

    private GameObject playerHead;
    private GameObject playerName;

    private GameObject reviewGoodPic;
    private GameObject reviewBadPic;
    private GameObject reviewPositiveText;

    private GameObject commentString;
    private GameObject postString;

    public int listOrder;

    private void Awake()
    {
        patternReview = GameObject.Find("Canvas").gameObject.GetComponent<PatternReview>();
        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();

        GameObject playerInfoContent = this.gameObject.transform.Find("PlayerInfoContent").gameObject;

        GameObject headContent = playerInfoContent.transform.Find("HeadContent").gameObject;
        playerHead = headContent.transform.Find("PlayerHead").gameObject;

        GameObject nameContent = playerInfoContent.transform.Find("NameContent").gameObject;
        playerName = nameContent.transform.Find("PlayerName").gameObject;

        GameObject reviewContent = playerInfoContent.transform.Find("ReviewContent").gameObject;
        GameObject positiveRatingContent = reviewContent.transform.Find("PositiveRatingContent").gameObject;
        GameObject positiveContent = positiveRatingContent.transform.Find("PositiveContent").gameObject;
        reviewGoodPic = positiveContent.transform.Find("ReviewGood").gameObject;
        reviewBadPic = positiveContent.transform.Find("ReviewBad").gameObject;
        GameObject positiveTextContent = positiveRatingContent.transform.Find("PositiveTextContent").gameObject;
        reviewPositiveText = positiveTextContent.transform.Find("PositiveText").gameObject;
        GameObject commentContent = reviewContent.transform.Find("CommentContent").gameObject;
        commentString = commentContent.transform.Find("CommentString").gameObject;

        GameObject timeContent = positiveRatingContent.transform.Find("TimeContent").gameObject;
        postString = timeContent.transform.Find("PostTime").gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    public void InfoRefresh()
    {
        if(listOrder < 0)
        {
            if(this.gameObject.activeSelf == true)
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            if (this.gameObject.activeSelf == false)
            {
                this.gameObject.SetActive(true);
            }
        }

        //信息
        if(listOrder >= 0)
        {
            string headPicString = patternReview.playerReviewList[listOrder].playerHead;
            playerHead.GetComponent<Image>().sprite = Resources.Load<Sprite>("Player/Head/" + headPicString);
            playerName.GetComponent<Text>().text = patternReview.playerReviewList[listOrder].playerName;

            if (patternReview.playerReviewList[listOrder].isPositive == true)
            {
                reviewGoodPic.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                reviewBadPic.transform.localScale = new Vector3(0.3f, 0, 1);
                reviewPositiveText.GetComponent<Text>().text = "Recommanded";
            }
            else
            {
                reviewGoodPic.transform.localScale = new Vector3(0.3f, 0, 1);
                reviewBadPic.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                reviewPositiveText.GetComponent<Text>().text = "Not Recommanded";
            }

            commentString.GetComponent<Text>().text = patternReview.playerReviewList[listOrder].comment;

            int postD = patternReview.playerReviewList[listOrder].commentDay;
            int currentD = gameMode.statisticesTimes;
            int delPostD = currentD - postD;
            if (delPostD == 0)
            {
                postString.GetComponent<Text>().text = "Recently";
            }
            else
            {
                postString.GetComponent<Text>().text = delPostD.ToString() + " days ago";
            }

            //Debug.Log(listOrder);
        }
        
    }
}
