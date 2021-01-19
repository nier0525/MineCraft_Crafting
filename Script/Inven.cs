using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public class JsonPack
{
    public Item item;
    public bool none;

    public JsonPack() { }
}

public class Inven : MonoBehaviour
{
    [HideInInspector]
    public ItemSetting m_list;

    [HideInInspector]
    public ItemInfo[] m_Slot;

    [HideInInspector]
    public List<JsonPack> m_Json = new List<JsonPack>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LateStart()
    {
        m_list = GetComponent<ItemSetting>();
        m_Slot = GetComponentsInChildren<ItemInfo>();

        yield return new WaitForSeconds(0.01f);

        for (int i = 0; i < m_Slot.Length; i++)
        {
            if (m_Slot[i] == null || !m_Slot[i].m_SettingClear)
            {
                print("Inven in " + m_Slot[i].name + " Not Setting");
            }
        }

        DataLoad();
        yield return null;
    }

    public void Initialized()
    {
        int index = 0;

        m_Slot[index].UpdateItem(m_list.m_ItemMap["조약돌"]);
        m_Slot[index].m_info.m_Count = 100;
        m_Slot[index++].UpdateItem();

        m_Slot[index].UpdateItem(m_list.m_ItemMap["참나무 판자"]);
        m_Slot[index].m_info.m_Count = 100;
        m_Slot[index++].UpdateItem();

        m_Slot[index].UpdateItem(m_list.m_ItemMap["막대기"]);
        m_Slot[index].m_info.m_Count = 100;
        m_Slot[index++].UpdateItem();

        m_Slot[index].UpdateItem(m_list.m_ItemMap["실"]);
        m_Slot[index].m_info.m_Count = 100;
        m_Slot[index++].UpdateItem();

        m_Slot[index].UpdateItem(m_list.m_ItemMap["당근"]);
        m_Slot[index].m_info.m_Count = 100;
        m_Slot[index++].UpdateItem();

        m_Slot[index].UpdateItem(m_list.m_ItemMap["종이"]);
        m_Slot[index].m_info.m_Count = 100;
        m_Slot[index++].UpdateItem();

        m_Slot[index].UpdateItem(m_list.m_ItemMap["레드스톤가루"]);
        m_Slot[index].m_info.m_Count = 100;
        m_Slot[index++].UpdateItem();

        m_Slot[index].UpdateItem(m_list.m_ItemMap["철괴"]);
        m_Slot[index].m_info.m_Count = 100;
        m_Slot[index++].UpdateItem();

        m_Slot[index].UpdateItem(m_list.m_ItemMap["금괴"]);
        m_Slot[index].m_info.m_Count = 100;
        m_Slot[index++].UpdateItem();

        m_Slot[index].UpdateItem(m_list.m_ItemMap["다이아몬드"]);
        m_Slot[index].m_info.m_Count = 100;
        m_Slot[index++].UpdateItem();

        for (var i = index; i < m_Slot.Length; i++)
            m_Slot[i].NoneItem();

    }

    void DataLoad()
    {
        var json = JsonIOStream.Json_Load<JsonPack>();
        for (int i = 0; i < json.Length; i++)
        {
            m_Json.Add(json[i]);
        }

        for (int i = 0; i < m_Json.Count; i++)
        {
            if (!m_Json[i].none)
            {
                m_Slot[i].UpdateItem(m_list.m_ItemMap[m_Json[i].item.m_Name]);
                m_Slot[i].m_info.m_Count = m_Json[i].item.m_Count;
                m_Slot[i].UpdateItem();
            }
            else
            {
                m_Slot[i].NoneItem();
            }
        }

        m_Json.Clear();
    }

    private void OnApplicationQuit()
    {
        for (var i = 0; i < m_Slot.Length; i++)
        {
            JsonPack data = new JsonPack();
            data.item = m_Slot[i].m_info;
            data.none = m_Slot[i].m_none;

            m_Json.Add(data);
        }

        string json = JsonHelper.ToJson(m_Json.ToArray());
        JsonIOStream.Json_Save(json);
    }

    public void Cost_UpdateSlotInfo(Item item)
    {
        for (int i = 0; i < m_Slot.Length; i++)
        {
            if (m_Slot[i].m_info == item)
            {
                m_Slot[i].m_info.m_Count--;

                if (m_Slot[i].m_info.m_Count == 0)
                {
                    m_Slot[i].NoneItem();
                }
                else
                {
                    m_Slot[i].UpdateItem();
                }
                
                return;
            }
        }
    }

    public int Cost_SlotInfo(Item item)
    {
        for (int i = 0; i < m_Slot.Length; i++)
        {
            if (m_Slot[i].m_info == item)
            {
                return m_Slot[i].m_info.m_Count;
            }
        }

        return 0;
    }
}
