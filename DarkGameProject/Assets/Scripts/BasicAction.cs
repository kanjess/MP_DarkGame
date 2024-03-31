using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BasicAction : MonoBehaviour
{
    static public bool gameplayItemSetMode = false;
    static public bool gameplayItemAction = false;
    static public bool gameplayItemRotationMode = false;
    static public bool roadEditMode = false;

    public GameObject mousHitObject;

    private GameObject gameplayItem;

    //InsGameplayItemSet(); 修改配对
    public GameObject gpItem_101;
    public GameObject gpItem_102;
    public GameObject gpItem_103;
    public GameObject gpItem_104;
    public GameObject gpItem_999;
    public GameObject gpItem_401;
    public GameObject gpItem_402;
    public GameObject gpItem_403;
    public GameObject gpItem_411;
    public GameObject gpItem_412;
    public GameObject gpItem_421;
    public GameObject gpItem_422;
    public GameObject gpItem_431;
    public GameObject gpItem_432;
    public GameObject gpItem_433;
    public GameObject gpItem_434;
    public GameObject gpItem_435;
    public GameObject gpItem_436;

    private GameplayMapping gameplayMapping;
    private GameMode gameMode;

    public GameObject gameplayItemTem;
    private GameObject gameplayObjectLayer;
    private GameObject temGameplayObjectLayer;

    //菜单拖动
    private DesignPanel designPenel;
    private GameObject designPenalCheckPoint;
    private bool panelIsDragging = false;
    public GameObject targetPanelItem;
    public GameObject gameplayItemUIItemTem;
    public bool dragMapValid = true;
    //private List<Vector3Int> preItemOcuPosList;

    //鼠标拖动
    private Vector3 mosStartPosition;
    private bool mosIsDragging = false;
    private GameObject targetOJ;
    private GameObject temTargetOJ;
    private GameObject ojt;
    private GameObject uijt;
    public GameObject otherTargetOJ;

    private int dragPosState = 0;

    private Tilemap tilemap; // Tilemap的引用
    private Vector3Int previousCellPosition = Vector3Int.zero; // 上一帧的瓦片坐标

    //寻路
    public GameObject mainPathItem;
    private GameObject temRoadObjectLayer;
    private bool validPath;
    //
    public bool roadEditDefaultDirection = true;
    public Vector3Int roadEditStart0Pos;
    public Vector3Int roadEditStartPos;
    public Vector3Int roadEditEndPos;
    public Vector3Int roadEditEnd1Pos;
    public GameObject roadEditPointItem;
    private bool otherTarget = false;
    public bool specialItemLink = false;

    private GameObject roadLayer;

    private GameObject startItemObject;

    //dark patterns
    public bool darkHasLink = false;
    private GameObject darkLinkPoint;
    private Vector3Int darkLinkPos;

    private void Awake()
    {
        //preItemOcuPosList = new List<Vector3Int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameplayObjectLayer = GameObject.Find("GameplayObject").gameObject;
        temGameplayObjectLayer = GameObject.Find("TemGameplayObject").gameObject;

        tilemap = GameObject.Find("Tilemap").gameObject.GetComponent<Tilemap>();

        designPenel = GameObject.Find("Canvas").gameObject.GetComponent<DesignPanel>();
        designPenalCheckPoint = designPenel.designPanelPosCheckPoint;

        gameplayMapping = this.gameObject.GetComponent<GameplayMapping>();
        gameMode = this.gameObject.GetComponent<GameMode>();

        temRoadObjectLayer = GameObject.Find("TemRoadObject").gameObject;
        roadLayer = GameObject.Find("RoadObject").gameObject;

        //开局
        Item999Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameplayItemSetMode == true)
        {
            MouseDragCheck();
        }

    }

    //各种动作
    public void MouseDragCheck()
    {
        //鼠标移动监听
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D mHit = Physics2D.Raycast(mPosition, Vector2.zero);
        if(mHit.collider != null)
        {
            mousHitObject = mHit.collider.gameObject;
        }
        else
        {
            mousHitObject = null;
        }

        //旋转
        if(mousHitObject != null && mousHitObject.gameObject.name == "RotationBtn" && gameplayItemRotationMode == true)
        {
            if (Input.GetMouseButton(0))
            {
                gameplayMapping.AllMapRoadListNew(); //地图位置列表获取
                mousHitObject.gameObject.GetComponentInParent<GameplayItem>().DirectionSet();
                gameplayItemAction = true;
                gameMode.gameProcessPause = true;
            }
        }

        // 检测鼠标左键按下（一般拖拽）&& (拖拽路线）
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                if(hit.collider.gameObject.name == "MoveBtn" && gameplayItemRotationMode == false)
                {
                    //preItemOcuPosList = new List<Vector3Int>();
                    targetOJ = hit.collider.gameObject.transform.parent.gameObject;
                    //
                    if (targetOJ.GetComponent<GameplayItem>().isValidItem == true)
                    {
                        mosIsDragging = true;
                        mosStartPosition = hit.collider.gameObject.transform.position;
                        // 因为是2D，所以忽略Z轴
                        mosStartPosition.z = 0;
                        //
                        ojt = Instantiate(gameplayItemTem) as GameObject;
                        ojt.transform.SetParent(gameplayObjectLayer.transform);
                        ojt.transform.position = new Vector3(0, 0, 0);
                        //如果可回收，则创建UI实体
                        if(targetOJ.GetComponent<GameplayItem>().canPutBack == true)
                        {
                            if (designPenel.designItemBtnState == true)
                            {
                                uijt = Instantiate(gameplayItemUIItemTem) as GameObject;
                                uijt.transform.SetParent(designPenel.designPanel.transform);
                                uijt.transform.position = Input.mousePosition;
                            }
                        }
                        //创建临时实体（创建本体）
                        int temID = hit.collider.gameObject.transform.parent.gameObject.GetComponent<GameplayItem>().itemID;
                        
                        gameplayItem = targetOJ;

                        //拖动 - 复制原个体
                        temTargetOJ = Instantiate(gameplayItem) as GameObject;
                        temTargetOJ.transform.SetParent(temGameplayObjectLayer.transform);
                        temTargetOJ.transform.position = ojt.transform.position;
                        temTargetOJ.GetComponent<GameplayItem>().SetItemID(temID);
                        temTargetOJ.GetComponent<GameplayItem>().temPreObject = targetOJ;
                        temTargetOJ.GetComponent<GameplayItem>().SetValid(false);
                        //复制后携带的dark也要valid=false掉
                        if(temTargetOJ.GetComponent<GameplayItem>().canDark == true)
                        {
                            for(int i = 0; i < temTargetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.childCount; i++)
                            {
                                GameObject soc = temTargetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject;
                                if(soc.transform.childCount > 1)
                                {
                                    for(int aa = 0; aa < soc.transform.childCount; aa++)
                                    {
                                        GameObject gii = soc.transform.GetChild(aa).gameObject;
                                        if (gii.name.Contains("GameplayItem_"))
                                        {
                                            gii.GetComponent<GameplayItem>().SetValid(false);
                                        }
                                    }
                                }
                            }
                        }
                        temTargetOJ.transform.rotation = targetOJ.transform.rotation;
                        temTargetOJ.GetComponent<GameplayItem>().gameplayPic.transform.localRotation = targetOJ.GetComponent<GameplayItem>().gameplayPic.transform.localRotation;
                        temTargetOJ.GetComponent<GameplayItem>().moveBtn.GetComponent<BoxCollider2D>().enabled = false;

                        gameplayItemAction = true;
                        gameMode.gameProcessPause = true;
                    }
                }
                //拖拽路线
                else if ((hit.collider.gameObject.name == "OutputBtn" || hit.collider.gameObject.name == "InputBtn" || hit.collider.gameObject.name == "SpecialInputBtn" || hit.collider.gameObject.name == "SpecialOutputBtn") && gameplayItemRotationMode == false)
                {
                    //寻路标记开启
                    roadEditMode = true;
                    targetOJ = hit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                    //
                    ojt = Instantiate(roadEditPointItem) as GameObject;
                    ojt.transform.SetParent(gameplayObjectLayer.transform);
                    ojt.transform.position = new Vector3(0, 0, 0);
                    //起始点&方向创建
                    if (hit.collider.gameObject.name == "OutputBtn" || hit.collider.gameObject.name == "SpecialOutputBtn")
                    {
                        roadEditDefaultDirection = true;
                        if(hit.collider.gameObject.name == "OutputBtn")
                        {
                            specialItemLink = false;
                            GameObject start0P = targetOJ.GetComponent<GameplayItem>().outputPoint.gameObject;
                            GameObject startP = targetOJ.GetComponent<GameplayItem>().outputStartPoint.gameObject;
                            roadEditStart0Pos = new Vector3Int(Mathf.RoundToInt(start0P.transform.position.x), Mathf.RoundToInt(start0P.transform.position.y), Mathf.RoundToInt(start0P.transform.position.z));
                            roadEditStartPos = new Vector3Int(Mathf.RoundToInt(startP.transform.position.x), Mathf.RoundToInt(startP.transform.position.y), Mathf.RoundToInt(startP.transform.position.z));
                        }
                        else if (hit.collider.gameObject.name == "SpecialOutputBtn")
                        {
                            specialItemLink = true;
                            GameObject start0P = targetOJ.GetComponent<GameplayItem>().specialOutputPoint.gameObject;
                            GameObject startP = targetOJ.GetComponent<GameplayItem>().specialOutputStartPoint.gameObject;
                            roadEditStart0Pos = new Vector3Int(Mathf.RoundToInt(start0P.transform.position.x), Mathf.RoundToInt(start0P.transform.position.y), Mathf.RoundToInt(start0P.transform.position.z));
                            roadEditStartPos = new Vector3Int(Mathf.RoundToInt(startP.transform.position.x), Mathf.RoundToInt(startP.transform.position.y), Mathf.RoundToInt(startP.transform.position.z));
                        }
                        //
                        ojt.transform.Find("SuccessItem").gameObject.transform.Find("InputItem").gameObject.transform.localScale = new Vector3(0, 0, 0);
                    }
                    else if (hit.collider.gameObject.name == "InputBtn" || hit.collider.gameObject.name == "SpecialInputBtn")
                    {
                        roadEditDefaultDirection = false;
                        if(hit.collider.gameObject.name == "InputBtn")
                        {
                            specialItemLink = false;
                            GameObject start0P = targetOJ.GetComponent<GameplayItem>().inputPoint.gameObject;
                            GameObject startP = targetOJ.GetComponent<GameplayItem>().inputStartPoint.gameObject;
                            roadEditStart0Pos = new Vector3Int(Mathf.RoundToInt(start0P.transform.position.x), Mathf.RoundToInt(start0P.transform.position.y), Mathf.RoundToInt(start0P.transform.position.z));
                            roadEditStartPos = new Vector3Int(Mathf.RoundToInt(startP.transform.position.x), Mathf.RoundToInt(startP.transform.position.y), Mathf.RoundToInt(startP.transform.position.z));
                        }
                        else if (hit.collider.gameObject.name == "SpecialInputBtn")
                        {
                            specialItemLink = true;
                            GameObject start0P = targetOJ.GetComponent<GameplayItem>().specialInputPoint.gameObject;
                            GameObject startP = targetOJ.GetComponent<GameplayItem>().specialInputStartPoint.gameObject;
                            roadEditStart0Pos = new Vector3Int(Mathf.RoundToInt(start0P.transform.position.x), Mathf.RoundToInt(start0P.transform.position.y), Mathf.RoundToInt(start0P.transform.position.z));
                            roadEditStartPos = new Vector3Int(Mathf.RoundToInt(startP.transform.position.x), Mathf.RoundToInt(startP.transform.position.y), Mathf.RoundToInt(startP.transform.position.z));
                        }
                        //
                        ojt.transform.Find("SuccessItem").gameObject.transform.Find("OutputItem").gameObject.transform.localScale = new Vector3(0, 0, 0);
                    }

                    gameplayItemAction = true;
                    gameMode.gameProcessPause = true;

                    gameplayMapping.AllMapRoadListNew(); //地图位置列表获取
                }
            }           
        }

        //菜单拖拽（启动）
        if(designPenel.designItemBtnState == true && designPenel.designItemDragState == true && ojt == null)
        {
            panelIsDragging = true;
            ojt = Instantiate(gameplayItemTem) as GameObject;
            ojt.transform.SetParent(gameplayObjectLayer.transform);
            ojt.transform.position = new Vector3(0, 0, 0);
            ojt.transform.localScale = new Vector3(1, 1, 1);

            gameplayItemAction = true;
            gameMode.gameProcessPause = true;

            gameplayMapping.AllMapRoadListNew(); //地图位置列表获取
        }

        // 拖动物体 (一般拖动 & 菜单拖拽）
        if (ojt != null && roadEditMode == false)
        {
            gameplayItemAction = true;
            gameMode.gameProcessPause = true;

            if (mosIsDragging == true || panelIsDragging == true)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // 因为是2D，所以忽略Z轴
                mousePosition.z = 0;
                mousePosition.x += 1f;
                mousePosition.y += 1f;
                Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
                //Debug.Log(cellPosition + "  " + mousePosition);
                // 如果格子位置发生变化
                if (cellPosition != previousCellPosition)
                {
                    previousCellPosition = cellPosition;
                    //Debug.Log("当前格子坐标: " + cellPosition);
                }
                ojt.transform.position = cellPosition;
                temTargetOJ.transform.position = ojt.transform.position;
                //
                if (uijt != null)
                {
                    uijt.transform.position = Input.mousePosition;
                }

                //鼠标位置&界面位置监听
                if (Input.mousePosition.x >= designPenalCheckPoint.transform.position.x && designPenel.designItemBtnState == true)
                {
                    ojt.transform.localScale = new Vector3(0, 0, 0);
                    temTargetOJ.transform.localScale = new Vector3(0, 0, 0);
                    dragPosState = 1;
                    if (uijt != null)
                    {
                        uijt.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else
                {
                    ojt.transform.localScale = new Vector3(1, 1, 1);
                    temTargetOJ.transform.localScale = new Vector3(1, 1, 1);
                    dragPosState = 0;
                    if (uijt != null)
                    {
                        uijt.transform.localScale = new Vector3(0, 0, 0);
                    }
                }

                //如果是dark，查看鼠标是否点击到目标位置上
                if (temTargetOJ.GetComponent<GameplayItem>().isMain == false)
                {
                    darkHasLink = false;

                    if (dragPosState == 0)
                    {
                        Vector3 mmPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        RaycastHit2D mmhit = Physics2D.Raycast(mmPosition, Vector2.zero);
                        dragMapValid = true;

                        //是否dark可连接的检测
                        if (mmhit.collider != null)
                        {
                            if (mmhit.collider.gameObject.name == "MoveBtn")
                            {
                                if (mmhit.collider.gameObject.transform.parent.gameObject.GetComponent<GameplayItem>().canDark == true)
                                {
                                    if (mmhit.collider.gameObject.transform.parent.gameObject.GetComponent<GameplayItem>().canDarkList.Contains(temTargetOJ.GetComponent<GameplayItem>().itemID))
                                    {
                                        darkHasLink = true;
                                        //
                                        otherTargetOJ = mmhit.collider.gameObject.transform.parent.gameObject;
                                    }
                                    else
                                    {
                                        darkHasLink = false;
                                    }
                                }
                                else
                                {
                                    darkHasLink = false;
                                }
                            }
                            else if (mmhit.collider.gameObject.name == "DarkSocketPoint")
                            {
                                if (mmhit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<GameplayItem>().canDark == true)
                                {
                                    if (mmhit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<GameplayItem>().canDarkList.Contains(temTargetOJ.GetComponent<GameplayItem>().itemID))
                                    {
                                        darkHasLink = true;
                                        //
                                        otherTargetOJ = mmhit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                                    }
                                    else
                                    {
                                        darkHasLink = false;
                                    }
                                }
                                else
                                {
                                    darkHasLink = false;
                                }
                            }

                            //如果dark链接上了，放置在对应位置
                            if(darkHasLink == true)
                            {
                                darkLinkPoint = mmhit.collider.gameObject;
                                //如果链接的是连接点
                                if(mmhit.collider.gameObject.name == "DarkSocketPoint")
                                {
                                    darkLinkPoint = mmhit.collider.gameObject;
                                    //查看连接点下面有没有东西，没有则直接放，有则无效
                                    int cc = darkLinkPoint.transform.childCount;
                                    if(cc > 1)
                                    {
                                        darkHasLink = false;
                                    }
                                }
                                //如果链接的是本体
                                else if (mmhit.collider.gameObject.name == "MoveBtn")
                                {
                                    //筛选连接点
                                    GameObject darkSocketC = mmhit.collider.gameObject.transform.parent.gameObject.GetComponent<GameplayItem>().darkSocketContent;
                                    int c0 = darkSocketC.transform.childCount;

                                    darkHasLink = false;
                                    for (int i = 0; i < c0; i++)
                                    {
                                        darkLinkPoint = darkSocketC.transform.GetChild(i).gameObject;

                                        int cc = darkLinkPoint.transform.childCount;
                                        if (cc <= 1)
                                        {
                                            darkHasLink = true;
                                            break;
                                        }
                                    }
                                }

                                if(darkHasLink == true)
                                {
                                    GameObject tpoint = darkLinkPoint.transform.Find("Point").gameObject;
                                    //确认朝向并放置临时道具
                                    Vector3Int p0 = new Vector3Int(Mathf.RoundToInt(darkLinkPoint.transform.position.x), Mathf.RoundToInt(darkLinkPoint.transform.position.y), Mathf.RoundToInt(darkLinkPoint.transform.position.z));
                                    Vector3Int p1 = new Vector3Int(Mathf.RoundToInt(tpoint.transform.position.x), Mathf.RoundToInt(tpoint.transform.position.y), Mathf.RoundToInt(tpoint.transform.position.z));

                                    if (p1.x > p0.x && p1.y == p0.y)
                                    {
                                        //右
                                        temTargetOJ.transform.eulerAngles = new Vector3(0, 0, 0);
                                        temTargetOJ.GetComponent<GameplayItem>().stablePic.transform.eulerAngles = new Vector3(0, 0, 0);
                                    }
                                    else if (p1.x < p0.x && p1.y == p0.y)
                                    {
                                        //左
                                        temTargetOJ.transform.eulerAngles = new Vector3(0, 0, -180);
                                        temTargetOJ.GetComponent<GameplayItem>().stablePic.transform.eulerAngles = new Vector3(0, 0, 0);
                                    }
                                    else if (p1.x == p0.x && p1.y > p0.y)
                                    {
                                        //上
                                        temTargetOJ.transform.eulerAngles = new Vector3(0, 0, 90);
                                        temTargetOJ.GetComponent<GameplayItem>().stablePic.transform.eulerAngles = new Vector3(0, 0, 0);
                                    }
                                    else if (p1.x == p0.x && p1.y < p0.y)
                                    {
                                        //下
                                        temTargetOJ.transform.eulerAngles = new Vector3(0, 0, -90);
                                        temTargetOJ.GetComponent<GameplayItem>().stablePic.transform.eulerAngles = new Vector3(0, 0, 0);
                                    }

                                    temTargetOJ.transform.position = darkLinkPoint.transform.position;
                                    GameObject adPoint = temTargetOJ.GetComponent<GameplayItem>().darkPlugPoint;
                                    Vector3Int adp2 = new Vector3Int(Mathf.RoundToInt(adPoint.transform.position.x), Mathf.RoundToInt(adPoint.transform.position.y), Mathf.RoundToInt(adPoint.transform.position.z));
                                    if (adp2.x < p0.x && adp2.y == p0.y)
                                    {
                                        //右移
                                        darkLinkPos = new Vector3Int(Mathf.RoundToInt(temTargetOJ.transform.position.x + 1), Mathf.RoundToInt(temTargetOJ.transform.position.y), Mathf.RoundToInt(temTargetOJ.transform.position.z));
                                        temTargetOJ.transform.position = darkLinkPos;
                                    }
                                    else if (adp2.x > p0.x && adp2.y == p0.y)
                                    {
                                        //左移
                                        darkLinkPos = new Vector3Int(Mathf.RoundToInt(temTargetOJ.transform.position.x - 1), Mathf.RoundToInt(temTargetOJ.transform.position.y), Mathf.RoundToInt(temTargetOJ.transform.position.z));
                                        temTargetOJ.transform.position = darkLinkPos;
                                    }
                                    else if (adp2.x == p0.x && adp2.y < p0.y)
                                    {
                                        //上移
                                        darkLinkPos = new Vector3Int(Mathf.RoundToInt(temTargetOJ.transform.position.x), Mathf.RoundToInt(temTargetOJ.transform.position.y + 1), Mathf.RoundToInt(temTargetOJ.transform.position.z));
                                        temTargetOJ.transform.position = darkLinkPos;
                                    }
                                    else if (adp2.x == p0.x && adp2.y > p0.y)
                                    {
                                        //下移
                                        darkLinkPos = new Vector3Int(Mathf.RoundToInt(temTargetOJ.transform.position.x), Mathf.RoundToInt(temTargetOJ.transform.position.y - 1), Mathf.RoundToInt(temTargetOJ.transform.position.z));
                                        temTargetOJ.transform.position = darkLinkPos;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ojt.transform.position = cellPosition;
                            temTargetOJ.transform.position = ojt.transform.position;
                            //
                            temTargetOJ.transform.eulerAngles = new Vector3(0, 0, 0);
                            temTargetOJ.GetComponent<GameplayItem>().stablePic.transform.localEulerAngles = new Vector3(0, 0, 0);
                        }
                    }
                }

                //放置格是否合法判定
                //普通检测占位格；dark检测碰撞体
                if (temTargetOJ.GetComponent<GameplayItem>().isMain == true)
                {
                    dragMapValid = temTargetOJ.GetComponent<GameplayItem>().temItemPosValid;
                }
                else if (temTargetOJ.GetComponent<GameplayItem>().isMain == false)
                {
                    dragMapValid = temTargetOJ.GetComponent<GameplayItem>().temItemPosValid;
                }
            } 
        }

        //拖拽路线
        else if (ojt != null && roadEditMode == true)
        {
            gameplayItemAction = true;
            gameMode.gameProcessPause = true;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 因为是2D，所以忽略Z轴
            mousePosition.z = 0;
            mousePosition.x += 1f;
            mousePosition.y += 1f;
            Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
            //
            //鼠标位置&界面位置监听
            if (Input.mousePosition.x >= designPenalCheckPoint.transform.position.x && designPenel.designItemBtnState == true)
            {
                ojt.transform.localScale = new Vector3(0, 0, 0);
                dragPosState = 1;
                //
                roadEditEndPos = roadEditStartPos;
                roadEditEnd1Pos = roadEditStartPos;
            }
            else
            {
                ojt.transform.localScale = new Vector3(1, 1, 1);
                dragPosState = 0;
                //
                roadEditEndPos = cellPosition;
                roadEditEnd1Pos = cellPosition;
            }

            //放置格位置判定
            if(dragPosState == 0)
            {
                Vector3 mmPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D mmhit = Physics2D.Raycast(mmPosition, Vector2.zero);
                dragMapValid = true;
                if (mmhit.collider != null)
                {
                    otherTargetOJ = targetOJ;
                    if (mmhit.collider.gameObject.name == "MoveBtn")
                    {
                        if (targetOJ == mmhit.collider.gameObject.transform.parent.gameObject)
                        {
                            otherTarget = false;
                        }
                        else
                        {
                            otherTarget = true;
                            otherTargetOJ = mmhit.collider.gameObject.transform.parent.gameObject;
                        }
                    }
                    else if (mmhit.collider.gameObject.name == "InputBtn" || mmhit.collider.gameObject.name == "OutputBtn" || mmhit.collider.gameObject.name == "SpecialInputBtn" || mmhit.collider.gameObject.name == "SpecialOutputBtn")
                    {
                        if (targetOJ == mmhit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject)
                        {
                            otherTarget = false;
                        }
                        else
                        {
                            otherTarget = true;
                            otherTargetOJ = mmhit.collider.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                        }
                    }

                    //起点-终点
                    if (otherTarget == true)
                    {
                        if (roadEditDefaultDirection == true)
                        {
                            if(specialItemLink == false)
                            {
                                if (otherTargetOJ.GetComponent<GameplayItem>().inputStartPoint != null)
                                {
                                    Vector3 roadPP = otherTargetOJ.GetComponent<GameplayItem>().inputStartPoint.transform.position;
                                    ojt.transform.position = otherTargetOJ.GetComponent<GameplayItem>().inputBtn.transform.position;
                                    roadEditEndPos = new Vector3Int(Mathf.RoundToInt(roadPP.x), Mathf.RoundToInt(roadPP.y), Mathf.RoundToInt(roadPP.z));
                                    //
                                    Vector3 road1PP = otherTargetOJ.GetComponent<GameplayItem>().inputPoint.transform.position;
                                    roadEditEnd1Pos = new Vector3Int(Mathf.RoundToInt(road1PP.x), Mathf.RoundToInt(road1PP.y), Mathf.RoundToInt(road1PP.z));
                                }
                                else
                                {
                                    ojt.transform.position = cellPosition;
                                    //
                                    roadEditEndPos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                                    roadEditEnd1Pos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                                }  
                            }
                            else if (specialItemLink == true)
                            {
                                if (otherTargetOJ.GetComponent<GameplayItem>().specialInputStartPoint != null)
                                {
                                    Vector3 roadPP = otherTargetOJ.GetComponent<GameplayItem>().specialInputStartPoint.transform.position;
                                    ojt.transform.position = otherTargetOJ.GetComponent<GameplayItem>().specialInputBtn.transform.position;
                                    //
                                    roadEditEndPos = new Vector3Int(Mathf.RoundToInt(roadPP.x), Mathf.RoundToInt(roadPP.y), Mathf.RoundToInt(roadPP.z));
                                    //
                                    Vector3 road1PP = otherTargetOJ.GetComponent<GameplayItem>().specialInputPoint.transform.position;
                                    roadEditEnd1Pos = new Vector3Int(Mathf.RoundToInt(road1PP.x), Mathf.RoundToInt(road1PP.y), Mathf.RoundToInt(road1PP.z));
                                }
                                else
                                {
                                    ojt.transform.position = cellPosition;
                                    //
                                    roadEditEndPos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                                    roadEditEnd1Pos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                                }
                            }
                        }
                        else if (roadEditDefaultDirection == false)
                        {
                            if (specialItemLink == false)
                            {
                                if(otherTargetOJ.GetComponent<GameplayItem>().outputStartPoint != null)
                                {
                                    Vector3 roadPP = otherTargetOJ.GetComponent<GameplayItem>().outputStartPoint.transform.position;
                                    ojt.transform.position = otherTargetOJ.GetComponent<GameplayItem>().outputBtn.transform.position;
                                    //
                                    roadEditEndPos = new Vector3Int(Mathf.RoundToInt(roadPP.x), Mathf.RoundToInt(roadPP.y), Mathf.RoundToInt(roadPP.z));
                                    //
                                    Vector3 road1PP = otherTargetOJ.GetComponent<GameplayItem>().outputPoint.transform.position;
                                    roadEditEnd1Pos = new Vector3Int(Mathf.RoundToInt(road1PP.x), Mathf.RoundToInt(road1PP.y), Mathf.RoundToInt(road1PP.z));
                                }
                                else
                                {
                                    ojt.transform.position = cellPosition;
                                    //
                                    roadEditEndPos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                                    roadEditEnd1Pos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                                }
                            }
                            else if(specialItemLink == true)
                            {
                                if (otherTargetOJ.GetComponent<GameplayItem>().specialOutputStartPoint != null)
                                {
                                    Vector3 roadPP = otherTargetOJ.GetComponent<GameplayItem>().specialOutputStartPoint.transform.position;
                                    ojt.transform.position = otherTargetOJ.GetComponent<GameplayItem>().specialOutputBtn.transform.position;
                                    //
                                    roadEditEndPos = new Vector3Int(Mathf.RoundToInt(roadPP.x), Mathf.RoundToInt(roadPP.y), Mathf.RoundToInt(roadPP.z));
                                    //
                                    Vector3 road1PP = otherTargetOJ.GetComponent<GameplayItem>().specialOutputPoint.transform.position;
                                    roadEditEnd1Pos = new Vector3Int(Mathf.RoundToInt(road1PP.x), Mathf.RoundToInt(road1PP.y), Mathf.RoundToInt(road1PP.z));
                                }
                                else
                                {
                                    ojt.transform.position = cellPosition;
                                    //
                                    roadEditEndPos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                                    roadEditEnd1Pos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                                }
                            } 
                        }
                    }
                    else
                    {
                        ojt.transform.position = cellPosition;
                        //
                        roadEditEndPos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                        roadEditEnd1Pos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                    }
                }
                else
                {
                    otherTarget = false;
                    ojt.transform.position = cellPosition;
                    //
                    roadEditEndPos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                    roadEditEnd1Pos = new Vector3Int(Mathf.RoundToInt(ojt.transform.position.x), Mathf.RoundToInt(ojt.transform.position.y), Mathf.RoundToInt(ojt.transform.position.z));
                }
                
            }

            //寻路算法
            RoadSetLogic();

            //路线标识符
            if(otherTarget == true && (validPath || roadEditStartPos == roadEditEndPos))
            {
                ojt.transform.Find("SuccessItem").gameObject.transform.localScale = new Vector3(1, 1, 1);
                ojt.transform.Find("WrongItem").gameObject.transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                ojt.transform.Find("SuccessItem").gameObject.transform.localScale = new Vector3(0, 0, 0);
                ojt.transform.Find("WrongItem").gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            
        }

        // 释放鼠标左键（一般拖拽 && 放置）
        if (Input.GetMouseButtonUp(0) && mosIsDragging && ojt != null)
        {
            if (ojt != null)
            {
                Destroy(ojt);
            }
            if (uijt != null)
            {
                Destroy(uijt);
            }
            //mousePosition.z = 0;
            //放置
            if (dragPosState == 0)
            {
                //重新放置（普通）
                if(dragMapValid == true && targetOJ.GetComponent<GameplayItem>().isMain == true)
                {
                    //非法坐标调整
                    //前占位坐标移除
                    targetOJ.GetComponent<GameplayItem>().IllegalMapRemove();
                    //前占位坐标移除（自带dark)
                    if(targetOJ.GetComponent<GameplayItem>().canDark == true)
                    {
                        for(int i = 0; i < targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.childCount; i++)
                        {
                            int childc = targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject.transform.childCount;
                            if(childc > 1)
                            {
                                //含有已附着的dark patterns
                                for(int aa = 0; aa < targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject.transform.childCount; aa++)
                                {
                                    GameObject gitem = targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject.transform.GetChild(aa).gameObject;
                                    if (gitem.name.Contains("GameplayItem_"))
                                    {
                                        gitem.GetComponent<GameplayItem>().IllegalMapRemove();
                                    }
                                }
                            }
                        }
                    }

                    //新坐标加入
                    targetOJ.transform.position = previousCellPosition;
                    targetOJ.GetComponent<GameplayItem>().IllegalMapAdd();
                    //新坐标加入（自带dark)
                    if (targetOJ.GetComponent<GameplayItem>().canDark == true)
                    {
                        for (int i = 0; i < targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.childCount; i++)
                        {
                            int childc = targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject.transform.childCount;
                            if (childc > 1)
                            {
                                //含有已附着的dark patterns
                                for (int aa = 0; aa < targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject.transform.childCount; aa++)
                                {
                                    GameObject gitem = targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject.transform.GetChild(aa).gameObject;
                                    if (gitem.name.Contains("GameplayItem_"))
                                    {
                                        gitem.GetComponent<GameplayItem>().IllegalMapAdd();
                                    }
                                }
                            }
                        }
                    }

                    //刷新占位坐标
                    targetOJ.GetComponent<GameplayItem>().itemOccupiedAreaList.Clear();
                    targetOJ.GetComponent<GameplayItem>().OccupiedAreaListUpdate();
                    //刷新占位坐标（自带dark)
                    if (targetOJ.GetComponent<GameplayItem>().canDark == true)
                    {
                        for (int i = 0; i < targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.childCount; i++)
                        {
                            int childc = targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject.transform.childCount;
                            if (childc > 1)
                            {
                                //含有已附着的dark patterns
                                for (int aa = 0; aa < targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject.transform.childCount; aa++)
                                {
                                    GameObject gitem = targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject.transform.GetChild(aa).gameObject;
                                    if (gitem.name.Contains("GameplayItem_"))
                                    {
                                        gitem.GetComponent<GameplayItem>().itemOccupiedAreaList.Clear();
                                        gitem.GetComponent<GameplayItem>().OccupiedAreaListUpdate();
                                    }
                                }
                            }
                        }
                    }

                    //重建路网
                    AutoRoadRecreateForItem(targetOJ);

                }
                //dark patterns 重新放置
                else if (dragMapValid == true && targetOJ.GetComponent<GameplayItem>().isMain == false && darkHasLink == true)
                {
                    //是否变位置？
                    if (darkLinkPoint == targetOJ.GetComponent<GameplayItem>().linkDarkSocketPoint)
                    {
                        //位置没变，则无效
                        //
                    }
                    //销毁后重建
                    else
                    {
                        //前占位坐标移除
                        targetOJ.GetComponent<GameplayItem>().IllegalMapRemove();
                        int newid = targetOJ.GetComponent<GameplayItem>().itemID;
                        //
                        Destroy(targetOJ);

                        //重建
                        InsNewGameplayItem(newid, true, true);
                    }
                }
            }
            //回收
            else if(dragPosState == 1)
            {
                //判定是否回收
                if(targetOJ.GetComponent<GameplayItem>().canPutBack == true)
                {
                    //回收
                    if (targetOJ != null)
                    {
                        //玩法列表清除
                        if (gameplayMapping.mainGameplayItemList.Contains(targetOJ))
                        {
                            gameplayMapping.mainGameplayItemList.Remove(targetOJ);
                        }else if (gameplayMapping.darkGameplayItemList.Contains(targetOJ))
                        {
                            gameplayMapping.darkGameplayItemList.Remove(targetOJ);
                        }

                        //前占位坐标移除
                        targetOJ.GetComponent<GameplayItem>().IllegalMapRemove();
                        //连带dark一起移除
                        if(targetOJ.GetComponent<GameplayItem>().canDark == true)
                        {
                            for (int i = 0; i < targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.childCount; i++)
                            {
                                GameObject sock = targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject;
                                if (sock.transform.childCount > 1)
                                {
                                    for (int aa = 0; aa < sock.transform.childCount; aa++)
                                    {
                                        GameObject gi = sock.transform.GetChild(aa).gameObject;
                                        if (gi.name.Contains("GameplayItem_"))
                                        {
                                            gi.GetComponent<GameplayItem>().IllegalMapRemove();
                                        }
                                    }
                                }
                            }
                        }

                        //道路摧毁
                        if(targetOJ.GetComponent<GameplayItem>().outputLinkItemList.Count > 0)
                        {
                            for(int i = 0; i < targetOJ.GetComponent<GameplayItem>().outputLinkItemList.Count; i++)
                            {
                                GameObject clearLinkTarget = targetOJ.GetComponent<GameplayItem>().outputLinkItemList[i];
                                LinkObjectDataClear(targetOJ, clearLinkTarget, true, false);
                            }
                        }
                        if (targetOJ.GetComponent<GameplayItem>().inputLinkItemList.Count > 0)
                        {
                            for (int i = 0; i < targetOJ.GetComponent<GameplayItem>().inputLinkItemList.Count; i++)
                            {
                                GameObject clearLinkTarget = targetOJ.GetComponent<GameplayItem>().inputLinkItemList[i];
                                LinkObjectDataClear(targetOJ, clearLinkTarget, false, false);
                            }
                        }
                        if (targetOJ.GetComponent<GameplayItem>().specialOutputLinkItemList.Count > 0)
                        {
                            for (int i = 0; i < targetOJ.GetComponent<GameplayItem>().specialOutputLinkItemList.Count; i++)
                            {
                                GameObject clearLinkTarget = targetOJ.GetComponent<GameplayItem>().specialOutputLinkItemList[i];
                                LinkObjectDataClear(targetOJ, clearLinkTarget, true, true);
                            }
                        }
                        if (targetOJ.GetComponent<GameplayItem>().specialInputLinkItemList.Count > 0)
                        {
                            for (int i = 0; i < targetOJ.GetComponent<GameplayItem>().specialInputLinkItemList.Count; i++)
                            {
                                GameObject clearLinkTarget = targetOJ.GetComponent<GameplayItem>().specialInputLinkItemList[i];
                                LinkObjectDataClear(targetOJ, clearLinkTarget, false, true);
                            }
                        }

                        //dark先销毁
                        if (targetOJ.GetComponent<GameplayItem>().canDark == true)
                        {
                            for (int i = 0; i < targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.childCount; i++)
                            {
                                GameObject sock = targetOJ.GetComponent<GameplayItem>().darkSocketContent.transform.GetChild(i).gameObject;
                                if (sock.transform.childCount > 1)
                                {
                                    for (int aa = 0; aa < sock.transform.childCount; aa++)
                                    {
                                        GameObject gi = sock.transform.GetChild(aa).gameObject;
                                        if (gi.name.Contains("GameplayItem_"))
                                        {
                                            int iidd = gi.GetComponent<GameplayItem>().itemID;
                                            //ui数量变更
                                            designPenel.GameItemNumChange(iidd, true, true);

                                            //黑暗玩法列表清除
                                            if (gameplayMapping.darkGameplayItemList.Contains(gi))
                                            {
                                                gameplayMapping.darkGameplayItemList.Remove(gi);
                                            }

                                            Destroy(gi);
                                        }
                                    }
                                }
                            }
                        }

                        //本体销毁
                        int iiddd = targetOJ.GetComponent<GameplayItem>().itemID;
                        bool isdarkkk = !targetOJ.GetComponent<GameplayItem>().isMain;
                        //ui数量变更
                        designPenel.GameItemNumChange(iiddd, isdarkkk, true);

                        Destroy(targetOJ);
                    }
                }
                else
                {

                }  
            }

            mosIsDragging = false;
            //清理临时指示物
            TemGameplayItemClear();
            //
            gameplayItemAction = false;
            gameMode.gameProcessPause = false;

            Item101StartCheck();  //判定是否形成闭环

            //刷新全局
            designPenel.DesignReset();
        }

        // 释放鼠标左键（拖拽路线）
        if (Input.GetMouseButtonUp(0) && roadEditMode && ojt != null)
        {
            if (ojt != null)
            {
                Destroy(ojt);
            }
            //特殊连接下是否符合目标的检测
            if(specialItemLink == true)
            {
                if(roadEditDefaultDirection == true)
                {
                    if(targetOJ.GetComponent<GameplayItem>().specialOutputLinkTargetList.Count > 0)
                    {
                        if (targetOJ.GetComponent<GameplayItem>().specialOutputLinkTargetList.Contains(otherTargetOJ.GetComponent<GameplayItem>().itemID))
                        {
                            //合理
                        }
                        else
                        {
                            otherTarget = false;
                        }
                    }
                    //
                    if (otherTargetOJ.GetComponent<GameplayItem>().specialInputLinkTargetList.Count > 0)
                    {
                        if (otherTargetOJ.GetComponent<GameplayItem>().specialInputLinkTargetList.Contains(targetOJ.GetComponent<GameplayItem>().itemID))
                        {
                            //合理
                        }
                        else
                        {
                            otherTarget = false;
                        }
                    }
                }
                else if (roadEditDefaultDirection == false)
                {
                    if (targetOJ.GetComponent<GameplayItem>().specialInputLinkTargetList.Count > 0)
                    {
                        if (targetOJ.GetComponent<GameplayItem>().specialInputLinkTargetList.Contains(otherTargetOJ.GetComponent<GameplayItem>().itemID))
                        {
                            //合理
                        }
                        else
                        {
                            otherTarget = false;
                        }
                    }
                    //
                    if (otherTargetOJ.GetComponent<GameplayItem>().specialOutputLinkTargetList.Count > 0)
                    {
                        if (otherTargetOJ.GetComponent<GameplayItem>().specialOutputLinkTargetList.Contains(targetOJ.GetComponent<GameplayItem>().itemID))
                        {
                            //合理
                        }
                        else
                        {
                            otherTarget = false;
                        }
                    }
                }
            }

            //连接到其他item，否则不算
            if(otherTarget == true && (validPath || roadEditStartPos == roadEditEndPos))
            {
                if (temRoadObjectLayer.transform.childCount > 0)
                {
                    //两个连接体相互连接 && 数据更新 && 清理
                    LinkObjectDataClear(targetOJ, otherTargetOJ, roadEditDefaultDirection, specialItemLink);

                    //查看是否超过最大连接
                    LinkObjectMaxLinkCheck(targetOJ, otherTargetOJ, roadEditDefaultDirection);

                    //
                    GameObject outout = targetOJ;
                    GameObject inin = otherTargetOJ;
                    //建立连接
                    if (roadEditDefaultDirection == true)
                    {
                        outout = targetOJ;
                        inin = otherTargetOJ;
                    }
                    else
                    {
                        outout = otherTargetOJ;
                        inin = targetOJ;
                    }

                    if(specialItemLink == false)
                    {
                        outout.GetComponent<GameplayItem>().outputLinkItemList.Add(inin);
                        inin.GetComponent<GameplayItem>().inputLinkItemList.Add(outout);
                        outout.GetComponent<GameplayItem>().outputPathPosListList.Add(new List<Vector3Int>());
                        outout.GetComponent<GameplayItem>().outputPathItemListList.Add(new List<GameObject>());
                    }
                    else if(specialItemLink == true)
                    {
                        outout.GetComponent<GameplayItem>().specialOutputLinkItemList.Add(inin);
                        inin.GetComponent<GameplayItem>().specialInputLinkItemList.Add(outout);
                        outout.GetComponent<GameplayItem>().specialOutputPathPosListList.Add(new List<Vector3Int>());
                        outout.GetComponent<GameplayItem>().specialOutputPathItemListList.Add(new List<GameObject>());
                    }
                    
                    //
                    for (int i = 0; i < temRoadObjectLayer.transform.childCount; i++)
                    {
                        //Debug.Log(temRoadObjectLayer.transform.childCount);
                        GameObject pathIII = temRoadObjectLayer.transform.GetChild(i).gameObject;
                        pathIII.GetComponent<MainPathItem>().linkGameItemList.Add(outout);
                        pathIII.GetComponent<MainPathItem>().linkGameItemList.Add(inin);
                        pathIII.GetComponent<MainPathItem>().TemRoadDestroy(true);
                    }                    
                }
            }
            else
            {
                if (temRoadObjectLayer.transform.childCount > 0)
                {
                    for (int i = 0; i < temRoadObjectLayer.transform.childCount; i++)
                    {
                        GameObject pathIII = temRoadObjectLayer.transform.GetChild(i).gameObject;
                        pathIII.GetComponent<MainPathItem>().TemRoadDestroy(false);
                    }
                    //
                }
            }
            
            //
            roadEditMode = false;
            //
            gameplayItemAction = false;
            gameMode.gameProcessPause = false;

            Item101StartCheck();  //判定是否形成闭环

            //刷新全局
            designPenel.DesignReset();
        }

        //菜单拖拽取消或结束
        if (designPenel.designItemBtnState == true && designPenel.designItemDragState == false && ojt != null && mosIsDragging == false && roadEditMode == false)
        {
            //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            panelIsDragging = false;
            if (ojt != null)
            {
                Destroy(ojt);
            }
            if (uijt != null)
            {
                Destroy(uijt);
            }
            //清理临时指示物
            TemGameplayItemClear();
            //
            gameplayItemAction = false;
            gameMode.gameProcessPause = false;
        }

        // 检测鼠标右键点击
        if (Input.GetMouseButtonDown(1))
        {
            if (mosIsDragging)
            {
                mosIsDragging = false;
                if (ojt != null)
                {
                    Destroy(ojt);
                }
                if (uijt != null)
                {
                    Destroy(uijt);
                }
                //清理临时指示物
                TemGameplayItemClear();
                //
                gameplayItemAction = false;
                gameMode.gameProcessPause = false;
            }
            else if (designPenel.designItemDragState)
            {
                if(targetPanelItem != null)
                {
                    targetPanelItem.GetComponent<GameplayItemUIItemDrag>().CancelDrag();
                }
                else
                {
                    Debug.Log("找不到正在拖动的UI组件");
                }
                //清理临时指示物
                TemGameplayItemClear();
                //
                gameplayItemAction = false;
                gameMode.gameProcessPause = false;
            }
        }
    }

    //实例化玩法
    public void InsNewGameplayItem(int id, bool vv, bool isdark)
    {
        if(vv == true)
        {
            if (dragPosState == 0)
            {
                if(isdark == false)
                {
                    //创建
                    InsGameplayItemSet(id);
                    GameObject oj = Instantiate(gameplayItem) as GameObject;
                    oj.transform.SetParent(gameplayObjectLayer.transform);
                    oj.transform.position = previousCellPosition;
                    oj.transform.eulerAngles = temTargetOJ.transform.eulerAngles;
                    //
                    oj.GetComponent<GameplayItem>().SetItemID(id);
                    oj.GetComponent<GameplayItem>().SetValid(true);

                    //主/dark玩法列表 - 添加
                    if (oj.GetComponent<GameplayItem>().isMain == true)
                    {
                        gameplayMapping.mainGameplayItemList.Add(oj);
                    }

                    //非法通行-新坐标加入
                    oj.GetComponent<GameplayItem>().IllegalMapAdd();

                    //清理临时指示物
                    TemGameplayItemClear();
                    //
                    gameplayItemAction = false;
                    gameMode.gameProcessPause = false;

                    //ui数量变更
                    designPenel.GameItemNumChange(id, false, false);
                }
                else if(isdark == true && darkHasLink == true)
                {
                    //创建
                    InsGameplayItemSet(id);
                    GameObject oj = Instantiate(gameplayItem) as GameObject;
                    oj.transform.SetParent(darkLinkPoint.transform);
                    oj.GetComponent<GameplayItem>().linkDarkSocketPoint = darkLinkPoint;
                    oj.transform.position = darkLinkPos;
                    oj.transform.eulerAngles = temTargetOJ.transform.eulerAngles;
                    //
                    oj.GetComponent<GameplayItem>().SetItemID(id);
                    oj.GetComponent<GameplayItem>().SetValid(true);

                    //主/dark玩法列表 - 添加
                    if (oj.GetComponent<GameplayItem>().isMain == false)
                    {
                        gameplayMapping.darkGameplayItemList.Add(oj);
                    }

                    //非法通行-新坐标加入
                    oj.GetComponent<GameplayItem>().IllegalMapAdd();
                    //母gameplayItem占位坐标更新
                    otherTargetOJ.GetComponent<GameplayItem>().OccupiedAreaListUpdate();

                    //清理临时指示物
                    TemGameplayItemClear();
                    //
                    gameplayItemAction = false;
                    gameMode.gameProcessPause = false;

                    //ui数量变更
                    designPenel.GameItemNumChange(id, true, false);
                }
                
            }
            else if (dragPosState == 1)
            {
                //无效
            }
        }else if(vv == false)
        {
            InsGameplayItemSet(id);
            temTargetOJ = Instantiate(gameplayItem) as GameObject;
            temTargetOJ.transform.SetParent(temGameplayObjectLayer.transform);
            temTargetOJ.GetComponent<GameplayItem>().SetItemID(id);
            temTargetOJ.GetComponent<GameplayItem>().SetValid(false);
            temTargetOJ.GetComponent<GameplayItem>().moveBtn.GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }

    //清理临时指示物
    void TemGameplayItemClear()
    {
        if (temGameplayObjectLayer.transform.childCount != 0)
        {
            for (int i = 0; i < temGameplayObjectLayer.transform.childCount; i++)
            {
                Destroy(temGameplayObjectLayer.transform.GetChild(i).gameObject);
            }
        }
    }

    //ID物体配对！！！！！！！！
    public void InsGameplayItemSet(int id)
    {
        if (id == 101)
        {
            gameplayItem = gpItem_101;
        }
        else if (id == 102)
        {
            gameplayItem = gpItem_102;
        }
        else if (id == 103)
        {
            gameplayItem = gpItem_103;
        }
        else if (id == 104)
        {
            gameplayItem = gpItem_104;
        }
        else if (id == 999)
        {
            gameplayItem = gpItem_999;
        }
        else if (id == 401)
        {
            gameplayItem = gpItem_401;
        }
        else if (id == 402)
        {
            gameplayItem = gpItem_402;
        }
        else if (id == 403)
        {
            gameplayItem = gpItem_403;
        }
        else if (id == 411)
        {
            gameplayItem = gpItem_411;
        }
        else if (id == 412)
        {
            gameplayItem = gpItem_412;
        }
        else if (id == 421)
        {
            gameplayItem = gpItem_421;
        }
        else if (id == 422)
        {
            gameplayItem = gpItem_422;
        }
        else if (id == 431)
        {
            gameplayItem = gpItem_431;
        }
        else if (id == 432)
        {
            gameplayItem = gpItem_432;
        }
        else if (id == 433)
        {
            gameplayItem = gpItem_433;
        }
        else if (id == 434)
        {
            gameplayItem = gpItem_434;
        }
        else if (id == 435)
        {
            gameplayItem = gpItem_435;
        }
        else if (id == 436)
        {
            gameplayItem = gpItem_436;
        }
        else
        {
            Debug.LogError("No Item!");
        }
    }

    //路径构建
    public void RoadSetLogic()
    {
        validPath = false;
        //自动路径
        if (dragMapValid == true && dragPosState == 0 && gameplayMapping.mapIllegalList.Contains(roadEditStartPos) == false && gameplayMapping.mapIllegalList.Contains(roadEditEndPos) == false)
        {
            List<Vector3Int> pathL = new List<Vector3Int>();
            pathL = gameplayMapping.FindPath(roadEditStartPos, roadEditEndPos);
            //
            if(pathL != null && pathL.Count > 0 && roadEditStartPos != roadEditEndPos)
            {
                validPath = true;
                pathL.Insert(0, roadEditStartPos);
                //生成标记
                if (temRoadObjectLayer.transform.childCount < pathL.Count)
                {
                    for (int i = 0; i < pathL.Count - temRoadObjectLayer.transform.childCount; i++)
                    {
                        GameObject ptItem = Instantiate(mainPathItem) as GameObject;
                        ptItem.transform.SetParent(temRoadObjectLayer.transform);
                        ptItem.GetComponent<MainPathItem>().SetValid(false);  //临时道路
                        ptItem.GetComponent<MainPathItem>().defaltDirection = roadEditDefaultDirection;
                    }
                }
                //标记排序 && 道路绘制
                for (int i = 0; i < temRoadObjectLayer.transform.childCount; i++)
                {
                    if (i + 1 <= pathL.Count)
                    {
                        temRoadObjectLayer.transform.GetChild(i).gameObject.transform.position = pathL[i];
                        temRoadObjectLayer.transform.GetChild(i).gameObject.transform.localScale = new Vector3(1, 1, 1);
                        //绘制
                        Vector3Int startPPP = new Vector3Int(0,0,0);
                        Vector3Int endPPP = new Vector3Int(0, 0, 0);
                        if (i == 0)
                        {
                            //开头
                            startPPP = roadEditStart0Pos;
                            endPPP = pathL[i + 1];
                        }
                        else if(i == pathL.Count - 1)
                        {
                            //结尾
                            startPPP = pathL[i - 1];
                            endPPP = roadEditEnd1Pos;
                        }
                        else
                        {
                            //中间
                            startPPP = pathL[i - 1];
                            endPPP = pathL[i + 1];
                        }
                        //赋值
                        temRoadObjectLayer.transform.GetChild(i).gameObject.GetComponent<MainPathItem>().SetDetail(pathL[i], startPPP, endPPP);
                        //绘图
                        temRoadObjectLayer.transform.GetChild(i).gameObject.GetComponent<MainPathItem>().RoadPicSet();
                    }
                    else
                    {
                        temRoadObjectLayer.transform.GetChild(i).gameObject.transform.localScale = new Vector3(0, 0, 0);
                    }
                }
            }
            else if(roadEditStartPos == roadEditEndPos)
            {
                //生成标记
                if (temRoadObjectLayer.transform.childCount == 0)
                {
                    GameObject ptItem = Instantiate(mainPathItem) as GameObject;
                    ptItem.transform.SetParent(temRoadObjectLayer.transform);
                    ptItem.GetComponent<MainPathItem>().SetValid(false);  //临时道路
                    ptItem.GetComponent<MainPathItem>().defaltDirection = roadEditDefaultDirection;
                }
                //标记排序 && 道路绘制
                for (int i = 0; i < temRoadObjectLayer.transform.childCount; i++)
                {
                    if (i == 0)
                    {
                        temRoadObjectLayer.transform.GetChild(i).gameObject.transform.position = roadEditStartPos;
                        temRoadObjectLayer.transform.GetChild(i).gameObject.transform.localScale = new Vector3(1, 1, 1);
                        //绘制
                        Vector3Int startPPP = roadEditStart0Pos;
                        Vector3Int endPPP = roadEditEnd1Pos;
                        //赋值
                        temRoadObjectLayer.transform.GetChild(i).gameObject.GetComponent<MainPathItem>().SetDetail(roadEditStartPos, startPPP, endPPP);
                        //绘图
                        temRoadObjectLayer.transform.GetChild(i).gameObject.GetComponent<MainPathItem>().RoadPicSet();
                    }
                    else
                    {
                        temRoadObjectLayer.transform.GetChild(i).gameObject.transform.localScale = new Vector3(0, 0, 0);
                    }
                }
            }
        }
        else
        {
            if (temRoadObjectLayer.transform.childCount != 0)
            {
                for (int i = 0; i < temRoadObjectLayer.transform.childCount; i++)
                {
                    temRoadObjectLayer.transform.GetChild(i).gameObject.transform.localScale = new Vector3(0, 0, 0);
                }
            }
        }
    }

    //清理道路连接数据，并清除道路
    void LinkObjectDataClear(GameObject aa, GameObject bb, bool direction, bool isSpecial)
    {
        bool canClear = false;

        GameObject outout = aa;
        GameObject inin = bb;

        if(isSpecial == false)
        {
            if (aa.GetComponent<GameplayItem>().outputLinkItemList.Contains(bb) && direction == true)
            {
                outout = aa;
                inin = bb;
                canClear = true;
            }
            else if (aa.GetComponent<GameplayItem>().inputLinkItemList.Contains(bb) && direction == false)
            {
                outout = bb;
                inin = aa;
                canClear = true;
            }
        }
        else if(isSpecial == true)
        {
            if (aa.GetComponent<GameplayItem>().specialOutputLinkItemList.Contains(bb) && direction == true)
            {
                outout = aa;
                inin = bb;
                canClear = true;
            }
            else if (aa.GetComponent<GameplayItem>().specialInputLinkItemList.Contains(bb) && direction == false)
            {
                outout = bb;
                inin = aa;
                canClear = true;
            }
        }

        if(canClear == true)
        {
            if(isSpecial == false)
            {
                int orderOut = 0;
                int orderIn = 0;
                //确定Out序号位置
                for (int i = 0; i < outout.GetComponent<GameplayItem>().outputLinkItemList.Count; i++)
                {
                    if (outout.GetComponent<GameplayItem>().outputLinkItemList[i] == inin)
                    {
                        orderOut = i;
                        break;
                    }
                }
                //清理连接对象
                outout.GetComponent<GameplayItem>().outputLinkItemList.RemoveAt(orderOut);
                //清理道路位置
                outout.GetComponent<GameplayItem>().outputPathPosListList.RemoveAt(orderOut);
                //清理道路
                List<GameObject> pathItemList = new List<GameObject>();
                pathItemList = outout.GetComponent<GameplayItem>().outputPathItemListList[orderOut];
                for (int i = 0; i < pathItemList.Count; i++)
                {
                    Destroy(pathItemList[i]);
                }
                outout.GetComponent<GameplayItem>().outputPathItemListList.RemoveAt(orderOut);

                //确定in序号位置
                for (int i = 0; i < inin.GetComponent<GameplayItem>().inputLinkItemList.Count; i++)
                {
                    if (inin.GetComponent<GameplayItem>().inputLinkItemList[i] == outout)
                    {
                        orderIn = i;
                        break;
                    }
                }
                //清理连接对象
                inin.GetComponent<GameplayItem>().inputLinkItemList.RemoveAt(orderIn);
            }
            else if(isSpecial == true)
            {
                int orderOut = 0;
                int orderIn = 0;
                //确定Out序号位置
                for (int i = 0; i < outout.GetComponent<GameplayItem>().specialOutputLinkItemList.Count; i++)
                {
                    if (outout.GetComponent<GameplayItem>().specialOutputLinkItemList[i] == inin)
                    {
                        orderOut = i;
                        break;
                    }
                }
                //清理连接对象
                outout.GetComponent<GameplayItem>().specialOutputLinkItemList.RemoveAt(orderOut);
                //清理道路位置
                outout.GetComponent<GameplayItem>().specialOutputPathPosListList.RemoveAt(orderOut);
                //清理道路
                List<GameObject> pathItemList = new List<GameObject>();
                pathItemList = outout.GetComponent<GameplayItem>().specialOutputPathItemListList[orderOut];
                for (int i = 0; i < pathItemList.Count; i++)
                {
                    Destroy(pathItemList[i]);
                }
                outout.GetComponent<GameplayItem>().specialOutputPathItemListList.RemoveAt(orderOut);

                //确定in序号位置
                for (int i = 0; i < inin.GetComponent<GameplayItem>().specialInputLinkItemList.Count; i++)
                {
                    if (inin.GetComponent<GameplayItem>().specialInputLinkItemList[i] == outout)
                    {
                        orderIn = i;
                        break;
                    }
                }
                //清理连接对象
                inin.GetComponent<GameplayItem>().specialInputLinkItemList.RemoveAt(orderIn);
            }
        }
        
    }

    //最大连接数判定，超过则清理掉第一条
    void LinkObjectMaxLinkCheck(GameObject aa, GameObject bb, bool direction)
    {
        GameObject outout = aa;
        GameObject inin = bb;

        if(direction == true)
        {
            outout = aa;
            inin = bb;
        }
        else
        {
            outout = bb;
            inin = aa;
        }

        if(specialItemLink == false)
        {
            if (outout.GetComponent<GameplayItem>().outputLinkItemList.Count >= outout.GetComponent<GameplayItem>().outputCount)
            {
                GameObject removeLink = outout.GetComponent<GameplayItem>().outputLinkItemList[0];
                LinkObjectDataClear(outout, removeLink, true, specialItemLink);
            }

            if (inin.GetComponent<GameplayItem>().inputLinkItemList.Count >= inin.GetComponent<GameplayItem>().inputCount)
            {
                GameObject removeLink = inin.GetComponent<GameplayItem>().inputLinkItemList[0];
                LinkObjectDataClear(inin, removeLink, false, specialItemLink);
            }
        }
        else if(specialItemLink == true)
        {
            if (outout.GetComponent<GameplayItem>().specialOutputLinkItemList.Count >= outout.GetComponent<GameplayItem>().specialOutputCount)
            {
                GameObject removeLink = outout.GetComponent<GameplayItem>().specialOutputLinkItemList[0];
                LinkObjectDataClear(outout, removeLink, true, specialItemLink);
            }

            if (inin.GetComponent<GameplayItem>().specialInputLinkItemList.Count >= inin.GetComponent<GameplayItem>().specialInputCount)
            {
                GameObject removeLink = inin.GetComponent<GameplayItem>().specialInputLinkItemList[0];
                LinkObjectDataClear(inin, removeLink, false, specialItemLink);
            }
        }
    }

    //开局
    void Item999Start()
    {
        //创建
        InsGameplayItemSet(999);
        GameObject oj = Instantiate(gameplayItem) as GameObject;
        oj.transform.SetParent(gameplayObjectLayer.transform);
        oj.transform.position = new Vector3(0, 0, 0);
        //
        oj.GetComponent<GameplayItem>().SetItemID(999);
        oj.GetComponent<GameplayItem>().SetValid(true);
        //主玩法列表
        if (oj.GetComponent<GameplayItem>().isMain == true)
        {
            gameplayMapping.mainGameplayItemList.Add(oj);
        }
        //非法通行-新坐标加入
        oj.GetComponent<GameplayItem>().IllegalMapAdd();
    }

    //自动道路重建（单路）
    void AutoRoadRecreate(GameObject item1, GameObject item2, bool direction, bool isSpecial)
    {
        //主体 item
        GameObject outout = item1;
        GameObject inin = item2;
        if(direction == true)
        {
            outout = item1;
            inin = item2;
        }
        else if (direction == false)
        {
            outout = item2;
            inin = item1;
        }

        //起始点&方向创建
        Vector3Int start0Pos = new Vector3Int(0, 0, 0);
        Vector3Int startPos = new Vector3Int(0, 0, 0);
        Vector3Int endPos = new Vector3Int(0, 0, 0);
        Vector3Int end1Pos = new Vector3Int(0, 0, 0);

        if(isSpecial == true)
        {
            GameObject start0PP = outout.GetComponent<GameplayItem>().specialOutputPoint.gameObject;
            GameObject startPP = outout.GetComponent<GameplayItem>().specialOutputStartPoint.gameObject;
            start0Pos = new Vector3Int(Mathf.RoundToInt(start0PP.transform.position.x), Mathf.RoundToInt(start0PP.transform.position.y), Mathf.RoundToInt(start0PP.transform.position.z));
            startPos = new Vector3Int(Mathf.RoundToInt(startPP.transform.position.x), Mathf.RoundToInt(startPP.transform.position.y), Mathf.RoundToInt(startPP.transform.position.z));
        }
        else if (isSpecial == false)
        {
            GameObject start0PP = outout.GetComponent<GameplayItem>().outputPoint.gameObject;
            GameObject startPP = outout.GetComponent<GameplayItem>().outputStartPoint.gameObject;
            start0Pos = new Vector3Int(Mathf.RoundToInt(start0PP.transform.position.x), Mathf.RoundToInt(start0PP.transform.position.y), Mathf.RoundToInt(start0PP.transform.position.z));
            startPos = new Vector3Int(Mathf.RoundToInt(startPP.transform.position.x), Mathf.RoundToInt(startPP.transform.position.y), Mathf.RoundToInt(startPP.transform.position.z));
        }

        //结束点
        if (isSpecial == true)
        {
            Vector3 roadPPP = inin.GetComponent<GameplayItem>().specialInputStartPoint.transform.position;
            endPos = new Vector3Int(Mathf.RoundToInt(roadPPP.x), Mathf.RoundToInt(roadPPP.y), Mathf.RoundToInt(roadPPP.z));
            Vector3 road1PPP = inin.GetComponent<GameplayItem>().specialInputPoint.transform.position;
            end1Pos = new Vector3Int(Mathf.RoundToInt(road1PPP.x), Mathf.RoundToInt(road1PPP.y), Mathf.RoundToInt(road1PPP.z));
        }
        else if (isSpecial == false)
        {
            Vector3 roadPPP = inin.GetComponent<GameplayItem>().inputStartPoint.transform.position;
            endPos = new Vector3Int(Mathf.RoundToInt(roadPPP.x), Mathf.RoundToInt(roadPPP.y), Mathf.RoundToInt(roadPPP.z));
            Vector3 road1PPP = inin.GetComponent<GameplayItem>().inputPoint.transform.position;
            end1Pos = new Vector3Int(Mathf.RoundToInt(road1PPP.x), Mathf.RoundToInt(road1PPP.y), Mathf.RoundToInt(road1PPP.z));
        }

        //自动路径
        if (gameplayMapping.mapIllegalList.Contains(startPos) == false && gameplayMapping.mapIllegalList.Contains(endPos) == false)
        {
            List<Vector3Int> pathL = new List<Vector3Int>();
            pathL = gameplayMapping.FindPath(startPos, endPos);
            //
            if ((pathL != null && pathL.Count > 0) || startPos == endPos)
            {
                if(pathL != null && pathL.Count > 0)
                {
                    pathL.Insert(0, startPos);
                }
                else if(startPos == endPos)
                {
                    pathL = new List<Vector3Int>();
                    pathL.Add(startPos);
                }

                //数据填充 物体联系建立
                if (isSpecial == false)
                {
                    outout.GetComponent<GameplayItem>().outputLinkItemList.Add(inin);
                    inin.GetComponent<GameplayItem>().inputLinkItemList.Add(outout);
                    outout.GetComponent<GameplayItem>().outputPathPosListList.Add(new List<Vector3Int>());
                    outout.GetComponent<GameplayItem>().outputPathItemListList.Add(new List<GameObject>());
                }
                else if (isSpecial == true)
                {
                    outout.GetComponent<GameplayItem>().specialOutputLinkItemList.Add(inin);
                    inin.GetComponent<GameplayItem>().specialInputLinkItemList.Add(outout);
                    outout.GetComponent<GameplayItem>().specialOutputPathPosListList.Add(new List<Vector3Int>());
                    outout.GetComponent<GameplayItem>().specialOutputPathItemListList.Add(new List<GameObject>());
                }

                //生成正式道路
                for (int i = 0; i < pathL.Count; i++)
                {
                    GameObject pItem = Instantiate(mainPathItem) as GameObject;
                    pItem.transform.SetParent(roadLayer.transform);
                    pItem.transform.position = pathL[i];
                    Vector3Int pointStartPos = new Vector3Int(0, 0, 0);
                    Vector3Int pointEndPos = new Vector3Int(0, 0, 0);
                    if (i == 0)
                    {
                        if(pathL.Count == 1)
                        {
                            pointStartPos = start0Pos;
                            pointEndPos = end1Pos;
                        }
                        else
                        {
                            pointStartPos = start0Pos;
                            pointEndPos = pathL[i + 1];
                        } 
                    }
                    else if (i == pathL.Count - 1)
                    {
                        pointStartPos = pathL[i - 1];
                        pointEndPos = end1Pos;
                    }
                    else
                    {
                        pointStartPos = pathL[i - 1];
                        pointEndPos = pathL[i + 1];
                    }
                    pItem.GetComponent<MainPathItem>().SetDetail(pathL[i], pointStartPos, pointEndPos);
                    pItem.GetComponent<MainPathItem>().RoadPicSet();
                    pItem.GetComponent<MainPathItem>().SetValid(true);
                    pItem.GetComponent<MainPathItem>().linkGameItemList = new List<GameObject>();
                    pItem.GetComponent<MainPathItem>().linkGameItemList.Add(outout);
                    pItem.GetComponent<MainPathItem>().linkGameItemList.Add(inin);
                    pItem.GetComponent<MainPathItem>().defaltDirection = true;

                    //道路格数据填充
                    if (isSpecial == false)
                    {
                        int count = outout.GetComponent<GameplayItem>().outputPathPosListList.Count - 1;
                        outout.GetComponent<GameplayItem>().outputPathPosListList[count].Add(pathL[i]);
                        outout.GetComponent<GameplayItem>().outputPathItemListList[count].Add(pItem);
                    }
                    else if (isSpecial == true)
                    {
                        int count = outout.GetComponent<GameplayItem>().specialOutputPathPosListList.Count - 1;
                        outout.GetComponent<GameplayItem>().specialOutputPathPosListList[count].Add(pathL[i]);
                        outout.GetComponent<GameplayItem>().specialOutputPathItemListList[count].Add(pItem);
                    }
                }
            }
        }
    }
    //自动道路重建（item发起）
    public void AutoRoadRecreateForItem(GameObject myItem)
    {
        //重建路网
        //1.确定联络物
        List<GameObject> preOutputList = new List<GameObject>();
        List<GameObject> preInputList = new List<GameObject>();
        List<GameObject> preSpecialOutputList = new List<GameObject>();
        List<GameObject> preSpecialInputList = new List<GameObject>();

        preOutputList.AddRange(myItem.GetComponent<GameplayItem>().outputLinkItemList);
        preInputList.AddRange(myItem.GetComponent<GameplayItem>().inputLinkItemList);
        preSpecialOutputList.AddRange(myItem.GetComponent<GameplayItem>().specialOutputLinkItemList);
        preSpecialInputList.AddRange(myItem.GetComponent<GameplayItem>().specialInputLinkItemList);

        //2.清洗原路网
        if (preOutputList.Count > 0)
        {
            for (int i = 0; i < preOutputList.Count; i++)
            {
                GameObject tt = preOutputList[i];
                LinkObjectDataClear(myItem, tt, true, false);
            }
        }
        if (preInputList.Count > 0)
        {
            for (int i = 0; i < preInputList.Count; i++)
            {
                GameObject tt = preInputList[i];
                LinkObjectDataClear(myItem, tt, false, false);
            }
        }
        if (preSpecialOutputList.Count > 0)
        {
            for (int i = 0; i < preSpecialOutputList.Count; i++)
            {
                GameObject tt = preSpecialOutputList[i];
                LinkObjectDataClear(myItem, tt, true, true);
            }
        }
        if (preSpecialInputList.Count > 0)
        {
            for (int i = 0; i < preSpecialInputList.Count; i++)
            {
                GameObject tt = preSpecialInputList[i];
                LinkObjectDataClear(myItem, tt, false, true);
            }
        }

        //3.路网重建
        if (preOutputList.Count > 0)
        {
            for (int i = 0; i < preOutputList.Count; i++)
            {
                GameObject tt = preOutputList[i];
                AutoRoadRecreate(myItem, tt, true, false);
            }
        }
        if (preInputList.Count > 0)
        {
            for (int i = 0; i < preInputList.Count; i++)
            {
                GameObject tt = preInputList[i];
                AutoRoadRecreate(myItem, tt, false, false);
            }
        }
        if (preSpecialOutputList.Count > 0)
        {
            for (int i = 0; i < preSpecialOutputList.Count; i++)
            {
                GameObject tt = preSpecialOutputList[i];
                AutoRoadRecreate(myItem, tt, true, true);
            }
        }
        if (preSpecialInputList.Count > 0)
        {
            for (int i = 0; i < preSpecialInputList.Count; i++)
            {
                GameObject tt = preSpecialInputList[i];
                AutoRoadRecreate(myItem, tt, false, true);
            }
        }
    }

    //循环构建成功检测
    void Item101StartCheck()
    {
        if(gameplayMapping.mainGameplayItemList.Count > 0 && startItemObject == null)
        {
            for(int i = 0; i < gameplayMapping.mainGameplayItemList.Count; i++)
            {
                GameObject aa = gameplayMapping.mainGameplayItemList[i];
                if(aa.gameObject.name == "GameplayItem_101(Clone)")
                {
                    startItemObject = aa;
                    break;
                }
            }
        }

        if(startItemObject != null)
        {
            startItemObject.GetComponent<GameplayItem>().GameProcessStart();
        }
        else
        {
            gameMode.gameDynamicProcess = false;
        }

    }


}


