using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    private void Awake()
    {
        mainSceneUI = this.gameObject.transform.Find("MainSceneUI").gameObject;
        designItemBtn = mainSceneUI.transform.Find("DesignItemBtn").gameObject;
        designPanel = mainSceneUI.transform.Find("DesignPanel").gameObject;
        designPanelBg = designPanel.transform.Find("Bg").gameObject;
        designPanelContent = designPanel.transform.Find("DesignPanelContent").gameObject;
        designPanelClose = designPanel.transform.Find("DesignPanelClose").gameObject;
        designPanelPosCheckPoint = designPanel.transform.Find("PosCheckPoint").gameObject;
        itemRotationBtn = designPanel.transform.Find("ItemRotationBtn").gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        GameItemIn();

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
    }

    // Update is called once per frame
    void Update()
    {
        if(BasicAction.gameplayItemSetMode == true)
        {
            if(Input.mousePosition.x > designPanelPosCheckPoint.transform.position.x)
            {
                CameraControl.cameraCanMove = false;
            }
            else
            {
                CameraControl.cameraCanMove = true;
            }
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
                Tweener anime = designItemBtn.transform.DOMoveX(designItemBtn.transform.position.x + 450f, tt);
                designPanel.transform.DOMoveX(designPanel.transform.position.x - 450f, tt);
                designItemBtnState = true;
                BasicAction.gameplayItemSetMode = true;
                anime.OnComplete(() => designItemBtnAnime = false);
            }
            else
            {
                Tweener anime = designItemBtn.transform.DOMoveX(designItemBtn.transform.position.x - 450f, tt);
                designPanel.transform.DOMoveX(designPanel.transform.position.x + 450f, tt);
                designItemBtnState = false;
                BasicAction.gameplayItemSetMode = false;
                anime.OnComplete(() => designItemBtnAnime = false);
            }
        }
    }

    void GameItemIn()
    {
        GameObject oj = Instantiate(gameplayItemUIItem) as GameObject;
        oj.transform.SetParent(designPanelContent.transform);
        oj.transform.localPosition = new Vector3(0, 0, 0);
        //测试ID=101
        oj.GetComponent<GameplayItemUIItem>().SetItemID(101);
        //
        GameObject oj2 = Instantiate(gameplayItemUIItem) as GameObject;
        oj2.transform.SetParent(designPanelContent.transform);
        oj2.transform.localPosition = new Vector3(0, 150f, 0);
        //测试ID=102
        oj2.GetComponent<GameplayItemUIItem>().SetItemID(102);
    }
}
