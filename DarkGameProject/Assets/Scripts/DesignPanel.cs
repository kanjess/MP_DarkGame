using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Tilemaps;

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
        moneyShow = topContent.transform.Find("MoneyShow").gameObject;
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
        moneyShow.GetComponent<Text>().text = "Income: " + gameMode.companyMoney.ToString();

        //等级
        playerLvText.GetComponent<Text>().text = gameMode.playerLevel.ToString();
        playerExpText.GetComponent<Text>().text = gameMode.playerExp.ToString() + " / " + gameMode.levelUpExp.ToString();
        playerExpBar.GetComponent<Image>().fillAmount = ((float)gameMode.playerExp / (float)gameMode.levelUpExp);
        //升级
        if((float)gameMode.playerExp / (float)gameMode.levelUpExp >= 1f)
        {
            gameMode.playerLevel++;
            gameMode.playerExp = 0;
            gameMode.levelUpExp = (int)(gameMode.basicLevelUpExp * (1 + (gameMode.playerLevel * gameMode.levelUpExpIncreaseValue / 10f)));
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
        if(scenePanelAnime == false && sceneNo != no)
        {
            if(no == 1 || no == 3)
            {
                scenePanelAnime = true;

                sceneContentBg.transform.localScale = new Vector3(1, 1, 1);
                sceneContentBg.GetComponent<Image>().DOFade(0.8f, 0.3f);

                if(no == 1)
                {
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
            }
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

    }
}
