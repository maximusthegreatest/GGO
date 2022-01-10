using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class VolLaserTest : MonoBehaviour
{
    public float rayMaxDistance;
    public LayerMask ignoreMe;
    private VolumetricLineBehavior _vl;

    // Start is called before the first frame update
    void Start()
    {
        _vl = GetComponent<VolumetricLineBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {        
        RaycastHit hit;        
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, rayMaxDistance))
        {
            if (hit.collider)
            {
                Debug.Log("col: " + hit.collider.gameObject.name);
                Debug.Log("hit point:" + hit.point);
                float distance =  hit.point.z - transform.position.z;
                _vl.StartPos = new Vector3(0, distance, 0);
                //transform.localScale = new Vector3(transform.localScale.x, hit.point.z, transform.localScale.z);
            }
        }
        else
        {
            Debug.Log("hit nothing");
            _vl.StartPos = new Vector3(0, 5000, 0);
            //transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        }
    }
}
