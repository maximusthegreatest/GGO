using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSniperState : AiState
{

    private bool isFiring;
    
   

    public void Enter(AiAgent agent)
    {
        Debug.Log("entering sniper state");
    }

    public void Exit(AiAgent agent)
    {
        throw new System.NotImplementedException();
    }

    public AiStateId GetId()
    {
        return AiStateId.ShootRifle;
    }

    public void Update(AiAgent agent)
    {
        //listen for laser to be spawned
        

        //if laser is spawned and animation not currently firing
        if(agent.spawnedLaser)
        {
            if(!isFiring)
            {
                isFiring = true;
                agent.spawnedLaser = false;
                agent.deathGunAnimator.SetTrigger("FireRifle");
                //get length of rifle fire animation
                agent.StartCoroutine(PlayRifleAnimation());
                

            }
            //play animation
            
            //have callback on animation that resets isFiring to 0
        }

        //if it's spawned then set is firing to true
        //play firing animation

        

    }

    IEnumerator PlayRifleAnimation()
    {
        yield return new WaitForSeconds(1.167f);
        isFiring = false;
    }
}
