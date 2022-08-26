using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Core;

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
    public GameObject bulletHitPlayerDecalPrefab;


    private bool canDamage = true;
    private Rigidbody rb;
    private Collider bulletCollider;
    private GameObject bulletLine;

    [SerializeField]
    private float bulletHitWeaponForce;

    [SerializeField]
    private Transform leftShoulder;

    [SerializeField]
    private Transform rightShoulder;

    [SerializeField]
    private GameObject laserBulletDecal;
    
    [SerializeField]
    private AudioClip laserBlockSound;

    [SerializeField]
    private AudioClip bulletHitSound;

    [SerializeField]
    private AudioClip bulletHitMetalSound;




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
            //Debug.Log("Yeet");
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

        if (collision.gameObject.name == "PhotonSwordContainer")
        {
            ThrowWeapon(collision.gameObject);
        }


        if (collision.gameObject.name == "Blade")
        {
            Debug.Log("blade col");
            //Debug.Log("laser hit blade");
            DestroyLaser(true);
            return;
        }
        

        //TODO only if within bounds of sphere
        if (collision.gameObject.name == "MadsonD9")
        {
            Debug.Log("laser hit gun");
            ContactPoint contact = collision.contacts[0];
            //DamagePlayer(contact);
            ThrowWeapon(collision.gameObject);
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
            if (collision.gameObject.transform.root.gameObject.name == "XRRig")
            {
                //Debug.Log("Is hit?");
                Debug.Log("laser hit player");
                
                Instantiate(bulletTriangleParticleEffect, transform.position, Quaternion.identity);
                Instantiate(bulletParticleEffect, transform.position, Quaternion.identity);

                if (canDamage)
                {
                    ContactPoint contact = collision.contacts[0];
                    DamagePlayer(contact);
                    
                    

                }
                DestroyLaser();
                return;
            }
        }

        




        Debug.Log("Hit something else");
        
    }

    private void ThrowWeapon(GameObject weaponCollidedWith)
    {
        //play metal hit sound at location of collision
        SFXPlayer.Instance.PlaySFX(bulletHitMetalSound, weaponCollidedWith.transform.position);
        //get grabbable
        HVRGrabbable wepGrabbable = weaponCollidedWith.GetComponent<HVRGrabbable>();

        wepGrabbable.ForceRelease();
        Rigidbody wepRb = weaponCollidedWith.GetComponent<Rigidbody>();
        wepRb.AddForce(bulletHitWeaponForce * transform.forward, ForceMode.Impulse);

    }

    private void DamagePlayer(ContactPoint contact)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        SFXPlayer.Instance.PlaySFX(bulletHitSound, transform.position);
        //instantiate decal hit        
        //Vector3 pos = contact.point;
       // Instantiate(bulletHitPlayerDecalPrefab, pos, Quaternion.FromToRotation(Vector3.forward, contact.normal));
        Debug.Log("Should have spawned bullet decal");
        player.Damage(damage);


        /*
        Vector3 decalSpawnPoint = new Vector3(collision.transform.position.x, collision.transform.position.y - 0.001f, collision.transform.position.z);
        Vector3 myNormal = new Vector3();
        foreach (ContactPoint contact in collision.contacts)
        {
            myNormal = contact.normal;
        }

        Instantiate(laserBulletDecal, decalSpawnPoint, Quaternion.FromToRotation(Vector3.forward, myNormal));
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BulletSoundCollider")
        {
            
            
            
                
                //get it off of the collider
                BulletSound sound = other.gameObject.GetComponent<BulletSound>();
                //distance from this to left and then right
                float leftDistance = Vector3.Distance(transform.position, sound.leftShoulder.position);
                float rightDistance = Vector3.Distance(transform.position, sound.rightShoulder.position);

                if (leftDistance > rightDistance)
                {
                    //spawn on right side
                    SFXPlayer.Instance.PlaySFX(sound.GetWhizSound(), sound.rightShoulder.position);
                }
                else
                {
                    SFXPlayer.Instance.PlaySFX(sound.GetWhizSound(), sound.leftShoulder.position);
                }

                
            


            




        }
    }
}
