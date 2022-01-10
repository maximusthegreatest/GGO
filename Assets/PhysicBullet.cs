using HurricaneVR.Framework.Weapons;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PhysicBullet : MonoBehaviour
{

    public float raycastLength;
    public float bulletVelocity;
    public HVRRayCastGun gun;
    public Collider[] gunColliders;
    public GameObject colliders;
    public float bulletAliveTime;
    
    private float elapsed;
    private Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        bulletVelocity = gun.BulletTrailSpeed;
        rb = GetComponent<Rigidbody>();
        Collider myCollider = GetComponent<Collider>();

        //1 game object with many colliders
        //

        gunColliders = colliders.GetComponentsInChildren<Collider>();
        foreach(Collider gunCollider in gunColliders)
        {
            Physics.IgnoreCollision(gunCollider, myCollider);
        }
        
        //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), myCollider);

        //do we move in start or update
        elapsed = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;                    
        if(elapsed > bulletAliveTime)
        {
            Destroy(gameObject);
        }

        raycastLength = bulletVelocity * Time.deltaTime;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out var hit, raycastLength))
        {
            var bomb = hit.collider.GetComponent<Bomb>();
            if (bomb)
            {
                Debug.Log("Has bomb");
                Destroy(bomb.gameObject);
            }
            else
            {
                Debug.Log("No bomb");
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(gameObject.transform.forward * bulletVelocity * Time.deltaTime, ForceMode.Impulse);
    }




    void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.tag == "Pistol")
        {
            Debug.Log("yeet");            
            return;
        }
        */

        Debug.Log("Name" + collision.gameObject.name);
        Destroy(gameObject);
    }
}
