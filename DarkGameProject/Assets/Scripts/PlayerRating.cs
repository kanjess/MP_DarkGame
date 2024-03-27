using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;


public class PlayerRating : MonoBehaviour
{
    private DesignPanel designPanel;
    private GameMode gameMode;
    private PatternReview patternReview;

    private int reviewNum = 0;

    private GameObject mainSceneUI;
    private GameObject sceneContent;
    private GameObject playerPanel;
    //rating 报表
    public GameObject ratingPointItem;
    private GameObject ratingPanel;
    private GameObject ratingChartContent;
    private GameObject ratingItemContent;
    private int ratingDataNum = 0;
    //
    private GameObject ratingScorePanel;
    private GameObject ratingScore;
    private GameObject ratingScoreText;
    private GameObject ratingScoreReviewNum;
    private GameObject ratingScoreRecent;
    private GameObject ratingScoreHighest;
    private GameObject ratingScoreReleaseDate;

    //评论
    public GameObject reviewItem;
    private GameObject reviewItemContent;
    public int defaultReviewShowNum;
    private int basicRatingCreateNum;

    private GameObject allBtn;
    private GameObject allBtnText;
    private GameObject allBtnLine;
    private GameObject mostHelpfulBtn;
    private GameObject mostHelpfulBtnText;
    private GameObject mostHelpfulBtnLine;
    private GameObject prePageBtn;
    private GameObject pageBtnInterval;
    private GameObject nextPageBtn;

    public bool isAllReviewPage = true;
    private int reviewStartOrder;
    private int reviewEndOrder;
    private int mostHelpfulStartIndex;

    private void Awake()
    {
        isAllReviewPage = true;

        basicRatingCreateNum = 10;
        defaultReviewShowNum = 10;

        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();
        patternReview = this.gameObject.GetComponent<PatternReview>();
        designPanel = this.gameObject.GetComponent<DesignPanel>();

        mainSceneUI = this.gameObject.transform.Find("MainSceneUI").gameObject;
        sceneContent = mainSceneUI.transform.Find("SceneContent").gameObject;
        playerPanel = sceneContent.transform.Find("PlayerPanel").gameObject;

        GameObject playerPanelScrollView = playerPanel.transform.Find("ScrollView").gameObject;
        GameObject playerPanelViewport = playerPanelScrollView.transform.Find("Viewport").gameObject;
        GameObject playerPanelChartContent = playerPanelViewport.transform.Find("ChartContent").gameObject;
        GameObject playerPanelChartRatingContent = playerPanelChartContent.transform.Find("RatingContent").gameObject;

        GameObject ratingList = playerPanelChartRatingContent.transform.Find("RatingList").gameObject;

        ratingPanel = ratingList.transform.Find("RatingChart").gameObject;
        ratingChartContent = ratingPanel.transform.Find("ChartContent").gameObject;
        GameObject ratingScrollView = ratingChartContent.transform.Find("ScrollView").gameObject;
        GameObject ratingViewport = ratingScrollView.transform.Find("Viewport").gameObject;
        ratingItemContent = ratingViewport.transform.Find("PointContent").gameObject;

        ratingScorePanel = ratingList.transform.Find("RatingScore").gameObject;
        ratingScore = ratingScorePanel.transform.Find("Score").gameObject;
        ratingScoreText = ratingScorePanel.transform.Find("ScoreText").gameObject;
        ratingScoreReviewNum = ratingScorePanel.transform.Find("ReviewNum").gameObject;
        ratingScoreRecent = ratingScorePanel.transform.Find("RecentDesc").gameObject;
        ratingScoreHighest = ratingScorePanel.transform.Find("PastDesc").gameObject;
        ratingScoreReleaseDate = ratingScorePanel.transform.Find("ReleaseDesc").gameObject;

        GameObject playerPanelChartReviewContent = playerPanelChartContent.transform.Find("ReviewContent").gameObject;
        reviewItemContent = playerPanelChartReviewContent.transform.Find("ReviewItemContent").gameObject;

        GameObject playerPanelChartPageContent = playerPanelChartContent.transform.Find("PageContent").gameObject;
        allBtn = playerPanelChartPageContent.transform.Find("AllBtn").gameObject;
        allBtnText = allBtn.transform.Find("Text").gameObject;
        allBtnLine = allBtn.transform.Find("Line").gameObject;
        mostHelpfulBtn = playerPanelChartPageContent.transform.Find("HelpBtn").gameObject;
        mostHelpfulBtnText = mostHelpfulBtn.transform.Find("Text").gameObject;
        mostHelpfulBtnLine = mostHelpfulBtn.transform.Find("Line").gameObject;
        prePageBtn = playerPanelChartPageContent.transform.Find("PrePageBtn").gameObject;
        pageBtnInterval = playerPanelChartPageContent.transform.Find("PageBtnInterval").gameObject;
        nextPageBtn = playerPanelChartPageContent.transform.Find("NextPageBtn").gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        allBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if(isAllReviewPage == false)
            {
                isAllReviewPage = true;
                allBtnText.GetComponent<Text>().color = new Color(207 / 255f, 161 / 255f, 30 / 255f, 1f);
                allBtnLine.GetComponent<Image>().color = new Color(207 / 255f, 161 / 255f, 30 / 255f, 1f);
                mostHelpfulBtnText.GetComponent<Text>().color = new Color(182 / 255f, 182 / 255f, 182 / 255f, 1f);
                mostHelpfulBtnLine.GetComponent<Image>().color = new Color(182 / 255f, 182 / 255f, 182 / 255f, 1f);

                int cc = patternReview.playerReviewList.Count - 1 - reviewStartOrder;
                if(cc < 0)
                {
                    cc = 0;
                }
                ReviewItemOrderRefresh(cc);
            }
        });
        mostHelpfulBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if (isAllReviewPage == true)
            {
                isAllReviewPage = false;
                allBtnText.GetComponent<Text>().color = new Color(182 / 255f, 182 / 255f, 182 / 255f, 1f);
                allBtnLine.GetComponent<Image>().color = new Color(182 / 255f, 182 / 255f, 182 / 255f, 1f);
                mostHelpfulBtnText.GetComponent<Text>().color = new Color(207 / 255f, 161 / 255f, 30 / 255f, 1f);
                mostHelpfulBtnLine.GetComponent<Image>().color = new Color(207 / 255f, 161 / 255f, 30 / 255f, 1f);

                int cc = patternReview.playerReviewList.Count - 1 - reviewStartOrder;
                if (cc < 0)
                {
                    cc = 0;
                }
                ReviewItemOrderRefresh(cc);
            }
        });

        prePageBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if(isAllReviewPage == true)
            {
                reviewStartOrder = reviewStartOrder + reviewItemContent.transform.childCount;
            }
            else
            {
                int countN = 0;

                for(int i = reviewStartOrder + 1; i < patternReview.playerReviewList.Count; i++)
                {
                    if(patternReview.playerReviewList[i].level >= 2)
                    {
                        countN++;
                        if(countN >= reviewItemContent.transform.childCount)
                        {
                            reviewStartOrder = i;
                            break;
                        }
                    }
                }
                if(countN < reviewItemContent.transform.childCount)
                {
                    reviewStartOrder = patternReview.playerReviewList.Count - 1;
                }
            }

            int cc = patternReview.playerReviewList.Count - 1 - reviewStartOrder;
            if (cc < 0)
            {
                cc = 0;
            }
            ReviewItemOrderRefresh(cc);

        });
        nextPageBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            reviewStartOrder = reviewEndOrder - 1;

            int cc = patternReview.playerReviewList.Count - 1 - reviewStartOrder;
            if (cc < 0)
            {
                cc = 0;
            }
            ReviewItemOrderRefresh(cc);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RatingChart()
    {
        if (ratingDataNum >= gameMode.ratingList.Count)
        {
            //数据无更新
        }
        else if (ratingDataNum < gameMode.ratingList.Count)
        {
            //更新
            int needAddNum = gameMode.ratingList.Count - ratingDataNum;

            for (int i = 0; i < needAddNum; i++)
            {
                float rrr = gameMode.ratingList[ratingDataNum + i];

                GameObject pItem = Instantiate(ratingPointItem) as GameObject;
                pItem.transform.SetParent(ratingItemContent.transform);

                GameObject preItem = pItem;
                if (ratingDataNum + i > 0)
                {
                    preItem = ratingItemContent.transform.GetChild(ratingDataNum + i - 1).gameObject;
                }
                else
                {
                    preItem = null;
                }

                pItem.GetComponent<ChartPointItem>().SetDetail(rrr, 10, ratingDataNum + i, playerPanel, ratingPanel, ratingChartContent, preItem);

                pItem.GetComponent<ChartPointItem>().pointImage.GetComponent<Image>().color = new Color(207f / 255f, 161f / 255f, 30f / 255f, 1f);
                pItem.GetComponent<ChartPointItem>().lineImage.GetComponent<Image>().color = new Color(207f / 255f, 161f / 255f, 30f / 255f, 1f);

                if (ratingItemContent.transform.childCount >= 45)
                {
                    //删减
                    //Destroy(pcrItemContent.transform.GetChild(0).gameObject);
                }

                //加评论
                PlayerReviewCreate();

            }

            ratingDataNum = gameMode.ratingList.Count;

            Invoke("RatingChartPosReset", 0.1f);
        }
    }
    private void RatingChartPosReset()
    {
        float ss = ratingItemContent.GetComponent<RectTransform>().sizeDelta.x;
        ratingItemContent.transform.DOLocalMoveX(-ss, 0f);

        //点的外观
        if(ratingItemContent.transform.childCount != 0)
        {
            ratingItemContent.transform.BroadcastMessage("RatingStar");
        }
        
    }

    //评分的计算
    public float RatingCalulation(float ratingN)
    {
        float ratingS = 0f;

        float ratingSMin = ratingN / 10f * 0.95f;
        float ratingSMax = ratingN / 10f * 1.05f;

        ratingS = Random.Range(ratingSMin, ratingSMax);

        return ratingS;

    }

    //基本UI信息
    public void UISet()
    {
        if(gameMode.ratingList.Count > 0)
        {
            float ratingC = gameMode.ratingList[gameMode.ratingList.Count - 1];
            float ratingH = gameMode.ratingList.Max();

            ratingScore.GetComponent<Text>().text = ratingC.ToString("0.0");
            ratingScoreText.GetComponent<Text>().text = RatingDesc(ratingC);
            reviewNum = patternReview.playerReviewList.Count * 10;
            ratingScoreReviewNum.GetComponent<Text>().text = reviewNum.ToString("N0") + " Reviews";
            ratingScoreRecent.GetComponent<Text>().text = RatingDesc(ratingC) + " (" + ratingC.ToString("0.0") + ")";
            ratingScoreHighest.GetComponent<Text>().text = RatingDesc(ratingH) + " (" + ratingH.ToString("0.0") + ")";
            ratingScoreReleaseDate.GetComponent<Text>().text = gameMode.statisticesTimes.ToString("N0") + " Days ago";
        }
        else
        {
            ratingScore.GetComponent<Text>().text = "--";
            ratingScoreText.GetComponent<Text>().text = "";
            ratingScoreReviewNum.GetComponent<Text>().text = "0 Reviews";
            ratingScoreRecent.GetComponent<Text>().text = "--";
            ratingScoreHighest.GetComponent<Text>().text = "--";
            ratingScoreReleaseDate.GetComponent<Text>().text = gameMode.statisticesTimes.ToString("N0") + " Days ago";
        }

        //评论进场
        ReviewItemShow();
    }

    private string RatingDesc(float ratingS)
    {
        string ratingD = "";

        if(ratingS >= 9.5f)
        {
            ratingD = "Overwhelmingly Positive";
        }
        else if (ratingS >= 9f && ratingS < 9.5f)
        {
            ratingD = "Very Positive";
        }
        else if (ratingS >= 8f && ratingS < 9f)
        {
            ratingD = "Positive";
        }
        else if (ratingS >= 7f && ratingS < 8f)
        {
            ratingD = "Mostly Positive";
        }
        else if (ratingS >= 4f && ratingS < 7f)
        {
            ratingD = "Mixed";
        }
        else if (ratingS >= 3f && ratingS < 4f)
        {
            ratingD = "Mostly Negative";
        }
        else if (ratingS >= 2f && ratingS < 3f)
        {
            ratingD = "Negative";
        }
        else if (ratingS >= 1f && ratingS < 2f)
        {
            ratingD = "Very Negative";
        }
        else if (ratingS >= 0f && ratingS < 1f)
        {
            ratingD = "Overwhelmingly Negative";
        }

        return ratingD;
    }

    //评论生成
    public void PlayerReviewCreate()
    {
        if(gameMode.ratingList.Count != 0)
        {
            float cRating = gameMode.ratingList[gameMode.ratingList.Count - 1];

            //生成评论的数量
            int ratingCreateNum = basicRatingCreateNum;
            if(gameMode.ratingList.Count >= 3)
            {
                //比较3点落差
                float rating_0 = gameMode.ratingList[gameMode.ratingList.Count - 3];
                float deltaRating0_3 = Mathf.Abs(cRating - rating_0);
                if(deltaRating0_3 > 0.5f)
                {
                    ratingCreateNum = Mathf.RoundToInt(basicRatingCreateNum * 1.5f);
                }
                else if (deltaRating0_3 >= 0.3f && deltaRating0_3 < 0.5f)
                {
                    ratingCreateNum = Mathf.RoundToInt(basicRatingCreateNum * 0.6f);
                }
                else if (deltaRating0_3 >= 0 && deltaRating0_3 < 0.3f)
                {
                    ratingCreateNum = Mathf.RoundToInt(basicRatingCreateNum * 0.1f);
                }
            }
            int finalRatingCreateNum = Mathf.RoundToInt(Random.Range(ratingCreateNum * 0.8f, ratingCreateNum * 1.2f));
            if(finalRatingCreateNum  < 1)
            {
                finalRatingCreateNum = 1;
            }

            //好评差评比例
            int goodReviewNum = Mathf.RoundToInt(finalRatingCreateNum * (cRating / 10f));
            int badReviewNum = finalRatingCreateNum - goodReviewNum;

            //等级比例
            float goodLevel1Ratio = 0.9f;
            float goodL2Ratio = 0.1f;
            float badLevel1Ratio = 0.5f;
            float badLevel2Ratio = 0.35f;

            int goodLevel1ReviewNum = Mathf.RoundToInt(goodReviewNum * goodLevel1Ratio);
            int goodLevel2ReviewNum = Mathf.RoundToInt(goodReviewNum * goodL2Ratio);
            int goodLevel3ReviewNum = goodReviewNum - goodLevel1ReviewNum - goodLevel2ReviewNum;
            if(goodLevel3ReviewNum < 0)
            {
                goodLevel3ReviewNum = 0;
            }

            int badLevel1ReviewNum = Mathf.RoundToInt(badReviewNum * badLevel1Ratio);
            int badLevel2ReviewNum = Mathf.RoundToInt(badReviewNum * badLevel2Ratio);
            int badLevel3ReviewNum = badReviewNum - badLevel1ReviewNum - badLevel2ReviewNum;
            if (badLevel3ReviewNum < 0)
            {
                badLevel3ReviewNum = 0;
            }

            //创建评论
            patternReview.temPlayerReviewList = new List<PlayerReview>();
            patternReview.PlayerReviewDataCreate(true, 1, goodLevel1ReviewNum);
            patternReview.PlayerReviewDataCreate(true, 2, goodLevel2ReviewNum);
            //patternReview.PlayerReviewDataCreate(true, 3, goodLevel3ReviewNum);
            patternReview.PlayerReviewDataCreate(false, 1, badLevel1ReviewNum);
            patternReview.PlayerReviewDataCreate(false, 2, badLevel2ReviewNum);
            patternReview.PlayerReviewDataCreate(false, 3, badLevel3ReviewNum);

            //乱序
            Shuffle(patternReview.temPlayerReviewList);
            //评论并入
            patternReview.playerReviewList.AddRange(patternReview.temPlayerReviewList);
        }
    }

    public void Shuffle<T>(List<T> list)
    {
        //Random rng = new Random(); // 实例化一个随机数生成器
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1); // 生成一个0到n的随机数
            T value = list[k]; // 取出随机位置的元素
            list[k] = list[n]; // 将当前元素与随机位置的元素交换
            list[n] = value;
        }
    }

    //评论展示
    public void ReviewItemShow()
    {
        //补充item
        int legalReviewShowNum = defaultReviewShowNum;
        if(legalReviewShowNum > patternReview.playerReviewList.Count)
        {
            legalReviewShowNum = patternReview.playerReviewList.Count;
        }
        if (reviewItemContent.transform.childCount < legalReviewShowNum)
        {
            int delAdd = defaultReviewShowNum - reviewItemContent.transform.childCount;
            for (int i = 0; i < delAdd; i++)
            {
                GameObject rItem = Instantiate(reviewItem) as GameObject;
                rItem.transform.SetParent(reviewItemContent.transform);
                rItem.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        //刷新mostHelpfulStartIndex
        for(int i = patternReview.playerReviewList.Count - 1; i >= 0 ; i--)
        {
            if(patternReview.playerReviewList[i].level >= 2)
            {
                mostHelpfulStartIndex = i;
                break;
            }
        }

        //刷新序号
        ReviewItemOrderRefresh(0);
    }

    public void ReviewItemOrderRefresh(int startNum)
    {
        int reviewItemNum = reviewItemContent.transform.childCount;
        int keepOrder = patternReview.playerReviewList.Count - startNum;
        for (int i = 0; i < reviewItemNum; i++)
        {
            int order = -1;
            //倒序编号
            if (isAllReviewPage == true)
            {
                order = patternReview.playerReviewList.Count - 1 - i - startNum;
            }
            else
            {
                for(int aa = keepOrder - 1; aa >= 0; aa--)
                {
                    if(patternReview.playerReviewList[aa].level >= 2)
                    {
                        order = aa;
                        keepOrder = aa;
                        break;
                    }
                }
            }

            reviewItemContent.transform.GetChild(i).gameObject.GetComponent<ReviewItem>().listOrder = order;

            reviewItemContent.transform.GetChild(i).gameObject.GetComponent<ReviewItem>().InfoRefresh();

            //翻页按钮
            if(i == 0) //第一个
            {
                reviewStartOrder = reviewItemContent.transform.GetChild(i).gameObject.GetComponent<ReviewItem>().listOrder;

                int checkIndex = 0;
                if(isAllReviewPage == true)
                {
                    checkIndex = patternReview.playerReviewList.Count - 1;
                }
                else
                {
                    checkIndex = mostHelpfulStartIndex;
                }

                if (reviewItemContent.transform.GetChild(i).gameObject.GetComponent<ReviewItem>().listOrder < checkIndex)
                {
                    //非首页
                    prePageBtn.transform.localScale = new Vector3(1, 1, 1);
                    pageBtnInterval.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    //第一页
                    prePageBtn.transform.localScale = new Vector3(1, 0, 1);
                    pageBtnInterval.transform.localScale = new Vector3(1, 0, 1);
                }
            }
            else if (i == reviewItemNum - 1)  //最后一个
            {
                reviewEndOrder = reviewItemContent.transform.GetChild(i).gameObject.GetComponent<ReviewItem>().listOrder;

                if (reviewItemContent.transform.GetChild(i).gameObject.GetComponent<ReviewItem>().listOrder < 0)
                {
                    //最后一页
                    nextPageBtn.transform.localScale = new Vector3(1, 0, 1);
                    pageBtnInterval.transform.localScale = new Vector3(1, 0, 1);
                }
                else
                {
                    //非最后一页
                    nextPageBtn.transform.localScale = new Vector3(1, 1, 1);
                    pageBtnInterval.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }


}
