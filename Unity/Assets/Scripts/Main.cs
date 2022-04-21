using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using SimpleJSON;
using System.Security.Cryptography;
using System;
using System.Text;

public class Main : MonoBehaviour
{
    public MenuController menuController;
    public Dictionary<string, string> tables = Constants.MAIN_MENU;
    public List<int> years;
    public Dictionary<string, string> categories;

    public EarthController earthController;
    private Module _module = null;
    private bool continueWaiting = true; 
    
    private AuthService _authService;
    private MicroService _microService;
    private CommunicationService _communicationService;
    void Start()
    {
        _module = Module.GetInstance(gameObject.GetComponent<Main>());
        _authService = _module.AuthService();
        _microService = _module.MicroService();
        _communicationService = _module.CommunicationService();
    }

    public void CreateRequestAsync(string url, string requestBodyJsonString, string method, Action<string> callback){
        StartCoroutine(CreateRequest(url, requestBodyJsonString, method, callback));
    }

    public void AuthAsync(string user, string pass, Action<string> callback){
        StartCoroutine(Auth(user, pass, callback));
    }

    public IEnumerator Auth(string user, string pass, Action<string> callback)
    {
        string url = $"{Constants.HOST}/requesttoken";
        string method = "POST";
        string parameter = "{\"graph\": \""+Constants.DB+"\"}";
        
        Debug.Log(url);
        Debug.Log(parameter);
        UnityWebRequest request = UnityWebRequest.Post(url, parameter);

        Debug.Log(user+":"+pass);
        byte[] bytesToEncode = Encoding.ASCII.GetBytes (user+":"+pass);
        string encodedText = Convert.ToBase64String (bytesToEncode);
        request.SetRequestHeader("Authorization", $"Basic {encodedText}");

        request.SetRequestHeader("Content-Type", "application/json");
        byte[] jsonToSend = new UTF8Encoding().GetBytes(parameter);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);

        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log("Error While Sending: " + request.error);
        }
        else
        {
            try {
                callback(request.downloadHandler.text);
            } catch (Exception e) {
                Debug.LogError(method);
            }
        }
    }

    public void GetDataAsync(string url, string token, Action<string> callback){
        StartCoroutine(GetData(url, token, callback));
    }

    public IEnumerator GetData(string url, string token, Action<string> callback)
    {
        string method = "GET";
        UnityWebRequest request = UnityWebRequest.Get(url);

        request.SetRequestHeader("Authorization", $"Bearer {token}");

        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log("Error While Sending: " + request.error);
        }
        else
        {
            try {
                callback(request.downloadHandler.text);
            } catch (Exception e) {
                Debug.LogError(url);
                Debug.LogError(method);
            }
        }
    }

    public IEnumerator CreateRequest(string url, string requestBodyJsonString, string method, Action<string> callback)
    {
        Debug.Log(method);
        UnityWebRequest request;
        byte[] bytes;
        byte[] bytesToEncode;
        string encodedText;
        switch (method)
        {
            case "PATCH":
            // Defaults are fine for PUT
            case "PUT":
                bytes = new System.Text.UTF8Encoding().GetBytes(requestBodyJsonString);
                request = UnityWebRequest.Put(url, bytes);
                request.SetRequestHeader("X-HTTP-Method-Override", method);
                request.SetRequestHeader("accept", "application/json; charset=UTF-8");
                request.SetRequestHeader("content-type", "application/json; charset=UTF-8");
                break;
            case "POST":
                bytes = new System.Text.UTF8Encoding().GetBytes("{\"graph\": \"ODS\"}");
                request = UnityWebRequest.Put(url, bytes);
                bytesToEncode = Encoding.UTF8.GetBytes (requestBodyJsonString);
                encodedText = Convert.ToBase64String (bytesToEncode);
                request.SetRequestHeader("Authorization", $"Basic {encodedText}");
                request.SetRequestHeader("X-HTTP-Method-Override", method);
                request.SetRequestHeader("content-type", "application/x-www-form-urlencoded");
                break;
            case "GET":
                // Defaults are fine for GET
                request = UnityWebRequest.Get(url);
                bytesToEncode = Encoding.UTF8.GetBytes (requestBodyJsonString);
                encodedText = Convert.ToBase64String (bytesToEncode);
                request.SetRequestHeader("Authorization", $"Basic {encodedText}");
                break;
            case "DELETE":
                // Defaults are fine for DELETE
                request = UnityWebRequest.Delete(url);
                break;
            default:
                throw new Exception("Invalid HTTP Method");
        }

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("Error While Sending: " + request.error);
        }
        else
        {
            try {
                Debug.Log(method);
                callback(request.downloadHandler.text);
            } catch (Exception e) {
                Debug.LogError(method);
            }
        }
    }

    public void SetTimeIntervalAsync(float delay, int steps, Action<string> callback, Action<string> callbackOnFinish)
    {
        StartCoroutine(SetTimeInterval(delay, steps, callback, callbackOnFinish));
    }

    private IEnumerator SetTimeInterval(float delay, int steps, Action<string> callback, Action<string> callbackOnFinish)
    {
        continueWaiting = true;

        yield return new WaitForSeconds(delay);
        callback("");
        
        /*for(int i = 0; i < steps; i++){
            callback("");
            if(continueWaiting){
                yield return new WaitForSeconds(delay);
            }
        }/**/
    }

    public void StopWaiting(){
        continueWaiting = false;
    }

    public void SetPositions(List<ItemData> items){
        earthController.SetActive(true);
        earthController.SetItems(items);
    }

    public void ApplyUpdate(){
        Debug.Log("----0");
        this.menuController.ApplyUpdate();
    }
}
