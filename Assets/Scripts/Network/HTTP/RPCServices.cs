using Newtonsoft.Json;
using RPS.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RPCServices : Singleton<RPCServices>
{
    private const string baseUrl = "http://localhost:5166/api/";

    private RPSHTTPEndPoint endpoint = RPSHTTPEndPoint.none;

    #region room
    
    // for later

    #endregion

    #region Game
    public IEnumerator GetIsReady(System.Action<bool> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl + endpoint.ToString()))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string jsonString = www.downloadHandler.text;
                bool dataObject = JsonConvert.DeserializeObject<bool>(jsonString);
                callback?.Invoke(dataObject);
            }
        }
    }

    // Method to send a GET request to retrieve data from the server
    public IEnumerator GetIsPlaying( System.Action<bool> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl + endpoint))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string jsonString = www.downloadHandler.text;
                bool dataObject = JsonConvert.DeserializeObject<bool>(jsonString);
                callback?.Invoke(dataObject);
            }
        }
    }

    public IEnumerator GetEnemySelection(System.Action<RoleType> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl + endpoint))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                string jsonString = www.downloadHandler.text;
                RoleType dataObject = JsonConvert.DeserializeObject<RoleType>(jsonString);
                callback?.Invoke(dataObject);
            }
        }
    }

    // Method to send a POST request to create data on the server
    public IEnumerator SetPlayerGame(string endpoint, RoleType role, System.Action<string> callback)
    {
        string jsonData = JsonConvert.SerializeObject(role);

        using (UnityWebRequest www = UnityWebRequest.Post(baseUrl + endpoint, jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                callback?.Invoke(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator UpdatePlayerSelection(string endpoint, RoleType role, System.Action<string> callback = null)
    {
        string jsonData = JsonConvert.SerializeObject(role);

        using (UnityWebRequest www = UnityWebRequest.Put(baseUrl + endpoint, jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                callback?.Invoke(www.downloadHandler.text);
            }
        }
    }
    #endregion

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}


public enum RPSHTTPEndPoint
{
    none,

}