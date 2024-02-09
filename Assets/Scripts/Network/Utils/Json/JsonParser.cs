
using UnityEngine;
public class JsonParser
{
    public string Serialize(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public T Deserialize<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
