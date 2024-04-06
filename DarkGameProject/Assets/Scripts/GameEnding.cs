using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class GameEnding : MonoBehaviour
{
    private GameMode gameMode;
    private PlayerRating playerRating;

    private GameObject endingUI;
    private GameObject endingBg;
    private GameObject endingLine0;
    private GameObject endingLine1;
    private GameObject endingLine2;
    private GameObject endingLine3;
    private GameObject endingLine4;
    private GameObject endingLine5;

    public GameObject clvPointItem;
    public GameObject ratingPointItem;

    private GameObject endingPanel;
    private GameObject endingChartContent;

    private GameObject endingViewWin;

    private GameObject endingCLVItemContent;
    private GameObject endingRatingItemContent;

    private int endingDataNum = 0;

    private float endingLine0_length;
    private float endingViewWin_length;

    //private int endingShowAnimeStage = 0;

    private List<float> endingCLVList;
    private List<float> endingRatingList;

    private GameObject clvTipsItem;
    private GameObject clvTipsValue;
    private GameObject ratingTipsItem;
    private GameObject ratingTipsValue;

    public GameObject simReviewItem;
    private GameObject reviewContent;
    private GameObject reviewSampleContent;
    private List<Vector3> reviewItemPosList;

    public List<PlayerReview> hasShowReviewList;

    private GameObject summaryContent;


    private void Awake()
    {
        endingDataNum = 17;

        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();
        playerRating = this.gameObject.GetComponent<PlayerRating>();

        endingUI = this.gameObject.transform.Find("EndingUI").gameObject;
        endingBg = endingUI.transform.Find("Bg").gameObject;
        endingPanel = endingUI.transform.Find("EndingChart").gameObject;
        endingChartContent = endingPanel.transform.Find("ChartContent").gameObject;
        endingLine0 = endingChartContent.transform.Find("Line_0").gameObject;
        endingLine1 = endingChartContent.transform.Find("Line_1").gameObject;
        endingLine2 = endingChartContent.transform.Find("Line_2").gameObject;
        endingLine3 = endingChartContent.transform.Find("Line_3").gameObject;
        endingLine4 = endingChartContent.transform.Find("Line_4").gameObject;
        endingLine5 = endingChartContent.transform.Find("Line_5").gameObject;
        endingViewWin = endingChartContent.transform.Find("ViewWin").gameObject;

        GameObject endingCLVScrollView = endingViewWin.transform.Find("ScrollView").gameObject;
        GameObject endingCLVViewport = endingCLVScrollView.transform.Find("Viewport").gameObject;
        endingCLVItemContent = endingCLVViewport.transform.Find("PointContent").gameObject;

        GameObject endingRatingScrollView = endingViewWin.transform.Find("ScrollView2").gameObject;
        GameObject endingRatingViewport = endingRatingScrollView.transform.Find("Viewport").gameObject;
        endingRatingItemContent = endingRatingViewport.transform.Find("PointContent").gameObject;

        GameObject viewTipsContent = endingChartContent.transform.Find("ViewTipsContent").gameObject;
        clvTipsItem = viewTipsContent.transform.Find("CLVTipsItem").gameObject;
        clvTipsValue = clvTipsItem.transform.Find("CLVValue").gameObject;
        ratingTipsItem = viewTipsContent.transform.Find("RatingTipsItem").gameObject;
        ratingTipsValue = ratingTipsItem.transform.Find("RatingValue").gameObject;

        reviewContent = endingPanel.transform.Find("ReviewContent").gameObject;
        reviewSampleContent = reviewContent.transform.Find("SampleContent").gameObject;

        summaryContent = endingPanel.transform.Find("SummaryContent").gameObject;

        endingCLVList = new List<float>();
        endingRatingList = new List<float>();
        reviewItemPosList = new List<Vector3>();
        hasShowReviewList = new List<PlayerReview>();
    }

    // Start is called before the first frame update
    void Start()
    {
        endingLine0_length = endingLine0.GetComponent<RectTransform>().sizeDelta.x;
        endingLine0.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3f);
        endingLine1.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3f);
        endingLine2.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3f);
        endingLine3.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3f);
        endingLine4.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3f);
        endingLine5.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3f);

        endingViewWin_length = endingViewWin.GetComponent<RectTransform>().sizeDelta.x;
        endingViewWin.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 700f);

        clvTipsItem.transform.localScale = new Vector3(0, 1, 1);
        ratingTipsItem.transform.localScale = new Vector3(0, 1, 1);

        summaryContent.transform.localScale = new Vector3(0, 0, 0);

        //测试
        if (gameMode.testMode == true)
        {
            EndingTest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //测试数据
    void EndingTest()
    {
        float clv = 1.1f;
        float rating = 9.8f;

        for(int i = 0; i < 56; i++)
        {
            clv += 0.1f;
            rating -= 0.1f;
            gameMode.clvList.Add(clv);
            gameMode.ratingList.Add(rating);
        }

        playerRating.RatingTest(5.2f);
    }

    public void PanelAnime()
    {
        endingPanel.transform.localScale = new Vector3(0, 0, 0);

        endingBg.transform.localScale = new Vector3(1, 1, 1);
        endingBg.GetComponent<Image>().DOFade(0.5f, 0.3f); //背景透明度

        Tweener anime = endingPanel.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        anime.OnComplete(() => EndingShowAnime());
    }

    void EndingShowAnime()
    {
        Tweener anime0 = endingLine0.GetComponent<RectTransform>().DOSizeDelta(new Vector2(endingLine0_length, 3f), 1.5f);
        endingLine1.GetComponent<RectTransform>().DOSizeDelta(new Vector2(endingLine0_length, 3f), 1.5f);
        endingLine2.GetComponent<RectTransform>().DOSizeDelta(new Vector2(endingLine0_length, 3f), 1.5f);
        endingLine3.GetComponent<RectTransform>().DOSizeDelta(new Vector2(endingLine0_length, 3f), 1.5f);
        endingLine4.GetComponent<RectTransform>().DOSizeDelta(new Vector2(endingLine0_length, 3f), 1.5f);
        endingLine5.GetComponent<RectTransform>().DOSizeDelta(new Vector2(endingLine0_length, 3f), 1.5f);

        anime0.OnComplete(() => EndingShowPointShowAnime());
    }
    void EndingShowPointShowAnime()
    {
        //CLV修正计算
        if(gameMode.clvList.Count != 0)
        {
            //CLV
            int clvCountInterval = 1;
            float countI = (float)gameMode.clvList.Count / (float)endingDataNum;

            if (countI < 1f)
            {
                clvCountInterval = 1;
            }
            else
            {
                clvCountInterval = (int)countI;
            }
            //赋值
            int countCCC = 0;
            float addValue = 0f;
            for (int i = 0; i < gameMode.clvList.Count; i++)
            {
                countCCC++;
                addValue += gameMode.clvList[i];

                if (countCCC >= clvCountInterval)
                {
                    float avValue = addValue / countCCC;
                    endingCLVList.Add(avValue);
                    countCCC = 0;
                    addValue = 0;
                }
                else if(countCCC < clvCountInterval && i == gameMode.clvList.Count - 1)
                {
                    float avValue = addValue / countCCC;
                    endingCLVList.Add(avValue);
                    countCCC = 0;
                    addValue = 0;
                }
            }
            //清扫
            if(endingCLVList.Count > endingDataNum)
            {
                for(int i = 0; i < endingCLVList.Count; i++)
                {
                    int xx = endingCLVList.Count / 2;
                    endingCLVList.RemoveAt(xx);
                    if(endingCLVList.Count == endingDataNum)
                    {
                        break;
                    }
                }
            }
            //头尾修正
            endingCLVList.RemoveAt(0);
            endingCLVList.Insert(0, gameMode.clvList[0]);
            endingCLVList.RemoveAt(endingCLVList.Count - 1);
            endingCLVList.Add(gameMode.clvList[gameMode.clvList.Count - 1]);



        }

        //评分修正计算
        if (gameMode.ratingList.Count != 0)
        {
            //rating
            int ratingCountInterval = 1;
            float countI = (float)gameMode.ratingList.Count / (float)endingDataNum;

            if (countI < 1f)
            {
                ratingCountInterval = 1;
            }
            else
            {
                ratingCountInterval = (int)countI;
            }
            //赋值
            int countCCC = 0;
            float addValue = 0f;
            for (int i = 0; i < gameMode.ratingList.Count; i++)
            {
                countCCC++;
                addValue += gameMode.ratingList[i];

                if (countCCC >= ratingCountInterval)
                {
                    float avValue = addValue / countCCC;
                    endingRatingList.Add(avValue);
                    countCCC = 0;
                    addValue = 0;
                }
                else if (countCCC < ratingCountInterval && i == gameMode.ratingList.Count - 1)
                {
                    float avValue = addValue / countCCC;
                    endingRatingList.Add(avValue);
                    countCCC = 0;
                    addValue = 0;
                }
            }
            //清扫
            if (endingRatingList.Count > endingDataNum)
            {
                for (int i = 0; i < endingRatingList.Count; i++)
                {
                    int xx = endingRatingList.Count / 2;
                    endingRatingList.RemoveAt(xx);
                    if (endingRatingList.Count == endingDataNum)
                    {
                        break;
                    }
                }
            }
            //头尾修正
            endingRatingList.RemoveAt(0);
            endingRatingList.Insert(0, gameMode.ratingList[0]);
            endingRatingList.RemoveAt(endingRatingList.Count - 1);
            endingRatingList.Add(gameMode.ratingList[gameMode.ratingList.Count - 1]);



        }

        //数据点灌入
        if(endingCLVList.Count != 0)
        {
            float maxR = endingCLVList.Max();
            for (int i = 0; i < endingCLVList.Count; i++)
            {
                float rrr = endingCLVList[i];

                GameObject pItem = Instantiate(clvPointItem) as GameObject;
                pItem.transform.SetParent(endingCLVItemContent.transform);

                GameObject preItem = pItem;
                if (i > 0)
                {
                    preItem = endingCLVItemContent.transform.GetChild(i - 1).gameObject;
                }
                else
                {
                    preItem = null;
                }

                pItem.GetComponent<ChartPointItem>().SetDetail(rrr, maxR, i, endingUI, endingPanel, endingChartContent, preItem);
                pItem.GetComponent<ChartPointItem>().pointText.transform.localScale = new Vector3(0, 1, 1);

                pItem.GetComponent<ChartPointItem>().pointImage.GetComponent<Image>().color = new Color(233f / 255f, 23f / 255f, 8f / 255f, 1f);
                pItem.GetComponent<ChartPointItem>().lineImage.GetComponent<Image>().color = new Color(233f / 255f, 23f / 255f, 8f / 255f, 1f);
            }

            //数量不足
            if(endingCLVList.Count < endingDataNum)
            {
                float originalSpcing = endingCLVItemContent.GetComponent<HorizontalLayoutGroup>().spacing;
                endingCLVItemContent.GetComponent<HorizontalLayoutGroup>().spacing = (originalSpcing * endingDataNum) / endingCLVList.Count;
            }
        }

        if (endingRatingList.Count != 0)
        {
            float maxR = endingRatingList.Max();
            for (int i = 0; i < endingRatingList.Count; i++)
            {
                float rrr = endingRatingList[i];

                GameObject pItem = Instantiate(ratingPointItem) as GameObject;
                pItem.transform.SetParent(endingRatingItemContent.transform);

                GameObject preItem = pItem;
                if (i > 0)
                {
                    preItem = endingRatingItemContent.transform.GetChild(i - 1).gameObject;
                }
                else
                {
                    preItem = null;
                }

                pItem.GetComponent<ChartPointItem>().SetDetail(rrr, maxR, i, endingUI, endingPanel, endingChartContent, preItem);
                pItem.GetComponent<ChartPointItem>().pointText.transform.localScale = new Vector3(0, 1, 1);

                pItem.GetComponent<ChartPointItem>().pointImage.GetComponent<Image>().color = new Color(207f / 255f, 161f / 255f, 30f / 255f, 1f);
                pItem.GetComponent<ChartPointItem>().lineImage.GetComponent<Image>().color = new Color(207f / 255f, 161f / 255f, 30f / 255f, 1f);
            }

            //数量不足
            if (endingRatingList.Count < endingDataNum)
            {
                float originalSpcing = endingRatingItemContent.GetComponent<HorizontalLayoutGroup>().spacing;
                endingRatingItemContent.GetComponent<HorizontalLayoutGroup>().spacing = (originalSpcing * endingDataNum) / endingRatingList.Count;
            }
        }

        //指示牌
        clvTipsItem.transform.position = endingCLVItemContent.transform.GetChild(0).gameObject.GetComponent<ChartPointItem>().pointImage.transform.position;
        ratingTipsItem.transform.position = endingRatingItemContent.transform.GetChild(0).gameObject.GetComponent<ChartPointItem>().pointImage.transform.position;
        clvTipsValue.GetComponent<Text>().text = endingCLVItemContent.transform.GetChild(0).gameObject.GetComponent<ChartPointItem>().showNum.ToString("0.0");
        ratingTipsValue.GetComponent<Text>().text = endingRatingItemContent.transform.GetChild(0).gameObject.GetComponent<ChartPointItem>().showNum.ToString("0.0");
        TipsItemAnime(clvTipsItem, clvTipsValue, 0, endingCLVItemContent);
        TipsItemAnime(ratingTipsItem, ratingTipsValue, 0, endingRatingItemContent);
        Invoke("TipsItemScale", 0.3f);

        //拉出动画
        Tweener anime1 = endingViewWin.GetComponent<RectTransform>().DOSizeDelta(new Vector2(endingViewWin_length, 700f), 4f).SetEase(Ease.Linear);
    }
    void TipsItemScale()
    {
        clvTipsItem.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        ratingTipsItem.transform.localScale = new Vector3(0.8f, 0.8f, 1);
    }
    void TipsItemAnime(GameObject self, GameObject selfText, int i, GameObject lay)
    {
        if(i + 1 < lay.transform.childCount)
        {
            //self.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            self.transform.position = lay.transform.GetChild(i).gameObject.GetComponent<ChartPointItem>().pointImage.transform.position;
            Vector3 tarPos = lay.transform.GetChild(i + 1).gameObject.GetComponent<ChartPointItem>().pointImage.transform.position;
            Tweener anime = self.transform.DOMove(tarPos, 4f * 1.2f / endingDataNum).SetEase(Ease.Linear);
            selfText.GetComponent<Text>().text = lay.transform.GetChild(i + 1).gameObject.GetComponent<ChartPointItem>().showNum.ToString("0.0");
            i++;
            anime.OnComplete(() => TipsItemAnime(self, selfText, i, lay));
        }
        else if(i + 1 >= lay.transform.childCount)
        {
            MemoPadAnime();
        }
    }

    void MemoPadAnime()
    {
        Tweener anime = summaryContent.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        anime.OnComplete(() => MemoPadAnime2());
    }
    void MemoPadAnime2()
    {
        summaryContent.transform.Find("MemoPad").gameObject.GetComponent<MemoPad>().CEOStringAnime();
    }

    public void ReviewItemShow()
    {
        //样本坐标
        if(reviewItemPosList.Count == 0)
        {
            for (int i = 0; i < reviewSampleContent.transform.childCount; i++)
            {
                GameObject sam = reviewSampleContent.transform.GetChild(i).gameObject;
                reviewItemPosList.Add(sam.transform.position);
            }
            playerRating.Shuffle(reviewItemPosList);

            //放置
            for (int i = 0; i < reviewItemPosList.Count; i++)
            {
                GameObject rItem = Instantiate(simReviewItem) as GameObject;
                rItem.transform.SetParent(reviewContent.transform);
                rItem.transform.position = reviewItemPosList[i];
                rItem.transform.localScale = new Vector3(0, 0, 0);
                rItem.GetComponent<SimReviewItem>().showT = i * 0.5f;
            }
        }
        

    }

}
