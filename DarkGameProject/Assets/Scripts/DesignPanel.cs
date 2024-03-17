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

    private GameObject gamePromotionBtn;
    private GameObject gamePromotionBtnShow0;
    private GameObject gamePromotionBtnShow1;
    private GameObject gamePromotionBtnShow1Item;
    private GameObject gamePromotionBtnShow1Point0;
    private GameObject gamePromotionBtnShow1Point1;

    private GameObject itemMoveBtn;
    private GameObject itemRotationBtn;
    private GameObject moveSlide;

    private GameMode gameMode;
    private CameraControl cameraControl;
    private GameplayMapping gameplayMapping;
    private GameplayEffect gameplayEffect;

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
    //clv
    private GameObject CLVNumText;
    public GameObject clvPointItem;
    private GameObject clvPanel;
    private GameObject clvChartContent;
    private GameObject clvItemContent;
    private int clvDataNum = 0;
    private GameObject clvYText1;
    private GameObject clvYText2;
    private GameObject clvYText3;
    private GameObject clvYText4;
    private GameObject clvYText5;
    //付费率 报表
    public GameObject pcrPointItem;
    private GameObject pcrPanel;
    private GameObject pcrChartContent;
    private GameObject pcrItemContent;
    private int pcrDataNum = 0;
    //付费额 报表
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
    //留存率 报表
    public GameObject rrPointItem;
    private GameObject rrPanel;
    private GameObject rrChartContent;
    private GameObject rrItemContent;
    private int rrDataNum = 0;
    //lifetime 报表
    public GameObject ltPointItem;
    private GameObject ltPanel;
    private GameObject ltChartContent;
    private GameObject ltItemContent;
    private int ltDataNum = 0;
    private GameObject ltYText1;
    private GameObject ltYText2;
    private GameObject ltYText3;
    private GameObject ltYText4;
    private GameObject ltYText5;
    //lifetime贡献
    private GameObject ltContributionPanel;
    private GameObject ltContributionCicle_1;
    private GameObject ltContributionCicle_2;
    private GameObject ltContributionCicle_3;
    private GameObject ltContributionCicle_4;
    private GameObject ltContributionCicle_5;
    private GameObject ltContributionCicle_6;
    private GameObject ltContributionCicle_7;
    private GameObject ltContributionCicle_8;
    private GameObject ltContributionCicle_9;
    private GameObject ltContributionCicle_10;
    private GameObject ltContributionItemDesc_1;
    private GameObject ltContributionItemDesc_2;
    private GameObject ltContributionItemDesc_3;
    private GameObject ltContributionItemDesc_4;
    private GameObject ltContributionItemDesc_5;
    private GameObject ltContributionItemDesc_6;
    private GameObject ltContributionItemDesc_7;
    private GameObject ltContributionItemDesc_8;
    private GameObject ltContributionItemDesc_9;
    private GameObject ltContributionItemDesc_10;
    private GameObject ltContributionItemDesc_11;
    //revenue贡献
    private GameObject reContributionPanel;
    private GameObject reContributionCicle_1;
    private GameObject reContributionCicle_2;
    private GameObject reContributionCicle_3;
    private GameObject reContributionCicle_4;
    private GameObject reContributionCicle_5;
    private GameObject reContributionCicle_6;
    private GameObject reContributionCicle_7;
    private GameObject reContributionCicle_8;
    private GameObject reContributionCicle_9;
    private GameObject reContributionCicle_10;
    private GameObject reContributionItemDesc_1;
    private GameObject reContributionItemDesc_2;
    private GameObject reContributionItemDesc_3;
    private GameObject reContributionItemDesc_4;
    private GameObject reContributionItemDesc_5;
    private GameObject reContributionItemDesc_6;
    private GameObject reContributionItemDesc_7;
    private GameObject reContributionItemDesc_8;
    private GameObject reContributionItemDesc_9;
    private GameObject reContributionItemDesc_10;
    private GameObject reContributionItemDesc_11;

    private bool companyReportBtnShow = false;
    private bool playerReportBtnShow = false;

    //升级panel
    public bool designItemPanelOpen = false;
    private GameObject designItemLevelPanelTarget;
    private GameObject designItemLevelPanelContent;
    private GameObject designItemLevelPanelBg;
    private GameObject designItemLevelPanel;
    private GameObject designItemLevelBgPicContent;
    private GameObject designItemLevelNum;
    private GameObject designItemLevelItemName;
    private GameObject designItemLevelPanelItemShow;
    private GameObject designItemLevelPanelCloseBtn;
    private GameObject designItemLevelPanelItemDesc;
    private GameObject designItemLevelPanelEffectContent;
    private GameObject designItemLevelPanelEffectItem_1;
    private GameObject designItemLevelPanelEffectItem_2;
    private GameObject designItemLevelPanelEffectItem_3;
    private GameObject designItemLevelPanelLevelUpBtn;
    private GameObject designItemLevelPanelLevelUpCost;
    private GameObject designItemLevelPanelLevelUpAllMoney;

    //推广
    public bool gamePromotionPanelOpen = false;
    private GameObject gamePromotionPanelContent;
    private GameObject gamePromotionPanelBg;
    private GameObject gamePromotionPanel;
    private GameObject gamePromotionCloseBtn;
    private GameObject promotionBasicInfo;
    private GameObject promotionBasicInfoCostPrice;
    private GameObject promotionBasicInfoIncomePrice;
    private GameObject promotionBasicInfoOutputUserText;
    private GameObject promotionBasicInfoOutputRevenueText;
    private GameObject promotingInfo;
    private GameObject promotingInfoUserText;
    private GameObject promotingInfoRevenueText;
    private GameObject promoteBtn;
    private GameObject promoteBtnCostContent;
    private GameObject promoteBtnMoneyCost;
    private GameObject promoteBtnInfoText;
    private bool gamePromotionBtnShow = false;
    public bool gamePromotionPanelPointAnime = false;

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

        gamePromotionBtn = mainSceneUI.transform.Find("GamePromotionBtn").gameObject;
        gamePromotionBtnShow0 = gamePromotionBtn.transform.Find("PromotionMode_0").gameObject;
        gamePromotionBtnShow1 = gamePromotionBtn.transform.Find("PromotionMode_1").gameObject;
        gamePromotionBtnShow1Item = gamePromotionBtnShow1.transform.Find("PromotionMode_1Item").gameObject;
        gamePromotionBtnShow1Point0 = gamePromotionBtnShow1.transform.Find("Point0").gameObject;
        gamePromotionBtnShow1Point1 = gamePromotionBtnShow1.transform.Find("Point1").gameObject;

        GameObject moveRotationContent = designPanel.transform.Find("MoveRotationContent").gameObject;
        moveSlide = moveRotationContent.transform.Find("Slide").gameObject;
        itemMoveBtn = moveRotationContent.transform.Find("ItemMoveBtn").gameObject;
        itemRotationBtn = moveRotationContent.transform.Find("ItemRotationBtn").gameObject;

        GameObject designPanelScrollView = designPanelContent.transform.Find("ScrollView").gameObject;
        GameObject designPanelViewport = designPanelScrollView.transform.Find("Viewport").gameObject;
        GameObject designPanelViewContent = designPanelViewport.transform.Find("ViewContent").gameObject;
        GameObject designPanelViewContentGameContent = designPanelViewContent.transform.Find("GameItemContent").gameObject;
        gameItemContent = designPanelViewContentGameContent.transform.Find("ItemContent").gameObject;
        GameObject designPanelViewContentDarkContent = designPanelViewContent.transform.Find("DarkItemContent").gameObject;
        darkItemContent = designPanelViewContentDarkContent.transform.Find("ItemContent").gameObject;

        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();
        cameraControl = GameObject.Find("Main Camera").gameObject.GetComponent<CameraControl>();
        gameplayMapping = GameObject.Find("Main Camera").gameObject.GetComponent<GameplayMapping>();
        gameplayEffect = GameObject.Find("Main Camera").gameObject.GetComponent<GameplayEffect>();

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

        GameObject companyPanelCLVContent = companyPanelBasicInfoBanner.transform.Find("CLVContent").gameObject;
        CLVNumText = companyPanelCLVContent.transform.Find("NumText").gameObject;
        clvPanel = companyPanelCLVContent.transform.Find("CLVChart").gameObject;
        clvChartContent = clvPanel.transform.Find("ChartContent").gameObject;
        GameObject clvScrollView = clvChartContent.transform.Find("ScrollView").gameObject;
        GameObject clvViewport = clvScrollView.transform.Find("Viewport").gameObject;
        clvItemContent = clvViewport.transform.Find("PointContent").gameObject;
        GameObject clvLine1 = clvChartContent.transform.Find("Line_1").gameObject;
        clvYText1 = clvLine1.transform.Find("Text").gameObject;
        GameObject clvLine2 = clvChartContent.transform.Find("Line_2").gameObject;
        clvYText2 = clvLine2.transform.Find("Text").gameObject;
        GameObject clvLine3 = clvChartContent.transform.Find("Line_3").gameObject;
        clvYText3 = clvLine3.transform.Find("Text").gameObject;
        GameObject clvLine4 = clvChartContent.transform.Find("Line_4").gameObject;
        clvYText4 = clvLine4.transform.Find("Text").gameObject;
        GameObject clvLine5 = clvChartContent.transform.Find("Line_5").gameObject;
        clvYText5 = clvLine5.transform.Find("Text").gameObject;

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

        rrPanel = companyPanelChartListContent.transform.Find("AverageRetentionRatePanel").gameObject;
        rrChartContent = rrPanel.transform.Find("ChartContent").gameObject;
        GameObject rrScrollView = rrChartContent.transform.Find("ScrollView").gameObject;
        GameObject rrViewport = rrScrollView.transform.Find("Viewport").gameObject;
        rrItemContent = rrViewport.transform.Find("PointContent").gameObject;

        ltPanel = companyPanelChartListContent.transform.Find("AverageLifetimePanel").gameObject;
        ltChartContent = ltPanel.transform.Find("ChartContent").gameObject;
        GameObject ltScrollView = ltChartContent.transform.Find("ScrollView").gameObject;
        GameObject ltViewport = ltScrollView.transform.Find("Viewport").gameObject;
        ltItemContent = ltViewport.transform.Find("PointContent").gameObject;
        GameObject ltLine1 = ltChartContent.transform.Find("Line_1").gameObject;
        ltYText1 = ltLine1.transform.Find("Text").gameObject;
        GameObject ltLine2 = ltChartContent.transform.Find("Line_2").gameObject;
        ltYText2 = ltLine2.transform.Find("Text").gameObject;
        GameObject ltLine3 = ltChartContent.transform.Find("Line_3").gameObject;
        ltYText3 = ltLine3.transform.Find("Text").gameObject;
        GameObject ltLine4 = ltChartContent.transform.Find("Line_4").gameObject;
        ltYText4 = ltLine4.transform.Find("Text").gameObject;
        GameObject ltLine5 = ltChartContent.transform.Find("Line_5").gameObject;
        ltYText5 = ltLine5.transform.Find("Text").gameObject;

        ltContributionPanel = companyPanelChartListContent.transform.Find("LifetimeContributionPanel").gameObject;
        GameObject ltContributionChartContent = ltContributionPanel.transform.Find("ChartContent").gameObject;
        GameObject ltContributionCirclePointContent = ltContributionChartContent.transform.Find("CirclePointContent").gameObject;
        ltContributionCicle_1 = ltContributionCirclePointContent.transform.GetChild(0).gameObject;
        ltContributionCicle_2 = ltContributionCirclePointContent.transform.GetChild(1).gameObject;
        ltContributionCicle_3 = ltContributionCirclePointContent.transform.GetChild(2).gameObject;
        ltContributionCicle_4 = ltContributionCirclePointContent.transform.GetChild(3).gameObject;
        ltContributionCicle_5 = ltContributionCirclePointContent.transform.GetChild(4).gameObject;
        ltContributionCicle_6 = ltContributionCirclePointContent.transform.GetChild(5).gameObject;
        ltContributionCicle_7 = ltContributionCirclePointContent.transform.GetChild(6).gameObject;
        ltContributionCicle_8 = ltContributionCirclePointContent.transform.GetChild(7).gameObject;
        ltContributionCicle_9 = ltContributionCirclePointContent.transform.GetChild(8).gameObject;
        ltContributionCicle_10 = ltContributionCirclePointContent.transform.GetChild(9).gameObject;
        GameObject ltContributionItemDescContent = ltContributionChartContent.transform.Find("DesItemContent").gameObject;
        ltContributionItemDesc_1 = ltContributionItemDescContent.transform.GetChild(0).gameObject;
        ltContributionItemDesc_2 = ltContributionItemDescContent.transform.GetChild(1).gameObject;
        ltContributionItemDesc_3 = ltContributionItemDescContent.transform.GetChild(2).gameObject;
        ltContributionItemDesc_4 = ltContributionItemDescContent.transform.GetChild(3).gameObject;
        ltContributionItemDesc_5 = ltContributionItemDescContent.transform.GetChild(4).gameObject;
        ltContributionItemDesc_6 = ltContributionItemDescContent.transform.GetChild(5).gameObject;
        ltContributionItemDesc_7 = ltContributionItemDescContent.transform.GetChild(6).gameObject;
        ltContributionItemDesc_8 = ltContributionItemDescContent.transform.GetChild(7).gameObject;
        ltContributionItemDesc_9 = ltContributionItemDescContent.transform.GetChild(8).gameObject;
        ltContributionItemDesc_10 = ltContributionItemDescContent.transform.GetChild(9).gameObject;
        ltContributionItemDesc_11 = ltContributionItemDescContent.transform.GetChild(10).gameObject;

        reContributionPanel = companyPanelChartListContent.transform.Find("RevenueContributionPanel").gameObject;
        GameObject reContributionChartContent = reContributionPanel.transform.Find("ChartContent").gameObject;
        GameObject reContributionCirclePointContent = reContributionChartContent.transform.Find("CirclePointContent").gameObject;
        reContributionCicle_1 = reContributionCirclePointContent.transform.GetChild(0).gameObject;
        reContributionCicle_2 = reContributionCirclePointContent.transform.GetChild(1).gameObject;
        reContributionCicle_3 = reContributionCirclePointContent.transform.GetChild(2).gameObject;
        reContributionCicle_4 = reContributionCirclePointContent.transform.GetChild(3).gameObject;
        reContributionCicle_5 = reContributionCirclePointContent.transform.GetChild(4).gameObject;
        reContributionCicle_6 = reContributionCirclePointContent.transform.GetChild(5).gameObject;
        reContributionCicle_7 = reContributionCirclePointContent.transform.GetChild(6).gameObject;
        reContributionCicle_8 = reContributionCirclePointContent.transform.GetChild(7).gameObject;
        reContributionCicle_9 = reContributionCirclePointContent.transform.GetChild(8).gameObject;
        reContributionCicle_10 = reContributionCirclePointContent.transform.GetChild(9).gameObject;
        GameObject reContributionItemDescContent = reContributionChartContent.transform.Find("DesItemContent").gameObject;
        reContributionItemDesc_1 = reContributionItemDescContent.transform.GetChild(0).gameObject;
        reContributionItemDesc_2 = reContributionItemDescContent.transform.GetChild(1).gameObject;
        reContributionItemDesc_3 = reContributionItemDescContent.transform.GetChild(2).gameObject;
        reContributionItemDesc_4 = reContributionItemDescContent.transform.GetChild(3).gameObject;
        reContributionItemDesc_5 = reContributionItemDescContent.transform.GetChild(4).gameObject;
        reContributionItemDesc_6 = reContributionItemDescContent.transform.GetChild(5).gameObject;
        reContributionItemDesc_7 = reContributionItemDescContent.transform.GetChild(6).gameObject;
        reContributionItemDesc_8 = reContributionItemDescContent.transform.GetChild(7).gameObject;
        reContributionItemDesc_9 = reContributionItemDescContent.transform.GetChild(8).gameObject;
        reContributionItemDesc_10 = reContributionItemDescContent.transform.GetChild(9).gameObject;
        reContributionItemDesc_11 = reContributionItemDescContent.transform.GetChild(10).gameObject;

        designItemLevelPanelContent = mainSceneUI.transform.Find("DesignItemLevelPanel").gameObject;
        designItemLevelPanelBg = designItemLevelPanelContent.transform.Find("Bg").gameObject;
        designItemLevelPanel = designItemLevelPanelContent.transform.Find("LevelPanel").gameObject;
        GameObject designItemLeveBgPicMask = designItemLevelPanel.transform.Find("BgPicMask").gameObject;
        designItemLevelBgPicContent = designItemLeveBgPicMask.transform.Find("BgPicContent").gameObject;
        GameObject designItemLevelPanelLevelContent = designItemLevelPanel.transform.Find("LevelContent").gameObject;
        designItemLevelNum = designItemLevelPanelLevelContent.transform.Find("LevelNum").gameObject;
        designItemLevelItemName = designItemLevelPanel.transform.Find("ItemName").gameObject;
        designItemLevelPanelItemShow = designItemLevelPanel.transform.Find("ItemShow").gameObject;
        designItemLevelPanelCloseBtn = designItemLevelPanel.transform.Find("CloseBtn").gameObject;
        designItemLevelPanelItemDesc = designItemLevelPanel.transform.Find("ItemDesc").gameObject;
        designItemLevelPanelEffectContent = designItemLevelPanel.transform.Find("ItemEffect").gameObject;
        designItemLevelPanelEffectItem_1 = designItemLevelPanelEffectContent.transform.Find("ItemEffectItem_1").gameObject;
        designItemLevelPanelEffectItem_2 = designItemLevelPanelEffectContent.transform.Find("ItemEffectItem_2").gameObject;
        designItemLevelPanelEffectItem_3 = designItemLevelPanelEffectContent.transform.Find("ItemEffectItem_3").gameObject;
        designItemLevelPanelLevelUpBtn = designItemLevelPanel.transform.Find("LevelUpBtn").gameObject;
        GameObject designItemLevelPanelLevelUpCostContent = designItemLevelPanelLevelUpBtn.transform.Find("CostContent").gameObject;
        designItemLevelPanelLevelUpCost = designItemLevelPanelLevelUpCostContent.transform.Find("MoneyCost").gameObject;
        designItemLevelPanelLevelUpAllMoney = designItemLevelPanelLevelUpCostContent.transform.Find("AllMoney").gameObject;

        gamePromotionPanelContent = mainSceneUI.transform.Find("GamePromotionPanel").gameObject;
        gamePromotionPanelBg = gamePromotionPanelContent.transform.Find("Bg").gameObject;
        gamePromotionPanel = gamePromotionPanelContent.transform.Find("PromotionPanel").gameObject;
        gamePromotionCloseBtn = gamePromotionPanel.transform.Find("CloseBtn").gameObject;
        promotionBasicInfo = gamePromotionPanel.transform.Find("PromotionBasicInfo").gameObject;
        promotionBasicInfoCostPrice = promotionBasicInfo.transform.Find("CostContent").gameObject.transform.Find("Price").gameObject;
        promotionBasicInfoIncomePrice = promotionBasicInfo.transform.Find("IncomeContent").gameObject.transform.Find("Price").gameObject;
        promotionBasicInfoOutputUserText = promotionBasicInfo.transform.Find("OutputContent").gameObject.transform.Find("ResultContent").gameObject.transform.Find("UserResult").gameObject.transform.Find("Text").gameObject;
        promotionBasicInfoOutputRevenueText = promotionBasicInfo.transform.Find("OutputContent").gameObject.transform.Find("ResultContent").gameObject.transform.Find("RevenueResult").gameObject.transform.Find("Text").gameObject;
        promotingInfo = gamePromotionPanel.transform.Find("PromotingInfo").gameObject;
        promotingInfoUserText = promotingInfo.transform.Find("UserResult").gameObject.transform.Find("Text").gameObject;
        promotingInfoRevenueText = promotingInfo.transform.Find("RevenueResult").gameObject.transform.Find("Text").gameObject;
        promoteBtn = gamePromotionPanel.transform.Find("PromoteBtn").gameObject;
        promoteBtnCostContent = promoteBtn.transform.Find("CostContent").gameObject;
        promoteBtnMoneyCost = promoteBtnCostContent.transform.Find("MoneyCost").gameObject;
        promoteBtnInfoText = promoteBtn.transform.Find("InfoText").gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        //GameItemIn();
        sceneContentBg.transform.localScale = new Vector3(0, 0, 0);

        designItemBtn.GetComponent<Button>().onClick.AddListener(DesignPanelAnime);
        designPanelClose.GetComponent<Button>().onClick.AddListener(DesignPanelAnime);

        //旋转模式
        itemMoveBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if (BasicAction.gameplayItemRotationMode == true)
            {
                BasicAction.gameplayItemRotationMode = false;
                moveSlide.transform.DOMove(itemMoveBtn.transform.position, 0.2f);
            }
        });
        itemRotationBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if(BasicAction.gameplayItemRotationMode == false)
            {
                BasicAction.gameplayItemRotationMode = true;
                moveSlide.transform.DOMove(itemRotationBtn.transform.position, 0.2f);
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
                //简单粗暴的升级（效果+3%）
                gameplayEffect.GameItemEffectLevelUp(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID);

                //新价格
                designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().levelPrice *= designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemLevel;
            }
        });

        //推广
        gamePromotionBtn.GetComponent<Button>().onClick.AddListener(GamePromotionPanelAnime);
        gamePromotionCloseBtn.GetComponent<Button>().onClick.AddListener(GamePromotionPanelAnime);
        promoteBtn.GetComponent<Button>().onClick.AddListener(delegate
        {
            if(gameMode.promotionMode == 0 && gameMode.promotionPrice <= gameMode.companyMoney)
            {
                gameMode.companyMoney -= gameMode.promotionPrice;
                gameMode.GamePromotionStart();
                GamePromotionPanelAnime();
            }
            else if (gameMode.promotionMode == 2)
            {
                gameMode.GamePromotionReset();
            }
        });

        //临时
        PlayerReportBtnIn();
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
            if (designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().levelPrice <= gameMode.companyMoney)
            {
                designItemLevelPanelLevelUpCost.GetComponent<Text>().color = Color.black;
            }
            else
            {
                designItemLevelPanelLevelUpCost.GetComponent<Text>().color = Color.red;
            }
            designItemLevelPanelLevelUpAllMoney.GetComponent<Text>().text = numberText(gameMode.companyMoney);

            //图片
            for (int i = 0; i < designItemLevelBgPicContent.transform.childCount; i++)
            {
                GameObject bgPicItem = designItemLevelBgPicContent.transform.GetChild(i).gameObject;

                string iName = "BgPic_" + designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID;
                if (bgPicItem.name == iName)
                {
                    bgPicItem.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                else
                {
                    bgPicItem.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                }
            }

            //属性
            if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).retention != 0 || gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).socialBound != 0)
            {
                designItemLevelPanelEffectItem_1.transform.localScale = new Vector3(1, 1, 1);
                designItemLevelPanelEffectItem_1.GetComponent<RectTransform>().sizeDelta = new Vector2(700f, 74f);
                //
                string retentionS = "";
                if(gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).retention != 0)
                {
                    string reSS = "";
                    if(Mathf.Abs(gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).retention) < 0.1)
                    {
                        reSS = "Marginally";
                    }
                    else if (Mathf.Abs(gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).retention) >= 0.1 && Mathf.Abs(gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).retention) < 0.2)
                    {
                        reSS = "Slightly";
                    }
                    else if (Mathf.Abs(gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).retention) >= 0.2 && Mathf.Abs(gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).retention) < 0.3)
                    {
                        reSS = "Moderately";
                    }
                    else if (Mathf.Abs(gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).retention) >= 0.3)
                    {
                        reSS = "Significantly";
                    }
                    string reSS2 = "";
                    if(gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).retention > 0)
                    {
                        reSS2 = " improves";
                        designItemLevelPanelEffectItem_1.transform.Find("Bg").gameObject.GetComponent<Image>().color = new Color(174 / 255f, 241 / 255f, 175 / 255f, 1f);
                    }
                    else
                    {
                        reSS2 = " diminishes";
                        designItemLevelPanelEffectItem_1.transform.Find("Bg").gameObject.GetComponent<Image>().color = new Color(241 / 255f, 174 / 255f, 175 / 255f, 1f);
                    }
                    retentionS = reSS + reSS2 + " player short-term retention. ";
                }

                string socialBoundS = "";
                if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).socialBound != 0)
                {
                    string sbSS = "";
                    if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).socialBound < 0.1)
                    {
                        sbSS = "Marginally";
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).socialBound >= 0.1 && gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).socialBound < 0.2)
                    {
                        sbSS = "Slightly";
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).socialBound >= 0.2 && gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).socialBound < 0.3)
                    {
                        sbSS = "Moderately";
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).socialBound >= 0.3)
                    {
                        sbSS = "Significantly";
                    }
                    designItemLevelPanelEffectItem_1.transform.Find("Bg").gameObject.GetComponent<Image>().color = new Color(174 / 255f, 241 / 255f, 175 / 255f, 1f);
                    socialBoundS = sbSS + " improves player's social connections to increase the cost of leaving for the long-term.";
                }

                designItemLevelPanelEffectItem_1.transform.Find("EffectDesc").gameObject.GetComponent<Text>().text = "· " + retentionS + socialBoundS;
            }
            else
            {
                designItemLevelPanelEffectItem_1.transform.localScale = new Vector3(1, 0, 1);
                designItemLevelPanelEffectItem_1.GetComponent<RectTransform>().sizeDelta = new Vector2(700f, 0f);
            }
            //
            if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingRate != 0 || gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingAmount != 0)
            {
                designItemLevelPanelEffectItem_2.transform.localScale = new Vector3(1, 1, 1);
                designItemLevelPanelEffectItem_2.GetComponent<RectTransform>().sizeDelta = new Vector2(700f, 74f);
                //
                string payingRateS = "";
                if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingRate != 0)
                {
                    string prSS = "";
                    if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingRate < 0.1)
                    {
                        if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).triggerRate != 1)
                        {
                            //支付事件
                            prSS = "Marginal";
                        }
                        else
                        {
                            prSS = "Marginally";
                        }
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingRate >= 0.1 && gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingRate < 0.2)
                    {
                        if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).triggerRate != 1)
                        {
                            //支付事件
                            prSS = "Slight";
                        }
                        else
                        {
                            prSS = "Slightly";
                        }
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingRate >= 0.2 && gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingRate < 0.3)
                    {
                        if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).triggerRate != 1)
                        {
                            //支付事件
                            prSS = "Moderate";
                        }
                        else
                        {
                            prSS = "Moderately";
                        }
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingRate >= 0.3)
                    {
                        if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).triggerRate != 1)
                        {
                            //支付事件
                            prSS = "Significant";
                        }
                        else
                        {
                            prSS = "Significantly";
                        }
                    }

                    if(gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).triggerRate != 1)
                    {
                        //支付事件
                        payingRateS = prSS + " paying conversion rate";
                    }
                    else
                    {
                        payingRateS = prSS + " improves player paying conversion rate. ";
                    }
                    
                }

                string payingAmountS = "";
                if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingAmount != 0)
                {
                    string paSS = "";
                    if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingAmount < 0.1)
                    {
                        if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).triggerRate != 1)
                        {
                            //支付事件
                            paSS = "Marginal";
                        }
                        else
                        {
                            paSS = "Marginally";
                        }
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingAmount >= 0.1 && gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingAmount < 0.2)
                    {
                        if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).triggerRate != 1)
                        {
                            //支付事件
                            paSS = "Slight";
                        }
                        else
                        {
                            paSS = "Slightly";
                        }
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingAmount >= 0.2 && gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingAmount < 0.3)
                    {
                        if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).triggerRate != 1)
                        {
                            //支付事件
                            paSS = "Moderate";
                        }
                        else
                        {
                            paSS = "Moderately";
                        }
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).payingAmount >= 0.3)
                    {
                        if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).triggerRate != 1)
                        {
                            //支付事件
                            paSS = "Significant";
                        }
                        else
                        {
                            paSS = "Significantly";
                        }
                    }

                    if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).triggerRate != 1)
                    {
                        //支付事件
                        payingAmountS = paSS + " paying amount";
                    }
                    else
                    {
                        payingAmountS = paSS + " improves player's paying amount.";
                    }
                }

                if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).triggerRate != 1)
                {
                    //支付事件
                    string linkw = "";
                    if(payingRateS != "" && payingAmountS != "")
                    {
                        linkw = " and ";
                    }
                    designItemLevelPanelEffectItem_2.transform.Find("EffectDesc").gameObject.GetComponent<Text>().text = "· Probability encourages player's payment, with " + payingRateS + linkw + payingAmountS + ".";
                }
                else
                {
                    designItemLevelPanelEffectItem_2.transform.Find("EffectDesc").gameObject.GetComponent<Text>().text = "· " + payingRateS + payingAmountS;
                }
            }
            else
            {
                designItemLevelPanelEffectItem_2.transform.localScale = new Vector3(1, 0, 1);
                designItemLevelPanelEffectItem_2.GetComponent<RectTransform>().sizeDelta = new Vector2(700f, 0f);
            }
            //心情
            if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).mood != 0)
            {
                designItemLevelPanelEffectItem_3.transform.localScale = new Vector3(1, 1, 1);
                designItemLevelPanelEffectItem_3.GetComponent<RectTransform>().sizeDelta = new Vector2(700f, 74f);
                //
                string moodS = "";
                if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).mood != 0)
                {
                    string mdSS = "";
                    if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).mood < 0 && gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).mood >= -5)
                    {
                        mdSS = "Marginally";
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).mood >= -10 && gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).mood < -5)
                    {
                        mdSS = "Slightly";
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).mood >= -15 && gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).mood < -10)
                    {
                        mdSS = "Moderately";
                    }
                    else if (gameplayEffect.GetGameItemEffectList(designItemLevelPanelTarget.GetComponent<GameplayItemUIItem>().itemID).mood < -15)
                    {
                        mdSS = "Significantly";
                    }
                    moodS = mdSS + " impacts the player's attitude towards the game.";
                }

                designItemLevelPanelEffectItem_3.transform.Find("EffectDesc").gameObject.GetComponent<Text>().text = "· " + moodS;
            }
            else
            {
                designItemLevelPanelEffectItem_3.transform.localScale = new Vector3(1, 0, 1);
                designItemLevelPanelEffectItem_3.GetComponent<RectTransform>().sizeDelta = new Vector2(700f, 0f);
            }

        }

        PromotingUI();
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

                moveSlide.transform.position = itemMoveBtn.transform.position;
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

                gameplayMapping.AllMapRoadListNew(); //地图位置列表获取

                //刷新一圈的距离
                if(gameMode.gameDynamicProcess == true)
                {
                    GameObject.Find("GameplayItem_101(Clone)").GetComponent<GameplayItem>().MainRoadDistanceCal();
                }

                //检测场上item的全局效果并构建效果列表
                gameMode.ObjectGlobalEffectListCreate();
            }
        }
    }

    void UIUpdate()
    {
        //钱
        if(gameMode.companyMoney < 1000)
        {
            moneyShow.GetComponent<Text>().text = gameMode.companyMoney.ToString("0");
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
            gameMode.maxPlayer0 += gameMode.maxPlayerAdd;
            gameMode.playerExp = 0;
            gameMode.levelUpExp = (int)(gameMode.basicLevelUpExp * (1 + (gameMode.playerLevel * gameMode.levelUpExpIncreaseValue / 10f)));

            GameItemUnlock();

            //公司报表界面解锁
            if(companyReportBtnShow == false && gameMode.playerLevel >= 3 && gameMode.startStatistics == true)
            {
                companyReportBtnShow = true;

                ComReportBtnIn();
            }

            //玩家界面解锁
            if (playerReportBtnShow == false && gameMode.playerLevel >= 1)
            {
                playerReportBtnShow = true;

                PlayerReportBtnIn();
            }

            //推广界面解锁
            if (gamePromotionBtnShow == false && gameMode.lastCLV > 0 && gameMode.playerLevel >= 1)
            {
                gamePromotionBtnShow = true;

                GamePromotionBtnIn();

                gameMode.GamePromotionCalculation();
            }
        }
    }

    void ScenePanelSwitch(int no)
    {
        //切换
        if(scenePanelAnime == false && sceneNo != no)
        {
            if(no == 1 || no == 3)
            {
                //游戏暂停
                gameMode.gameProcessPause = true;
                //地图锁定
                cameraControl.cameraCanMove = false;

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
                cameraControl.cameraCanMove = true;
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

    //推广界面
    public void GamePromotionPanelAnime()
    {
        if (gamePromotionPanelOpen == false)
        {
            gameMode.gameProcessPause = true;

            gamePromotionPanel.transform.localScale = new Vector3(0, 0, 0);

            gamePromotionPanelOpen = true;

            gamePromotionPanelBg.transform.localScale = new Vector3(1, 1, 1);
            gamePromotionPanelBg.GetComponent<Image>().DOFade(0.8f, 0.3f);

            Tweener anime = gamePromotionPanel.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
            anime.OnComplete(() => gamePromotionPanelPointAnime = true);
        }
        else
        {
            gamePromotionPanelPointAnime = false;

            Tweener anime = gamePromotionPanel.transform.DOScale(new Vector3(0, 0, 0), 0.3f);
            gamePromotionPanelBg.GetComponent<Image>().DOFade(0f, 0.3f);

            anime.OnComplete(() => gamePromotionPanelBg.transform.localScale = new Vector3(0, 0, 0));
            anime.OnKill(() => gamePromotionPanelOpen = false);

            gameMode.gameProcessPause = false;
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
    //具体报表
    //付费率
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

                pItem.GetComponent<ChartPointItem>().pointImage.GetComponent<Image>().color = new Color(72f / 255f, 173 / 255f, 0f, 1f);
                pItem.GetComponent<ChartPointItem>().lineImage.GetComponent<Image>().color = new Color(72f / 255f, 173 / 255f, 0f, 1f);

                if (pcrItemContent.transform.childCount >= 45)
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
    //每用户付费额
    public void AverageRevenuePerUserChart()
    {
        if(gameMode.revenuePerUsersList.Count > 0)
        {
            if (arptDataNum >= gameMode.revenuePerUsersList.Count)
            {
                //数据无更新
            }
            else if (arptDataNum < gameMode.revenuePerUsersList.Count)
            {
                //更新
                //标线更新
                float arptMax = (float)gameMode.revenuePerUsersList.Max();
                float? arptMin = gameMode.revenuePerUsersList.Where(x => x != 0).DefaultIfEmpty().Min();
                if (arptMin.HasValue == false)
                {
                    arptMin = 0;
                }
                float rrrmax = 1f;
                float rrrmaxf = 1f;
                //Debug.Log(arptMin + " - " + arptMax);
                if(arptMin == 0f)
                {
                    if(arptMax == 0f)
                    {
                        arptMax = 100f;
                        arptMin = 50f;
                    }
                    else
                    {
                        arptMin = arptMax / 2f;

                        if(arptMin == 0f)
                        {
                            arptMin = arptMax;
                        }
                    }
                    
                }

                if (arptMax / arptMin <= 2f)
                {
                    rrrmaxf = arptMax * 3f;
                }
                else if (arptMax / arptMin > 2f && arptMax / arptMin <= 3f)
                {
                    rrrmaxf = arptMax * 2.5f;
                }
                else if (arptMax / arptMin > 3f && arptMax / arptMin <= 4f)
                {
                    rrrmaxf = arptMax * 2f;
                }
                else if (arptMax / arptMin > 4f && arptMax / arptMin <= 5f)
                {
                    rrrmaxf = arptMax * 1.5f;
                }
                else if (arptMax / arptMin > 5f)
                {
                    rrrmaxf = arptMax * 1.2f;
                }

                if (rrrmaxf < 1f)
                {
                    rrrmax = rrrmaxf;
                }
                else if (rrrmaxf >= 1 && rrrmaxf < 10)
                {
                    rrrmax = rrrmaxf;
                }
                else if (rrrmaxf >= 10 && rrrmaxf < 100)
                {
                    rrrmax = Mathf.CeilToInt(rrrmaxf / 10) * 10;
                }
                else if (rrrmaxf >= 100 && rrrmaxf < 1000)
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
                float rrrLine1 = 1 * rrrInter;
                float rrrLine2 = 2 * rrrInter;
                float rrrLine3 = 3 * rrrInter;
                float rrrLine4 = 4 * rrrInter;
                arptYText1.GetComponent<Text>().text = numberText(rrrLine1);
                arptYText2.GetComponent<Text>().text = numberText(rrrLine2);
                arptYText3.GetComponent<Text>().text = numberText(rrrLine3);
                arptYText4.GetComponent<Text>().text = numberText(rrrLine4);
                arptYText5.GetComponent<Text>().text = numberText(rrrmax);

                //内容更新
                for (int i = 0; i < gameMode.revenuePerUsersList.Count; i++)
                {
                    float rrr = gameMode.revenuePerUsersList[i];

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

                        pItem.GetComponent<ChartPointItem>().SetDetail(rrr, rrrmax, i, companyPanel, arptPanel, arptChartContent, preItem);

                        pItem.GetComponent<ChartPointItem>().pointImage.GetComponent<Image>().color = new Color(219f / 255f, 0f, 189f / 255f, 1f);
                        pItem.GetComponent<ChartPointItem>().lineImage.GetComponent<Image>().color = new Color(219f / 255f, 0f, 189f / 255f, 1f);
                    }

                    if (arptItemContent.transform.childCount >= 45)
                    {
                        //删减
                        //Destroy(pcrItemContent.transform.GetChild(0).gameObject);
                    }
                }

                arptDataNum = gameMode.revenuePerUsersList.Count;

                Invoke("AverageRevenuePerUserChartPosReset", 0.1f);
            }
        }
        
    }
    private void AverageRevenuePerUserChartPosReset()
    {
        float ss = arptItemContent.GetComponent<RectTransform>().sizeDelta.x;
        arptItemContent.transform.DOLocalMoveX(-ss, 0f);
    }
    //留存率
    public void RetentionRateChart()
    {
        if (rrDataNum >= gameMode.retentionRateList.Count)
        {
            //数据无更新
        }
        else if (rrDataNum < gameMode.retentionRateList.Count)
        {
            //更新
            int needAddNum = gameMode.retentionRateList.Count - rrDataNum;

            for (int i = 0; i < needAddNum; i++)
            {
                float rrr = gameMode.retentionRateList[rrDataNum + i];

                GameObject pItem = Instantiate(rrPointItem) as GameObject;
                pItem.transform.SetParent(rrItemContent.transform);

                GameObject rrItem = pItem;
                if (rrDataNum + i > 0)
                {
                    rrItem = rrItemContent.transform.GetChild(rrDataNum + i - 1).gameObject;
                }
                else
                {
                    rrItem = null;
                }

                pItem.GetComponent<ChartPointItem>().SetDetail(rrr, 1, rrDataNum + i, companyPanel, rrPanel, rrChartContent, rrItem);

                pItem.GetComponent<ChartPointItem>().pointImage.GetComponent<Image>().color = new Color(1f, 138f / 255f, 0f, 1f);
                pItem.GetComponent<ChartPointItem>().lineImage.GetComponent<Image>().color = new Color(1f, 138f / 255f, 0f, 1f);

                if (rrItemContent.transform.childCount >= 45)
                {
                    //删减
                    //Destroy(rrItemContent.transform.GetChild(0).gameObject);
                }
            }

            rrDataNum = gameMode.retentionRateList.Count;

            Invoke("RetentionRateChartPosReset", 0.1f);
        }
    }
    private void RetentionRateChartPosReset()
    {
        float ss = rrItemContent.GetComponent<RectTransform>().sizeDelta.x;
        rrItemContent.transform.DOLocalMoveX(-ss, 0f);
    }
    //时段lifetime
    public void AverageLifetimeChart()
    {
        if (gameMode.userLifetimeList.Count > 0)
        {
            if (ltDataNum >= gameMode.userLifetimeList.Count)
            {
                //数据无更新
            }
            else if (ltDataNum < gameMode.userLifetimeList.Count)
            {
                //更新
                //标线更新
                float ltMax = (float)gameMode.userLifetimeList.Max();
                float? ltMin = gameMode.userLifetimeList.Where(x => x != 0).DefaultIfEmpty().Min();
                if(ltMin.HasValue == false)
                {
                    ltMin = 0;
                }
                float rrrmax = 1f;
                float rrrmaxf = 1f;
                //Debug.Log(arptMin + " - " + arptMax);
                if (ltMin == 0f)
                {
                    if (ltMax == 0f)
                    {
                        ltMax = 100f;
                        ltMin = 50f;
                    }
                    else
                    {
                        ltMin = ltMax / 2f;

                        if (ltMin == 0f)
                        {
                            ltMin = ltMax;
                        }
                    }

                }

                if (ltMax / ltMin <= 2f)
                {
                    rrrmaxf = ltMax * 3f;
                }
                else if (ltMax / ltMin > 2f && ltMax / ltMin <= 3f)
                {
                    rrrmaxf = ltMax * 2.5f;
                }
                else if (ltMax / ltMin > 3f && ltMax / ltMin <= 4f)
                {
                    rrrmaxf = ltMax * 2f;
                }
                else if (ltMax / ltMin > 4f && ltMax / ltMin <= 5f)
                {
                    rrrmaxf = ltMax * 1.5f;
                }
                else if (ltMax / ltMin > 5f)
                {
                    rrrmaxf = ltMax * 1.2f;
                }

                if (rrrmaxf < 1f)
                {
                    rrrmax = rrrmaxf;
                }
                else if (rrrmaxf >= 1 && rrrmaxf < 10)
                {
                    rrrmax = rrrmaxf;
                }
                else if (rrrmaxf >= 10 && rrrmaxf < 100)
                {
                    rrrmax = Mathf.CeilToInt(rrrmaxf / 10) * 10;
                }
                else if (rrrmaxf >= 100 && rrrmaxf < 1000)
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
                float rrrLine1 = 1 * rrrInter;
                float rrrLine2 = 2 * rrrInter;
                float rrrLine3 = 3 * rrrInter;
                float rrrLine4 = 4 * rrrInter;
                ltYText1.GetComponent<Text>().text = numberText(rrrLine1);
                ltYText2.GetComponent<Text>().text = numberText(rrrLine2);
                ltYText3.GetComponent<Text>().text = numberText(rrrLine3);
                ltYText4.GetComponent<Text>().text = numberText(rrrLine4);
                ltYText5.GetComponent<Text>().text = numberText(rrrmax);

                //内容更新
                for (int i = 0; i < gameMode.userLifetimeList.Count; i++)
                {
                    float rrr = gameMode.userLifetimeList[i];

                    GameObject ltItem = null;
                    if (i == 0)
                    {
                        ltItem = null;
                    }
                    else
                    {
                        ltItem = ltItemContent.transform.GetChild(i - 1).gameObject;
                    }

                    if (i < ltDataNum)
                    {
                        //刷新
                        ltItemContent.transform.GetChild(i).gameObject.GetComponent<ChartPointItem>().SetDetail(rrr, rrrmax, i, companyPanel, ltPanel, ltChartContent, ltItem);
                    }
                    else if (i >= ltDataNum)
                    {
                        //新增
                        GameObject pItem = Instantiate(ltPointItem) as GameObject;
                        pItem.transform.SetParent(ltItemContent.transform);

                        pItem.GetComponent<ChartPointItem>().SetDetail(rrr, rrrmax, i, companyPanel, ltPanel, ltChartContent, ltItem);

                        pItem.GetComponent<ChartPointItem>().pointImage.GetComponent<Image>().color = new Color(222f / 255f, 29f / 255f, 32f / 255f, 1f);
                        pItem.GetComponent<ChartPointItem>().lineImage.GetComponent<Image>().color = new Color(222f / 255f, 29f / 255f, 32f / 255f, 1f);
                    }

                    if (ltItemContent.transform.childCount >= 45)
                    {
                        //删减
                        //Destroy(pcrItemContent.transform.GetChild(0).gameObject);
                    }
                }

                ltDataNum = gameMode.userLifetimeList.Count;

                Invoke("AverageLifetimeChartPosReset", 0.1f);
            }
        }
    }
    private void AverageLifetimeChartPosReset()
    {
        float ss = ltItemContent.GetComponent<RectTransform>().sizeDelta.x;
        ltItemContent.transform.DOLocalMoveX(-ss, 0f);
    }
    //lifetime的贡献饼状图
    public void LifetimePieChart()
    {
        if(gameMode.sortedContributionOfLivetime.Count > 0)
        {
            float allLifeT = 0f;

            for (int i = 0; i < gameMode.sortedContributionOfLivetime.Count; i++)
            {
                float targetLT = gameMode.sortedContributionOfLivetime[i][1];
                allLifeT += targetLT;
            }

            //计算比例并排序
            float top10LT = 0f;
            for (int i = 0; i < 10; i++)
            {
                int itemID = 0;
                float targetLT = 0f;

                if (i < gameMode.sortedContributionOfLivetime.Count)
                {
                    itemID = (int)gameMode.sortedContributionOfLivetime[i][0];
                    targetLT = gameMode.sortedContributionOfLivetime[i][1];
                }

                if (itemID != 0)
                {
                    top10LT += targetLT;
                }

                float propotion = targetLT / allLifeT;
                float preCumulativePropotion = (top10LT - targetLT) / allLifeT;

                GameObject targetCircleItme = ltContributionCicle_1;
                GameObject targetDescItme = ltContributionItemDesc_1;

                if (i == 0)
                {
                    targetCircleItme = ltContributionCicle_1;
                    targetDescItme = ltContributionItemDesc_1;
                }
                else if (i == 1)
                {
                    targetCircleItme = ltContributionCicle_2;
                    targetDescItme = ltContributionItemDesc_2;
                }
                else if (i == 2)
                {
                    targetCircleItme = ltContributionCicle_3;
                    targetDescItme = ltContributionItemDesc_3;
                }
                else if (i == 3)
                {
                    targetCircleItme = ltContributionCicle_4;
                    targetDescItme = ltContributionItemDesc_4;
                }
                else if (i == 4)
                {
                    targetCircleItme = ltContributionCicle_5;
                    targetDescItme = ltContributionItemDesc_5;
                }
                else if (i == 5)
                {
                    targetCircleItme = ltContributionCicle_6;
                    targetDescItme = ltContributionItemDesc_6;
                }
                else if (i == 6)
                {
                    targetCircleItme = ltContributionCicle_7;
                    targetDescItme = ltContributionItemDesc_7;
                }
                else if (i == 7)
                {
                    targetCircleItme = ltContributionCicle_8;
                    targetDescItme = ltContributionItemDesc_8;
                }
                else if (i == 8)
                {
                    targetCircleItme = ltContributionCicle_9;
                    targetDescItme = ltContributionItemDesc_9;
                }
                else if (i == 9)
                {
                    targetCircleItme = ltContributionCicle_10;
                    targetDescItme = ltContributionItemDesc_10;
                }

                if (itemID != 0)
                {
                    targetCircleItme.transform.eulerAngles = new Vector3(0, 0, -preCumulativePropotion * 360f);
                    targetCircleItme.GetComponent<Image>().fillAmount = propotion;

                    targetDescItme.SetActive(true);
                    //targetDescItme.transform.localScale = new Vector3(1, 1, 1);
                    //targetDescItme.GetComponent<RectTransform>().sizeDelta = new Vector2(33f, 57f);
                    targetDescItme.transform.Find("Text").gameObject.GetComponent<Text>().text = itemName(itemID);
                }
                else
                {
                    targetCircleItme.GetComponent<Image>().fillAmount = 0;

                    targetDescItme.SetActive(false);
                    //targetDescItme.transform.localScale = new Vector3(1, 0, 1);
                    //targetDescItme.GetComponent<RectTransform>().sizeDelta = new Vector2(33f, 0f);
                }

                if (top10LT / allLifeT < 0.99f)
                {
                    ltContributionItemDesc_11.SetActive(true);
                    //ltContributionItemDesc_11.transform.localScale = new Vector3(1, 0, 1);
                    //ltContributionItemDesc_11.GetComponent<RectTransform>().sizeDelta = new Vector2(33f, 0f);
                }
                else
                {
                    ltContributionItemDesc_11.SetActive(false);
                    //ltContributionItemDesc_11.transform.localScale = new Vector3(1, 1, 1);
                    //ltContributionItemDesc_11.GetComponent<RectTransform>().sizeDelta = new Vector2(33f, 57f);
                }

            }
        }
    }
    //收入的贡献饼状图
    public void RevenuePieChart()
    {
        if (gameMode.sortedContributionOfRevenue.Count > 0)
        {
            float allM = 0f;

            for (int i = 0; i < gameMode.sortedContributionOfRevenue.Count; i++)
            {
                float targetM = gameMode.sortedContributionOfRevenue[i][1];
                allM += targetM;
            }

            //计算比例并排序
            float top10M = 0f;
            for (int i = 0; i < 10; i++)
            {
                int itemID = 0;
                float targetM = 0f;

                if (i < gameMode.sortedContributionOfRevenue.Count)
                {
                    itemID = (int)gameMode.sortedContributionOfRevenue[i][0];
                    targetM = gameMode.sortedContributionOfRevenue[i][1];
                }

                if (itemID != 0)
                {
                    top10M += targetM;
                }

                float propotion = targetM / allM;
                float preCumulativePropotion = (top10M - targetM) / allM;

                GameObject targetCircleItme = reContributionCicle_1;
                GameObject targetDescItme = reContributionItemDesc_1;

                if (i == 0)
                {
                    targetCircleItme = reContributionCicle_1;
                    targetDescItme = reContributionItemDesc_1;
                }
                else if (i == 1)
                {
                    targetCircleItme = reContributionCicle_2;
                    targetDescItme = reContributionItemDesc_2;
                }
                else if (i == 2)
                {
                    targetCircleItme = reContributionCicle_3;
                    targetDescItme = reContributionItemDesc_3;
                }
                else if (i == 3)
                {
                    targetCircleItme = reContributionCicle_4;
                    targetDescItme = reContributionItemDesc_4;
                }
                else if (i == 4)
                {
                    targetCircleItme = reContributionCicle_5;
                    targetDescItme = reContributionItemDesc_5;
                }
                else if (i == 5)
                {
                    targetCircleItme = reContributionCicle_6;
                    targetDescItme = reContributionItemDesc_6;
                }
                else if (i == 6)
                {
                    targetCircleItme = reContributionCicle_7;
                    targetDescItme = reContributionItemDesc_7;
                }
                else if (i == 7)
                {
                    targetCircleItme = reContributionCicle_8;
                    targetDescItme = reContributionItemDesc_8;
                }
                else if (i == 8)
                {
                    targetCircleItme = reContributionCicle_9;
                    targetDescItme = reContributionItemDesc_9;
                }
                else if (i == 9)
                {
                    targetCircleItme = reContributionCicle_10;
                    targetDescItme = reContributionItemDesc_10;
                }

                if (itemID != 0)
                {
                    targetCircleItme.transform.eulerAngles = new Vector3(0, 0, -preCumulativePropotion * 360f);
                    targetCircleItme.GetComponent<Image>().fillAmount = propotion;

                    targetDescItme.SetActive(true);
                    //targetDescItme.transform.localScale = new Vector3(1, 1, 1);
                    //targetDescItme.GetComponent<RectTransform>().sizeDelta = new Vector2(33f, 57f);
                    targetDescItme.transform.Find("Text").gameObject.GetComponent<Text>().text = itemName(itemID);
                }
                else
                {
                    targetCircleItme.GetComponent<Image>().fillAmount = 0;

                    targetDescItme.SetActive(false);
                    //targetDescItme.transform.localScale = new Vector3(1, 0, 1);
                    //targetDescItme.GetComponent<RectTransform>().sizeDelta = new Vector2(33f, 0f);
                }

                if (top10M / allM < 0.99f)
                {
                    ltContributionItemDesc_11.SetActive(false);
                    //ltContributionItemDesc_11.transform.localScale = new Vector3(1, 0, 1);
                    //ltContributionItemDesc_11.GetComponent<RectTransform>().sizeDelta = new Vector2(33f, 0f);
                }
                else
                {
                    ltContributionItemDesc_11.SetActive(true);
                    //ltContributionItemDesc_11.transform.localScale = new Vector3(1, 1, 1);
                    //ltContributionItemDesc_11.GetComponent<RectTransform>().sizeDelta = new Vector2(33f, 57f);
                }

            }
        }
    }
    //clv
    public void CustomerLifetimeValueChart()
    {
        CLVNumText.GetComponent<Text>().text = numberText(gameMode.lastCLV);

        if (gameMode.clvList.Count > 0)
        {
            if (clvDataNum >= gameMode.clvList.Count)
            {
                //数据无更新
            }
            else if (clvDataNum < gameMode.clvList.Count)
            {
                //更新
                //标线更新
                float clvMax = (float)gameMode.clvList.Max();
                float? clvMin = gameMode.clvList.Where(x => x != 0).DefaultIfEmpty().Min();
                if (clvMin.HasValue == false)
                {
                    clvMin = 0;
                }
                float rrrmax = 1f;
                float rrrmaxf = 1f;
                //Debug.Log(arptMin + " - " + arptMax);
                if (clvMin == 0f)
                {
                    if (clvMax == 0f)
                    {
                        clvMax = 100f;
                        clvMin = 50f;
                    }
                    else
                    {
                        clvMin = clvMax / 2f;

                        if (clvMin == 0f)
                        {
                            clvMin = clvMax;
                        }
                    }

                }

                if (clvMax / clvMin <= 2f)
                {
                    rrrmaxf = clvMax * 3f;
                }
                else if (clvMax / clvMin > 2f && clvMax / clvMin <= 3f)
                {
                    rrrmaxf = clvMax * 2.5f;
                }
                else if (clvMax / clvMin > 3f && clvMax / clvMin <= 4f)
                {
                    rrrmaxf = clvMax * 2f;
                }
                else if (clvMax / clvMin > 4f && clvMax / clvMin <= 5f)
                {
                    rrrmaxf = clvMax * 1.5f;
                }
                else if (clvMax / clvMin > 5f)
                {
                    rrrmaxf = clvMax * 1.2f;
                }

                if (rrrmaxf < 1f)
                {
                    rrrmax = rrrmaxf;
                }
                else if (rrrmaxf >= 1 && rrrmaxf < 10)
                {
                    rrrmax = rrrmaxf;
                }
                else if (rrrmaxf >= 10 && rrrmaxf < 100)
                {
                    rrrmax = Mathf.CeilToInt(rrrmaxf / 10) * 10;
                }
                else if (rrrmaxf >= 100 && rrrmaxf < 1000)
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
                float rrrLine1 = 1 * rrrInter;
                float rrrLine2 = 2 * rrrInter;
                float rrrLine3 = 3 * rrrInter;
                float rrrLine4 = 4 * rrrInter;
                clvYText1.GetComponent<Text>().text = numberText(rrrLine1);
                clvYText2.GetComponent<Text>().text = numberText(rrrLine2);
                clvYText3.GetComponent<Text>().text = numberText(rrrLine3);
                clvYText4.GetComponent<Text>().text = numberText(rrrLine4);
                clvYText5.GetComponent<Text>().text = numberText(rrrmax);

                //内容更新
                for (int i = 0; i < gameMode.clvList.Count; i++)
                {
                    float rrr = gameMode.clvList[i];

                    GameObject preItem = null;
                    if (i == 0)
                    {
                        preItem = null;
                    }
                    else
                    {
                        preItem = clvItemContent.transform.GetChild(i - 1).gameObject;
                    }

                    if (i < clvDataNum)
                    {
                        //刷新
                        clvItemContent.transform.GetChild(i).gameObject.GetComponent<ChartPointItem>().SetDetail(rrr, rrrmax, i, companyPanel, clvPanel, clvChartContent, preItem);
                    }
                    else if (i >= clvDataNum)
                    {
                        //新增
                        GameObject pItem = Instantiate(clvPointItem) as GameObject;
                        pItem.transform.SetParent(clvItemContent.transform);

                        pItem.GetComponent<ChartPointItem>().SetDetail(rrr, rrrmax, i, companyPanel, clvPanel, clvChartContent, preItem);

                        pItem.GetComponent<ChartPointItem>().pointImage.GetComponent<Image>().color = new Color(233f / 255f, 23f / 255f, 8f / 255f, 1f);
                        pItem.GetComponent<ChartPointItem>().lineImage.GetComponent<Image>().color = new Color(233f / 255f, 23f / 255f, 8f / 255f, 1f);
                    }

                    if (clvItemContent.transform.childCount >= 45)
                    {
                        //删减
                        //Destroy(pcrItemContent.transform.GetChild(0).gameObject);
                    }
                }

                clvDataNum = gameMode.clvList.Count;

                Invoke("CustomerLifetimeValueChartPosReset", 0.1f);
            }
        }
    }
    private void CustomerLifetimeValueChartPosReset()
    {
        float ss = clvItemContent.GetComponent<RectTransform>().sizeDelta.x;
        clvItemContent.transform.DOLocalMoveX(-ss, 0f);
    }

    public string numberText(float num)
    {
        string ttt = "";

        if (num < 1)
        {
            ttt = num.ToString("0.00");
        }
        else if (num >= 1 && num < 10)
        {
            ttt = num.ToString("0.00");
        }
        else if (num >= 10 && num < 1000)
        {
            ttt = ((int)num).ToString();
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

    //报表按钮入场
    public void ReportBtnIn(int type)
    {
        if(type == 1 && gameMode.startStatistics == false)
        {
            gameMode.startStatistics = true;
        }
        else if(type == 2)
        {

        }
    }
    public void ComReportBtnIn()
    {
        companyReportBtnShow = true;
        companySceneBtn.transform.DOLocalMoveX(200f, 0.3f).SetRelative();
    }
    public void PlayerReportBtnIn()
    {
        playerReportBtnShow = true;
        playerSceneBtn.transform.DOLocalMoveX(200f, 0.3f).SetRelative();
    }

    //推广按钮入场
    public void GamePromotionBtnIn()
    {
        gamePromotionBtnShow = true;
        gamePromotionBtn.transform.DOLocalMoveY(300f, 0.3f).SetRelative();
    }

    string itemName(int itemID)
    {
        string name = "";

        name = GameObject.Find("GameplayItemUIItem_" + itemID).gameObject.GetComponent<GameplayItemUIItem>().itemName;

        return name;
    }

    //推广UI
    void PromotingUI()
    {
        if(gamePromotionPanelOpen == true)
        {
            if(gameMode.promotionMode == 0)
            {
                promotionBasicInfo.transform.localScale = new Vector3(1, 1, 1);
                promotingInfo.transform.localScale = new Vector3(0, 1, 1);
                //
                promoteBtnCostContent.transform.localScale = new Vector3(1, 1, 1);
                promoteBtnInfoText.transform.localScale = new Vector3(1, 0, 1);
                promoteBtn.GetComponent<Button>().enabled = true;
                promoteBtn.GetComponent<Image>().color = new Color(126 / 255f, 248 / 255f, 78 / 255f, 1f);

                //界面信息
                string moneyC = "";
                if(gameMode.promotionPrice == 0f)
                {
                    moneyC = "Free for First-time";
                }
                else
                {
                    moneyC = numberText(gameMode.promotionPrice);
                }
                promoteBtnMoneyCost.GetComponent<Text>().text = moneyC;
                if(gameMode.promotionPrice <= gameMode.companyMoney)
                {
                    promoteBtnMoneyCost.GetComponent<Text>().color = Color.black;
                }
                else
                {
                    promoteBtnMoneyCost.GetComponent<Text>().color = Color.red;
                }

                promotionBasicInfoCostPrice.GetComponent<Text>().text = gameMode.promotionUserCost.ToString("0.00");
                promotionBasicInfoIncomePrice.GetComponent<Text>().text = gameMode.lastCLV.ToString("0.00");
                promotionBasicInfoOutputUserText.GetComponent<Text>().text = "New Users " + gameMode.promotionUserNumRandomMinNum.ToString() + " ~ " + gameMode.promotionUserNumRandomMaxNum.ToString();
                promotionBasicInfoOutputRevenueText.GetComponent<Text>().text = "Revenue " + numberText(gameMode.promotionUserNumRandomMinNum * gameMode.lastCLV) + " ~ " + numberText(gameMode.promotionUserNumRandomMaxNum * gameMode.lastCLV);
            }
            else if (gameMode.promotionMode == 1)
            {
                promotionBasicInfo.transform.localScale = new Vector3(0, 1, 1);
                promotingInfo.transform.localScale = new Vector3(1, 1, 1);
                //
                promoteBtnCostContent.transform.localScale = new Vector3(1, 0, 1);
                promoteBtnInfoText.transform.localScale = new Vector3(1, 1, 1);
                promoteBtnInfoText.GetComponent<Text>().text = "Promoting ...";
                promoteBtn.GetComponent<Button>().enabled = false;
                promoteBtn.GetComponent<Image>().color = new Color(233 / 255f, 23 / 255f, 8 / 255f, 1f);

                //界面信息
                promotingInfoUserText.GetComponent<Text>().text = gameMode.promotingUserNum.ToString();
                promotingInfoRevenueText.GetComponent<Text>().text = numberText(gameMode.promotingRevenue);
            }
            else if (gameMode.promotionMode == 2)
            {
                promotionBasicInfo.transform.localScale = new Vector3(0, 1, 1);
                promotingInfo.transform.localScale = new Vector3(1, 1, 1);
                //
                promoteBtnCostContent.transform.localScale = new Vector3(1, 0, 1);
                promoteBtnInfoText.transform.localScale = new Vector3(1, 1, 1);
                promoteBtnInfoText.GetComponent<Text>().text = "New Promotion";
                promoteBtn.GetComponent<Button>().enabled = true;
                promoteBtn.GetComponent<Image>().color = new Color(90 / 255f, 140 / 255f, 255 / 255f, 1f);

                //界面信息
                promotingInfoUserText.GetComponent<Text>().text = gameMode.promotingUserNum.ToString();
                promotingInfoRevenueText.GetComponent<Text>().text = numberText(gameMode.promotingRevenue);
            }
        }

        //
        if(gameMode.promotionMode == 1)
        {
            gamePromotionBtnShow1.transform.localScale = new Vector3(1, 1, 1);
            gamePromotionBtnShow0.transform.localScale = new Vector3(1, 0, 1);

            float x = gamePromotionBtnShow1Item.transform.position.x;
            x -= 0.1f * 2f;
            gamePromotionBtnShow1Item.transform.position = new Vector3(x, gamePromotionBtnShow1Item.transform.position.y, 0f);
            if(gamePromotionBtnShow1Item.transform.position.x <= gamePromotionBtnShow1Point1.transform.position.x)
            {
                gamePromotionBtnShow1Item.transform.position = gamePromotionBtnShow1Point0.transform.position;
            }

        }
        else
        {
            gamePromotionBtnShow1.transform.localScale = new Vector3(1, 0, 1);
            gamePromotionBtnShow0.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
