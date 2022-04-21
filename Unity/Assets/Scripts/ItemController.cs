using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject item;
    public float scale = 0.5f;
    
    void Start()
    {
       //this.SetScale(this.scale);
       //this.SetLatLon(0.0f,0.0f);
    }

    public void SetScale(float scale){
        this.scale = scale;
        this.item.transform.localScale = new Vector3(scale,1.0f,1.0f);
    }

    public void SetLatLon(float lon, float lat){
        //Debug.Log($"{lon} {lat}");
        transform.rotation = Quaternion.Euler(0,-lon,lat);
    }

    public void SetActive(bool v)
    {
        gameObject.SetActive(v);
    }
}
