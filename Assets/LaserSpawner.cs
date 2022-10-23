using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RootMotion.FinalIK;

public class LaserSpawner : MonoBehaviour
{

    public static event Action<bool> LaserSpawned;

    public float laserCooldownTime;    
    public Vector3 minSpawnDistance;
    public float lastSpawnTime;
    public GameObject player;
    public GameObject laser;

    public Transform laserSpawnLocation;


    
    public VRIK ik;


    private CharacterController controller;
    [SerializeField]
    private float distanceWanted;

    
    


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

        distanceWanted = transform.position.z - player.transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        float diff =  transform.position.z - player.transform.position.z;
        //lets say the player has moved towards the laser spawner
        float adjustment = distanceWanted - diff;

        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z + adjustment);
    }

    Vector3 GetLaserHitPoint()
    {
        //get random vert from Global verts

        //TODO make sure its also not within a certain range of any of the current spawn points
        Vector3 hitPoint = GetRandomLaserSpawnPoint();
        while (laserSpawns.Contains(hitPoint)) 
        {
            GetLaserHitPoint();
        }

        return hitPoint;


        

        //check that it is not the same as the last one and that is a minimum distance away 


        //return that
    }

    Vector3 GetRandomLaserSpawnPoint()
    {
        int myIndex = UnityEngine.Random.Range(0, GlobalVertices.Count);
        Vector3 hitPoint = GlobalVertices[myIndex];
        return hitPoint;
    }


    void AddGlobalVertices()
    {
        foreach (Vector3 point in LocalVertices)
        {
            GlobalVertices.Add(transform.TransformPoint(point));
        }
    }

    void ClearGlobalVertices()
    {
        GlobalVertices.Clear();
    }

    void GetVertices()
    {
        LocalVertices = new List<Vector3>(GetComponent<MeshFilter>().mesh.vertices);
        AddGlobalVertices();
        
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

    

    public int Spawn(int currentLaserCount , float minSpawnTime, float maxSpawnTime)
    {
        return ++currentLaserCount;
        if (lastSpawnTime + laserCooldownTime <= Time.time)
        {
            ClearGlobalVertices();
            AddGlobalVertices();
            Debug.Log("spawning laser");
            Vector3 hitLocation = GetLaserHitPoint();
            Debug.Log("laser hit location " + hitLocation);

            //set a max size of the 

            //Vector3 direction = player.transform.position - spawnLocation;

            //swap this to spawn it at one point and then angle towards your spawnLocation
            //angle transform.forward to hitLocation

            //find the vector pointing from our position to the target
            Vector3 _direction = (hitLocation - laserSpawnLocation.position).normalized;

            //create the rotation we need to be in to look at the target
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);

            Instantiate(laser, laserSpawnLocation.position, _lookRotation);
            LaserSpawned?.Invoke(true);
            //right here we need to inform AiSniperState that we are firing

            //add spawnLocation to the laserSpawns list, as long as it hasn't gone over a certain size
            if (laserSpawns.Count < 5)
            {
                laserSpawns.Add(hitLocation);
            }
            else
            {
                laserSpawns.RemoveAt(0);
                laserSpawns.Add(hitLocation);
            }


            lastSpawnTime = Time.time;
            laserCooldownTime = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);            
            return ++currentLaserCount;
        } else
        {
            return 0;
        }




    }
}
