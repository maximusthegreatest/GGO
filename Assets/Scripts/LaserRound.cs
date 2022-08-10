using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRound : MonoBehaviour
{
    
    public LaserSpawner laserSpawner;
    public BombSpawner bombSpawner;

    [SerializeField]
    private List<LaserSetting> laserRoundSettings = new List<LaserSetting>();

    [SerializeField]
    private List<BombSetting> bombRoundSettings = new List<BombSetting>();

    private int laserCount;
    private int currentLaserCount;

    private int bombCount;
    private int currentBombCount;
    public int currentBombExplodedCount;

    private float laserMinSpawnTime;
    private float laserMaxSpawnTime;

    private float bombMinSpawnTime;
    private float bombMaxSpawnTime;

    private bool startLaserSpawning;
    private bool startBombSpawning;

    private bool hasSpawnedAllLasers;
    private bool hasSpawnedAllBombs;

    private bool startMoveRound;

    [SerializeField]
    private List<int> roundTimes = new List<int>();

    int myCurrentRound;
    int delayTime;


    public void StartRound(int currentRound)
    {

        
        
        Debug.Log("Starting laser round " + currentRound);

        startMoveRound = false;
        
        //things we want 
        currentLaserCount = 0;
        laserCount = laserRoundSettings[currentRound].laserCount;
        laserMaxSpawnTime = laserRoundSettings[currentRound].maxSpawnTime;
        laserMinSpawnTime = laserRoundSettings[currentRound].minSpawnTime;
        hasSpawnedAllLasers = false;
        


        startLaserSpawning = true;

        myCurrentRound = currentRound;

        currentBombCount = 0;
        bombCount = bombRoundSettings[currentRound].bombCount;
        bombMaxSpawnTime = bombRoundSettings[currentRound].maxSpawnTime;
        bombMinSpawnTime = bombRoundSettings[currentRound].minSpawnTime;

        hasSpawnedAllBombs = false;
        currentBombExplodedCount = 0;


        startLaserSpawning = true;
        startBombSpawning = true;

        Debug.Log("round " + currentRound + " laser count: " + laserCount);
        
    }


    private void Update()
    {
        if (GunGame.instance.currentState != GunGameState.LaserRound) {

            return;
        
        }

        
        if(startLaserSpawning)
        {
            Debug.Log("current laser count " + currentLaserCount);
            if(currentLaserCount < laserCount)
            {
                int spawnReturn = laserSpawner.Spawn(currentLaserCount, laserMaxSpawnTime, laserMinSpawnTime);
                Debug.Log("spawn return " + spawnReturn);
                if(spawnReturn != 0)
                {
                    currentLaserCount = spawnReturn;
                }
                //currentLaserCount++;
            } else
            {
                hasSpawnedAllLasers = true;
                startLaserSpawning = false;                
            }
            //if currentLaserCount and currentBombCount equal the respective counts, then 
        }


        if (startBombSpawning)
        {
            if (currentBombCount < bombCount)
            {
                int spawnReturn = bombSpawner.Spawn(currentBombCount, bombMaxSpawnTime, bombMinSpawnTime);
                if (spawnReturn != 0)
                {
                    currentBombCount = spawnReturn;
                }
                //currentBombCount++;
            }
            else
            {
                hasSpawnedAllBombs = true; 
                startBombSpawning = false;                
            }            
        }

        if(hasSpawnedAllBombs && hasSpawnedAllLasers && !startMoveRound )
        {            
            if(currentBombExplodedCount == bombCount)
            {
                //then wait for time, then change gunmode round
                delayTime = roundTimes[myCurrentRound];

                StartCoroutine(WaitForNextRound());
            }
            
        }



        
    }


    IEnumerator WaitForNextRound()
    {
        startMoveRound = true;
        yield return new WaitForSeconds(delayTime);
        //advance to next round  
        Debug.Log("moving to sniper round " + Time.time);
        GunGame.instance.SetGunGameState(GunGameState.SniperRound);
        
    }


}








