using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BasicAction : MonoBehaviour
{
    public GameObject gameplayItem;

    public GameObject gameplayItemTem;
    private GameObject gameplayObjectLayer;

    //菜单拖动
    private DesignPanel designPenel;
    private GameObject designPenalCheckPoint;
    private bool panelIsDragging = false;
    public GameObject targetPanelItem;
    public GameObject gameplayItemUIItemTem;

    //鼠标拖动
    private Vector3 mosStartPosition;
    private bool mosIsDragging = false;
    private GameObject targetOJ;
    private GameObject ojt;
    private GameObject uijt;

    private int dragPosState = 0;

    private Tilemap tilemap; // Tilemap的引用
    private Vector3Int previousCellPosition = Vector3Int.zero; // 上一帧的瓦片坐标



    // Start is called before the first frame update
    void Start()
    {
        gameplayObjectLayer = GameObject.Find("GameplayObject").gameObject;
        tilemap = GameObject.Find("Tilemap").gameObject.GetComponent<Tilemap>();

        designPenel = GameObject.Find("Canvas").gameObject.GetComponent<DesignPanel>();
        designPenalCheckPoint = designPenel.designPanelPosCheckPoint;

    }

    // Update is called once per frame
    void Update()
    {
        MouseDragCheck();

    }

    void MouseDragCheck()
    {
        // 检测鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.name == "GameplayItem(Clone)")
            {
                mosIsDragging = true;
                mosStartPosition = hit.collider.gameObject.transform.position;
                targetOJ = hit.collider.gameObject;
                // 因为是2D，所以忽略Z轴
                mosStartPosition.z = 0;
                //
                ojt = Instantiate(gameplayItemTem) as GameObject;
                ojt.transform.SetParent(gameplayObjectLayer.transform);
                ojt.transform.position = new Vector3(0, 0, 0);
                //如果窗口打开，则创建UI实体
                if(designPenel.designItemBtnState == true)
                {
                    uijt = Instantiate(gameplayItemUIItemTem) as GameObject;
                    uijt.transform.SetParent(designPenel.designPanel.transform);
                    uijt.transform.position = Input.mousePosition;
                }
            }
        }

        //菜单拖拽
        if(designPenel.designItemBtnState == true && designPenel.designItemDragState == true && ojt == null)
        {
            panelIsDragging = true;
            ojt = Instantiate(gameplayItemTem) as GameObject;
            ojt.transform.SetParent(gameplayObjectLayer.transform);
            ojt.transform.position = new Vector3(0, 0, 0);
            ojt.transform.localScale = new Vector3(1, 1, 1);
        }

        // 拖动物体
        if (ojt != null)
        {
            if(mosIsDragging == true || panelIsDragging == true)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // 因为是2D，所以忽略Z轴
                mousePosition.z = 0;
                mousePosition.x += 0.5f;
                mousePosition.y += 0.5f;
                Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
                // 如果格子位置发生变化
                if (cellPosition != previousCellPosition)
                {
                    previousCellPosition = cellPosition;
                    //Debug.Log("当前格子坐标: " + cellPosition);
                }
                ojt.transform.position = cellPosition;

                //
                if(uijt != null)
                {
                    uijt.transform.position = Input.mousePosition;
                }

                //鼠标位置&界面位置监听
                if (Input.mousePosition.x >= designPenalCheckPoint.transform.position.x && designPenel.designItemBtnState == true)
                {
                    ojt.transform.localScale = new Vector3(0, 0, 0);
                    dragPosState = 1;
                    if (uijt != null)
                    {
                        uijt.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else
                {
                    ojt.transform.localScale = new Vector3(1, 1, 1);
                    dragPosState = 0;
                    if (uijt != null)
                    {
                        uijt.transform.localScale = new Vector3(0, 0, 0);
                    }
                }
            } 
        }
        
        // 释放鼠标左键
        if (Input.GetMouseButtonUp(0) && mosIsDragging && ojt != null)
        {
            //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mosIsDragging = false;
            if (ojt != null)
            {
                Destroy(ojt);
            }
            if (uijt != null)
            {
                Destroy(uijt);
            }
            //mousePosition.z = 0;
            if (dragPosState == 0)
            {
                //重新放置
                targetOJ.transform.position = previousCellPosition;
            }
            else if(dragPosState == 1)
            {
                //回收
                if(targetOJ != null)
                {
                    Destroy(targetOJ);
                }
            }   
        }

        //菜单拖拽取消或结束
        if (designPenel.designItemBtnState == true && designPenel.designItemDragState == false && ojt != null && mosIsDragging == false)
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
            }
        }
    }

    //实例化玩法
    public void InsNewGameplayItem()
    {
        if (dragPosState == 0)
        {
            //创建
            GameObject oj = Instantiate(gameplayItem) as GameObject;
            oj.transform.SetParent(gameplayObjectLayer.transform);
            oj.transform.position = previousCellPosition;
        }else if(dragPosState == 1)
        {
            //无效
        }
    }
}
