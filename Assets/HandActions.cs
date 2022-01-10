using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;


public class HandActions : MonoBehaviour
{
    public Saber saber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabbedAction(HVRGrabberBase grabber, HVRGrabbable grabbable)
    {
        
        /*

        if(grabbable.gameObject.name == "Saber")
        {
            foreach (SaberCapsule capsule in saber.capsules)
            {
                capsule.SpawnSaberCapsuleFollower();
            }
        }
        */
    }
}
