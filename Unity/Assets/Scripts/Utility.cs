using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using SimpleJSON;

public class TempChangeItem
{
    public string country;
    public string country_code;
    public float latitude;
    public float longitude;
    public int year;
    public float variable;
    public TempChangeItem(TempChangeRaw s){
        country = s.country[0];
        country_code = s.country_code[0];
        latitude = s.latitude[0];
        longitude = s.longitude[0];
        year = s.year;
        variable = s.variable[0];
    }
}

public class TempChangeMap
{
    public static bool error;
    public static string message;
    public static List<TempChangeItem> Map(string x){
        DataResponse<TempChangeRaw> s =
            JsonUtility.FromJson<DataResponse<TempChangeRaw>>(x);
        List<TempChangeItem> list = new List<TempChangeItem>();
        for(var i=0;i<s.results[0].S1.Count;i++){
            list.Add(new TempChangeItem(s.results[0].S1[i].attributes));
        }
        TempChangeMap.error = s.error;
        TempChangeMap.message = s.message;
        return list;
    }
}

[System.Serializable]
public class TempChangeRaw
{
    public List<string> country;
    public List<string> country_code;
    public List<float> latitude;
    public List<float> longitude;
    public List<float> variable;
    public int year;
}

[System.Serializable]
public class S1Item<T>
{
    public string v_id;
    public string v_type;
    public string message;
    public T attributes;
}
[System.Serializable]
public class DataResultsResponse<T>
{
    public List<S1Item<T>> S1;
}
[System.Serializable]
public class DataResponse<T>
{
    public bool error;
    public string message;
    public List<DataResultsResponse<T>> results;
}

//---------------------------------------------------------------------------------



public class MenuMap
{
    public static bool error;
    public static string message;
    public static Dictionary<string, string> Map(string x){
        MenuResponse s = JsonUtility.FromJson<MenuResponse>(x);
        Dictionary<string, string> dic = new Dictionary<string, string>();
        for(var i=0;i<s.results.Count;i++){
            dic.Add(s.results[i].key, s.results[i].value[0]);
        }
        MenuMap.error = s.error;
        MenuMap.message = s.message;
        return dic;
    }
    
    public static List<int> MapYears(string x){
        MenuYearsResponse s = JsonUtility.FromJson<MenuYearsResponse>(x);
        MenuMap.error = s.error;
        MenuMap.message = s.message;
        return s.results[0].years;
    }
}

[System.Serializable]
public class MenuResultsResponse
{
    public string key;
    public string message;
    public List<string> value;
}

[System.Serializable]
public class MenuResponse
{
    public bool error;
    public string message;
    public List<MenuResultsResponse> results;
}

//--------------------------------
[System.Serializable]
public class MenuYearsItemResponse
{
    public List<int> years;
}

[System.Serializable]
public class MenuYearsResponse
{
    public bool error;
    public string message;
    public List<MenuYearsItemResponse> results;
}
