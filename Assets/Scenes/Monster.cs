using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HitCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (HitCount >= 3)
        {
            Destroy(this.gameObject);
        }
}

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "bullet")
        {
            HitCount++;
        }

    }
}
