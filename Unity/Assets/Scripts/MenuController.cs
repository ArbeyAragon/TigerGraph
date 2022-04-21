using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private string tablesC = "tables";
    private string itemsC = "items";
    private string yearsC = "years"; 

    private string tablesV = "";
    private string itemsV = "";
    private int yearsV = 0; 

    public GameObject referenceMainMenu;
    public GameObject referenceItems;
    public GameObject referenceYears;

    public GameObject button;
    public Main main;
    public Dictionary<int, GameObject> buttonsMainMenu = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> buttonsItems = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> buttonsYears = new Dictionary<int, GameObject>();

    public void ApplyUpdate(){
        Debug.Log("----1");
        this.SetButtons(main.tables, buttonsMainMenu, tablesC, referenceMainMenu);
        this.SetButtons(main.categories, buttonsItems, itemsC, referenceItems);
        Dictionary<string, string> l = new Dictionary<string, string>();
        for(var i=0; i<main.years.Count; i++){
            l.Add($"{main.years[i]}", $"{main.years[i]}");
        }
        this.SetButtons(l, buttonsYears, yearsC, referenceYears);
    }

    public void SetButtons(Dictionary<string, string> l, Dictionary<int, GameObject> buttons, string type, GameObject reference){
        foreach(KeyValuePair<int,GameObject> b in buttons)
        {
            buttons[b.Key].SetActive(false);
        }
        if(l.Count > buttons.Count){
            for(var i=buttons.Count; i<l.Count; i++){
                GameObject go = Instantiate(button, transform.position, Quaternion.identity);
                go.transform.position = reference.transform.position;
                go.transform.rotation = reference.transform.rotation;
                go.transform.SetParent(transform);
                buttons.Add(i,go);
            }
        }
        int j = 0;
        foreach(KeyValuePair<string, string> b in l){
            buttons[j].SetActive(true);
            buttons[j].transform.position = reference.transform.position + new Vector3(0,-j*0.1f,0);
            buttons[j].GetComponent<ButtonController>().SetMenuController(this);
            buttons[j].GetComponent<ButtonController>().SetText(b.Key, b.Value, type);
            j++;
        }
    }

    public void OnClick(string type, string key){
        if(type == tablesC){
            tablesV = key;
            UpdateFilter();
        } else if(type == itemsC) {
            itemsV = key;
            UpdateFilter();
        } else if(type == yearsC) {
            yearsV = int.Parse(key);
            UpdateFilter();
        }
    }

    public void UpdateFilter(){
        if(tablesV != "" && itemsV != "" && yearsV != 0){

        }
    }
}
