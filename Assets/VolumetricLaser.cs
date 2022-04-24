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
    private GameObject laserBullet;
    public float sphereRadius;
    

    private VolumetricLineBehavior _vl;

    // Start is called before the first frame update
    void Start()
    {
        _vl = GetComponent<VolumetricLineBehavior>();
        laserBullet = gameObject.transform.parent.gameObject;
    }

    private void Update()
    {
       


        /*
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, sphereRadius, Vector3.forward, out hit, rayMaxDistance, ~ignoreMe ))
        {
            Debug.Log("laser col any: " + hit.collider.gameObject.name);

            //and exists in a list of layers
            if (hit.collider && !laserIgnoreCol.Contains(hit.collider.gameObject.name))
            {
                Debug.Log("laser col: " + hit.collider.gameObject.name);
                //Debug.Log("hit point:" + hit.point);
                //float distance = hit.point.z - transform.position.z - 0.19f;
                //float distance = hit.point.z - transform.position.z;
                float distance = hit.point.z - rb.position.z;

                Debug.Log("rb pos " + rb.position.z);

                //float distance = hit.point.z - transform.position.z - 0.28726f - 0.19f;
                Debug.Log("laser start pos " + laserBullet.transform.position.z);
                Debug.Log("laser hit point" + hit.point + " " + Time.time);

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
        */

        /*
        RaycastHit hit;
        if (Physics.Raycast(transform.position, laserBullet.transform.forward, out hit, rayMaxDistance))
        {
            Debug.Log("idk bro " + hit.collider.gameObject.name);

            //and exists in a list of layers
            if (hit.collider && !laserIgnoreCol.Contains(hit.collider.gameObject.name))
            {
                Debug.Log("laser col: " + hit.collider.gameObject.name);
                //Debug.Log("hit point:" + hit.point);
                //float distance = hit.point.z - transform.position.z - 0.19f;
                //float distance = hit.point.z - transform.position.z;
                float distance = hit.point.z - rb.position.z;

                Debug.Log("rb pos " + rb.position.z);

                //float distance = hit.point.z - transform.position.z - 0.28726f - 0.19f;
                Debug.Log("laser start pos " + laserBullet.transform.position.z);
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
        */

    }

    private void FixedUpdate()
    {
        RaycastHit[] hits = Physics.SphereCastAll(rb.position, sphereRadius, Vector3.forward, rayMaxDistance, ~ignoreMe);
        _vl.EndPos = new Vector3(0, 100, 0);
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                //Debug.Log("laser col any: " + hit.collider.gameObject.name);
                //and exists in a list of layers
                if (hit.collider && !laserIgnoreCol.Contains(hit.collider.gameObject.name))
                {
                    
                    //Debug.Log("hit point:" + hit.point);
                    //float distance = hit.point.z - transform.position.z - 0.19f;
                    //float distance = hit.point.z - transform.position.z;
                    //float distance = hit.point.z - rb.position.z;

                    //Debug.Log("rb pos " + rb.position.z);

                    //float distance = hit.point.z - transform.position.z - 0.28726f - 0.19f;
                    Debug.Log("laser start pos " + laserBullet.transform.position.z);
                    Debug.Log("laser col: " + hit.collider.gameObject.name + " laser hit point" + hit.point + " " + Time.time);

                    //Debug.Log("Distance " + distance);

                    //Debug.Log("Col name " + hit.collider.gameObject.name + " distance: " + distance + " hit point" + hit.point.z + " transform pos: " + transform.position.z);
                    //_vl.StartPos = new Vector3(0, distance, 0);
                    _vl.EndPos = new Vector3(0, hit.distance, 0);
                    //_vl.SetStartAndEndPoints(_vl.StartPos, _vl.EndPos);
                }                
            } 
            
        }
        


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }




}
