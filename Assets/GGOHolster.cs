using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Samples;
using UnityEngine;

public class GGOHolster : MonoBehaviour
{

    public GameObject holsterItem;
    private Holster holster;
    
    // Start is called before the first frame update
    void Start()
    {
        holster = gameObject.GetComponent<Holster>();
        HolsterItem();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void HolsterItem()
    {                    
            var itemGrabbable = holsterItem.GetComponent<HVRGrabbable>();
            if (itemGrabbable)
            {
                //holster.TryGrab(itemGrabbable, true, true);
            }                    
    }

    
}
