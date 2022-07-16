using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum GunGameState { LaserRound, SniperRound, MinionRound, MeleeRound  };

public class GunGame : MonoBehaviour
{

    public static GunGame instance;

    public static event Action<GunGameState> OnGunGameStateChanged;


    public GunGameState currentState;

    public int totalRounds;

    private bool gunGameRunning;
    [SerializeField]
    private LaserRound laserRound;

    [SerializeField]
    private SniperRound sniperRound;

    

    

    private void Awake()
    {
        Cardinal.OnGameModeChanged += OnGameModeChanged;
        instance = this;
    }

    private void OnGameModeChanged(GameMode gameMode)
    {
        Debug.Log("game mode changed");
        if (gameMode == GameMode.GunGame)
        {
            //start gungame
            if(!gunGameRunning)
            {
                gunGameRunning = true;
                SetGunGameState(GunGameState.LaserRound);
            }
            //make sure gungame is not running already
        } else
        {
            //stop gungame
        }
    }


    public void SetGunGameState(GunGameState newGunGameState)
    {
        Debug.Log("Setting gun game state " + newGunGameState.ToString());
        currentState = newGunGameState;

        int currentRound = Cardinal.instance.GetCurrentRound();

        if(currentRound >= totalRounds)
        {
            //exit gun game 
            Debug.Log("Game over");
            Cardinal.instance.UpdateGameMode(GameMode.Default);
            return;
        }

        switch (currentState)
        {
            case GunGameState.LaserRound:                
                laserRound.StartRound(currentRound);
                break;
            case GunGameState.SniperRound:
                sniperRound.StartRound(currentRound);
                //do sniper round
                break;
            case GunGameState.MinionRound:
                //do sniper round
                break;
            case GunGameState.MeleeRound:
                //do sniper round
                break;


        }


        OnGunGameStateChanged?.Invoke(newGunGameState);

        //set gun game state to laseround


        //Cardinal.instance.IncrementRound();

        //set up round specific settings


    }


    
}
