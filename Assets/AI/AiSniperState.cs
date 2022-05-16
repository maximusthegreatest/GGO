using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSniperState : AiState
{
    public void Enter(AiAgent agent)
    {
       
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
        //this is where we will play the rifle firing animation

        //this needs knowledge of our cardinal system

    }
}
