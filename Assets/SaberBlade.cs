using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberBlade : MonoBehaviour
{
    private Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SaberBlade" + collision.gameObject);
        if (collision.gameObject.name == "SaberFollower(Clone)")
        {
            Debug.Log("yaaaa gurl");
            Physics.IgnoreCollision(collision.collider, collider);
        }
    }
}

    
