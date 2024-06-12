using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameplayItem : MonoBehaviour
{
    public int itemID;

    public bool isMain;

    public bool canPutBack = true;

    public bool canRotate = true;

    private int rotationDirection = 0;
    private bool rotationAnime = false;

    public bool isValidItem = false;

    public GameObject gameplayPic;
    public GameObject stablePic;

    public GameObject occupiedArea;

    public GameObject inputPoint;
    public GameObject outputPoint;
    public GameObject inputStartPoint;
    public GameObject outputStartPoint;

    public GameObject specialInputPoint;
    public GameObject specialInputStartPoint;
    public GameObject specialOutputPoint;
    public GameObject specialOutputStartPoint;

    public GameObject inputBtn;
    public GameObject outputBtn;
    public GameObject specialInputBtn;
    public GameObject specialOutputBtn;

    public GameObject rotationBg;
    public GameObject rotationBtn;
    public GameObject moveBtn;

    private BasicAction basicAction;
    private GameplayMapping gameplayMapping;

    public List<Vector3Int> itemOccupiedAreaList0;
    public List<Vector3Int> itemOccupiedAreaList;

    public List<GameObject> outputLinkItemList; //我连接了多少个他
    public List<List<GameObject>> outputPathItemListList;
    public List<List<Vector3Int>> outputPathPosListList;

    public List<GameObject> specialOutputLinkItemList; //我连接了多少个他（特殊）
    public List<List<GameObject>> specialOutputPathItemListList;
    public List<List<Vector3Int>> specialOutputPathPosListList;

    public List<GameObject> inputLinkItemList; //多少个他连接了我

    public List<GameObject> specialInputLinkItemList; //多少个他连接了我（特殊）

    public List<GameObject> outputPathItemList;
    public List<Vector3Int> outputPathPosList;

    public List<GameObject> specialOutputPathItemList;
    public List<Vector3Int> specialOutputPathPosList;

    public int outputCount = 1;
    public int inputCount = 1;
    public int specialOutputCount = 0;
    public int specialInputCount = 0;

    //占位合法性
    public List<bool> temItemPosValidList;
    public bool temItemPosValid;
    public GameObject temPreObject;
    public List<Vector3Int> temItemPosList;

    //约定目标
    public List<int> specialOutputLinkTargetList;
    public List<int> specialInputLinkTargetList;

    private GameMode gameMode;
    private GameplayEffect gameplayEffect;
    private CameraControl cameraControl;

    //黑暗模式
    public bool canDark = false;
    public List<int> canDarkList;
    public GameObject darkSocketContent;
    public GameObject darkPlugPoint;
    public GameObject linkDarkSocketPoint;
    private bool darkMove = true;   //拥有dark patterns拖拽中，检测是否复制体的检测标签
    public bool hasPath = true;    //本dark patterns是否包含路径
    public GameObject darkRoadContent;  //黑暗模式的具体路径
    public int darkRoadEventNum;

    //关于统计
    public int inToOutBlockNum;

    //效果
    public bool isGlobalEffect;
    public bool isTriggerEffect;
    public int triggerEventType;  //0数值事件，1付费事件

    private float rotationBtnAnimeDirection = 1f;

    private bool normalOutputAnime = false;
    private bool normalInputAnime = false;
    private bool specialOutputAnime = false;
    private bool specialInputAnime = false;
    public GameObject normalOutputAnimeP;
    public GameObject normalInputAnimeP;
    public GameObject specialOutputAnimeP;
    public GameObject specialInputAnimeP;

    private float darkTipsAnimeDirection = 1f;
    public GameObject darkTipsNotMe;
    public GameObject darkTipsTips;

    //道具内路径
    public GameObject itemInternalPath;

    //未连接图片展示
    public GameObject inputBlockPath;
    public GameObject outputBlockPath;
    public GameObject specialInputBlockPath;
    public GameObject specialOutputBlockPath;

    //点击打开编辑
    //public GameObject touchBtn;
    private DesignPanel designPanel;
    private SoundEffect soundEffect;


    private void Awake()
    {
        basicAction = GameObject.Find("Main Camera").gameObject.GetComponent<BasicAction>();
        gameplayMapping = GameObject.Find("Main Camera").gameObject.GetComponent<GameplayMapping>();
        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();
        gameplayEffect = GameObject.Find("Main Camera").gameObject.GetComponent<GameplayEffect>();
        cameraControl = GameObject.Find("Main Camera").gameObject.GetComponent<CameraControl>();
        designPanel = GameObject.Find("Canvas").gameObject.GetComponent<DesignPanel>();
        soundEffect = GameObject.Find("Canvas").gameObject.GetComponent<SoundEffect>();

        itemOccupiedAreaList0 = new List<Vector3Int>();
        itemOccupiedAreaList = new List<Vector3Int>();

        outputLinkItemList = new List<GameObject>();
        outputPathItemListList = new List<List<GameObject>>();
        outputPathPosListList = new List<List<Vector3Int>>();

        inputLinkItemList = new List<GameObject>();

        outputPathItemList = new List<GameObject>();
        outputPathPosList = new List<Vector3Int>();

        specialOutputLinkItemList = new List<GameObject>();
        specialOutputPathItemListList = new List<List<GameObject>>();
        specialOutputPathPosListList = new List<List<Vector3Int>>();

        specialInputLinkItemList = new List<GameObject>();

        specialOutputPathItemList = new List<GameObject>();
        specialOutputPathPosList = new List<Vector3Int>();

        specialOutputLinkTargetList = new List<int>();
        specialInputLinkTargetList = new List<int>();

        temItemPosValidList = new List<bool>();
        canDarkList = new List<int>();
        darkMove = true;

        //初始赋值
        if (itemID == 101)
        {
            outputCount = 1;
            inputCount = 1;
            //
            specialInputLinkTargetList.Add(999);
            specialInputCount = 1;
            //加入dark
            canDarkList.Add(421);  //dark列表
            canDarkList.Add(422);
            canDarkList.Add(423);
            canDarkList.Add(424);
            canDarkList.Add(425);
        }
        else if(itemID == 102)  //pve
        {
            outputCount = 1;
            inputCount = 1;
            //加入dark
            canDarkList.Add(401);  //dark列表
            canDarkList.Add(402);
            canDarkList.Add(403);
            canDarkList.Add(404);
            canDarkList.Add(405);
        }
        else if (itemID == 103)  //pvp
        {
            outputCount = 1;
            inputCount = 1;
            //加入dark
            canDarkList.Add(411);  //dark列表
            canDarkList.Add(412);
            canDarkList.Add(413);
            canDarkList.Add(414);
            canDarkList.Add(415);
        }
        else if (itemID == 104)   //运营
        {
            outputCount = 1;
            inputCount = 1;
            //加入dark
            canDarkList.Add(431);  //dark列表
            canDarkList.Add(432);
            canDarkList.Add(433);
            canDarkList.Add(434);
            canDarkList.Add(435);
            canDarkList.Add(436);
        }
        else if (itemID == 999)
        {
            outputCount = 1;
            inputCount = 0;
            //
            specialOutputLinkTargetList.Add(101);
            specialOutputCount = 1;
        }
        else if (itemID > 400 && itemID < 499)
        {
            outputCount = 0;
            inputCount = 0;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if(isValidItem == false)
        {
            canRotate = false;
            gameplayPic.GetComponent<SpriteRenderer>().DOFade(0.7f, 0.1f);
            gameplayPic.GetComponent<SpriteRenderer>().sortingOrder = 10;
            //
            if(inputBtn != null)
            {
                inputBtn.transform.localScale = new Vector3(0, 0, 0);
            }
            if(outputBtn != null)
            {
                outputBtn.transform.localScale = new Vector3(0, 0, 0);
            }
            if (rotationBg != null)
            {
                rotationBg.transform.localScale = new Vector3(0, 0, 0);
            }
            if (rotationBtn != null)
            {
                rotationBtn.transform.localScale = new Vector3(0, 0, 0);
            }
            if(moveBtn != null)
            {
                moveBtn.transform.localScale = new Vector3(0, 0, 0);
            }
            //
            if (specialInputBtn != null)
            {
                specialInputBtn.transform.localScale = new Vector3(0, 0, 0);
            }
            if (specialOutputBtn != null)
            {
                specialOutputBtn.transform.localScale = new Vector3(0, 0, 0);
            }

        }
        else if (isValidItem == true)
        {
            if (inputBtn != null)
            {
                if (inputCount > 0)
                {
                    inputBtn.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    inputBtn.transform.localScale = new Vector3(0, 0, 0);
                }
                inputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
            }

            if(outputBtn != null)
            {
                if (outputCount > 0)
                {
                    outputBtn.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    outputBtn.transform.localScale = new Vector3(0, 0, 0);
                }
                outputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
            }

            if (specialInputBtn != null)
            {
                if (specialInputCount > 0)
                {
                    specialInputBtn.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    specialInputBtn.transform.localScale = new Vector3(0, 0, 0);
                }
                specialInputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
            }

            if (specialOutputBtn != null)
            {
                if (specialOutputCount > 0)
                {
                    specialOutputBtn.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    specialOutputBtn.transform.localScale = new Vector3(0, 0, 0);
                }
                specialOutputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
            }

            if (canRotate == true)
            {
                //rotationBtn.transform.localScale = new Vector3(1, 1, 1);
                if(rotationBg != null)
                {
                    rotationBg.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                }
                if(rotationBtn != null)
                {
                    rotationBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                }
                
            }
            else
            {
                if (rotationBg != null)
                {
                    rotationBg.transform.localScale = new Vector3(0, 0, 0);
                }
                if (rotationBtn != null)
                {
                    rotationBtn.transform.localScale = new Vector3(0, 0, 0);
                }
                
            }

            //角度调整
            if(stablePic != null)
            {
                stablePic.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            
        }

        OccupiedAreaListUpdate();


    }

    // Update is called once per frame
    void Update()
    {
        if (BasicAction.gameplayItemSetMode == true && isValidItem == true)
        {
            if(BasicAction.gameplayItemAction == false)
            {
                if (basicAction.mousHitObject == rotationBtn && canRotate == true)
                {
                    if(inputBtn != null)
                    {
                        inputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    if(outputBtn != null)
                    {
                        outputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    //
                    if (specialInputBtn != null)
                    {
                        specialInputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    if (specialOutputBtn != null)
                    {
                        specialOutputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }

                }
                else if (basicAction.mousHitObject == inputBtn)
                {
                    if (inputBtn != null)
                    {
                        inputBtn.GetComponent<SpriteRenderer>().DOFade(1f, 0.3f);
                    }
                    if (outputBtn != null)
                    {
                        outputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    //
                    if (specialInputBtn != null)
                    {
                        specialInputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    if (specialOutputBtn != null)
                    {
                        specialOutputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                }
                else if (basicAction.mousHitObject == outputBtn)
                {
                    if (inputBtn != null)
                    {
                        inputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    if (outputBtn != null)
                    {
                        outputBtn.GetComponent<SpriteRenderer>().DOFade(1f, 0.3f);
                    }
                    //
                    if (specialInputBtn != null)
                    {
                        specialInputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    if (specialOutputBtn != null)
                    {
                        specialOutputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                }
                else if (basicAction.mousHitObject == specialInputBtn)
                {
                    if (inputBtn != null)
                    {
                        inputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    if (outputBtn != null)
                    {
                        outputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    //
                    if (specialInputBtn != null)
                    {
                        specialInputBtn.GetComponent<SpriteRenderer>().DOFade(1f, 0.3f);
                    }
                    if (specialOutputBtn != null)
                    {
                        specialOutputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                }
                else if (basicAction.mousHitObject == specialOutputBtn)
                {
                    if (inputBtn != null)
                    {
                        inputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    if (outputBtn != null)
                    {
                        outputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    //
                    if (specialInputBtn != null)
                    {
                        specialInputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    if (specialOutputBtn != null)
                    {
                        specialOutputBtn.GetComponent<SpriteRenderer>().DOFade(1f, 0.3f);
                    }
                }
                else
                {
                    if (inputBtn != null)
                    {
                        inputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    if (outputBtn != null)
                    {
                        outputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    //
                    if (specialInputBtn != null)
                    {
                        specialInputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                    if (specialOutputBtn != null)
                    {
                        specialOutputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                    }
                }
                
            }
            else if (BasicAction.gameplayItemAction == true)
            {
                if (inputBtn != null)
                {
                    inputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                }
                if (outputBtn != null)
                {
                    outputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                }
                //
                if (specialInputBtn != null)
                {
                    specialInputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                }
                if (specialOutputBtn != null)
                {
                    specialOutputBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
                }
            }
            //
            if (BasicAction.gameplayItemRotationMode == true && canRotate == true)
            {
                //rotationBtn.transform.localScale = new Vector3(1, 1, 1);
                moveBtn.transform.localScale = new Vector3(0, 0, 0);
                if(rotationBg != null)
                {
                    rotationBg.GetComponent<SpriteRenderer>().DOFade(0.8f, 0f);
                }
                if (rotationBtn != null)
                {
                    rotationBtn.GetComponent<SpriteRenderer>().DOFade(1f, 0f);
                }
                
            }
            else
            {
                if (rotationBg != null)
                {
                    rotationBg.transform.localScale = new Vector3(0, 0, 0);
                }
                if (rotationBtn != null)
                {
                    rotationBtn.transform.localScale = new Vector3(0, 0, 0);
                }
                
                moveBtn.transform.localScale = new Vector3(1, 1, 1);
            }

        }
        else
        {
            if (rotationBg != null)
            {
                rotationBg.transform.localScale = new Vector3(0, 0, 0);
            }
            if (rotationBtn != null)
            {
                rotationBtn.transform.localScale = new Vector3(0, 0, 0);
            }
            
            moveBtn.transform.localScale = new Vector3(1, 1, 1);
        }

        //占位图片刷新
        if(isValidItem == false && isMain == true)
        {
            temItemPosValidList = new List<bool>();
            temItemPosList = new List<Vector3Int>();
            //占位显示
            for (int i = 0; i < occupiedArea.transform.childCount; i++)
            {
                bool isV = false;
                GameObject blockC = occupiedArea.transform.GetChild(i).gameObject;
                Vector3Int blockP = new Vector3Int(Mathf.RoundToInt(blockC.transform.position.x), Mathf.RoundToInt(blockC.transform.position.y), Mathf.RoundToInt(blockC.transform.position.z));
                if (temItemPosList.Contains(blockP) == false)
                {
                    temItemPosList.Add(blockP);
                }

                if (gameplayMapping.mapIllegalList.Contains(blockP))
                {
                    //若目标格子为禁止格，看是否是复制体的占位格
                    if(temPreObject != null)
                    {
                        if (temPreObject.GetComponent<GameplayItem>().itemOccupiedAreaList.Contains(blockP))
                        {
                            isV = true;
                        }
                        else
                        {
                            isV = false;
                        }
                    }
                    else
                    {
                        isV = false;
                    }
                }
                else
                {
                    isV = true;
                }

                //颜色

                if(isV == true)
                {
                    blockC.GetComponent<SpriteRenderer>().color = new Color(0f, 128f / 255, 0f, 100f / 255);
                }
                else if (isV == false)
                {
                    blockC.GetComponent<SpriteRenderer>().color = new Color(255f / 255, 0f, 0f, 100f / 255);
                }

                temItemPosValidList.Add(isV);
            }

            if (temItemPosValidList.Contains(false))
            {
                temItemPosValid = false;
            }
            else
            {
                temItemPosValid = true;
            }
        }
        else if (isValidItem == false && isMain == false)
        {
            temItemPosValidList = new List<bool>();
            temItemPosList = new List<Vector3Int>();

            if (this.gameObject.transform.parent.gameObject.name == "DarkSocketPoint")
            {
                //此为拖拽复制体
                temPreObject = this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<GameplayItem>().temPreObject;
                darkMove = false;
            }

            //占位显示
            if(temPreObject == null)
            {
                //新建
                if (basicAction.darkHasLink == false)
                {
                    //没连上，则全红
                    for (int i = 0; i < occupiedArea.transform.childCount; i++)
                    {
                        bool isV = false;
                        GameObject blockC = occupiedArea.transform.GetChild(i).gameObject;
                        Vector3Int blockP = new Vector3Int(Mathf.RoundToInt(blockC.transform.position.x), Mathf.RoundToInt(blockC.transform.position.y), Mathf.RoundToInt(blockC.transform.position.z));

                        //全红
                        isV = false;

                        //颜色
                        if (isV == true)
                        {
                            blockC.GetComponent<SpriteRenderer>().color = new Color(0f, 128f / 255, 0f, 100f / 255);
                        }
                        else if (isV == false)
                        {
                            blockC.GetComponent<SpriteRenderer>().color = new Color(255f / 255, 0f, 0f, 100f / 255);
                        }

                        temItemPosValidList.Add(isV);
                    }
                }
                else if(basicAction.darkHasLink == true)
                {
                    //连上，则检测
                    for (int i = 0; i < occupiedArea.transform.childCount; i++)
                    {
                        bool isV = false;
                        GameObject blockC = occupiedArea.transform.GetChild(i).gameObject;
                        Vector3Int blockP = new Vector3Int(Mathf.RoundToInt(blockC.transform.position.x), Mathf.RoundToInt(blockC.transform.position.y), Mathf.RoundToInt(blockC.transform.position.z));

                        if (gameplayMapping.mapIllegalList.Contains(blockP))
                        {
                            //若目标格子为禁止格，看是否是连接母体的占位格 || 复制体的占位格
                            if (temPreObject != null)
                            {
                                //已创建dark的拖动，看是否复制体的占位格
                                if (temPreObject.GetComponent<GameplayItem>().itemOccupiedAreaList.Contains(blockP))
                                {
                                    isV = true;
                                }
                                else
                                {
                                    isV = false;
                                }
                            }
                            else if (temPreObject == null)
                            {
                                //新建，看是否是连接母体的占位格
                                if (basicAction.otherTargetOJ.GetComponent<GameplayItem>().itemOccupiedAreaList.Contains(blockP))
                                {
                                    isV = true;
                                }
                                else
                                {
                                    isV = false;
                                }
                            }
                        }
                        else
                        {
                            isV = true;
                        }

                        //颜色
                        if (isV == true)
                        {
                            blockC.GetComponent<SpriteRenderer>().color = new Color(0f, 128f / 255, 0f, 100f / 255);
                        }
                        else if (isV == false)
                        {
                            blockC.GetComponent<SpriteRenderer>().color = new Color(255f / 255, 0f, 0f, 100f / 255);
                        }

                        temItemPosValidList.Add(isV);
                    }
                }
                
            }
            else if(temPreObject != null)
            {
                if(darkMove == false)
                {
                    //被动挪动（复制母体）
                    //老
                    temItemPosValidList = new List<bool>();
                    //占位显示
                    for (int i = 0; i < occupiedArea.transform.childCount; i++)
                    {
                        bool isV = false;
                        GameObject blockC = occupiedArea.transform.GetChild(i).gameObject;
                        Vector3Int blockP = new Vector3Int(Mathf.RoundToInt(blockC.transform.position.x), Mathf.RoundToInt(blockC.transform.position.y), Mathf.RoundToInt(blockC.transform.position.z));

                        if (gameplayMapping.mapIllegalList.Contains(blockP))
                        {
                            //若目标格子为禁止格，看是否是复制体的占位格
                            if (temPreObject != null)
                            {
                                if (temPreObject.GetComponent<GameplayItem>().itemOccupiedAreaList.Contains(blockP))
                                {
                                    isV = true;
                                }
                                else
                                {
                                    isV = false;
                                }
                            }
                            else
                            {
                                isV = false;
                            }
                        }
                        else
                        {
                            isV = true;
                        }

                        //颜色
                        if (this.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<GameplayItem>().temItemPosList.Contains(blockP))
                        {
                            //无色
                            blockC.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
                        }
                        else
                        {
                            if (isV == true)
                            {
                                blockC.GetComponent<SpriteRenderer>().color = new Color(0f, 128f / 255, 0f, 100f / 255);
                            }
                            else if (isV == false)
                            {
                                blockC.GetComponent<SpriteRenderer>().color = new Color(255f / 255, 0f, 0f, 100f / 255);
                            }
                        }

                        temItemPosValidList.Add(isV);
                    }

                    if (temItemPosValidList.Contains(false))
                    {
                        temItemPosValid = false;
                    }
                    else
                    {
                        temItemPosValid = true;
                    }
                }
                else if (darkMove == true)
                {
                    //主动挪动（复制dark）
                    if (basicAction.darkHasLink == false)
                    {
                        //没连上，则全红
                        for (int i = 0; i < occupiedArea.transform.childCount; i++)
                        {
                            bool isV = false;
                            GameObject blockC = occupiedArea.transform.GetChild(i).gameObject;
                            Vector3Int blockP = new Vector3Int(Mathf.RoundToInt(blockC.transform.position.x), Mathf.RoundToInt(blockC.transform.position.y), Mathf.RoundToInt(blockC.transform.position.z));

                            //全红
                            isV = false;

                            //颜色
                            if (isV == true)
                            {
                                blockC.GetComponent<SpriteRenderer>().color = new Color(0f, 128f / 255, 0f, 100f / 255);
                            }
                            else if (isV == false)
                            {
                                blockC.GetComponent<SpriteRenderer>().color = new Color(255f / 255, 0f, 0f, 100f / 255);
                            }

                            temItemPosValidList.Add(isV);
                        }
                    }
                    else if (basicAction.darkHasLink == true)
                    {
                        //连上，则检测
                        for (int i = 0; i < occupiedArea.transform.childCount; i++)
                        {
                            bool isV = false;
                            GameObject blockC = occupiedArea.transform.GetChild(i).gameObject;
                            Vector3Int blockP = new Vector3Int(Mathf.RoundToInt(blockC.transform.position.x), Mathf.RoundToInt(blockC.transform.position.y), Mathf.RoundToInt(blockC.transform.position.z));

                            if (gameplayMapping.mapIllegalList.Contains(blockP))
                            {
                                //若目标格子为禁止格，看是否是连接母体的占位格 || 复制体的占位格
                                if (temPreObject != null)
                                {
                                    //已创建dark的拖动，看是否复制体的占位格
                                    if (temPreObject.GetComponent<GameplayItem>().itemOccupiedAreaList.Contains(blockP))
                                    {
                                        isV = true;
                                    }
                                    else
                                    {
                                        isV = false;
                                    }
                                }
                                else if (temPreObject == null)
                                {
                                    //新建，看是否是连接母体的占位格
                                    if (basicAction.otherTargetOJ.GetComponent<GameplayItem>().itemOccupiedAreaList.Contains(blockP))
                                    {
                                        isV = true;
                                    }
                                    else
                                    {
                                        isV = false;
                                    }
                                }
                            }
                            else
                            {
                                isV = true;
                            }

                            //颜色
                            if (isV == true)
                            {
                                blockC.GetComponent<SpriteRenderer>().color = new Color(0f, 128f / 255, 0f, 100f / 255);
                            }
                            else if (isV == false)
                            {
                                blockC.GetComponent<SpriteRenderer>().color = new Color(255f / 255, 0f, 0f, 100f / 255);
                            }

                            temItemPosValidList.Add(isV);
                        }
                    }
                }
                
            }

            if (temItemPosValidList.Contains(false))
            {
                temItemPosValid = false;
            }
            else
            {
                temItemPosValid = true;
            }
        }

        PathUnlinkShow();


    }

    private void FixedUpdate()
    {
        if(BasicAction.gameplayItemRotationMode == true && canRotate == true)
        {
            rotationBg.transform.localScale = new Vector3(1, 1, 1);

            if (rotationBtn.transform.localScale.x > 1.1f)
            {
                rotationBtn.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                rotationBtnAnimeDirection = -1;
            }
            else if (rotationBtn.transform.localScale.x < 0.9f)
            {
                rotationBtn.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                rotationBtnAnimeDirection = 1;
            }
            float dc = rotationBtnAnimeDirection * 0.005f;
            float dcc = rotationBtn.transform.localScale.x + dc;

            rotationBtn.transform.localScale = new Vector3(dcc, dcc, dcc);
        }
        else
        {
            //rotationBtn.transform.localScale = new Vector3(1, 1, 1);
        }

        if (isValidItem == true)
        {
            if(BasicAction.gameplayItemSetMode == true && BasicAction.gameplayItemAction == true)
            {
                RoadEditAnime();
                DarkTipsAnime();
            }
            else
            {
                if (normalInputAnimeP != null)
                {
                    normalInputAnimeP.transform.localScale = new Vector3(0, 0, 0);
                }
                if (normalOutputAnimeP != null)
                {
                    normalOutputAnimeP.transform.localScale = new Vector3(0, 0, 0);
                }
                if (specialInputAnimeP != null)
                {
                    specialInputAnimeP.transform.localScale = new Vector3(0, 0, 0);
                }
                if (specialOutputAnimeP != null)
                {
                    specialOutputAnimeP.transform.localScale = new Vector3(0, 0, 0);
                }

                if(darkTipsNotMe != null)
                {
                    darkTipsNotMe.transform.localScale = new Vector3(0, 0, 0);

                    if(this.gameObject.GetComponent<GameplayItemAnime>().animeContent != null)
                    {
                        for(int i = 0; i < this.gameObject.GetComponent<GameplayItemAnime>().animeContent.transform.childCount; i++)
                        {
                            GameObject animeP = this.gameObject.GetComponent<GameplayItemAnime>().animeContent.transform.GetChild(i).gameObject;
                            animeP.GetComponent<SpriteRenderer>().color = Color.white;
                        }
                    }
                }
                if (darkTipsTips != null)
                {
                    darkTipsTips.transform.localScale = new Vector3(0, 0, 0);
                }
            }
            
        }
    }

    public void SetItemID(int id)
    {
        itemID = id;
    }
    public void SetValid(bool vv)
    {
        isValidItem = vv;
    }

    //方向调整（旋转）
    public void DirectionSet()
    {
        if(rotationAnime == false)
        {
            rotationAnime = true;
            bool noBlock = true;
            rotationDirection++;
            if (rotationDirection >= 5)
            {
                this.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 0f);
                rotationBtn.transform.DOLocalRotate(new Vector3(0, 0, 0), 0f);
                rotationDirection = 1;
            }
            float z = 0;
            float rz = 0;
            if (rotationDirection == 0)
            {
                z = 0f;
                rz = 0f;
            }
            else if (rotationDirection == 1)
            {
                z = -90f;
                rz = 90f;
            }
            else if (rotationDirection == 2)
            {
                z = -180f;
                rz = 180f;
            }
            else if (rotationDirection == 3)
            {
                z = -270f;
                rz = 270f;
            }
            else if (rotationDirection == 4)
            {
                z = -360f;
                rz = 360f;
            }

            //旋转占位检测
            occupiedArea.transform.localEulerAngles = new Vector3(0,0,z);
            //dark
            if(canDark == true)
            {
                darkSocketContent.transform.localEulerAngles = new Vector3(0, 0, z);
            }
            //检测
            for(int i = 0; i < occupiedArea.transform.childCount; i++)
            {
                int xx = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.x);
                int yy = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.y);
                int zz = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.z);
                if(gameplayMapping.mapIllegalList.Contains(new Vector3Int(xx, yy, zz)) && itemOccupiedAreaList.Contains(new Vector3Int(xx, yy, zz)) == false)
                {
                    //旋转失败
                    Debug.Log("Rotate Failer");
                    noBlock = false;
                    rotationDirection--;
                    rotationAnime = false;
                    break;
                }
            }
            //dark
            if (canDark == true)
            {
                for(int i = 0; i < darkSocketContent.transform.childCount; i++)
                {
                    GameObject sock = darkSocketContent.transform.GetChild(i).gameObject;
                    if(sock.transform.childCount > 1)
                    {
                        for(int aa = 0; aa < sock.transform.childCount; aa++)
                        {
                            GameObject gi = sock.transform.GetChild(aa).gameObject;
                            if (gi.name.Contains("GameplayItem_"))
                            {
                                for (int bb = 0; bb < gi.GetComponent<GameplayItem>().occupiedArea.transform.childCount; bb++)
                                {
                                    int xx = Mathf.RoundToInt(gi.GetComponent<GameplayItem>().occupiedArea.transform.GetChild(bb).gameObject.transform.position.x);
                                    int yy = Mathf.RoundToInt(gi.GetComponent<GameplayItem>().occupiedArea.transform.GetChild(bb).gameObject.transform.position.y);
                                    int zz = Mathf.RoundToInt(gi.GetComponent<GameplayItem>().occupiedArea.transform.GetChild(bb).gameObject.transform.position.z);
                                    if (gameplayMapping.mapIllegalList.Contains(new Vector3Int(xx, yy, zz)) && itemOccupiedAreaList.Contains(new Vector3Int(xx, yy, zz)) == false)
                                    {
                                        //旋转失败
                                        Debug.Log("Rotate Failer");
                                        noBlock = false;
                                        rotationDirection--;
                                        rotationAnime = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //归位
            occupiedArea.transform.localEulerAngles = new Vector3(0, 0, 0);
            if (canDark == true)
            {
                darkSocketContent.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            //可以旋转
            if (noBlock == true)
            {
                //先移除非法占位
                IllegalMapRemove();

                Tweener anime = this.gameObject.transform.DOLocalRotate(new Vector3(0, 0, z), 0.3f);
                rotationBtn.transform.DOLocalRotate(new Vector3(0, 0, rz), 0.3f);
                if(stablePic != null)
                {
                    stablePic.transform.DOLocalRotate(new Vector3(0, 0, rz), 0.3f);
                }
                anime.OnComplete(() => RotateReset());
            }
            else
            {
                Sequence s = DOTween.Sequence();
                s.Insert(0f, this.gameObject.transform.DOLocalRotate(new Vector3(0, 0, -10f), 0.1f));
                s.Insert(0.1f, this.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f));

                s.OnComplete(() => RotateReset0());
            }
        } 
    }
    void RotateReset()
    {
        basicAction.AutoRoadRecreateForItem(this.gameObject);
        rotationAnime = false;
        BasicAction.gameplayItemAction = false;
        gameMode.gameProcessPause = false;
        //cameraControl.cameraCanMove = true;

        //再加入新占位
        IllegalMapAdd();
        OccupiedAreaListUpdate();
    }
    void RotateReset0()
    {
        //不可旋转
        rotationAnime = false;
        BasicAction.gameplayItemAction = false;
        gameMode.gameProcessPause = false;
        //cameraControl.cameraCanMove = true;
    }

    //非法通行-新坐标加入
    public void IllegalMapAdd()
    {
        for(int i = 0; i < occupiedArea.transform.childCount; i++)
        {
            int xx = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.x);
            int yy = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.y);
            int zz = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.z);
            Vector3Int pp = new Vector3Int(xx,yy,zz);
            //Debug.Log(pp);
            if (gameplayMapping.mapIllegalList.Contains(pp) == false)
            {
                gameplayMapping.mapIllegalList.Add(pp);
            }
        }
    }

    //非法通行-新坐标移除
    public void IllegalMapRemove()
    {
        for (int i = 0; i < occupiedArea.transform.childCount; i++)
        {
            int xx = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.x);
            int yy = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.y);
            int zz = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.z);
            Vector3Int pp = new Vector3Int(xx, yy, zz);
            if (gameplayMapping.mapIllegalList.Contains(pp) == true)
            {
                gameplayMapping.mapIllegalList.Remove(pp);
            }
        }
    }

    //占位更新
    public void OccupiedAreaListUpdate()
    {
        itemOccupiedAreaList0 = new List<Vector3Int>();
        itemOccupiedAreaList = new List<Vector3Int>();

        for (int i = 0; i < occupiedArea.transform.childCount; i++)
        {
            int xx = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.x);
            int yy = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.y);
            int zz = Mathf.RoundToInt(occupiedArea.transform.GetChild(i).gameObject.transform.position.z);
            Vector3Int pp = new Vector3Int(xx, yy, zz);
            if (itemOccupiedAreaList.Contains(pp) == false)
            {
                itemOccupiedAreaList.Add(pp);
            }
            //
            if (itemOccupiedAreaList0.Contains(pp) == false)
            {
                itemOccupiedAreaList0.Add(pp);
            }
        }

        //dark添加
        if(canDark == true)
        {
            for (int i = 0; i < darkSocketContent.transform.childCount; i++)
            {
                GameObject sock = darkSocketContent.transform.GetChild(i).gameObject;
                if (sock.transform.childCount > 1)
                {
                    for (int aa = 0; aa < sock.transform.childCount; aa++)
                    {
                        GameObject gi = sock.transform.GetChild(aa).gameObject;
                        if (gi.name.Contains("GameplayItem_"))
                        {
                            for (int bb = 0; bb < gi.GetComponent<GameplayItem>().occupiedArea.transform.childCount; bb++)
                            {
                                int xx = Mathf.RoundToInt(gi.GetComponent<GameplayItem>().occupiedArea.transform.GetChild(bb).gameObject.transform.position.x);
                                int yy = Mathf.RoundToInt(gi.GetComponent<GameplayItem>().occupiedArea.transform.GetChild(bb).gameObject.transform.position.y);
                                int zz = Mathf.RoundToInt(gi.GetComponent<GameplayItem>().occupiedArea.transform.GetChild(bb).gameObject.transform.position.z);
                                Vector3Int pp = new Vector3Int(xx, yy, zz);
                                if (itemOccupiedAreaList.Contains(pp) == false)
                                {
                                    itemOccupiedAreaList.Add(pp);
                                    //Debug.Log(pp);
                                }
                            }
                        }
                    }
                }
            }
        }


    }

    //101特殊逻辑，gamemode打开监听
    public void GameProcessStart()
    {
        bool seReset = false;
        if(gameMode.gameDynamicProcess == true)
        {
            seReset = false;
        }
        else
        {
            seReset = true;
        }

        gameMode.gameDynamicProcess = false;

        if (itemID == 101)
        {
            GameObject item999 = GameObject.Find("GameplayItem_999(Clone)").gameObject;
            //上游连接了999
            if (specialInputLinkItemList.Contains(item999))
            {
                //下游连接了一圈包含自己
                if(outputLinkItemList.Count > 0)
                {
                    
                    List<GameObject> checkL = new List<GameObject>();
                    checkL.AddRange(outputLinkItemList);

                    for(int ceng = 1; ceng < 100; ceng ++)
                    {
                        //如果包含101则闭环
                        if(checkL.Contains(this.gameObject))
                        {
                            if(seReset == true)
                            {
                                soundEffect.LoopStart();
                            }
                            
                            gameMode.gameDynamicProcess = true;
                            break;
                        }

                        //查看下一级
                        List<GameObject> nextLevelList = new List<GameObject>();
                        for(int i = 0; i < checkL.Count; i ++)
                        {
                            GameObject nextLevelT = checkL[i];
                            if(nextLevelT.GetComponent<GameplayItem>().outputLinkItemList.Count > 0)
                            {
                                //还有下一级
                                nextLevelList.AddRange(nextLevelT.GetComponent<GameplayItem>().outputLinkItemList);
                            }
                        }
                        checkL.Clear();
                        checkL.AddRange(nextLevelList);

                        //如果没有下一级，则停止
                        if(checkL.Count == 0)
                        {
                            break;
                        }
                    }
                    
                }
            }
        }

        //出生点获取
        if(gameMode.playerBornPoint == null && gameMode.gameDynamicProcess == true)
        {
            gameMode.playerStartObject = GameObject.Find("GameplayItem_999(Clone)").gameObject;
            gameMode.playerBornPoint = GameObject.Find("GameplayItem_999(Clone)").gameObject.transform.Find("PlayerBornPoint").gameObject;
        }

    }

    //主路数量计算
    public void MainRoadDistanceCal()
    {
        int roadCount = gameplayMapping.roadPointList.Count;

        int gItemCount = 0;
        for(int i = 0; i < gameplayMapping.mainGameplayItemList.Count; i++)
        {
            gItemCount += gameplayMapping.mainGameplayItemList[i].GetComponent<GameplayItem>().inToOutBlockNum;
        }

        int roudCRemove = GameObject.Find("GameplayItem_999(Clone)").gameObject.GetComponent<GameplayItem>().specialOutputPathPosListList[0].Count;

        gameMode.mainRoadDistance = (roadCount + gItemCount - roudCRemove) * 100;
    }

    //玩法触发
    public void GameplayEventLogic(GameObject playerItem)
    {
        //为玩家填充玩法事件
        if(isMain == true)
        {
            //划定item内路径
            int gotoDark = 0;
            int animeNO = 0;
            for(int i = 0; i < itemInternalPath.transform.childCount; i++)
            {
                GameObject pathIII = itemInternalPath.transform.GetChild(i).gameObject;
                if(pathIII.name.Contains("DarkPath_") == false)  //非dark
                {
                    //普通路点
                    Vector3Int roadPPP = new Vector3Int(Mathf.RoundToInt(pathIII.transform.position.x), Mathf.RoundToInt(pathIII.transform.position.y), Mathf.RoundToInt(pathIII.transform.position.z));
                    playerItem.GetComponent<PlayerItem>().gameplayActionObjectList.Add(this.gameObject);
                    playerItem.GetComponent<PlayerItem>().gameplayActionPosList.Add(roadPPP);  
                    playerItem.GetComponent<PlayerItem>().gameplayActionEventList.Add(false);

                    //是否动画
                    if(pathIII.name.Contains("AnimePath_") == true)
                    {
                        playerItem.GetComponent<PlayerItem>().gameplayActionAnimeList.Add(true);
                        playerItem.GetComponent<PlayerItem>().gameplayActionAnimeNOList.Add(animeNO);
                        animeNO++;
                    }
                    else
                    {
                        playerItem.GetComponent<PlayerItem>().gameplayActionAnimeList.Add(false);
                        playerItem.GetComponent<PlayerItem>().gameplayActionAnimeNOList.Add(animeNO);
                    }


                }
                else
                {
                    //先加进去再看dark路径
                    Vector3Int roadPPP0 = new Vector3Int(Mathf.RoundToInt(pathIII.transform.position.x), Mathf.RoundToInt(pathIII.transform.position.y), Mathf.RoundToInt(pathIII.transform.position.z));
                    playerItem.GetComponent<PlayerItem>().gameplayActionObjectList.Add(this.gameObject);
                    playerItem.GetComponent<PlayerItem>().gameplayActionPosList.Add(roadPPP0);
                    playerItem.GetComponent<PlayerItem>().gameplayActionEventList.Add(false);

                    playerItem.GetComponent<PlayerItem>().gameplayActionAnimeList.Add(false);
                    playerItem.GetComponent<PlayerItem>().gameplayActionAnimeNOList.Add(animeNO);

                    //导向dark路点
                    GameObject soc = darkSocketContent.transform.GetChild(gotoDark).gameObject;

                    if (soc.transform.childCount > 1)  //有dark patterns
                    {
                        //遍历找到dark 
                        for (int aa = 0; aa < soc.transform.childCount; aa++)
                        {
                            GameObject dItem = soc.transform.GetChild(aa).gameObject;
                            //找到dark
                            if (dItem.name.Contains("GameplayItem_"))
                            {
                                if (dItem.GetComponent<GameplayItem>().hasPath == true)  //有路径可触发
                                {
                                    //dark之路
                                    if (dItem.GetComponent<GameplayItem>().darkRoadContent != null)
                                    {
                                        int eventNum = dItem.GetComponent<GameplayItem>().darkRoadEventNum;
                                        //判断距离远近
                                        float dis0 = Vector2.Distance(playerItem.transform.position, dItem.GetComponent<GameplayItem>().darkRoadContent.transform.GetChild(0).gameObject.transform.position);
                                        float dis1 = Vector2.Distance(playerItem.transform.position, dItem.GetComponent<GameplayItem>().darkRoadContent.transform.GetChild(dItem.GetComponent<GameplayItem>().darkRoadContent.transform.childCount - 1).gameObject.transform.position);
                                        bool checkOrder = true;
                                        if (dis0 > dis1)
                                        {
                                            checkOrder = false;
                                        }

                                        for (int bb = 0; bb < dItem.GetComponent<GameplayItem>().darkRoadContent.transform.childCount; bb++)
                                        {
                                            int or = bb;

                                            if (checkOrder == false)
                                            {
                                                or = dItem.GetComponent<GameplayItem>().darkRoadContent.transform.childCount - 1 - bb;
                                            }

                                            GameObject pathPosItem = dItem.GetComponent<GameplayItem>().darkRoadContent.transform.GetChild(or).gameObject;
                                            Vector3Int pItemP = new Vector3Int(Mathf.RoundToInt(pathPosItem.transform.position.x), Mathf.RoundToInt(pathPosItem.transform.position.y), Mathf.RoundToInt(pathPosItem.transform.position.z));

                                            //填充事件
                                            playerItem.GetComponent<PlayerItem>().gameplayActionObjectList.Add(dItem);
                                            playerItem.GetComponent<PlayerItem>().gameplayActionPosList.Add(pItemP);

                                            bool isEvent = false;
                                            if (pathPosItem.name == "ActionPoint" && eventNum > 0)
                                            {
                                                isEvent = true;
                                                eventNum--;
                                            }
                                            playerItem.GetComponent<PlayerItem>().gameplayActionEventList.Add(isEvent);

                                            //dark的动画检测
                                            playerItem.GetComponent<PlayerItem>().gameplayActionAnimeList.Add(false);
                                            playerItem.GetComponent<PlayerItem>().gameplayActionAnimeNOList.Add(animeNO);

                                            //是否具有进入检测（概率进入）
                                            if (bb == 0)
                                            {
                                                if (pathPosItem.name == "EnterCheckPoint")
                                                {
                                                    bool canIn = false;
                                                    //检测是否进入
                                                    float ra = Random.Range(0f, 1f);
                                                    float enterR = gameplayEffect.GameItemEffect(itemID, "triggerRate");
                                                    //422广告的特殊处理
                                                    if (enterR == -1 && itemID == 422)
                                                    {
                                                        enterR = 1 - playerItem.GetComponent<PlayerItem>().finalPayingRate;
                                                        if (enterR < 0)
                                                        {
                                                            enterR = 0;
                                                        }
                                                    }

                                                    if (ra <= enterR)
                                                    {
                                                        canIn = true;
                                                    }

                                                    if (canIn == false)
                                                    {
                                                        //不能进，直接跳到最后一点
                                                        bb = dItem.GetComponent<GameplayItem>().darkRoadContent.transform.childCount - 1 - 1;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //如果这个点没有dark patterns, 视为普通路点
                        Vector3Int roadPPP = new Vector3Int(Mathf.RoundToInt(pathIII.transform.position.x), Mathf.RoundToInt(pathIII.transform.position.y), Mathf.RoundToInt(pathIII.transform.position.z));
                        playerItem.GetComponent<PlayerItem>().gameplayActionObjectList.Add(this.gameObject);
                        playerItem.GetComponent<PlayerItem>().gameplayActionPosList.Add(roadPPP);
                        playerItem.GetComponent<PlayerItem>().gameplayActionEventList.Add(false);

                        playerItem.GetComponent<PlayerItem>().gameplayActionAnimeList.Add(false);
                        playerItem.GetComponent<PlayerItem>().gameplayActionAnimeNOList.Add(animeNO);
                    }

                    gotoDark++;
                }

            }

            //最后是主玩法事件填充
            playerItem.GetComponent<PlayerItem>().gameplayActionObjectList.Add(this.gameObject);
            playerItem.GetComponent<PlayerItem>().gameplayActionPosList.Add(new Vector3Int(0, 0, 0));  //最后一个坐标要忽略
            playerItem.GetComponent<PlayerItem>().gameplayActionEventList.Add(true);

            playerItem.GetComponent<PlayerItem>().gameplayActionAnimeList.Add(false);
            playerItem.GetComponent<PlayerItem>().gameplayActionAnimeNOList.Add(animeNO);
        }
    }
    //触发玩法事件
    public void GameplayEvent(GameObject playerItem)
    {
        playerItem.GetComponent<PlayerItem>().moveAnime = false;
        //
        //Debug.Log(isTriggerEffect + " Event! + " + itemID);

        if(isTriggerEffect == true)  //具有触发效果
        { 
            if(triggerEventType == 0) //数值类触发
            {
                float retentionE = gameplayEffect.GameItemEffect(itemID, "retention");
                float socialBoundE = gameplayEffect.GameItemEffect(itemID, "socialBound");
                float payingRateE = gameplayEffect.GameItemEffect(itemID, "payingRate");
                float payingAmountE = gameplayEffect.GameItemEffect(itemID, "payingAmount");
                float moodE = gameplayEffect.GameItemEffect(itemID, "mood");

                if (retentionE != 0f)
                {
                    List<float> effectL = new List<float>();
                    effectL.Add(itemID);
                    effectL.Add(retentionE);

                    playerItem.GetComponent<PlayerItem>().triggerRetentionEffectList.Add(effectL);
                }

                if (socialBoundE != 0f)
                {
                    List<float> effectL = new List<float>();
                    effectL.Add(itemID);
                    effectL.Add(socialBoundE);

                    playerItem.GetComponent<PlayerItem>().triggerSocialBoundEffectList.Add(effectL);
                }

                if (payingRateE != 0f)
                {
                    List<float> effectL = new List<float>();
                    effectL.Add(itemID);
                    effectL.Add(payingRateE);

                    playerItem.GetComponent<PlayerItem>().triggerPayingRateEffectList.Add(effectL);
                }

                if (payingAmountE != 0f)
                {
                    List<float> effectL = new List<float>();
                    effectL.Add(itemID);
                    effectL.Add(payingAmountE);

                    playerItem.GetComponent<PlayerItem>().triggerPayingAmountEffectList.Add(effectL);
                }

                if (moodE != 0f)
                {
                    //List<float> effectL = new List<float>();
                    //effectL.Add(itemID);
                    //effectL.Add(moodE);

                    PlayerMoodDecrease(playerItem, moodE);
                }

                //Debug.Log("添加trigger数值, 来源" + itemID);
            }
            else if (triggerEventType == 1) //付费类事件
            {
                float payingRateE = gameplayEffect.GameItemEffect(itemID, "payingRate");
                float payingAmountE = gameplayEffect.GameItemEffect(itemID, "payingAmount");

                PlayerInstantPayment(playerItem, itemID, payingRateE, payingAmountE);

                //Debug.Log("付费事件触发, 来源" + itemID);
            }
        }

        //dark动画效果
        if(isMain == false)
        {
            if (this.gameObject.GetComponent<GameplayItemAnime>().animeContent != null)
            {
                this.gameObject.GetComponent<GameplayItemAnime>().AnimePlay(0);
            }
        }
    }

    //具体玩法事件
    //减少心情值
    public void PlayerMoodDecrease(GameObject playerItem, float nnn)
    {
        playerItem.GetComponent<PlayerItem>().satisfactionIndex += nnn;
    }
    //立即触发一个付费检测
    public void PlayerInstantPayment(GameObject playerItem, int itemID, float payR, float payM)  //临时付费额，临时付费率
    {
        playerItem.GetComponent<PlayerItem>().PlayerPayingCheckMethod2(itemID, payR, payM);
    }

    //路径编辑提示器
    void RoadEditAnime()
    {
        normalInputAnime = false;
        normalOutputAnime = false;
        specialInputAnime = false;
        specialOutputAnime = false;

        //路面编辑模式
        if (BasicAction.roadEditMode == true && isMain == true && basicAction.targetOJ != this.gameObject)
        {
            if(basicAction.specialItemLink == true) //特殊链接
            {
                if(basicAction.roadEditDefaultDirection == true)  //output拉出
                {
                    if (basicAction.targetOJ.GetComponent<GameplayItem>().specialOutputLinkTargetList.Contains(itemID))
                    {
                        //可以
                        if(specialInputLinkItemList.Count == 0)
                        {
                            specialInputAnime = true;
                        }  
                    }
                }
                else  //input拉出
                {
                    if (basicAction.targetOJ.GetComponent<GameplayItem>().specialInputLinkTargetList.Contains(itemID))
                    {
                        //可以
                        if(specialOutputLinkItemList.Count == 0)
                        {
                            specialOutputAnime = true;
                        }
                        
                    }
                }
            }
            else
            {
                if (basicAction.roadEditDefaultDirection == true)  //output拉出
                {
                    if (inputLinkItemList.Count == 0)
                    {
                        normalInputAnime = true;
                    } 
                }
                else  //input拉出
                {
                    if (outputLinkItemList.Count == 0)
                    {
                        normalOutputAnime = true;
                    }  
                }
            }

            //动画
            if(normalInputAnime == true && normalInputAnimeP != null)
            {
                normalInputAnimeP.transform.localScale = new Vector3(1, 1, 1);
                float zz = normalInputAnimeP.transform.eulerAngles.z;
                zz -= 3f;
                normalInputAnimeP.transform.eulerAngles = new Vector3(0,0, zz);
            }

            if (normalOutputAnime == true && normalOutputAnimeP != null)
            {
                normalOutputAnimeP.transform.localScale = new Vector3(1, 1, 1);
                float zz = normalOutputAnimeP.transform.eulerAngles.z;
                zz -= 3f;
                normalOutputAnimeP.transform.eulerAngles = new Vector3(0, 0, zz);
            }

            if (specialInputAnime == true && specialInputAnimeP != null)
            {
                specialInputAnimeP.transform.localScale = new Vector3(1, 1, 1);
                float zz = specialInputAnimeP.transform.eulerAngles.z;
                zz -= 3f;
                specialInputAnimeP.transform.eulerAngles = new Vector3(0, 0, zz);
            }

            if (specialOutputAnime == true && specialOutputAnimeP != null)
            {
                specialOutputAnimeP.transform.localScale = new Vector3(1, 1, 1);
                float zz = specialOutputAnimeP.transform.eulerAngles.z;
                zz -= 3f;
                specialOutputAnimeP.transform.eulerAngles = new Vector3(0, 0, zz);
            }


        }
    }

    //黑暗提示器
    void DarkTipsAnime()
    {
        if (isMain == true)
        {
            if(basicAction.temTargetOJ != null && basicAction.temTargetOJ.GetComponent<GameplayItem>().isMain == false)
            {
                bool darkA = false;
                if (canDarkList.Contains(basicAction.temTargetOJ.GetComponent<GameplayItem>().itemID))
                {
                    //合法
                    //是否有空位
                    for(int i = 0; i < darkSocketContent.transform.childCount; i++)
                    {
                        GameObject sock = darkSocketContent.transform.GetChild(i).gameObject;
                        if(sock.transform.childCount <= 1)
                        {
                            //最终合法
                            darkA = true;
                            break;
                        }
                    }
                }

                if(darkA == true)
                {
                    if (darkTipsTips.transform.localScale.x > 1f)
                    {
                        darkTipsTips.transform.localScale = new Vector3(1f, 1f, 1f);
                        darkTipsAnimeDirection = -1;
                    }
                    else if (darkTipsTips.transform.localScale.x < 0.9f)
                    {
                        darkTipsTips.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                        darkTipsAnimeDirection = 1;
                    }
                    float dc = darkTipsAnimeDirection * 0.005f;
                    float dcc = darkTipsTips.transform.localScale.x + dc;

                    darkTipsTips.transform.localScale = new Vector3(dcc, dcc, dcc);
                }
                else
                {
                    //非法
                    darkTipsNotMe.transform.localScale = new Vector3(1, 1, 1);

                    if (this.gameObject.GetComponent<GameplayItemAnime>().animeContent != null)
                    {
                        for (int i = 0; i < this.gameObject.GetComponent<GameplayItemAnime>().animeContent.transform.childCount; i++)
                        {
                            GameObject animeP = this.gameObject.GetComponent<GameplayItemAnime>().animeContent.transform.GetChild(i).gameObject;
                            animeP.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 160f / 255f);
                        }
                    }
                }
            }
        }
        
    }

    //路径未连接时的显示
    void PathUnlinkShow()
    {
        if(inputBlockPath != null)
        {
            if(isValidItem == true)
            {
                if (inputLinkItemList.Count == 0)
                {
                    if (BasicAction.roadEditMode == true && (basicAction.targetOJ == this.gameObject || basicAction.otherTargetOJ == this.gameObject))
                    {
                        inputBlockPath.transform.localScale = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        inputBlockPath.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else if (inputLinkItemList.Count != 0)
                {
                    inputBlockPath.transform.localScale = new Vector3(0, 0, 0);
                }
            }
            else if(isValidItem == false)
            {
                inputBlockPath.transform.localScale = new Vector3(0, 0, 0);
            }   
        }

        if (outputBlockPath != null)
        {
            if (isValidItem == true)
            {
                if (outputLinkItemList.Count == 0)
                {
                    if (BasicAction.roadEditMode == true && (basicAction.targetOJ == this.gameObject || basicAction.otherTargetOJ == this.gameObject))
                    {
                        outputBlockPath.transform.localScale = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        outputBlockPath.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else if (outputLinkItemList.Count != 0)
                {
                    outputBlockPath.transform.localScale = new Vector3(0, 0, 0);
                }
            }
            else if (isValidItem == false)
            {
                outputBlockPath.transform.localScale = new Vector3(0, 0, 0);
            }
        }

        if (specialInputBlockPath != null)
        {
            if (isValidItem == true)
            {
                if (specialInputLinkItemList.Count == 0)
                {
                    if (BasicAction.roadEditMode == true && (basicAction.targetOJ == this.gameObject || basicAction.otherTargetOJ == this.gameObject))
                    {
                        specialInputBlockPath.transform.localScale = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        specialInputBlockPath.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else if (specialInputLinkItemList.Count != 0)
                {
                    specialInputBlockPath.transform.localScale = new Vector3(0, 0, 0);
                }
            }
            else if (isValidItem == false)
            {
                specialInputBlockPath.transform.localScale = new Vector3(0, 0, 0);
            }
        }

        if (specialOutputBlockPath != null)
        {
            if (isValidItem == true)
            {
                if (specialOutputLinkItemList.Count == 0)
                {
                    if (BasicAction.roadEditMode == true && (basicAction.targetOJ == this.gameObject || basicAction.otherTargetOJ == this.gameObject))
                    {
                        specialOutputBlockPath.transform.localScale = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        specialOutputBlockPath.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else if (specialOutputLinkItemList.Count != 0)
                {
                    specialOutputBlockPath.transform.localScale = new Vector3(0, 0, 0);
                }
            }
            else if (isValidItem == false)
            {
                specialOutputBlockPath.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }


}
