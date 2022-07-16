using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{

    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public bool spawnedLaser;

    public LaserSpawner laserSpawner;


    public Animator deathGunAnimator;


    private void Awake()
    {
        LaserSpawner.LaserSpawned += OnLaserSpawned;
        deathGunAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new AiStateMachine(this);
        

        stateMachine.RegisterState(new AiSniperState());
        stateMachine.ChangeState(initialState);

    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        GunGame.OnGunGameStateChanged += GunGameStateChanged;
    }

    private void OnLaserSpawned(bool spawned)
    {
        Debug.Log("laser spawned from agent");
        spawnedLaser = true;
    }

    void GunGameStateChanged(GunGameState newState)
    {
        switch(newState)
        {
            case GunGameState.LaserRound:
                stateMachine.ChangeState(AiStateId.ShootRifle);
                break;

        }
    }
}
