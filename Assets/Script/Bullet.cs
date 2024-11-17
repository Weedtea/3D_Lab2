using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody Brb;
    // Start is called before the first frame update
    void Start()
    {
        Brb = GetComponent<Rigidbody>();
        Brb.AddForce(transform.forward, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
