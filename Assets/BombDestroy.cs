using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Core.Utils;

public class BombDestroy : HVRDestructible
{
    public Bomb bomb;
    public ParticleSystem bombShatterTriangleParticle;
    public ParticleSystem bombShatterParticle;
    public ParticleSystem bombShatterTriangleParticleRed;
    public ParticleSystem bombShatterParticleRed;


    [SerializeField]
    private AudioClip bombExplosionSound;

    public override void Destroy()
    {
        Debug.Log("Destroying from inside bomb destroy");
        
        if (bomb.hitPlayer)
        {
            //duplicate the particle but make it red
            Instantiate(bombShatterParticleRed, transform.position, Quaternion.identity);
            Instantiate(bombShatterTriangleParticleRed, transform.position, Quaternion.identity);
            SFXPlayer.Instance.PlaySFX(bombExplosionSound, transform.position, 1f, 10f);
        }
        else
        {
            //spawn particles from mesh for blue effect
            Instantiate(bombShatterParticle, transform.position, Quaternion.identity);
            Instantiate(bombShatterTriangleParticle, transform.position, Quaternion.identity);
            SFXPlayer.Instance.PlaySFX(bombExplosionSound, transform.position, 1f, 10f);
            Debug.Break();
        }


        Destroy(gameObject);
    }
}
