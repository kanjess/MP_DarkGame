using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class DesignPanel : MonoBehaviour
{
    public GameObject gameplayItemUIItem;

    private GameObject mainSceneUI;
    private GameObject designItemBtn;
    private bool designItemBtnAnime = false;
    public bool designItemBtnState = false;
    public bool designItemDragState = false;
    public GameObject designPanelPosCheckPoint;
    public GameObject designPanel;
    private GameObject designPanelBg;
    private GameObject designPanelContent;
    private GameObject designPanelClose;

    private GameObject itemRotationBtn;

    private GameMode gameMode;
    private CameraControl cameraControl;

    private GameObject moneyShow;

    private int sceneNo = 2;
    private GameObject sceneContent;
    private GameObject sceneContentBg;
    private GameObject companySceneBtn;
    private GameObject gameSceneBtn;
    private GameObject playerSceneBtn;
    private GameObject companyPanel;
    private GameObject playerPanel;
    private bool scenePanelAnime = false;
    private Tweener scenePanelTweener;

    //等级界面
    private GameObject playerLvText;
    private GameObject playerExpText;
    private GameObject playerExpBar;

    //item列表
    private GameObject gameItemContent;
    private GameObject darkItemContent;

    //报表
    private GameObject CumulativeRevenueNumText;
    private GameObject AverageRetentionNumText;
    private GameObject AveragePayingNumText;
    private GameObject AverageRevenuePerPlayerNumText;

    public GameObject pcrPointItem;
    private GameObject pcrPanel;
    private GameObject pcrChartContent;
    private GameObject pcrItemContent;
    private int pcrDataNum = 0;

    public GameObject arptPointItem;
    private GameObject arptPanel;
    private GameObject arptChartContent;
    private GameObject arptItemContent;
    private int arptDataNum = 0;
    private GameObject arptYText1;
    private GameObject arptYText2;
    private GameObject arptYText3;
    private GameObject arptYText4;
    private GameObject arptYText5;

    //升级panel
    public bool designItemPanelOpen = false;
    private GameObject designItemLevelPanelTarget;
    private GameObject designItemLevelPanelContent;
    private GameObject designItemLevelPanelBg;
    private GameObject designItemLevelPanel;
    private GameObject designItemLevelNum;
    private GameObject designItemLevelItemName;
    private GameObject designItemLevelPanelItemShow;
    private GameObject designItemLevelPanelCloseBtn;
    private GameObject designItemLevelPanelItemDesc;
    private GameObject designItemLevelPanelEffectContent;
    private GameObject designItemLevelPanelLevelUpBtn;
    private GameObject designItemLevelPanelLevelUpCost;
    private GameObject designItemLevelPanelLevelUpAllMoney;


    private void Awake()
    {
        sceneNo = 2;

        mainSceneUI = this.gameObject.transform.Find("MainSceneUI").gameObject;
        designItemBtn = mainSceneUI.transform.Find("DesignItemBtn").gameObject;
        designPanel = mainSceneUI.transform.Find("DesignPanel").gameObject;
        designPanelBg = designPanel.transform.Find("Bg").gameObject;
        designPanelContent = designPanel.transform.Find("DesignPanelContent").gameObject;
        designPanelClose = designPanel.transform.Find("DesignPanelClose").gameObject;
        designPanelPosCheckPoint = designPanel.transform.Find("PosCheckPoint").gameObject;
        itemRotationBtn = designPanel.transform.Find("ItemRotationBtn").gameObject;

        GameObject designPanelScrollView = designPanelContent.transform.Find("ScrollView").gameObject;
        GameObject designPanelViewport = designPanelScrollView.transform.Find("Viewport").gameObject;
        GameObject designPanelViewContent = designPanelViewport.transform.Find("ViewContent").gameObject;
        GameObject designPanelViewContentGameContent = designPanelViewContent.transform.Find("GameItemContent").gameObject;
        gameItemContent = designPanelViewContentGameContent.transform.Find("ItemContent").gameObject;
        GameObject designPanelViewContentDarkContent = designPanelViewContent.transform.Find("DarkItemContent").gameObject;
        darkItemContent = designPanelViewContentDarkContent.transform.Find("ItemContent").gameObject;

        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();
        cameraControl = GameObject.Find("Main Camera").gameObject.GetComponent<CameraControl>();

        GameObject mainContent = mainSceneUI.transform.Find("MainContent").gameObject;
        GameObject topContent = mainContent.transform.Find("TopContent").gameObject;
        GameObject moneyContent = topContent.transform.Find("MoneyContent").gameObject;
        moneyShow = moneyContent.transform.Find("MoneyShow").gameObject;
        GameObject levelContent = topContent.transform.Find("LevelContent").gameObject;
        playerLvText = levelContent.transform.Find("LevelNum").gameObject;
        playerExpText = levelContent.transform.Find("ExpNum").gameObject;
        playerExpBar = levelContent.transform.Find("ExpBar").gameObject;

        sceneContent = mainSceneUI.transform.Find("SceneContent").gameObject;
        sceneContentBg = sceneContent.transform.Find("Bg").gameObject;
        companySceneBtn = sceneContent.transform.Find("CompanySceneBtn").gameObject;
        gameSceneBtn = sceneContent.transform.Find("GameSceneBtn").gameObject;
        playerSceneBtn = sceneContent.transform.Find("PlayerSceneBtn").gameObject;
        companyPanel = sceneContent.transform.Find("CompanyPanel").gameObject;
        playerPanel = sceneContent.transform.Find("PlayerPanel").gameObject;

        GameObject companyPanelScrollView = companyPanel.transform.Find("ScrollView").gameObject;
        GameObject companyPanelViewport = companyPanelScrollView.transform.Find("Viewport").gameObject;
        GameObject companyPanelChartContent = companyPanelViewport.transform.Find("ChartContent").gameObject;
        GameObject companyPanelChartListContent = companyPanelChartContent.transform.Find("ChartListContent").gameObject;

        GameObject companyPanelBasicInfoBanner = companyPanelChartContent.transform.Find("BasicInfoBanner").gameObject;
        GameObject companyPanelCumulativeRevenueContent = companyPanelBasicInfoBanner.transform.Find("CumulativeRevenueContent").gameObject;
        CumulativeRevenueNumText = companyPanelCumulativeRevenueContent.transform.Find("NumText").gameObject;
        GameObject companyPanelAverageRetentionContent = companyPanelBasicInfoBanner.transform.Find("AverageRetentionContent").gameObject;
        AverageRetentionNumText = companyPanelAverageRetentionContent.transform.Find("NumText").gameObject;
        GameObject companyPanelAveragePayingContent = companyPanelBasicInfoBanner.transform.Find("AveragePayingContent").gameObject;
        AveragePayingNumText = companyPanelAveragePayingContent.transform.Find("NumText").gameObject;
        GameObject companyPanelAverageRevenuePerPlayerContent = companyPanelBasicInfoBanner.transform.Find("AverageRevenuePerPlayerContent").gameObject;
        AverageRevenuePerPlayerNumText = companyPanelAverageRevenuePerPlayerContent.transform.Find("NumText").gameObject;

        pcrPanel = companyPanelChartListContent.transform.Find("AveragePurchaseRatePanel").gameObject;
        pcrChartContent = pcrPanel.transform.Find("ChartContent").gameObject;
        GameObject pcrScrollView = pcrChartContent.transform.Find("ScrollView").gameObject;
        GameObject pcrViewport = pcrScrollView.transform.Find("Viewport").gameObject;
        pcrItemContent = pcrViewport.transform.Find("PointContent").gameObject;

        arptPanel = companyPanelChartListContent.transform.Find("AverageRevenuePanel").gameObject;
        arptChartContent = arptPanel.transform.Find("ChartContent").gameObject;
        GameObject arptScrollView = arptChartContent.transform.Find("ScrollView").gameObject;
        GameObject arptViewport = arptScrollView.transform.Find("Viewport").gameObject;
        arptItemContent = arptViewport.transform.Find("PointContent").gameObject;
        GameObject arptLine1 = arptChartContent.transform.Find("Line_1").gameObject;
        arptYText1 = arptLine1.transform.Find("Text").gameObject;
        GameObject arptLine2 = arptChartContent.transform.Find("Line_2").gameObject;
        arptYText2 = arptLine2.transform.Find("Text").gameObject;
        GameObject arptLine3 = arptChartContent.transform.Find("Line_3").gameObject;
        arptYText3 = arptLine3.transform.Find("Text").gameObject;
        GameObject arptLine4 = arptChartContent.transform.Find("Line_4").gameObject;
        arptYText4 = arptLine4.transform.Find("Text").gameObject;
        GameObject arptLine5 = arptChartContent.transform.Find("Line_5").gameObject;
        arptYText5 = arptLine5.transform.Find("Text").gameObject;

        designItemLevelPanelContent = mainSceneUI.transform.Find("DesignItemLevelPanel").gameObject;
        designItemLevelPanelBg = designItemLevelPanelContent.transform.Find("Bg").gameObject;
        designItemLevelPanel = designItemLevelPanelContent.transform.Find("LevelPanel").gameObject;
        GameObject designItemLevelPanelLevelContent = designItemLevelPanel.transform.Find("LevelContent").gameObject;
        designItemLevelNum = designItemLevelPanelLevelContent.transform.Find("LevelNum").gameObject;
        designItemLevelItemName = designItemLevelPanel.transform.Find("ItemName").gameObject;
        designItemLevelPanelItemShow = designItemLevelPanel.transform.Find("ItemShow").gameObject;
        designItemLevelPanelCloseBtn = designItemLevelPanel.transform.Find("CloseBtn").gameObject;
        designItemLevelPanelItemDesc = designItemLevelPanel.transform.Find("ItemDesc").gameObject;
        designItemLevelPanelEffectContent = designItemLevelPanel.transform.Find("ItemEffect").gameObject;
        designItemLevelPanelLevelUpBtn = designItemLevelPanel.transform.Find("LevelUpBtn").gameObject;
        GameObject designItemLevelPanelLevelUpCostContent = designItemLevelPanelLevelUpBtn.transform.Find("CostContent").gameObject;
        designItemLevelPanelLevelUpCost = designItemLevelPanelLevelUpCostContent.transform.Find("MoneyCost").gameObject;
        designItemLevelPanelLevelUpAllMoney = designItemLevelPanelLevelUpCostContent.transform.Find("AllMoney").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameItemIn();
        sceneContentBg.transform.localScale = new Vector3(0, 0, 0);

        designItemBtn.GetComponent<Button>().onClick.AddListener(DesignPanelAnime);
        designPanelClose.GetComponent<Button>().onClick.AddListener(DesignPanelAnime);

        itemRotationBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if(BasicAction.gameplayItemRotationMode == false)
            {
                BasicAction.gameplayItemRotationMode = true;
            }
            else
            {
                BasicAction.gameplayItemRotationMode = false;
            }
        });

        companySceneBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            ScenePanelSwitch(1);
        });
        gameSceneBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            ScenePanelSwitch(2);
        });
        playerSceneBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            ScenePanelSwitch(3);
        });

        designItemLevelPanelCloseBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if(designItemPanelOpen == true)
            {
                DesignItemLevelPanel(null);
            }
        });
        //item升级
        designItemLevelPanelLevelUpBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if(gameMode.companyMoney >= designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().levelPrice)
            {
                gameMode.companyMoney -= designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().levelPrice;
                designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemLevel++;

                //新价格
                designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().levelPrice *= designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemLevel;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(BasicAction.gameplayItemSetMode == true)
        {
            if (Input.mousePosition.x > designPanelPosCheckPoint.transform.position.x)
            {
                cameraControl.cameraCanMove = false;

                if (Input.GetMouseButtonDown(0))
                {
                    cameraControl.inDragScreen = true;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    cameraControl.inDragScreen = false;
                }
            }
            else
            {
                cameraControl.cameraCanMove = true;

                if (Input.GetMouseButtonUp(0))
                {
                    cameraControl.inDragScreen = false;
                }

            }
        }

        UIUpdate();

        //升级界面
        if(designItemPanelOpen == true)
        {
            designItemLevelNum.GetComponent<Text>().text = designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemLevel.ToString();
            designItemLevelItemName.GetComponent<Text>().text = designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemName;
            designItemLevelPanelLevelUpCost.GetComponent<Text>().text = numberText(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().levelPrice);
            designItemLevelPanelLevelUpAllMoney.GetComponent<Text>().text = numberText(gameMode.companyMoney);
        }
    }

    void DesignPanelAnime()
    {
        if (designItemBtnAnime == false)
        {
            float tt = 0.6f;
            designItemBtnAnime = true;
            if (designItemBtnState == false)
            {
                //打开界面
                Tweener anime = designItemBtn.transform.DOMoveX(designItemBtn.transform.position.x + 450f, tt);
                designPanel.transform.DOMoveX(designPanel.transform.position.x - 450f, tt);
                designItemBtnState = true;

                BasicAction.gameplayItemSetMode = true;

                anime.OnComplete(() => designItemBtnAnime = false);
                //暂停游戏
                gameMode.gameProcessPause = true;
            }
            else
            {
                //关闭界面
                Tweener anime = designItemBtn.transform.DOMoveX(designItemBtn.transform.position.x - 450f, tt);
                designPanel.transform.DOMoveX(designPanel.transform.position.x + 450f, tt);
                designItemBtnState = false;

                BasicAction.gameplayItemSetMode = false;
                BasicAction.gameplayItemAction = false;
                BasicAction.gameplayItemRotationMode = false;
                BasicAction.roadEditMode = false;

                cameraControl.inDragScreen = false;
                cameraControl.cameraCanMove = true;

                anime.OnComplete(() => designItemBtnAnime = false);
                //继续游戏
                gameMode.gameProcessPause = false;

                //消除new
                for (int i = 0; i < gameMode.gameItemList.Count; i++)
                {
                    if (gameMode.gameItemList[i].GetComponent<GameplayItemUIItem>().newItemShow == true)
                    {
                        gameMode.gameItemList[i].GetComponent<GameplayItemUIItem>().newItemShow = false;
                    }
                }
            }
        }
    }

    void GameItemIn()
    {
        //测试ID=101
        GameObject oj = Instantiate(gameplayItemUIItem) as GameObject;
        oj.transform.SetParent(designPanelContent.transform);
        oj.transform.localPosition = new Vector3(0, 0, 0);
        oj.GetComponent<GameplayItemUIItem>().SetItemID(101);

        //测试ID=102
        GameObject oj2 = Instantiate(gameplayItemUIItem) as GameObject;
        oj2.transform.SetParent(designPanelContent.transform);
        oj2.transform.localPosition = new Vector3(0, -150f, 0);
        oj2.GetComponent<GameplayItemUIItem>().SetItemID(102);

        //测试ID=401
        GameObject oj3 = Instantiate(gameplayItemUIItem) as GameObject;
        oj3.transform.SetParent(designPanelContent.transform);
        oj3.transform.localPosition = new Vector3(0, -300f, 0);
        oj3.GetComponent<GameplayItemUIItem>().SetItemID(401);
    } //临时

    void UIUpdate()
    {
        //钱
        if(gameMode.companyMoney < 1000)
        {
            moneyShow.GetComponent<Text>().text = gameMode.companyMoney.ToString();
        }
        else if(gameMode.companyMoney >= 1000 && gameMode.companyMoney < 1000000)
        {
            float mmm = gameMode.companyMoney / 1000f;
            moneyShow.GetComponent<Text>().text = mmm.ToString("0.00") + " K";
        }
        else if (gameMode.companyMoney >= 1000000 && gameMode.companyMoney < 1000000000)
        {
            float mmm = gameMode.companyMoney / 1000000f;
            moneyShow.GetComponent<Text>().text = mmm.ToString("0.00") + " M";
        }
        else if (gameMode.companyMoney >= 1000000000)
        {
            float mmm = gameMode.companyMoney / 1000000000f;
            moneyShow.GetComponent<Text>().text = mmm.ToString("0.00") + " B";
        }


        //等级
        playerLvText.GetComponent<Text>().text = gameMode.playerLevel.ToString();
        playerExpText.GetComponent<Text>().text = gameMode.playerExp.ToString() + " / " + gameMode.levelUpExp.ToString();
        playerExpBar.GetComponent<Image>().fillAmount = ((float)gameMode.playerExp / (float)gameMode.levelUpExp);

        //升级检测
        if((float)gameMode.playerExp / (float)gameMode.levelUpExp >= 1f)
        {
            gameMode.playerLevel++;
            gameMode.playerExp = 0;
            gameMode.levelUpExp = (int)(gameMode.basicLevelUpExp * (1 + (gameMode.playerLevel * gameMode.levelUpExpIncreaseValue / 10f)));

            GameItemUnlock();
        }
    }

    /**
    void MapSceneSwitch(int sceneN)
    {
        if(sceneChangeAnime == false && sceneN != sceneNo)
        {
            sceneChangeAnime = true;
            cameraControl.cameraCanMove = false;

            Tilemap tt = new Tilemap();
            if(sceneN == 1)
            {
                tt = cameraControl.upMap;
            }
            else if(sceneN == 2)
            {
                tt = cameraControl.normalMap;
            }
            else if (sceneN == 3)
            {
                tt = cameraControl.downMap;
            }

            float dis = 30f;

            dis = dis * (sceneN - sceneNo);

            Tweener anime = mainSceneContent.transform.DOMoveY(dis, 0.5f).SetRelative();

            sceneNo = sceneN;

            anime.OnComplete(() => cameraControl.TileSetup(tt));
            anime.OnKill(() => MapSceneSwitchAnimeEnd());
        }
    }
    void MapSceneSwitchAnimeEnd()
    {
        sceneChangeAnime = false;
        cameraControl.cameraCanMove = true;
    }
    **/
    void ScenePanelSwitch(int no)
    {
        //切换
        if(scenePanelAnime == false && sceneNo != no)
        {
            if(no == 1 || no == 3)
            {
                //游戏暂停
                gameMode.gameProcessPause = true;

                scenePanelAnime = true;

                sceneContentBg.transform.localScale = new Vector3(1, 1, 1);
                sceneContentBg.GetComponent<Image>().DOFade(0.8f, 0.3f);

                if(no == 1)
                {
                    //报表
                    BasicInfoShow();

                    scenePanelTweener = companyPanel.transform.DOLocalMoveY(0f, 0.5f);
                    playerPanel.transform.DOLocalMoveY(-1000f, 0.5f);
                    
                }
                else if (no == 3)
                {
                    scenePanelTweener = playerPanel.transform.DOLocalMoveY(0f, 0.5f);
                    companyPanel.transform.DOLocalMoveY(1000f, 0.5f);
                }

                sceneNo = no;

                scenePanelTweener.OnComplete(() => scenePanelAnime = false);
                
            }
            else if(no == 2)
            {
                scenePanelAnime = true;

                sceneContentBg.transform.localScale = new Vector3(1, 1, 1);
                sceneContentBg.GetComponent<Image>().DOFade(0f, 0.3f);

                scenePanelTweener = companyPanel.transform.DOLocalMoveY(1000f, 0.5f);
                playerPanel.transform.DOLocalMoveY(-1000f, 0.5f);

                sceneNo = no;

                scenePanelTweener.OnComplete(() => scenePanelAnime = false);
                scenePanelTweener.OnKill(() => sceneContentBg.transform.localScale = new Vector3(0,0,0));

                //游戏恢复
                gameMode.gameProcessPause = false;
            }
        }
        //界面缩回
        else if (scenePanelAnime == false && sceneNo == no)
        {
            scenePanelAnime = true;

            sceneContentBg.transform.localScale = new Vector3(1, 1, 1);
            sceneContentBg.GetComponent<Image>().DOFade(0f, 0.3f);

            scenePanelTweener = companyPanel.transform.DOLocalMoveY(1000f, 0.5f);
            playerPanel.transform.DOLocalMoveY(-1000f, 0.5f);

            sceneNo = 2;

            scenePanelTweener.OnComplete(() => scenePanelAnime = false);
            scenePanelTweener.OnKill(() => sceneContentBg.transform.localScale = new Vector3(0, 0, 0));

            //游戏恢复
            gameMode.gameProcessPause = false;
        }
    }

    public void DesignItemLevelPanel(GameObject gItem)
    {
        if(designItemPanelOpen == false)
        {
            if(designItemLevelPanelItemShow.transform.childCount > 0)
            {
                for(int i = 0; i < designItemLevelPanelItemShow.transform.childCount; i++)
                {
                    Destroy(designItemLevelPanelItemShow.transform.GetChild(i).gameObject);
                }
            }
            designItemLevelPanelTarget = gItem;
            GameObject pic = designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().gameplayItemBtn;
            GameObject ppp = Instantiate(pic) as GameObject;
            ppp.transform.SetParent(designItemLevelPanelItemShow.transform);
            ppp.transform.localPosition = new Vector3(0, 0, 0);
            ppp.transform.localScale = new Vector3(1, 1, 1);

            designItemLevelPanel.transform.localScale = new Vector3(0, 0, 0);

            designItemPanelOpen = true;

            designItemLevelPanelBg.transform.localScale = new Vector3(1, 1, 1);
            designItemLevelPanelBg.GetComponent<Image>().DOFade(0.8f, 0.3f);

            designItemLevelPanel.transform.DOScale(new Vector3(1,1,1), 0.3f);
        }
        else
        {
            Tweener anime = designItemLevelPanel.transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            designItemLevelPanelBg.GetComponent<Image>().DOFade(0f, 0.3f);

            anime.OnComplete(() => designItemLevelPanelBg.transform.localScale = new Vector3(0, 0, 0));
            anime.OnKill(() => designItemPanelOpen = false);
        }
    }



    //item次数变更
    public void GameItemNumChange(int itemid, bool isDark, bool isAdd)
    {
        if(isDark == false)
        {
            for (int i = 0; i < gameItemContent.transform.childCount; i++)
            {
                GameObject uiItem = gameItemContent.transform.GetChild(i).gameObject;
                if (uiItem.GetComponent<GameplayItemUIItem>().itemID == itemid)
                {
                    if (isAdd == false)
                    {
                        uiItem.GetComponent<GameplayItemUIItem>().itemNum -= 1;
                    }
                    else
                    {
                        uiItem.GetComponent<GameplayItemUIItem>().itemNum += 1;
                    }

                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < darkItemContent.transform.childCount; i++)
            {
                GameObject uiItem = darkItemContent.transform.GetChild(i).gameObject;
                if (uiItem.GetComponent<GameplayItemUIItem>().itemID == itemid)
                {
                    if (isAdd == false)
                    {
                        uiItem.GetComponent<GameplayItemUIItem>().itemNum -= 1;
                    }
                    else
                    {
                        uiItem.GetComponent<GameplayItemUIItem>().itemNum += 1;
                    }

                    break;
                }
            }
        }
        
    }

    //item解锁
    public void GameItemUnlock()
    {
        for(int i = 0; i < gameMode.gameItemList.Count; i++)
        {
            gameMode.gameItemList[i].GetComponent<GameplayItemUIItem>().UnlockListen();
        }
    }

    //统计数据报表
    public void BasicInfoShow()
    {
        CumulativeRevenueNumText.GetComponent<Text>().text = numberText(gameMode.cumulativeRevenue);
        AverageRetentionNumText.GetComponent<Text>().text = (gameMode.averageRetention * 100f).ToString("0.00") + "%";
        AveragePayingNumText.GetComponent<Text>().text = (gameMode.averagePeymentConversion * 100f).ToString("0.00") + "%";
        AverageRevenuePerPlayerNumText.GetComponent<Text>().text = gameMode.averageRevenuePerPlayer.ToString("0.00");
}

    public void PurchaseRateChart()
    {
        if(pcrDataNum >= gameMode.purchaseRateList.Count)
        {
            //数据无更新
        }
        else if (pcrDataNum < gameMode.purchaseRateList.Count)
        {
            //更新
            int needAddNum = gameMode.purchaseRateList.Count - pcrDataNum;

            for(int i = 0; i < needAddNum; i++)
            {
                float rrr = gameMode.purchaseRateList[pcrDataNum + i];

                GameObject pItem = Instantiate(pcrPointItem) as GameObject;
                pItem.transform.SetParent(pcrItemContent.transform);

                GameObject preItem = pItem;
                if(pcrDataNum + i > 0)
                {
                    preItem = pcrItemContent.transform.GetChild(pcrDataNum + i - 1).gameObject;
                }
                else
                {
                    preItem = null;
                }

                pItem.GetComponent<ChartPointItem>().SetDetail(rrr, 1, pcrDataNum + i, companyPanel, pcrPanel, pcrChartContent, preItem);

                if(pcrItemContent.transform.childCount >= 45)
                {
                    //删减
                    //Destroy(pcrItemContent.transform.GetChild(0).gameObject);
                }
            }

            pcrDataNum = gameMode.purchaseRateList.Count;

            Invoke("PurchaseRateChartPosReset", 0.1f);
        }
    }
    private void PurchaseRateChartPosReset()
    {
        float ss = pcrItemContent.GetComponent<RectTransform>().sizeDelta.x;
        pcrItemContent.transform.DOLocalMoveX(-ss, 0f);
    }

    public void AverageRevenueChart()
    {
        if (arptDataNum >= gameMode.revenuePerTimeList.Count)
        {
            //数据无更新
        }
        else if (arptDataNum < gameMode.revenuePerTimeList.Count)
        {
            //更新
            //标线更新
            int arptMax = gameMode.revenuePerTimeList.Max();
            int arptMin = gameMode.revenuePerTimeList.Min();
            int rrrmax = 1;
            float rrrmaxf = 1f;
            if (arptMax / arptMin <= 2)
            {
                rrrmaxf = arptMax * 2f;
            }
            else if (arptMax / arptMin > 2 && arptMax / arptMin <= 3)
            {
                rrrmaxf = arptMax * 1.8f;
            }
            else if (arptMax / arptMin > 3 && arptMax / arptMin <= 4)
            {
                rrrmaxf = arptMax * 1.6f;
            }
            else if (arptMax / arptMin > 4 && arptMax / arptMin <= 5)
            {
                rrrmaxf = arptMax * 1.4f;
            }
            else if (arptMax / arptMin > 5)
            {
                rrrmaxf = arptMax * 1.2f;
            }
            if (rrrmaxf < 1000)
            {
                rrrmax = Mathf.CeilToInt(rrrmaxf / 100) * 100;
            }
            else if (rrrmaxf >= 1000 && rrrmaxf < 1000000)
            {
                rrrmax = Mathf.CeilToInt(rrrmaxf / 1000) * 1000;
            }
            else if (rrrmaxf >= 1000000 && rrrmaxf < 1000000000)
            {
                rrrmax = Mathf.CeilToInt(rrrmaxf / 1000000) * 1000000;
            }
            else if (rrrmaxf >= 1000000000)
            {
                rrrmax = Mathf.CeilToInt(rrrmaxf / 1000000000) * 1000000000;
            }
            float rrrInter = rrrmax / 5f;
            int rrrLine1 = Mathf.RoundToInt(1 * rrrInter);
            int rrrLine2 = Mathf.RoundToInt(2 * rrrInter);
            int rrrLine3 = Mathf.RoundToInt(3 * rrrInter);
            int rrrLine4 = Mathf.RoundToInt(4 * rrrInter);
            arptYText1.GetComponent<Text>().text = numberText(rrrLine1);
            arptYText2.GetComponent<Text>().text = numberText(rrrLine2);
            arptYText3.GetComponent<Text>().text = numberText(rrrLine3);
            arptYText4.GetComponent<Text>().text = numberText(rrrLine4);
            arptYText5.GetComponent<Text>().text = numberText(rrrmax);

            //内容更新
            for (int i = 0; i < gameMode.revenuePerTimeList.Count; i++)
            {
                int rrr = gameMode.revenuePerTimeList[i];

                GameObject preItem = null;
                if (i == 0)
                {
                    preItem = null;
                }
                else
                {
                    preItem = arptItemContent.transform.GetChild(i - 1).gameObject;
                }

                if (i < arptDataNum)
                {
                    //刷新
                    arptItemContent.transform.GetChild(i).gameObject.GetComponent<ChartPointItem>().SetDetail(rrr, rrrmax, i, companyPanel, arptPanel, arptChartContent, preItem);
                }
                else if (i >= arptDataNum)
                {
                    //新增
                    GameObject pItem = Instantiate(arptPointItem) as GameObject;
                    pItem.transform.SetParent(arptItemContent.transform);

                    pItem.GetComponent<ChartPointItem>().SetDetail(rrr, rrrmax, arptDataNum + i, companyPanel, arptPanel, arptChartContent, preItem);
                }

                if (pcrItemContent.transform.childCount >= 45)
                {
                    //删减
                    //Destroy(pcrItemContent.transform.GetChild(0).gameObject);
                }
            }

            arptDataNum = gameMode.revenuePerTimeList.Count;

            Invoke("RevenuePerTimeChartPosReset", 0.1f);
        }
    }
    private void RevenuePerTimeChartPosReset()
    {
        float ss = arptItemContent.GetComponent<RectTransform>().sizeDelta.x;
        arptItemContent.transform.DOLocalMoveX(-ss, 0f);
    }

    public string numberText(int num)
    {
        string ttt = "";
        if(num < 1000)
        {
            ttt = num.ToString();
        }
        else if(num >= 1000 & num < 1000000)
        {
            ttt = ((float)num / 1000f).ToString("0.00") + "K";
        }
        else if (num >= 1000000 & num < 1000000000)
        {
            ttt = ((float)num / 1000000f).ToString("0.00") + "M";
        }
        else if (num >= 1000000000)
        {
            ttt = ((float)num / 1000000000f).ToString("0.00") + "B";
        }

        return ttt;
    }

}
