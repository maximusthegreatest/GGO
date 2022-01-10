using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberCapsule : MonoBehaviour
{
    public SaberCapsuleFollower follower;
    public SaberCapsuleFollower followerPrefab;
    public bool isTop;
    public bool isBottom;

    [SerializeField]
    private Saber _saber;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
        _saber = GameObject.Find("Saber").GetComponent<Saber>();
        _saber.capsules.Add(this);
        
        //SpawnSaberCapsuleFollower(); //wait to spawn this 
        //find where the holster action is to equip
        //then once thats done, loop  through the capsules and call spawnfollower
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSaberCapsuleFollower()
    {
        
        if(!follower)
        {
            Debug.Log("spawning follower");
            follower = Instantiate(followerPrefab);
            if(isTop)
            {
                Debug.Log("Reached Top");                
                _saber.SetTrail(true);
                
            }
            follower.transform.position = transform.position;
            follower.SetFollowTarget(this);
            follower.gameObject.SetActive(true);
        }
        

    }

    public void DespawnSaberCapsuleFollower()
    {
        
        if(follower)
        {
            Debug.Log("DeSpawning follower");
            Destroy(follower.gameObject);
            if (isBottom)
            {
                _saber.SetTrail(false);
            }
        }
        
    }
}
