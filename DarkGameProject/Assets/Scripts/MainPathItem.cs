using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPathItem : MonoBehaviour
{
    public Vector3Int pos;

    public GameObject pathItem;

    public bool isValidItem = true;

    public List<Vector3Int> startEndPosList; //起1终1

    private GameObject pathItem01;
    private GameObject pathItem02;
    private GameObject pathItem03;

    private GameplayMapping gameplayMapping;
    private BasicAction basicAction;

    private GameObject pathLayer;

    public List<GameObject> linkGameItemList;

    public bool defaltDirection = true;

    private void Awake()
    {
        pathItem01 = this.gameObject.transform.Find("PathContent").gameObject.transform.Find("Path_01").gameObject;
        pathItem02 = this.gameObject.transform.Find("PathContent").gameObject.transform.Find("Path_02").gameObject;
        pathItem03 = this.gameObject.transform.Find("PathContent").gameObject.transform.Find("Path_03").gameObject;

        gameplayMapping = GameObject.Find("Main Camera").gameObject.GetComponent<GameplayMapping>();
        basicAction = GameObject.Find("Main Camera").gameObject.GetComponent<BasicAction>();

        pathLayer = GameObject.Find("RoadObject").gameObject;

        linkGameItemList = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDetail(Vector3Int p, Vector3Int startPoint1, Vector3Int endPoint1)
    {
        if(pos != p)
        {
            pos = p;
        }
        //
        if(startEndPosList == null)
        {
            startEndPosList = new List<Vector3Int>();
        }
        else
        {
            startEndPosList.Clear();
        }

        startEndPosList.Add(startPoint1);
        startEndPosList.Add(endPoint1);
    }

    public void RoadPicSet()
    {
        if(startEndPosList.Count == 2)
        {
            pathItem02.transform.localScale = new Vector3(0, 0, 0);
            //
            Vector3Int p1 = startEndPosList[0];
            Vector3Int p2 = startEndPosList[1];
            Vector3Int p0 = new Vector3Int((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, (int)this.gameObject.transform.position.z);


            if (p1.x == p2.x && p1.y != p2.y)
            {
                pathItem01.transform.localScale = new Vector3(1, 1, 1);
                pathItem03.transform.localScale = new Vector3(0, 0, 0);
                pathItem01.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else if(p1.y == p2.y && p1.x != p2.x)
            {
                pathItem01.transform.localScale = new Vector3(1, 1, 1);
                pathItem03.transform.localScale = new Vector3(0, 0, 0);
                pathItem01.transform.localEulerAngles = new Vector3(0, 0, -90);
            }
            else if (p1.y != p2.y && p1.x != p2.x)
            {
                pathItem01.transform.localScale = new Vector3(0, 0, 0);
                pathItem03.transform.localScale = new Vector3(1, 1, 1);
                if((p1.y > p0.y && p2.x > p0.x) || (p2.y > p0.y && p1.x > p0.x))
                {
                    //右上
                    pathItem03.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else if ((p1.y < p0.y && p2.x > p0.x) || (p2.y < p0.y && p1.x > p0.x))
                {
                    //右下
                    pathItem03.transform.localEulerAngles = new Vector3(0, 0, -90);
                }
                else if ((p1.y < p0.y && p2.x < p0.x) || (p2.y < p0.y && p1.x < p0.x))
                {
                    //左下
                    pathItem03.transform.localEulerAngles = new Vector3(0, 0, -180);
                }
                else if ((p1.y > p0.y && p2.x < p0.x) || (p2.y > p0.y && p1.x < p0.x))
                {
                    //左上
                    pathItem03.transform.localEulerAngles = new Vector3(0, 0, -270);
                }
            }
        }
        else if (startEndPosList.Count == 4)
        {
            pathItem01.transform.localScale = new Vector3(0, 0, 0);
            pathItem02.transform.localScale = new Vector3(1, 1, 1);
            pathItem03.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void SetValid(bool bb)
    {
        isValidItem = bb;
    }

    public void TemRoadDestroy(bool beValid)
    {
        if(beValid == false)
        {
            Destroy(this.gameObject);
        }
        else if(beValid == true) //正式道路-转移
        {
            if(this.gameObject.transform.localScale == new Vector3(1, 1, 1))
            {
                GameObject pItem = Instantiate(pathItem) as GameObject;
                pItem.transform.SetParent(pathLayer.transform);
                pItem.transform.position = this.gameObject.transform.position;
                pItem.GetComponent<MainPathItem>().SetDetail(pos, startEndPosList[0], startEndPosList[1]);
                pItem.GetComponent<MainPathItem>().RoadPicSet();
                pItem.GetComponent<MainPathItem>().SetValid(true);
                pItem.GetComponent<MainPathItem>().linkGameItemList = new List<GameObject>();
                pItem.GetComponent<MainPathItem>().linkGameItemList.AddRange(linkGameItemList);
                pItem.GetComponent<MainPathItem>().defaltDirection = true;

                //路点数据
                if (basicAction.specialItemLink == false)
                {
                    int count = linkGameItemList[0].GetComponent<GameplayItem>().outputPathPosListList.Count - 1;
                    if (defaltDirection == true)
                    {
                        linkGameItemList[0].GetComponent<GameplayItem>().outputPathPosListList[count].Add(pos);
                        linkGameItemList[0].GetComponent<GameplayItem>().outputPathItemListList[count].Add(pItem);
                    }
                    else
                    {
                        linkGameItemList[0].GetComponent<GameplayItem>().outputPathPosListList[count].Insert(0, pos);
                        linkGameItemList[0].GetComponent<GameplayItem>().outputPathItemListList[count].Insert(0, pItem);
                    }
                }
                else if (basicAction.specialItemLink == true)
                {
                    int count = linkGameItemList[0].GetComponent<GameplayItem>().specialOutputPathPosListList.Count - 1;
                    if (defaltDirection == true)
                    {
                        linkGameItemList[0].GetComponent<GameplayItem>().specialOutputPathPosListList[count].Add(pos);
                        linkGameItemList[0].GetComponent<GameplayItem>().specialOutputPathItemListList[count].Add(pItem);
                    }
                    else
                    {
                        linkGameItemList[0].GetComponent<GameplayItem>().specialOutputPathPosListList[count].Insert(0, pos);
                        linkGameItemList[0].GetComponent<GameplayItem>().specialOutputPathItemListList[count].Insert(0, pItem);
                    }
                }

                Destroy(this.gameObject);

            }
            else
            {
                Destroy(this.gameObject);
            }
            
        }
    }
}
