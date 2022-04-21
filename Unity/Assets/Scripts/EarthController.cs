using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthController : MonoBehaviour
{
    public GameObject prefab;
    public Dictionary<int, ItemController> items = new Dictionary<int, ItemController>();

    public void SetItems(List<ItemData> l){
        foreach(KeyValuePair<int,ItemController> item in items)
        {
            items[item.Key].SetActive(false);
        }
        if(l.Count > items.Count){
            for(var i=items.Count; i<l.Count; i++){
                GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
                go.transform.position = transform.position;
                go.transform.rotation = transform.rotation;
                go.transform.SetParent(transform);
                items.Add(i,go.GetComponent<ItemController>());
            }
        }
        for(var i=0; i<l.Count; i++){
            items[i].SetActive(true);
            items[i].SetLatLon(l[i].longitud, l[i].latitud);
        }
    }

    public void SetActive(bool v)
    {
        gameObject.SetActive(v);
    }
}

public class ItemData
{
    public float latitud;
    public float longitud;
    public float size;
    public ItemData(float latitud, float longitud, float size){
        this.latitud = latitud;
        this.longitud = longitud;
        this.size = size;
    }
}