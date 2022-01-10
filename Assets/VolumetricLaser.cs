using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class VolumetricLaser : MonoBehaviour
{
    
    public float rayMaxDistance;
    public LayerMask ignoreMe;
    public List<string> laserColLayers;
    public List<string> laserIgnoreCol;

    

    private VolumetricLineBehavior _vl;

    // Start is called before the first frame update
    void Start()
    {
        _vl = GetComponent<VolumetricLineBehavior>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, rayMaxDistance, ~ignoreMe))
        {
                        
            //and exists in a list of layers
            if (hit.collider && !laserIgnoreCol.Contains(hit.collider.gameObject.name) )
            {
                //Debug.Log("laser col: " + hit.collider.gameObject.name);
                //Debug.Log("hit point:" + hit.point);
                float distance = hit.point.z - transform.position.z - 0.19f;
                //Debug.Log("Col name " + hit.collider.gameObject.name + " distance: " + distance + " hit point" + hit.point.z + " transform pos: " + transform.position.z);
                _vl.StartPos = new Vector3(0, distance, 0);                
            }
        }
        else
        {
            //Debug.Log("hit nothing");
            _vl.StartPos = new Vector3(0, 100, 0);
        }
    }




}
