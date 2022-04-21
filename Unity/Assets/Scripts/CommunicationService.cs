using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using SimpleJSON;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;

public class CommunicationService
{
    private Main _main;
    private AuthService _authService;
    private MicroService _microService;

    public CommunicationService(AuthService authService, 
                                MicroService microService, 
                                Main main) { 
        _main = main;
        _authService = authService;
        _microService = microService;

        string user = Constants.USER;
        string pass = Constants.PASS;
        
        this.AuthFirebase(user, pass);

    }

    public void AuthFirebase(string user, string pass) {
        Debug.Log("**AuthFirebase");
        //Authentication with firebase and receive token
        _authService.Login(user, pass, (string x) => {
            Debug.Log("*-*-*-*-*-*-*");
            this.StartRequestMenu();
        });
    }

    public void StartRequestMenu() {
        Debug.Log("**StartConnection");
        string table = "health";
        _microService.GetMenuData(table, (Dictionary<string, string> items)=>{   
            this._main.categories = items;
            Debug.Log(items.Count);
            _microService.GetMenuYears(table, (List<int> years)=>{    
                Debug.Log(years[0]);
                this._main.years = years;
                try {
                    this._main.ApplyUpdate();
                } catch (Exception e) {
                    Debug.LogError("StartRequestMenu");
                }
            });
        });
    }

    public void StartRequestData( string table, string variable, int year) {
        Debug.Log("**StartConnection");
        //string table = "health";
        //string variable = "SM.POP.REFG";
        //int year = 2020;
        _microService.GetTempChangeData(table, variable, year,(List<TempChangeItem> l)=>{    
            List<ItemData> items = new List<ItemData>();
            float min = l[0].variable;
            float max = l[0].variable;
            for(var i=0; i<l.Count; i++){
                if(l[i].variable < min) { min = l[i].variable; } 
                if(l[i].variable > max) { max = l[i].variable; } 
            }
            for(var i=0; i<l.Count; i++){
                float value = (l[i].variable-min)/(max-min);
                value = value * 10f + 1f;
                items.Add(new ItemData(l[i].latitude,l[i].longitude,value));
            }
            _main.SetPositions(items);
        });
    }

    public void GenerateKeyAndEncript(){
        Debug.Log("**GenerateKeyAndEncript");
    }


}