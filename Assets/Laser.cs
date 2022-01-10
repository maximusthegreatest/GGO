using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Rigidbody rb;
    public float thrust;
    public int damage;
    public float timeAlive;
    public float birth;

    private LineRenderer _lr;

    // Start is called before the first frame update
    void Start()
    {        
        _lr = GetComponent<LineRenderer>();
        
        

        //move the bomb towards the player         
        rb = GetComponent<Rigidbody>();
        thrust = Random.Range(350f, 450f);
        rb.AddForce(transform.forward * thrust);
        birth = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _lr.SetPosition(0, transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                _lr.SetPosition(1, hit.point);
            }
        }
        else
        {
            _lr.SetPosition(1, transform.position + (transform.forward * 5000));
        }

        if (birth + timeAlive < Time.time)
        {
            Destroy(gameObject);
        }
    }


    public void DestroyLaser()
    {
        Debug.Log("destorying laser");
        //TODO play sound and show sparks particle effect
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Name" + collision.gameObject.name);

        if (collision.gameObject.name == "MadsonD9")
        {
            DamagePlayer();
            DestroyLaser();
            return;
        }


        if (collision.gameObject.name == "Bullet(Clone)")
        {
            DestroyLaser();
            return;
        }


        if (collision.gameObject.transform.parent.gameObject.name == "XRRig" || collision.gameObject.name == "MadsonD9")
        {
            //Debug.Log("Is hit?");
            Destroy(gameObject);
            DamagePlayer();
        }
    }

    private void DamagePlayer()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.Damage(damage);
    }
}
