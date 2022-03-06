using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeetShooter : MonoBehaviour
{
    public GameObject skeet;
    public Transform launchPosition;

    public Transform pointA;
    public Transform pointB;

    bool _isShooting;

    Vector3 randomPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootSkeet()
    {
        //check whether shooting or not
        _isShooting = !_isShooting;

        //if we are then shoot skeet object at random angles and speeds within a range
        if(_isShooting)
        {
            //shoot skeet
            StartCoroutine(ScheduleShootSkeet());
        }
        else
        {
            
        }
    }

    //coroutine

    IEnumerator ScheduleShootSkeet()
    {
        while(_isShooting)
        {
            //get a random force


            //get a random angle


            //get random wait for seconds between range
            yield return new WaitForSeconds(3);
            Fire();
        }
        
    }

    private void Fire()
    {
        //launch skeet at
        GameObject newSkeet = Instantiate(skeet, launchPosition);
        
        //rotate it so it faces our angle
        //Get random vector along line between two points
        Vector3 difference = pointA.position - pointB.position;

        Vector3 newDifference = difference * Random.Range(0.0f, 1.0f);

        randomPosition = pointA.position + newDifference;
        
        Destroy(newSkeet, 10f);
        Rigidbody rb = newSkeet.GetComponent<Rigidbody>();


        //it goes along the force vector
        newSkeet.transform.LookAt(randomPosition);

        
        

        //need to maybe delete it or 
        rb.AddForce(newSkeet.transform.forward * Random.Range(4, 8), ForceMode.Impulse);


    

    }

    private void OnDrawGizmos()
    {
        
    }
}
