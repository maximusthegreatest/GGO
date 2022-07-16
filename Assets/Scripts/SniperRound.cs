using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRound : MonoBehaviour
{
    public GameObject cube;
    

    [SerializeField]
    private List<ParticleSystemRenderer> damagableShieldHexagons;
    bool wasHit;
    [SerializeField]
    private Material workingShieldMat;
    [SerializeField]
    private Material faultShieldMat;
    private ParticleSystemRenderer lastFaultedHexagon;

    public void StartRound(int currentRound)
    {
        //things we want 
        Debug.Log("starting sniper round");
        
        //startcountdown of round

        //start manipulation of shield

        
        
        StartCoroutine(StartRoundTime());
    }

    private IEnumerator ShieldFault()
    {
        //swap all shield icons back to blue mat
        foreach (ParticleSystemRenderer hexagon in damagableShieldHexagons)
        {
            hexagon.material = workingShieldMat;
        }


        //make sure that is not he last one
        ParticleSystemRenderer hexagonToFault = damagableShieldHexagons[Random.Range(0, damagableShieldHexagons.Count)];
        while (hexagonToFault == lastFaultedHexagon)
        {
            hexagonToFault = damagableShieldHexagons[Random.Range(0, damagableShieldHexagons.Count)];
        }

        lastFaultedHexagon = hexagonToFault;

        hexagonToFault.material = faultShieldMat;
        yield return new WaitForSeconds(5f);

        //end all the coroutines on the hit or the end round
        StartCoroutine(ShieldFault());

        //pick a random shield hexagon
        //make sure it's not last one picked
        //swap the material 
        //hold that mat for shield fault time referenced via dataobject for the round
        //wait here
    }
    

    



    IEnumerator StartRoundTime()
    {
        Debug.Log("starting round time");
        cube.SetActive(true);
        StartCoroutine(ShieldFault());
        
        //this needs to pull from a dataobject
        yield return new WaitForSeconds(20f); 
        //advance to next round
        if (!wasHit)
        {
            NextRound();
        }
       
    }


    public void NextRound()
    {
        StopAllCoroutines();

        //swap all shield icons back to blue mat
        foreach (ParticleSystemRenderer hexagon in damagableShieldHexagons)
        {
            hexagon.material = workingShieldMat;
        }

        

        cube.SetActive(false);
        //pretend it's final round
        Cardinal.instance.IncrementRound();
        GunGame.instance.SetGunGameState(GunGameState.LaserRound);
        Debug.Log("end show cube");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
