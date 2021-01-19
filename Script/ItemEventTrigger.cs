using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ItemEventTrigger : GlobalEventTrigger
{
    ItemInfo m_info;
    ItemInfo m_Object;

    Inven m_Inven;
    Crafting m_Crafting;

    // Start is called before the first frame update
    void Awake()
    {
        EventTrigger_Init();

        m_Inven = GameObject.Find("Inven").GetComponent<Inven>();
        m_Crafting = GameObject.Find("Crafting").GetComponent<Crafting>();
    }

    private void Start()
    {
        m_info = GetComponent<ItemInfo>();
        m_Object = null;
    }

    protected override void OnPointerEnter(PointerEventData data)
    {
        if (!data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().m_none)
        {
            m_info.Info_View();
        }
    }

    protected override void OnPointerExit(PointerEventData data)
    {
        m_info.Close_View();
    }


    protected override void OnPointerClick(PointerEventData data)
    {
        if (data.pointerId != -1)
        {
            if (m_info.tag == "Crafting")
            {
                if (!m_info.m_none)
                {
                    m_info.NoneItem();
                    m_Crafting.Result_Crafting();
                }
            }

            if (m_info.tag == "Result")
            {
                if (!m_info.m_none)
                {
                    int count = m_Crafting.Result_Count();
                    m_info.m_info.m_Count = count;
                    m_info.UpdateItem();
                }
            }
        }
        else
        {
            if (m_info.tag == "Result")
            {
                if (!m_info.m_none)
                {
                    int count = m_Crafting.Result_Count();
                    m_info.m_info.m_Count = m_info.m_info.m_Count + count;
                    m_info.UpdateItem();
                }
            }
        }
    }

    protected override void OnPointerDragBegin(PointerEventData data)
    {
        if (!m_info.m_none)
        {
            m_Object = GameObject.Instantiate(m_info) as ItemInfo;
            m_Object.transform.SetParent(GameObject.Find("TempObject").transform, false);

            m_Object.gameObject.tag = "Temp";
            m_Object.GetComponent<Image>().raycastTarget = false;
            m_Object.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);

            if (m_Object.transform.childCount >= 1)
                Destroy(m_Object.transform.GetChild(0).gameObject);
        }
    }

    protected override void OnPointerDrag(PointerEventData data)
    {
        if (m_Object != null)
        {
            Vector2 mousepos = Input.mousePosition;
            m_Object.transform.position = mousepos;
        }
    }

    protected override void OnPointerDragEnd(PointerEventData data)
    {
        if (m_Object != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Temp"));
            m_Object = null;
        }
    }

    protected override void OnPointerUp(PointerEventData data)
    {
        if (m_info.m_info != null)
        {
            if (data.pointerCurrentRaycast.gameObject.tag == "Inven")
            {
                if (data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().m_none)
                {
                    if (m_info.tag == "Inven")
                    {
                        data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().UpdateItem(m_info.m_info);
                        m_info.NoneItem();
                    }
                    else if (m_info.tag == "Result")
                    {
                        if (m_Crafting.Cost_Check(m_info.m_info.m_Count / m_Crafting.Result_Count()))
                        {
                            Item item = new Item(m_info.m_info);

                            data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().UpdateItem(item);

                            m_Crafting.Cost_Crafting(item.m_Count / m_Crafting.Result_Count());
                            m_Crafting.Result_Crafting();
                        }
                    }
                    else if (m_info.tag == "Crafting")
                    {
                        m_info.NoneItem();
                    }
                }

                else
                {
                    if (m_info.tag == "Inven")
                    {
                        string name = data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().m_info.m_Name;
                        int count = data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().m_info.m_Count;

                        if (name == m_info.m_info.m_Name && m_info != data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>())
                        {
                            Item item = new Item(m_info.m_info);

                            item.m_Count += count;

                            data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().UpdateItem(item);
                            m_info.NoneItem();
                        }

                        else 
                        {
                            Item _info = m_info.m_info;

                            m_info.UpdateItem(data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().m_info);
                            data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().UpdateItem(_info);
                        }

                    }
                    else if (m_info.tag == "Result")
                    {
                        string name = data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().m_info.m_Name;
                        int count = data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().m_info.m_Count;

                        if (name == m_info.m_info.m_Name)
                        {
                            if (m_Crafting.Cost_Check(m_info.m_info.m_Count / m_Crafting.Result_Count()))
                            {
                                Item item = new Item(m_info.m_info);

                                m_Crafting.Cost_Crafting(item.m_Count / m_Crafting.Result_Count());

                                item.m_Count += count;

                                data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().UpdateItem(item);

                                m_Crafting.Result_Crafting();
                            }
                        }
                    }
                    else if (m_info.tag == "Crafting")
                    {
                        m_info.NoneItem();
                    }
                }
            }

            else if (data.pointerCurrentRaycast.gameObject.tag == "Crafting")
            {
                if (data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().m_none)
                {
                    if (m_info.tag == "Inven")
                        data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().UpdateItem(m_info.m_info);
                    else if (m_info.tag == "Crafting")
                    {
                        data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().UpdateItem(m_info.m_info);
                        m_info.NoneItem();
                    }
                }

                else
                {
                    if (m_info.tag == "Crafting")
                    {
                        Item _info = m_info.m_info;

                        m_info.UpdateItem(data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().m_info);
                        data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().UpdateItem(_info);
                    }
                    else if (m_info.tag == "Inven")
                    {
                        Item _info = m_info.m_info;

                        data.pointerCurrentRaycast.gameObject.GetComponent<ItemInfo>().UpdateItem(_info);
                    }
                }

                m_Crafting.Result_Crafting();
            }

            else if (data.pointerCurrentRaycast.gameObject.tag == "Result")
            {
                if (m_info.tag == "Crafting")
                {
                    m_info.NoneItem();
                }
            }
        }
    }
}
