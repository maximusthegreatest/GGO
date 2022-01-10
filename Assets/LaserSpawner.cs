using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{

    public float laserCooldownTime;    
    public Vector3 minSpawnDistance;
    public float lastSpawnTime;
    public GameObject player;
    public GameObject laser;

    [Header("Laser Spawn Time")]
    public float minSpawnTime;
    public float maxSpawnTime;


    private CharacterController controller;

    List<Vector3> laserSpawns;

    List<Vector3> LocalVertices;
    List<Vector3> GlobalVertices;
    List<Vector3> CornerVertices;
    
    //float radius = 0.005f;
    


    // Start is called before the first frame update
    void Start()
    {        
        //set up spawn points on the laser spawner
        GlobalVertices = new List<Vector3>();
        laserSpawns = new List<Vector3>();
        GetVertices();


        laserCooldownTime = 0;
        lastSpawnTime = 0;
        
        player = GameObject.Find("PlayerController");
        controller = player.GetComponent<CharacterController>();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 GetLaserSpawnPoint()
    {
        //get random vert from Global verts

        //TODO make sure its also not within a certain range of any of the current spawn points
        Vector3 spawnPoint = GetRandomLaserSpawnPoint();
        while (laserSpawns.Contains(spawnPoint)) 
        {
            GetLaserSpawnPoint();
        }

        return spawnPoint;


        

        //check that it is not the same as the last one and that is a minimum distance away 


        //return that
    }

    Vector3 GetRandomLaserSpawnPoint()
    {
        int myIndex = Random.Range(0, GlobalVertices.Count);
        Vector3 spawnPoint = GlobalVertices[myIndex];
        return spawnPoint;
    }

    void GetVertices()
    {
        LocalVertices = new List<Vector3>(GetComponent<MeshFilter>().mesh.vertices);

        foreach(Vector3 point in LocalVertices)
        {
            GlobalVertices.Add(transform.TransformPoint(point));
        }
    }



    /*
    private void OnDrawGizmos()
    {
        return;
        if (GlobalVertices == null)
            return;

        Gizmos.color = Color.yellow;
        foreach(Vector3 point in GlobalVertices)
        {
            Gizmos.DrawSphere(point, radius);
        }

        Gizmos.color = Color.green;
        foreach (Vector3 point in CornerVertices)
        {
            Gizmos.DrawSphere(point, radius);
        }
    }
    */

    public void Spawn()
    {
        
        if (lastSpawnTime + laserCooldownTime <= Time.time)
        {
            Debug.Log("spawning laser");
            Vector3 spawnLocation = GetLaserSpawnPoint();

            //set a max size of the 
            
            //Vector3 direction = player.transform.position - spawnLocation;
            Instantiate(laser, spawnLocation, Quaternion.Euler(transform.forward));
            
            //add spawnLocation to the laserSpawns list, as long as it hasn't gone over a certain size
            if (laserSpawns.Count < 5)
            {
                laserSpawns.Add(spawnLocation);
            }
            else
            {
                laserSpawns.RemoveAt(0);
                laserSpawns.Add(spawnLocation);
            }


            lastSpawnTime = Time.time;
            laserCooldownTime = Random.Range(minSpawnTime, maxSpawnTime);
        }




    }
}
