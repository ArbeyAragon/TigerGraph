using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRotationController : MonoBehaviour
{
    private GameObject _camera;

    void Start () {
        _camera = GameObject.Find ("Main Camera");
        
    }
    void Update () {
        HeadLock (gameObject, 5.0f);
    }

    public void HeadLock (GameObject obj, float speed) {
        speed = Time.deltaTime * speed;
        Quaternion rotTo = Quaternion.LookRotation (-_camera.transform.position + obj.transform.position);
        obj.transform.rotation = Quaternion.Slerp (obj.transform.rotation, rotTo, speed);
    }

}
