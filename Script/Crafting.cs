using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Crafting : MonoBehaviour
{
    [HideInInspector]
    public ItemInfo[] m_ItemArray;

    [HideInInspector]
    public ItemInfo m_Result;

    [HideInInspector]
    public Inven m_Inven;

    [HideInInspector]
    public CraftingSetting m_CraftList;

    // Start is called before the first frame update
    void Awake()
    {
        m_CraftList = GetComponent<CraftingSetting>();
    }

    public void Initialized()
    {
        for (int i = 0; i < m_ItemArray.Length; i++)
        {
            m_ItemArray[i].NoneItem();
            Result_Crafting();
        }
    }

    int Cheak_Crafting()
    {
        int index = 0;

        for (int i = 0; i < m_ItemArray.Length; i++)
        {
            if (!m_ItemArray[i].m_none)
            {
                index++;
            }
        }

        return index - 1;
    }

    bool Compare_ItemArray(Item[] a, Item[] b, int lenght)
    {
        for (int i = 0; i < lenght; i++)
        {
            if (a[i] != null && b[i] != null)
            {
                if (a[i].m_Name != b[i].m_Name) return false;
            }

            if (a[i] == null && b[i] != null || a[i] != null && b[i] == null)
                return false;
        }
        return true;
    }

    public void Result_Crafting()
    {
        int index = Cheak_Crafting();

        if (index <= 0)
        {
            m_Result.NoneItem();
            return;
        }

        Item[] data = new Item[9];
        string code = "";

        for (int i = 0; i < m_ItemArray.Length; i++)
        {
            if (m_ItemArray[i].m_none)
            {
                data[i] = null;
            }
            else
            {
                data[i] = m_ItemArray[i].m_info;
                code += m_ItemArray[i].m_info.m_Code.ToString();
            }
        }

        if (!m_CraftList.m_CraftingMap[index].m_Data.ContainsKey(code)) return;

        if (Compare_ItemArray(m_CraftList.m_CraftingMap[index].m_Data[code].m_Array, data, 9))
        {
            Item result = new Item(m_Inven.m_list.m_ItemMap[m_CraftList.m_CraftingMap[index].m_Data[code].name]);
            result.m_Count = m_CraftList.m_CraftingMap[index].m_Data[code].count;
            m_Result.UpdateItem(result);
            return;
        }

        m_Result.NoneItem();
        return;
    }

    public int Result_Count()
    {
        int index = Cheak_Crafting();

        if (index <= 0) return 0;

        Item[] data = new Item[9];
        string code = "";

        for (int i = 0; i < m_ItemArray.Length; i++)
        {
            if (m_ItemArray[i].m_none) data[i] = null;
            else
            {
                data[i] = m_ItemArray[i].m_info;
                code += m_ItemArray[i].m_info.m_Code.ToString();
            }
        }

        if (Compare_ItemArray(m_CraftList.m_CraftingMap[index].m_Data[code].m_Array, data, 9))
        {
            return m_CraftList.m_CraftingMap[index].m_Data[code].count;
        }

        return 0;
    }

    public bool Cost_Check(int count)
    {
        Dictionary<string, Item> temp = new Dictionary<string, Item>();
        
        for (int i = 0; i < m_Inven.m_list.m_ItemList.Count; i++)
        {
            Item item = new Item(m_Inven.m_list.m_ItemList[i]);
            item.m_Count = 0;
            temp.Add(m_Inven.m_list.m_ItemList[i].m_Name, item); 
        }

        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < m_ItemArray.Length; j++)
            {
                if (!m_ItemArray[j].m_none)
                {
                    temp[m_ItemArray[j].m_info.m_Name].m_Count++;

                    if (temp[m_ItemArray[j].m_info.m_Name].m_Count > m_Inven.Cost_SlotInfo(m_ItemArray[j].m_info))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public void Cost_Crafting(int count)
    {
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < m_ItemArray.Length; j++)
            {
                if (!m_ItemArray[j].m_none)
                {
                    m_Inven.Cost_UpdateSlotInfo(m_ItemArray[j].m_info);
                }
            }
        }
    }
}
