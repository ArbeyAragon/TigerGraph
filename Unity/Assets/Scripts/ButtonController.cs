using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private string key; 
    private string value;
    private string type;
    private MenuController mc;

    public void SetText(string key, string value, string type)
    {
        this.key = key;
        this.value = value;
        this.type = type;
    }

    public void SetMenuController(MenuController mc)
    {
        this.mc = mc;
    }

    public void OnClick(){
        this.mc.OnClick(this.type, this.key);
    }
}
