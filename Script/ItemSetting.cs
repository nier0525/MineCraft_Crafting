using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetting : MonoBehaviour
{
    // 아이템을 바로 찾기 위한 딕셔너리
    public Dictionary<string, Item> m_ItemMap;

    // 아이템을 순회하기 위한 리스트
    public List<Item> m_ItemList;

    string path = "ItemSettingInfo";

    Dictionary<string, Sprite> m_ImageMap;

    public void Setting()
    {
        m_ItemMap = new Dictionary<string, Item>();
        m_ItemList = new List<Item>();

        m_ImageMap = new Dictionary<string, Sprite>();

        Sprite[] resource = Resources.LoadAll<Sprite>("Sprite");

        for (int i = 0; i < resource.Length; i++)
        {
            m_ImageMap.Add(resource[i].name, resource[i]);
        }

        List<Dictionary<string, object>> data = CSVReader.Read(path);

        for (var i = 0; i < data.Count; i++)
        {
            Item item;

            /*                         
            태그를 이용해 캐스팅을 통해서 다양하게 이용 가능 할 듯 하다. 
             if ((string)data[i]["tag"] == "Tool")
                item = new Tool( . . . );
            */

            item = new Item((string)data[i]["name"], m_ImageMap[(string)data[i]["image"]],
                (int)data[i]["count"], (int)data[i]["duraility"], (string)data[i]["tag"], 
                (int)data[i]["code"]);

            m_ItemMap.Add(item.m_Name, item);
            m_ItemList.Add(item);
        }

    }
}
