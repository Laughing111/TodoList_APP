using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScrollView : MonoBehaviour
{
    /// <summary>
    /// 拖拽标志位
    /// </summary>
    public bool isDrag;

    /// <summary>
    /// 需要拖拽的UI
    /// </summary>
    private RectTransform UIRect;

    /// <summary>
    /// 拖拽UI的背景框（用来规避拖拽超过背景框依旧存在的问题）
    /// </summary>
    public RectTransform ScreenRect;

    /// <summary>
    /// 拖拽UI和点击位置的固定差值
    /// </summary>
    private Vector2 deltaPos;

    /// <summary>
    /// 拖拽前，拖拽UI的初始位置
    /// </summary>
    private Vector2 originUIRectPos;
    public enum ScrollMoveType
    {
        horizontal,
        vertical,
        both
    }
    /// <summary>
    /// Scroll方式
    /// </summary>
    public ScrollMoveType scrollMoveType;

    // Start is called before the first frame update
    void Awake()
    {
        UIRect = transform.GetComponent<RectTransform>();
        originUIRectPos = UIRect.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 touchPos = GetTouchPos();
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            if (CheckTouchOverTheUIRect(touchPos))
            {
                //第一次点击屏幕
                deltaPos = touchPos - UIRect.anchoredPosition;
                isDrag = true;
            }
        }

        if ((Input.GetMouseButtonUp(0) && Input.touchCount <= 0)||! CheckTouchOverTheUIRect(touchPos))
        {
            isDrag = false;
            deltaPos = Vector2.zero;
        }

        if (isDrag)
        {
            ChangeUIRectPos(touchPos);
        }
        else
        {
            UIRect.anchoredPosition = originUIRectPos;
        }
    }

    /// <summary>
    /// 根据拖拽方式修改拖拽UI的位置
    /// </summary>
    /// <param name="touchPos">推拽位置</param>
    private void ChangeUIRectPos(Vector2 touchPos)
    {
        if (scrollMoveType == ScrollMoveType.vertical)
        {
            UIRect.SetAnchoredPosition(y: touchPos.y - deltaPos.y);
        }
        else if (scrollMoveType == ScrollMoveType.horizontal)
        {
            UIRect.SetAnchoredPosition(x: touchPos.x - deltaPos.x);
        }
    }
        
    /// <summary>
    /// 检查拖拽位置是否在UI内
    /// </summary>
    /// <param name="touchPos"></param>
    /// <returns></returns>
    private bool CheckTouchOverTheUIRect(Vector2 touchPos)
    {
        if (ScreenRect != null)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(UIRect, touchPos)&& RectTransformUtility.RectangleContainsScreenPoint(ScreenRect, touchPos);
        }
        else
        {
            return RectTransformUtility.RectangleContainsScreenPoint(UIRect, touchPos);
        }
        
    }

    /// <summary>
    /// 获得拖拽位置
    /// </summary>
    /// <returns></returns>
    private Vector2 GetTouchPos()
    {
#if TouchScreen
        return Input.GetTouch(0).position;
#else
        return Input.mousePosition;
#endif
    }
}
