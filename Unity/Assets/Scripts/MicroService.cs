using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using SimpleJSON;
public class MicroService
{   
    private Main _main;
    private AuthService _authService;
    
    public MicroService(AuthService authService, Main main) { 
        _main = main;
        _authService = authService;
    }

    public void GetTempChangeData(string table, string variable, int year, Action<List<TempChangeItem>> callback){
        string parm = $"?table={table}&variable={variable}&year={year}";
        string query = "sample_query";
        string url = $"{Constants.HOST}/query/{Constants.DB}/{query}{parm}";
        Debug.Log(url);
        this._main.GetDataAsync(url, this._authService.GetToken(), (string x) => {
            Debug.Log(x);
            List<TempChangeItem> t = TempChangeMap.Map(x);
            if(!TempChangeMap.error){
                callback(t);
            } else {
                Debug.Log(TempChangeMap.message);
                Debug.Log(TempChangeMap.error);
            }
        });
    }

    public void GetMenuData(string table, Action<Dictionary<string, string>> callback){
        string parm = $"?table={table}";
        string query = "menu_data";
        string url = $"{Constants.HOST}/query/{Constants.DB}/{query}{parm}";
        Debug.Log(url);
        this._main.GetDataAsync(url, this._authService.GetToken(), (string x) => {
            Dictionary<string, string> t = MenuMap.Map(x);
            if(!MenuMap.error){
                callback(t);
            } else {
                Debug.Log(MenuMap.message);
                Debug.Log(MenuMap.error);
            }
        });
    }

    public void GetMenuYears(string table, Action<List<int>> callback){
        string parm = $"?table={table}";
        string query = "menu_years";
        string url = $"{Constants.HOST}/query/{Constants.DB}/{query}{parm}";
        Debug.Log(url);
        this._main.GetDataAsync(url, this._authService.GetToken(), (string x) => {
            List<int> t = MenuMap.MapYears(x);
            if(!MenuMap.error){
                callback(t);
            } else {
                Debug.Log(MenuMap.message);
                Debug.Log(MenuMap.error);
            }
        });
    }
}
