using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public string m_Name;
    public Sprite m_Image;
    public string m_ImageName;
    public int m_Count;
    public int m_Durability;

    public string m_Tag;
    public int m_Code;

    public Item()
    {

    }

    public Item(string name, Sprite image, int count, int durability, string tag, int code)     // 이름, 이미지, 개수, 내구도, 태그
    {
        m_Name = name;
        m_Image = image;
        m_ImageName = m_Image.name;
        m_Count = count;
        m_Durability = durability;
        m_Tag = tag;
        m_Code = code;
    }

    public Item(Item _item)
    {
        m_Name = _item.m_Name;
        m_Image = _item.m_Image;
        m_ImageName = m_Image.name;
        m_Count = _item.m_Count;
        m_Durability = _item.m_Durability;
        m_Tag = _item.m_Tag;
        m_Code = _item.m_Code;
    }

    public virtual void Show()
    {
        Debug.Log(m_Name);
    }
}


// -----------------------
// 이후에 확장성을 위한 클래스 준비
// 이 과제에서는 쓰지 않음
// -----------------------


public class Tool<T> : Item
{
    public T m_Ablilty;

    public Tool() {}

    public Tool(string name, Sprite image, int count, int durability, T ability)
    {
        m_Name = name;
        m_Image = image;
        m_Count = count;
        m_Durability = durability;

        m_Ablilty = ability;

        m_Tag = "Tool";
    }

}

public class Wenpon : Item
{
    public int m_Attack;

    public Wenpon() { }

    public Wenpon(string name, Sprite image, int count, int attack, int durability)
    {
        m_Name = name;
        m_Image = image;
        m_Count = count;
        m_Attack = attack;
        m_Durability = durability;

        m_Tag = "Wenpon";
    }

    public override void Show()
    {
        Debug.Log(m_Name + " / " + m_Attack + " / " + m_Tag);
    }
}

public class Armor : Item
{
    public int m_Defense;

    public Armor() { }

    public Armor(string name, Sprite image, int count, int def, int dur)
    {
        m_Name = name;
        m_Image = image;
        m_Count = count;
        m_Defense = def;
        m_Durability = dur;

        m_Tag = "Armor";
    }

    public override void Show()
    {
        Debug.Log(m_Name + " / " + m_Defense + " / " + m_Tag);
    }
}

public class Shoes : Item
{
    public int m_Defense;
    public int m_Speed;

    public Shoes() { }

    public Shoes(string name, Sprite image, int count, int def, int spe, int dur)
    {
        m_Name = name;
        m_Image = image;
        m_Count = count;
        m_Defense = def;
        m_Speed = spe;
        m_Durability = dur;

        m_Tag = "Shoes";
    }

    public override void Show()
    {
        Debug.Log(m_Name + " / " + m_Defense + " / " + m_Speed + " / " + m_Tag);
    }
}