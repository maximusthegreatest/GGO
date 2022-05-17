using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;
using TMPro;

public enum GameMode { Default, GunGame, Settings, Dead };

public class Cardinal : MonoBehaviour
{
    public static Cardinal instance;
    public static event Action<GameMode> OnGameModeChanged;

   

    public Player player;

    public BombSpawner bombSpawner;

    public LaserSpawner laserSpawner;

    public GameMode currentMode;

    public GameObject deathScreen;

    public TextMeshProUGUI roundCounter;

    private int currentRound = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMode = GameMode.Default;
    }

    // Update is called once per frame
    void Update()
    {        
        /*
        //Debug.Log("Current Mode: " + currentMode);
        if (currentMode != GameMode.Dead)
        {
            CheckGameState();
        } else
        {
            //show death screen
        }
        */
    }

    public void IncrementRound()
    {
        currentRound++;
        roundCounter.text = "Round: " + currentRound;
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

    public void ResetSystem()
    {
        currentRound = 0;
    }

    public void UpdateGameMode(GameMode newGameMode)
    {
        currentMode = newGameMode;

        switch(newGameMode)
        {
            case GameMode.Default:
                break;
            
            case GameMode.GunGame:                
                break;

            case GameMode.Settings:
                break;

            case GameMode.Dead:
                Death();
                break;
        }

        OnGameModeChanged?.Invoke(newGameMode);
    }


    public void CheckGameState()
    {
        //check whether we are alive or not(get from player script)
        
        if(player.health > 0)
        {
            switch(currentMode)
            {
                case GameMode.GunGame:
                    GunGame();
                    break;
                case GameMode.Settings:
                    Debug.Log("Settings mode");
                    break;
                default:
                    Debug.Log("Regular default mode");
                    break;
            }
            
        } else
        {
            Death();
            currentMode = GameMode.Dead;            
        }
        
    }

   
    public void GunGame()
    {
        
        
        
        //break down this logic in a way that is condensed into it's own unit

        
        //Debug.Log("Gun Game is running");
        //get bomb game object and fire bombs
        
        //bombSpawner.Spawn();
        
        //laserSpawner.Spawn();

        //get laser gameobject and fire lasers
    }

    public void Death()
    {
        Debug.Log("Player is dead");
        Restart();
        //fade to black, show you are dead symbol

        //pointer button for quit and restart
    }


    public void Restart()
    {
        //this happens in the backgroud, they have to click restart to go back to default mode 
        
        //wipe all bombs and lasers

        Bomb[] bombs = GameObject.FindObjectsOfType<Bomb>();
        Debug.Log("restarting");

        foreach(Bomb bomb in bombs)
        {            
            Destroy(bomb.gameObject);
        }

        Laser[] lasers = GameObject.FindObjectsOfType<Laser>();

        foreach (Laser laser in lasers)
        {
            Destroy(laser.gameObject);
        }

        //reset death gun

        //reset player health
        player.health = 100;

        //show the deathscreen gameobject and move it in front of the player
        GameObject camera = GameObject.Find("Camera");

        deathScreen.transform.position = camera.transform.position + camera.transform.forward * 2;
        deathScreen.transform.rotation =  Quaternion.LookRotation( deathScreen.transform.position - camera.transform.position);                
        deathScreen.SetActive(true);
        



    }
}
