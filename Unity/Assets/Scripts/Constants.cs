using System;
using System.Collections;
using System.Collections.Generic;
public class Constants
{
    public static string HOST = "https://cetreal.i.tgcloud.io:9000";
    public static string USER = "tigergraph";
    public static string PASS = "casadepapel";
    public static string DB = "ODS";
    public static Dictionary<string, string> MAIN_MENU = new Dictionary<string, string>(){
        {"temperature", "Climate change"},
        {"health", "Health"},
        {"industry", "Industry"},
        {"education", "Education"},
        {"gender", "Gender"}
    };
}

