using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NestedScrollRect : ScrollRect
{
    private bool routeToParent = false;

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (horizontal && Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            routeToParent = false;
        }
        else if (!horizontal && Mathf.Abs(eventData.delta.y) > Mathf.Abs(eventData.delta.x))
        {
            routeToParent = true;
            // 通知父ScrollRect处理拖拽事件
            DoParentBeginDrag(eventData);
        }
        else
        {
            routeToParent = false;
        }

        if (!routeToParent)
        {
            base.OnBeginDrag(eventData);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (routeToParent)
        {
            // 将拖拽事件传递给父ScrollRect
            DoParentDrag(eventData);
        }
        else
        {
            base.OnDrag(eventData);
        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (routeToParent)
        {
            // 将结束拖拽事件传递给父ScrollRect
            DoParentEndDrag(eventData);
        }
        else
        {
            base.OnEndDrag(eventData);
        }

        routeToParent = false;
    }

    private void DoParentBeginDrag(PointerEventData eventData)
    {
        if (transform.parent != null)
        {
            Transform parent = transform.parent;
            while (parent != null)
            {
                if (parent.GetComponent<ScrollRect>() != null)
                {
                    parent.GetComponent<ScrollRect>().OnBeginDrag(eventData);
                    //Debug.Log(parent.parent.gameObject.name);
                    break;
                }
                parent = parent.parent;
            }
        }
    }

    private void DoParentDrag(PointerEventData eventData)
    {
        if (transform.parent != null)
        {
            Transform parent = transform.parent;
            while (parent != null)
            {
                if (parent.GetComponent<ScrollRect>() != null)
                {
                    parent.GetComponent<ScrollRect>().OnDrag(eventData);
                    //Debug.Log(parent.parent.gameObject.name);
                    break;
                }
                parent = parent.parent;
            }
        }
    }

    private void DoParentEndDrag(PointerEventData eventData)
    {
        if (transform.parent != null)
        {
            Transform parent = transform.parent;
            while (parent != null)
            {
                if (parent.GetComponent<ScrollRect>() != null)
                {
                    parent.GetComponent<ScrollRect>().OnEndDrag(eventData);
                    //Debug.Log(parent.parent.gameObject.name);
                    break;
                }
                parent = parent.parent;
            }
        }
    }
}
