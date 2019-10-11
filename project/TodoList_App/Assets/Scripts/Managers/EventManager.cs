using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventManager : UnityEngine.EventSystems.EventTrigger
{
    /// <summary>
    /// 事件管理器的点击事件委托
    /// </summary>
    /// <param name="eventData">固定参数</param>
    public delegate void ClickListened(PointerEventData eventData);

    public ClickListened clickListened;
    /// <summary>
    /// 获取监听组件
    /// </summary>
    /// <param name="Listened">被监听的对象</param>
    /// <returns></returns>
    public static EventManager GetEventTriggerManager(GameObject Listened)
    {
        EventManager eventTriggerManager = Listened.GetComponent<EventManager>();
        if (eventTriggerManager == null)
        {
            eventTriggerManager = Listened.AddComponent<EventManager>();
        }
        return eventTriggerManager;

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        clickListened(eventData);
    }

    public void AddClickEvent(ClickListened ClickEvent)
    {
        clickListened += ClickEvent;
    }

    public void RemoveAllRegister()
    {
        clickListened = null;
    }
}

public static class EventSystemRegisterSimplify
{
    public static EventManager RegisterEventTrigger(this GameObject obj)
    {
        return  EventManager.GetEventTriggerManager(obj);
    }
}
