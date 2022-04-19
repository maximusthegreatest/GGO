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
    public Rigidbody rb;
    

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
                //float distance = hit.point.z - transform.position.z - 0.19f;
                //float distance = hit.point.z - transform.position.z;
                float distance = hit.point.z - rb.position.z;
                
                Debug.Log("rb pos " + rb.position.z);

                //float distance = hit.point.z - transform.position.z - 0.28726f - 0.19f;
                Debug.Log("laser start pos " + transform.position.z);
                Debug.Log("laser hit point" + hit.point);

                Debug.Log("Distance " + distance);

                //Debug.Log("Col name " + hit.collider.gameObject.name + " distance: " + distance + " hit point" + hit.point.z + " transform pos: " + transform.position.z);
                //_vl.StartPos = new Vector3(0, distance, 0);
                _vl.EndPos = new Vector3(0, distance, 0);
                //_vl.SetStartAndEndPoints(_vl.StartPos, _vl.EndPos);
            }
        }
        else
        {            
            _vl.EndPos = new Vector3(0, 100, 0);
        }
    }




}
