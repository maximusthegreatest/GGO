using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Weapons.Guns;

public class SocketAutoSpawnCheck : MonoBehaviour
{
    //this is a failed attempt at generalizing the instantiation check.
    //idk how to make it general
    
    public HVRPistol pistol;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool Check()
    {
        if(pistol.autoReloadTime + pistol.magSpawnTime > Time.time)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
