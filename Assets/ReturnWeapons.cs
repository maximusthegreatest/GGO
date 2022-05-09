using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HurricaneVR.Framework.Core.Grabbers;

public class ReturnWeapons : MonoBehaviour
{
    
    public UnityEvent ReturnWeaponEvent;

    public List<GameObject> objectsToReturn =  new List<GameObject>();

    public float timeToReturn;
    
    private Collider _areaCollider;

    // Start is called before the first frame update
    void Start()
    {
        //_areaCollider = GetComponent<Collider>();        
        //StartCoroutine(EnableAfterSeconds(0.5f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnableAfterSeconds(float seconds)
    {        
        yield return new WaitForSeconds(seconds);
        _areaCollider.enabled = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("retrievable " + other.gameObject.name);
        if(objectsToReturn.Contains(other.gameObject))
        {
            //start return routine
            Debug.Log("bound routine " + other.gameObject.name);
            ReturnWeapon returnWep = other.gameObject.GetComponent<ReturnWeapon>();
            if (!returnWep.returnWepRunning)
            {                
                returnWep.ReturnWeaponToHolsterSetup(timeToReturn);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        return;
        Debug.Log("trigger" + col.gameObject);
        //get col gameobject 
        if(objectsToReturn.Contains(col.gameObject))
        {
            Debug.Log("inside trigger");
            
            //UnityEvent returnWeaponEvent = new UnityEvent();

            ReturnWeapon returnWep = col.gameObject.GetComponent<ReturnWeapon>();
            if(!returnWep.returnWepRunning)
            {
                returnWep.ReturnWeaponToHolsterSetup(timeToReturn);
            }
            
            
            //ReturnWeaponEvent.AddListener(returnWep.ReturnWeaponListener);
        }
        //if that gameobject is in objects to return
            //invoke an event with the timer
            //then subscribe to that event in the GO 
            //if that event happens check the return timer, then if timer runs out return to an empty holster
    }
}
