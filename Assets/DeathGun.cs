using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathGun : MonoBehaviour
{
    public int health = 100;
    
    private Animator anim;



    private void Awake()
    {
        Cardinal.OnGameModeChanged += OnGameModeChanged;
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageDeathGun(int damageToDeal)
    {
        health -= damageToDeal;
    }


    private void OnGameModeChanged(GameMode gameMode)
    {
        
        if (gameMode == GameMode.GunGame)
        {
            //set animator bool fire to true
            anim.SetBool("Fire", true);
        }
        else
        {
            //stop gungame

        }
    }

}
