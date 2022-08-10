using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Core.Utils;


public class Bomb : MonoBehaviour
{
    private Rigidbody rb;
    public float thrust;    
    public ParticleSystem bombShatterTriangleParticle;
    public ParticleSystem bombShatterParticle;
    public ParticleSystem bombShatterTriangleParticleRed;
    public ParticleSystem bombShatterParticleRed;

    public Vector3 rotSpeed;

    [SerializeField]
    private AudioClip bombExplosionSound;

    [SerializeField]
    private LaserRound laserRound;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy bomb regardless of what happens after certain amount of time
        Destroy(gameObject, 20f);


        GameObject gunGame = GameObject.Find("GunGame");
        laserRound = gunGame.GetComponent<LaserRound>();

        //move the bomb towards the player 
        rb = GetComponent<Rigidbody>();
        thrust = Random.Range(350f, 450f);
        rb.AddForce(transform.forward * thrust);
        var sh = bombShatterParticle.shape;
        var shR = bombShatterParticleRed.shape;
        Mesh bombMesh = gameObject.GetComponent<MeshFilter>().mesh; 
        sh.mesh = bombMesh;
        shR.mesh = bombMesh;

        var nsh = bombShatterTriangleParticle.shape;
        var nshR = bombShatterTriangleParticleRed.shape;
        nsh.mesh = bombMesh;
        nshR.mesh = bombMesh;





    }

    

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(rotSpeed * Time.deltaTime);
    }


    private void OnDestroy()
    {
        laserRound.currentBombExplodedCount++;        
    }


    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Bomb Col" + collision.gameObject.name);

        if (collision.gameObject.name == "MadsonD9")
        {            
            DamagePlayer(50);
            DestroyBomb(true);
            return;
            //DamagePlayer();
        }


        if (collision.gameObject.name == "Bullet(Clone)")
        {
            DestroyBomb();
            return;
        }

        if (collision.gameObject.name == "Blade")
        {
            DestroyBomb();
            return;
        }

        if (collision.gameObject.name == "mine(Clone)")
        {
            DestroyBomb();
            return;
        }


        if (collision.gameObject.transform?.parent.gameObject.name == "XRRig")
        {
            //Debug.Log("Is hit?");
            DestroyBomb(true);
            DamagePlayer(50);
        }
    }

    private void DamagePlayer(int damage)
    {
        Debug.Log("damging player");
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.Damage(damage);
    }

    

    public void DestroyBomb(bool hitPlayer = false)
    {        

        if (hitPlayer)
        {
            //duplicate the particle but make it red
            Instantiate(bombShatterParticleRed, transform.position, Quaternion.identity);
            Instantiate(bombShatterTriangleParticleRed, transform.position, Quaternion.identity);
            SFXPlayer.Instance.PlaySFX(bombExplosionSound, transform.position, 1f, 10f);
        } else
        {            
            //spawn particles from mesh for blue effect
            Instantiate(bombShatterParticle, transform.position, Quaternion.identity);
            Instantiate(bombShatterTriangleParticle, transform.position, Quaternion.identity);
            SFXPlayer.Instance.PlaySFX(bombExplosionSound, transform.position, 1f, 10f);
            

        }
        
        
        Destroy(gameObject);
    }
}
