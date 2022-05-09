using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;
using HurricaneVR.Framework.Core.Utils;

public class VolumetricLaser : MonoBehaviour
{
    
    public float rayMaxDistance;
    public LayerMask ignoreMe;
    public List<string> laserColLayers;
    public List<string> laserIgnoreCol;
    public List<string> laserTargetIgnoreCol;
    public Rigidbody rb;
    private GameObject laserBullet;
    public float sphereRadius;

    [SerializeField]
    private GameObject target;
    [SerializeField]
    private AudioClip laserDecalSound;

    private bool playedSound;


    private VolumetricLineBehavior _vl;

    // Start is called before the first frame update
    void Start()
    {
        target.SetActive(false);
        _vl = GetComponent<VolumetricLineBehavior>();
        laserBullet = gameObject.transform.parent.gameObject;
    }

    private void Update()
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

                    Debug.Log("laser hit " + hit.collider.gameObject.name);
                    //Debug.Log("laser transform pos " + transform.position.z + "laser rb pos " + rb.position.z + "laser col: " + hit.collider.gameObject.name + " laser hit point" + hit.point + " " + Time.time);
                    _vl.EndPos = new Vector3(0, hit.distance, 0);
                    
                    if (!laserTargetIgnoreCol.Contains(hit.collider.gameObject.name))
                    {
                        target.transform.position = hit.point;
                        target.SetActive(true);
                        if (!playedSound)
                        {
                            SFXPlayer.Instance.PlaySFX(laserDecalSound, hit.point, 1f, 1f);
                            playedSound = true;
                        }
                    }

                    break;
                    
                    
                    //_vl.SetStartAndEndPoints(_vl.StartPos, _vl.EndPos);
                }                
            }
            
        } else
        {
            target.SetActive(false);
        }
    }



  




}
