using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameplayItemUIItemDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public GameObject prefabToSpawn; // 场景中要生成的2D物体的预制体
    public Canvas canvas; // UI元素所在的Canvas
    private Vector3 startPosition; // 记录拖拽开始时物体的位置

    private DesignPanel designPenel;
    private BasicAction basicAction;


    // Start is called before the first frame update
    void Start()
    {
        designPenel = GameObject.Find("Canvas").gameObject.GetComponent<DesignPanel>();
        basicAction = GameObject.Find("Main Camera").gameObject.GetComponent<BasicAction>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position; // 记录开始拖拽时的位置
        designPenel.designItemDragState = true;
        basicAction.targetPanelItem = this.gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(designPenel.designItemDragState == true)
        {
            // 更新UI元素的位置
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (designPenel.designItemDragState == true)
        {
            // 在场景中生成2D物体
            basicAction.InsNewGameplayItem();
        }
        //
        designPenel.designItemDragState = false;
        // 将UI元素放回原位或进行其他处理
        transform.position = startPosition;
    }

    public void CancelDrag()
    {
        // 重置拖拽状态
        designPenel.designItemDragState = false;
        // 取消拖拽，物体返回原位置
        transform.position = startPosition;
    }
}
