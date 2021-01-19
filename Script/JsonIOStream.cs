using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UIElements;
using System.Text;

public static class JsonHelper
{
    class Wrapper<T>
    {
        public T[] items;
    }

    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper);
    }
}

public class JsonIOStream
{ 
    public static T[] Json_Load<T>()
    {
        FileStream file = new FileStream(Application.dataPath + "/ItemInfo.json", FileMode.Open);
        if (!File.Exists(Application.dataPath + "/ItemInfo.json"))
            return null;

        byte[] data = new byte[file.Length];
        file.Read(data, 0, data.Length);      
        file.Close();

        string json = Encoding.UTF8.GetString(data);
        return JsonHelper.FromJson<T>(json);
    }

    public static void Json_Save(string jsondata)
    {
        FileStream file = new FileStream(Application.dataPath + "/ItemInfo.json", FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsondata);
        file.Write(data, 0, data.Length);
        file.Close();
    }
}
