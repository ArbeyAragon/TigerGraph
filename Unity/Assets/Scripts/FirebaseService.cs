using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using SimpleJSON;

public class FirebaseService
{

    public FirebaseService() { }

    /*public void SendMessage(){
        string path = "in-msg";
        string requestBodyJsonString = "{\"user_id\" : \"jack\", \"text\" : \"Ahoy!\"}";
        string method = "POST";
        string url = $"{this.databaseURL}{path}.json?auth={this.idToken}";
        
        Action<string> callback = (string x) => {
            MsgSendedResponse obj =
                JsonUtility.FromJson<MsgSendedResponse>(x);
            this.key = obj.name;
        };
        StartCoroutine(CreateRequest(url, requestBodyJsonString, method, callback));
    }

    public void ReadResponse(){
        string path = $"out-msg/{this.localId}/{this.key}";
        string method = "GET";
        string url = $"{this.databaseURL}{path}.json?auth={this.idToken}";
        
        Action<string> callback = (string x) => {
            Debug.Log(x);
            DialogMock obj =
                JsonUtility.FromJson<DialogMock>(x);
        };
        string requestBodyJsonString = "";
        StartCoroutine(CreateRequest(url, requestBodyJsonString, method, callback));
    }

    public void GetAllDialogs(Action<string> callback){
        string path = $"dialog";
        string method = "GET";
        string url = $"{this.databaseURL}{path}.json?auth={this.idToken}";
        
        Action<string> callback1 = (string x) => {
            callback(x);
        };
        string requestBodyJsonString = "";
        StartCoroutine(CreateRequest(url, requestBodyJsonString, method, callback1));
    }

    public void GetConfiguration(Action<string> callback){
        string path = $"configuration";
        string method = "GET";
        string url = $"{this.databaseURL}{path}.json?auth={this.idToken}";
        
        Action<string> callback1 = (string x) => {
            callback(x);
        };
        string requestBodyJsonString = "";
        StartCoroutine(CreateRequest(url, requestBodyJsonString, method, callback1));
    }

    public void UpdateConfig(Configuration conf, Action<string> callback){
        string path = $"configuration";
        string method = "PUT";
        string url = $"{this.databaseURL}{path}.json?auth={this.idToken}";
        
        Action<string> callback1 = (string x) => {
            callback(x);
        };
        string requestBodyJsonString = "{\"language\" : \""+conf.language+"\"}";
        StartCoroutine(CreateRequest(url, requestBodyJsonString, method, callback1));
    }/**/



}

[Serializable]
public class MsgSendedResponse
{
    public string name;
}