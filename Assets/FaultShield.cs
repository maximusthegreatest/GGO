using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaultShield : MonoBehaviour
{

    public DeathGun deathGun;
    public Material damageMat;
    public Material hitMat;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Bullet(Clone)")
        {
            ParticleSystemRenderer hexagon = GetComponentInParent<ParticleSystemRenderer>();
            Debug.Log("hex mat " + hexagon.material.name);

            if (hexagon.material.name.Contains(damageMat.name))
            {
                deathGun.DamageDeathGun(10);
                Debug.Log(" Death Gun Health: " + deathGun.health);
                hexagon.material = hitMat;
            }
            

        }
    }
}

    
