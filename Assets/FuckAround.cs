using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Shared;
using UnityEngine.VFX;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Weapons;

public class FuckAround : MonoBehaviour
{

    HVRPlayerInputs playerInputs;
    public Transform bulletFirePoint;
    public AudioClip pewClip;
    public HVRRecoil RecoilComponent;
    public GameObject emojiPrefab;

    bool hasFired;
    // Start is called before the first frame update
    void Start()
    {
        playerInputs = GetComponent<HVRPlayerInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        return;
        if(playerInputs.IsRightHoldActive)
        {
            
            if(playerInputs.IsRightTriggerHoldActive && !hasFired)
            {
                //this will trigger a ton per trigger pull so need to limit it somehow
                //wait for 
                FingerCannon();

               
            }

            if (!playerInputs.IsRightTriggerHoldActive)
            {
                //reset fired
                hasFired = false;
            }



        }
    }

    void FingerCannon()
    {
        hasFired = true;        
        SFXPlayer.Instance.PlaySFX(pewClip, transform.position);

        RecoilComponent.Recoil();

        GameObject bullet = Instantiate(emojiPrefab, bulletFirePoint.position, Quaternion.identity);        
        bullet.transform.rotation = Quaternion.LookRotation(bulletFirePoint.forward);
        //Debug.Break();
        bullet.SetActive(true);
        
        
        


        

        Debug.Log("bang bang");
    }
}
