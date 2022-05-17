using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRound : MonoBehaviour
{
    public GameObject cube;

    public void StartRound(int currentRound)
    {
        //things we want 
        Debug.Log("starting sniper round");
        StartCoroutine(ShowCube());
    }


    IEnumerator ShowCube()
    {
        Debug.Log("start show cube");
        cube.SetActive(true);
        yield return new WaitForSeconds(3);
        //advance to next round
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
