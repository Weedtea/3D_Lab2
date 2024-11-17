using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Wall : MonoBehaviour
{

    private Rigidbody _rb;

    void Start(){
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            _rb.useGravity = true;
        }
    }


}
