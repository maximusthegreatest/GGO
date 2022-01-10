using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bomb : MonoBehaviour
{
    private Rigidbody rb;
    public float thrust;    
    public ParticleSystem bombShatterTriangleParticle;
    public ParticleSystem bombShatterParticle;
    public Vector3 rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //move the bomb towards the player 
        rb = GetComponent<Rigidbody>();
        thrust = Random.Range(350f, 450f);
        rb.AddForce(transform.forward * thrust);
        var sh = bombShatterParticle.shape;
        Mesh bombMesh = gameObject.GetComponent<MeshFilter>().mesh; 
        sh.mesh = bombMesh;

        var nsh = bombShatterTriangleParticle.shape;
        nsh.mesh = bombMesh;





    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(rotSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Name" + collision.gameObject.name);

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


        if (collision.gameObject.transform.parent.gameObject.name == "XRRig")
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
        } else
        {            
            //spawn particles from mesh for blue effect
            Instantiate(bombShatterParticle, transform.position, Quaternion.identity);
            Instantiate(bombShatterTriangleParticle, transform.position, Quaternion.identity);
            
        }
        
        
        Destroy(gameObject);
    }
}
