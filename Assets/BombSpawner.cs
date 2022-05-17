using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public float bombCooldownTime;

    public Vector3 spawnLocationA, spawnLocationB;

    public GameObject bomb;

    public bool spawnLocation;

    public float lastSpawnTime;

    public GameObject player;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        //generate a random number between 2-5 seconds
        bombCooldownTime = 0;
        lastSpawnTime = 0;
        spawnLocation = false;
        player = GameObject.Find("PlayerController");
        controller = player.GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Spawn(int currentBombCount, float minSpawnTime, float maxSpawnTime)
    {
        //check when last bomb was spawned, and check if cool down timer was hit 
        //check which spawner was used and alternate
        if(lastSpawnTime + bombCooldownTime <= Time.time)
        {
            if (spawnLocation)
            {                
                Vector3 playerPos = new Vector3(player.transform.position.x, controller.height * 1.5f, player.transform.position.z);                
                Vector3 direction = playerPos - spawnLocationA;
                Instantiate(bomb, spawnLocationA, Quaternion.LookRotation(direction));
                lastSpawnTime = Time.time;
                spawnLocation = false;
            }
            else
            {
                //spawn at spawn location B
                Vector3 playerPos = new Vector3(player.transform.position.x, controller.height * 1.5f, player.transform.position.z);
                Vector3 direction = playerPos - spawnLocationB;
                Instantiate(bomb, spawnLocationB, Quaternion.LookRotation(direction));
                lastSpawnTime = Time.time;
                spawnLocation = true;
            }

            bombCooldownTime = Random.Range(minSpawnTime, maxSpawnTime);
            return ++currentBombCount;
        } else
        {
            return 0;
        }
        

        

    }
}
