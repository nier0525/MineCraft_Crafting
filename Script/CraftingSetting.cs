using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CraftingData
{
    public Item[] m_Array;
    public string name;
    public int count;
    public string code;
}

public struct CraftingDictionary
{
    public Dictionary<string, CraftingData> m_Data;
}

public class CraftingSetting : MonoBehaviour
{
    ItemSetting m_Item;

    public CraftingDictionary[] m_CraftingMap = new CraftingDictionary[9];
    public CraftingDictionary m_Crafting;

    string path = "CraftingSettingInfo";

    private void Awake()
    {
        m_Crafting = new CraftingDictionary();
        Setting();
    }

    public void Setting()
    {
        m_Item = GameObject.Find("Inven").GetComponent<ItemSetting>();
        m_Item.Setting();        

        for (int i = 0; i < m_CraftingMap.Length; i++)
        {            
            m_CraftingMap[i].m_Data = new Dictionary<string, CraftingData>();
        }

        List<Dictionary<string, object>> data = CSVReader.Read(path);
        string name;

        for (var i = 0; i < data.Count; i++)
        {
            var item = new CraftingData();
            item.m_Array = new Item[9];

            int index = -1;

            for (int j = 0; j < item.m_Array.Length; j++)
            {
                name = (string)data[i][j.ToString()];

                if (m_Item.m_ItemMap.ContainsKey(name))
                {
                    item.m_Array[j] = m_Item.m_ItemMap[name];
                    item.code += m_Item.m_ItemMap[name].m_Code.ToString();                    
                    ++index;
                }
                else
                {
                    item.m_Array[j] = null;
                }
            }

            item.name = (string)data[i]["이름"];
            item.count = (int)data[i]["개수"];

            m_CraftingMap[index].m_Data.Add(item.code.ToString(), item);
        }
    }
}
