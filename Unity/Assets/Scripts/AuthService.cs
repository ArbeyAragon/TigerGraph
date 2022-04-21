using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using SimpleJSON;

public class AuthService
{
    private string token;
    private string refreshToken;
    private string localId;
    private bool logged = false;
    
    private AuthService _instance;
    private Main _main;

    public AuthService(Main main) { 
        _main = main;
    }

    public void Login(string user, string pass, Action<string> callback){
        this.logged = true;
        _main.AuthAsync(user, pass, (string x) => {
            LoginResponse obj =
                JsonUtility.FromJson<LoginResponse>(x);
            if(!obj.error){
                this.token = obj.results.token;
                callback(this.token);
            } else {
                Debug.Log(obj.code);
                Debug.Log(obj.message);
                Debug.Log(obj.error);
            }
        });
    }

    public string GetToken(){
        return token;
    }
}
[System.Serializable]
public class LoginResultsResponse
{
    public string token;
}
[System.Serializable]
public class LoginResponse
{
    public string code;
    public string expiration;
    public bool error;
    public string message;
    public LoginResultsResponse results;
}
