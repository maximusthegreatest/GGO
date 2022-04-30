using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Core.Utils;

public class LaserBullet : MonoBehaviour
{
    
    public float thrust = 80f;
    public int damage;
    public float timeAlive;
    public float birth;
    public GameObject bulletSpark;
    public GameObject bulletTriangleParticleEffect;
    public GameObject bulletParticleEffect;
    public float collisionTimeout;
    public bool hitSaber;

    private bool canDamage = true;
    private Rigidbody rb;
    private Collider bulletCollider;
    private GameObject bulletLine;
    
    [SerializeField]
    private AudioClip laserBlockSound;

    // Start is called before the first frame update
    void Start()
    {
        //move the bomb towards the player         
        rb = GetComponent<Rigidbody>();        
        rb.AddForce(transform.forward * thrust);
        birth = Time.time;
        bulletCollider = GetComponent<Collider>();
        bulletLine = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (birth + timeAlive < Time.time)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ReenableCollision()
    {
        yield return new WaitForSeconds(collisionTimeout);
        bulletCollider.enabled = true;
    }

    public void SaberDestroyLaser(Vector3 saberVelocity, Vector3 direction, Vector3 point)
    {
        
        //if velocity is fast enough then redirect
        if(true)
        {
            //draw a laser in the direction of the force
            Debug.Log("Yeet");
            Debug.DrawRay(point, direction, Color.red, 10000);
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation;
            //Time.timeScale = 0;
            rb.velocity = Vector3.zero;
            //disable collision then call a method that reenables it after a second
            bulletCollider.enabled = false;
            StartCoroutine(ReenableCollision());
            rb.AddForce(transform.forward * thrust); //times velocity multiplier
        }
        else
        {
            //spawn bullet particle effect at hit point
            Instantiate(bulletSpark, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        


        
    }


    public void DeflectLaser(float forceMultiplier, Vector3 direction)
    {
            
            rb.velocity = Vector3.zero;
            rb.AddForce(direction * forceMultiplier, ForceMode.Impulse);

            canDamage = false;
            Destroy(bulletLine);
            //spawn bullet particle effect at hit point
            //won't have cone effect if deflect
        Debug.Log("Deflecting");
            //Instantiate(bulletSpark, transform.position, Quaternion.identity);
        
        

        
        //TODO play sound and show sparks particle effect
        //spawn bullet particle effect at hit point

    }

    public void DestroyLaser(bool saberHit = false)
    {
        if(saberHit)
        {
            //spawn bullet particle effect at hit point
            //won't have cone effect if deflect
            Instantiate(bulletSpark, transform.position, Quaternion.identity);
            SFXPlayer.Instance.PlaySFX(laserBlockSound, transform.position);            
        }            
            Destroy(gameObject);
               
        
        //TODO play sound and show sparks particle effect
        //spawn bullet particle effect at hit point
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("laser bullet hit " +  collision.gameObject.name);

        if (collision.gameObject.name == "Blade")
        {

            //Debug.Log("laser hit blade");
            DestroyLaser(true);
            return;
        }
        

        //TODO only if within bounds of sphere
        if (collision.gameObject.name == "MadsonD9")
        {
            Debug.Log("laser hit gun");
            DamagePlayer();
            DestroyLaser();
            return;
        }


        if (collision.gameObject.name == "Bullet(Clone)")
        {
            Debug.Log("laser hit bullet");
            DestroyLaser();
            return;
        }

        //first check if the gameobject has a parent
        if(collision.gameObject.transform.parent)
        {
            if (collision.gameObject.transform.parent.gameObject.name == "XRRig")
            {
                //Debug.Log("Is hit?");
                Debug.Log("laser hit player");
                Destroy(gameObject);
                Instantiate(bulletTriangleParticleEffect, transform.position, Quaternion.identity);
                Instantiate(bulletParticleEffect, transform.position, Quaternion.identity);

                if (canDamage)
                {
                    DamagePlayer();
                }

                return;
            }
        }

        
        
        Debug.Log("Hit something else");
        Destroy(gameObject);
    }

    private void DamagePlayer()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.Damage(damage);
    }
}
