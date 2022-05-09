using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Samples;
using HurricaneVR.Framework.Core;

public class ReturnWeapon : MonoBehaviour
{
    public List<Holster> holsters = new List<Holster>();
    public bool returnWepRunning;
    private IEnumerator _coroutine;
    [SerializeField]
    private HVRGrabbable _grabbable;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReturnWeaponToHolsterSetup(float time)
    {
        Debug.Log("Running routine");
        _coroutine = ReturnWeaponRoutine(time);
        StartCoroutine(_coroutine);

    }

    IEnumerator ReturnWeaponRoutine(float waitTime)
    {
        Debug.Log("inside wep routine");
        returnWepRunning = true;
        yield return new WaitForSeconds(waitTime);

        Holster emptyHolster = null;
        //return this gameobject to an open holster
        foreach (Holster holster in holsters)
        {
            //check if holster has an item already
            if (!holster.hasItem)
            {
                emptyHolster = holster;
            }
        }
        Debug.Log("made it to empty holster check");
        if (emptyHolster)
        {
            Debug.Log("Grab routine");
            emptyHolster.TryGrab(_grabbable, true, true);
            returnWepRunning = false;
        }
        yield break;
        //on grabbed cancel this routine if running
    }

    public void StopReturnWeapon()
    {
        if(returnWepRunning)
        {
            StopCoroutine(_coroutine);
            returnWepRunning = false;
        }
        
    }
}
