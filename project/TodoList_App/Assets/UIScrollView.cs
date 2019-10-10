using System;
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
    /// 当前的子Item数量
    /// </summary>
    private int currentItemCount;

    /// <summary>
    /// 拖拽前，拖拽UI的两极位置（UIRect如果低于极最小值，则回归最小值，如果高于极最大值，则回归极最大值）
    /// </summary>
    public Vector2 originUIRectPos;

    /// <summary>
    /// Item内容所占的UI尺寸
    /// </summary>
    private Vector2 contentSize;

    private Action rebackFinish;

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
        //初始计算Item内容的尺寸
        CalculateOriginUIRectPos();
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

        if ((Input.GetMouseButtonUp(0) && Input.touchCount <= 0) || !CheckTouchOverTheUIRect(touchPos))
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
            //结束滑动时，按Item内容进行回归正常位置
            CheckAndSetOriginPosForUIRect(rebackFinish);
        }
    }


    #region 内部计算工具
    /// <summary>
    /// 计算回归位置（极值）
    /// </summary>
    private void CalculateOriginUIRectPos()
    {
        currentItemCount = transform.childCount - 1;

        if (scrollMoveType == ScrollMoveType.vertical)
        {
            originUIRectPos.x = originUIRectPos.y = UIRect.anchoredPosition.y;
            if (currentItemCount > 0)
            {
                contentSize.x = UIRect.GetChild(0).GetComponent<RectTransform>().rect.width;
                for (int i = 1; i < currentItemCount; i++)
                {
                    contentSize.y += UIRect.GetChild(i).GetComponent<RectTransform>().rect.height;
                }
            }
            if (contentSize.y > UIRect.rect.height)
            {
                originUIRectPos.x = UIRect.anchoredPosition.y + contentSize.y - UIRect.rect.height;
            }
        }
        else if (scrollMoveType == ScrollMoveType.horizontal)
        {

        }
    }

    /// <summary>
    /// 根据拖拽方式修改拖拽UI的位置（计算相对差值）
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
    /// 不计算相对差值
    /// </summary>
    /// <param name="originPosValue"></param>
    private void ChangeUIRectPos(float originPosValue,Action onFinish=null)
    {
        if (scrollMoveType == ScrollMoveType.vertical)
        {
            UIRect.SetAnchoredPosition(y: originPosValue);
        }
        else if (scrollMoveType == ScrollMoveType.horizontal)
        {
            UIRect.SetAnchoredPosition(x: originPosValue);
        }
        onFinish?.Invoke();
    }

    /// <summary>
    /// 设置位置回归
    /// </summary>
    private void CheckAndSetOriginPosForUIRect(Action rebackMethod)
    {
        if (scrollMoveType == ScrollMoveType.vertical)
        {
            if (UIRect.anchoredPosition.y > originUIRectPos.x)
            {
                ChangeUIRectPos(originUIRectPos.x);
            }
            else if (UIRect.anchoredPosition.y < originUIRectPos.y)
            {
                ChangeUIRectPos(originUIRectPos.y, rebackMethod);
            }
        }
        else if (scrollMoveType == ScrollMoveType.horizontal)
        {

        }
       
    }

    /// <summary>
    /// 检查拖拽位置是否在UI内，screenRect 是有效滑动区域，也是遮罩显示区域
    /// </summary>
    /// <param name="touchPos"></param>
    /// <returns></returns>
    private bool CheckTouchOverTheUIRect(Vector2 touchPos)
    {
        if (ScreenRect != null)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(ScreenRect, touchPos);
        }
        else
        {
            return false;
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

    #endregion

    #region 外部增加Item的调用方法
    /// <summary>
    /// 外部通知ScrollView,有Item增加
    /// </summary>
    public void AddItem(Rect itemRect)
    {
        if (scrollMoveType == ScrollMoveType.vertical)
        {
            contentSize.y += itemRect.height;

            if (contentSize.y > UIRect.rect.height)
            {
                originUIRectPos.x = UIRect.anchoredPosition.y + contentSize.y - UIRect.rect.height;
            }
        }
        else if (scrollMoveType == ScrollMoveType.horizontal)
        {

        }

    }

    /// <summary>
    /// 外部调用来注册 滑动结束后的 回调
    /// </summary>
    /// <param name="backCall">回调方法</param>
    public void AddtheRebackFinishEvent(Action backCall)
    {
        if (rebackFinish == null)
        {
            rebackFinish = backCall;
        }
    }
    #endregion 
}
