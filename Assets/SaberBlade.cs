using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberBlade : MonoBehaviour
{
    public Saber saber;    
    public float decalSpawnWaitTime;
    public GameObject decalPrefab;

    public Transform bladeTip;
    public Transform bladeBase;

    private Collider col;
    

    private float lastBurnSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();

        lastBurnSpawnTime = Time.time;

        
    }

    // Update is called once per frame
    void Update()
    {
        bool _pressedA = saber._pressedA;
        //raycast out from certain point a certain distance
        if (_pressedA && Time.time > lastBurnSpawnTime + decalSpawnWaitTime)
        {
            //needs to go from the base of the blade out to the tip of the blade plus 0.1
            float lengthOfBlade = Vector3.Distance(bladeBase.transform.position, bladeTip.transform.position);
            Debug.Log("length " + lengthOfBlade);
            Debug.DrawRay(bladeBase.transform.position, bladeBase.forward * lengthOfBlade, Color.red);
            RaycastHit[] hits = Physics.RaycastAll(bladeBase.transform.position, bladeBase.forward, lengthOfBlade);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.transform.gameObject.tag == "saberBurn")
                {
                    Vector3 decalSpawnPoint = new Vector3(hit.point.x, hit.point.y - 0.001f, hit.point.z);
                    Instantiate(decalPrefab, decalSpawnPoint, Quaternion.FromToRotation(Vector3.forward, hit.normal));

                    //rotate on z in the direction of the blade movement


                    //reset timer of decal
                    lastBurnSpawnTime = Time.time;
                }
            }
        }
    }

    private void FixedUpdate()
    {

        
    }


    private void OnCollisionEnter(Collision collision)
    {
        
    }
}

    
