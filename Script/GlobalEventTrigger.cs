using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class GlobalEventTrigger : MonoBehaviour
{
    protected void EventTrigger_Init()
    {
        EventTrigger m_event = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry_PointEnter = new EventTrigger.Entry();
        entry_PointEnter.eventID = EventTriggerType.PointerEnter;
        entry_PointEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        m_event.triggers.Add(entry_PointEnter);

        EventTrigger.Entry entry_PointExit = new EventTrigger.Entry();
        entry_PointExit.eventID = EventTriggerType.PointerExit;
        entry_PointExit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });
        m_event.triggers.Add(entry_PointExit);

        EventTrigger.Entry entry_PointClick = new EventTrigger.Entry();
        entry_PointClick.eventID = EventTriggerType.PointerClick;
        entry_PointClick.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        m_event.triggers.Add(entry_PointClick);

        EventTrigger.Entry entry_PointUp = new EventTrigger.Entry();
        entry_PointUp.eventID = EventTriggerType.PointerUp;
        entry_PointUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        m_event.triggers.Add(entry_PointUp);

        EventTrigger.Entry entry_PointDragBegin = new EventTrigger.Entry();
        entry_PointDragBegin.eventID = EventTriggerType.BeginDrag;
        entry_PointDragBegin.callback.AddListener((data) => { OnPointerDragBegin((PointerEventData)data); });
        m_event.triggers.Add(entry_PointDragBegin);

        EventTrigger.Entry entry_PointDrag = new EventTrigger.Entry();
        entry_PointDrag.eventID = EventTriggerType.Drag;
        entry_PointDrag.callback.AddListener((data) => { OnPointerDrag((PointerEventData)data); });
        m_event.triggers.Add(entry_PointDrag);

        EventTrigger.Entry entry_PointDragEnd = new EventTrigger.Entry();
        entry_PointDragEnd.eventID = EventTriggerType.EndDrag;
        entry_PointDragEnd.callback.AddListener((data) => { OnPointerDragEnd((PointerEventData)data); });
        m_event.triggers.Add(entry_PointDragEnd);
    }

    protected abstract void OnPointerEnter(PointerEventData data);
    protected abstract void OnPointerExit(PointerEventData data);
    protected abstract void OnPointerClick(PointerEventData data);
    protected abstract void OnPointerDragBegin(PointerEventData data);
    protected abstract void OnPointerDrag(PointerEventData data);
    protected abstract void OnPointerDragEnd(PointerEventData data);
    protected abstract void OnPointerUp(PointerEventData data);

}
