using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    public Item m_info;

    Image m_image;
    Sprite m_Noneimage;
    TextMeshProUGUI m_CountText;

    [HideInInspector]
    public bool m_none;

    [HideInInspector]
    public bool m_SettingClear;

    GameObject m_ItemNameView;

    [HideInInspector]
    public bool view;

    // Start is called before the first frame update
    void Start()
    {
        Setting();
    }

    // Update is called once per frame
    void Update()
    {
        if (view)
        {
            var MousePos = Input.mousePosition;

            MousePos.x = MousePos.x + (Screen.width / 12);

            m_ItemNameView.transform.position = new Vector3(MousePos.x, MousePos.y, 0);
        }
    }

    public void Info_View()
    {
        m_ItemNameView.SetActive(true);
        TextMeshProUGUI Text = m_ItemNameView.GetComponentInChildren<TextMeshProUGUI>();
        Text.text = m_info.m_Name;

        view = true;
    }

    public void Close_View()
    {
        m_ItemNameView.SetActive(false);
        view = false;
    }

    public void Setting()
    {
        m_image = GetComponent<Image>();
        m_Noneimage = m_image.sprite;

        if (gameObject.tag != "Crafting")
            m_CountText = GetComponentInChildren<TextMeshProUGUI>();

        NoneItem();
        m_ItemNameView = GameObject.Find("ItemInfo");
        view = false;

        m_SettingClear = true;
    }

    public bool MaxCount(ItemInfo info)
    {
        if (info.m_info.m_Count >= 999)
            return true;
        else
            return false;
    }    

    public void UpdateItem(Item _item)
    {
        m_info = _item;

        m_image = GetComponent<Image>();
        m_image.sprite = m_info.m_Image;

        if (m_CountText != null)
        {
            if (m_info.m_Count >= 2)
                m_CountText.text = m_info.m_Count.ToString();
            else
                m_CountText.text = "";
        }

        m_none = false;
    }    

    public void UpdateItem()
    {
        if (m_CountText != null)
        {
            if (m_info.m_Count >= 2)
                m_CountText.text = m_info.m_Count.ToString();
            else
                m_CountText.text = "";
        }
    }

    public void NoneItem()
    {
        m_info = null;
        m_image.sprite = m_Noneimage;

        if (m_CountText != null)
            m_CountText.text = "";

        m_none = true;
    }    
}
