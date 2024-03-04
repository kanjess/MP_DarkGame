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

    //黑暗模式
    public bool canDark = false;
    public List<int> canDarkList;
    public GameObject darkSocketContent;
    public GameObject darkPlugPoint;
    public GameObject linkDarkSocketPoint;
    private bool darkMove = true;


    private void Awake()
    {
        basicAction = GameObject.Find("Main Camera").gameObject.GetComponent<BasicAction>();
        gameplayMapping = GameObject.Find("Main Camera").gameObject.GetComponent<GameplayMapping>();
        gameMode = GameObject.Find("Main Camera").gameObject.GetComponent<GameMode>();

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
        }
        else if(itemID == 102)
        {
            outputCount = 1;
            inputCount = 1;
            //加入dark
            canDarkList.Add(401);
        }
        else if (itemID == 999)
        {
            outputCount = 1;
            inputCount = 0;
            //
            specialOutputLinkTargetList.Add(101);
            specialOutputCount = 1;
        }
        else if (itemID == 401)
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
            if(rotationBtn != null)
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
                rotationBtn.transform.localScale = new Vector3(1, 1, 1);
                rotationBtn.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
            }
            else
            {
                rotationBtn.transform.localScale = new Vector3(0, 0, 0);
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
                rotationBtn.transform.localScale = new Vector3(1, 1, 1);
                moveBtn.transform.localScale = new Vector3(0, 0, 0);
                rotationBtn.GetComponent<SpriteRenderer>().DOFade(1f, 0f);
            }
            else
            {
                rotationBtn.transform.localScale = new Vector3(0, 0, 0);
                moveBtn.transform.localScale = new Vector3(1, 1, 1);
            }

        }
        else
        {
            rotationBtn.transform.localScale = new Vector3(0, 0, 0);
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
                Tweener anime = this.gameObject.transform.DOLocalRotate(new Vector3(0, 0, z), 0.3f);
                rotationBtn.transform.DOLocalRotate(new Vector3(0, 0, rz), 0.3f);
                if(stablePic != null)
                {
                    stablePic.transform.DOLocalRotate(new Vector3(0, 0, rz), 0.3f);
                }
                anime.OnComplete(() => rotationAnime = false);
                anime.OnKill(() => basicAction.AutoRoadRecreateForItem(this.gameObject));
            }

            //
            BasicAction.gameplayItemAction = false;
        } 
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
        if(itemID == 101)
        {
            GameObject item999 = GameObject.Find("GameplayItem_999(Clone)").gameObject;
            //上游连接了999
            if (specialInputLinkItemList.Contains(item999))
            {
                //下游连接了一圈包含自己
                if(outputLinkItemList.Count > 0)
                {
                    gameMode.gameDynamicProcess = false;
                    List<GameObject> checkL = new List<GameObject>();
                    checkL.AddRange(outputLinkItemList);

                    for(int ceng = 1; ceng < 100; ceng ++)
                    {
                        //如果包含101则闭环
                        if(checkL.Contains(this.gameObject))
                        {
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



}
