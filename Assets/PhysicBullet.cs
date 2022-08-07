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
    
    
    public float bulletAliveTime;
    
    private float elapsed;
    private Rigidbody rb;
    private Collider myCollider;


    

    // Start is called before the first frame update
    void Start()
    {
        bulletVelocity = gun.BulletTrailSpeed;
        rb = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        
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
                //Debug.Log("Has bomb");
                Destroy(bomb.gameObject);
            }
            else
            {
                //Debug.Log("No bomb");
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(gameObject.transform.forward * bulletVelocity * Time.deltaTime, ForceMode.Impulse);
    }




    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("bullet collision " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
