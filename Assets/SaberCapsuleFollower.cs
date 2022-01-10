using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberCapsuleFollower : MonoBehaviour
{
    public GameObject parent;
    public GameObject sparkParticle;

    [SerializeField]
    private float maxVelocity = 20f;
    private SaberCapsule capsule;
    private Collider collider;
    private Rigidbody _rb;
    private Vector3 _velocity;

    public float sensitivity = 100f;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateFolllowerPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        UpdateFolllowerPosition();



    }

    private void UpdateFolllowerPosition()
    {
        Vector3 destination = capsule.transform.position;
        _rb.transform.rotation = capsule.transform.rotation;
        _velocity = (destination - _rb.transform.position) * sensitivity;

        _rb.velocity = _velocity;
    }
     
    public void SetFollowTarget(SaberCapsule saberCapsule)
    {
        capsule = saberCapsule;
    }

    private void OnCollisionEnter(Collision collision)
    {        
        Debug.Log("Follower" + collision.gameObject);

        if (collision.gameObject.tag != "LaserBullet")
        {
            Physics.IgnoreCollision(collision.collider, collider);
        }

        /*
        if (collision.gameObject.name == "SaberFollower(Clone)" || collision.gameObject.name == "Saber")
        {
            Debug.Log("yaaaa boi");
            Physics.IgnoreCollision(collision.collider, collider);
        }
        */


        if (collision.gameObject.tag == "LaserBullet")
        {
            Physics.IgnoreCollision(collision.collider, collider);
            //check if the laser bullet has collidedWithSaber
            LaserBullet bullet = collision.gameObject.GetComponent<LaserBullet>();

            if (!bullet.hitSaber)
            {
                Vector3 direction = (bullet.transform.position - collision.contacts[0].point).normalized;

                if (_rb.velocity.magnitude / maxVelocity > 0.5f)
                {
                    //_rb.velocity *= 3;
                    Debug.Log("deflect lazer");
                    float forceMultiplier = _rb.velocity.magnitude / maxVelocity * 30f;                    
                    bullet.DeflectLaser(forceMultiplier, direction);
                    
                    //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //cube.transform.position = transform.position;
                }
                else
                {
                    Debug.Log("destroy lazer");
                    bullet.DestroyLaser(true, sparkParticle);
                }
                bullet.hitSaber = true;
            }
        } else
        {
            Debug.Log("col name" + collision.gameObject);
            Physics.IgnoreCollision(collision.collider, collider);
        }
            return;
        if (collision.gameObject.tag != "LaserBullet")
        {
            Physics.IgnoreCollision(collision.collider, collider);
        } else
        {
            Physics.IgnoreCollision(collision.collider, collider);
            //check if the laser bullet has collidedWithSaber
            LaserBullet bullet = collision.gameObject.GetComponent<LaserBullet>();



            if(!bullet.hitSaber)
            {

                
                Vector3 direction = (bullet.transform.position - collision.contacts[0].point).normalized;
                
                if (_rb.velocity.magnitude / maxVelocity > 0.5f)
                {
                    //_rb.velocity *= 3;
                    float forceMultiplier = _rb.velocity.magnitude / maxVelocity * 30f;
                    bullet.DeflectLaser(forceMultiplier, direction);
                    //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //cube.transform.position = transform.position;
                }
                else
                {
                    bullet.DestroyLaser(true);
                }
                bullet.hitSaber = true;
            }
            //if not, then check if its moving fast enough for deflect, 
            //otherwise, bullet spark

           
        }
    }

    
}
