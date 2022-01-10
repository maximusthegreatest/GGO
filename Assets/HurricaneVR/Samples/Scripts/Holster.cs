using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Samples
{
    

    public class Holster : HVRSocket
    {
        public bool hasItem;
        public bool firstGrab;
        
        protected override Quaternion GetRotationOffset(HVRGrabbable grabbable)
        {
            var holsertOrientation = grabbable.GetComponent<HolsterOrientation>();
            if (holsertOrientation && holsertOrientation.Orientation)
                return holsertOrientation.Orientation.localRotation;
            return base.GetRotationOffset(grabbable);
        }

        protected override Vector3 GetPositionOffset(HVRGrabbable grabbable)
        {
            var holsertOrientation = grabbable.GetComponent<HolsterOrientation>();
            if (holsertOrientation && holsertOrientation.Orientation)
                return holsertOrientation.Orientation.localPosition;
            return base.GetPositionOffset(grabbable);
        }

        public void SetItem()
        {
            hasItem = !hasItem;            
        }

        public void DoHolsterAction(HVRGrabberBase grabber, HVRGrabbable grabbable)
        {
            
            
                
            //call spawn follower
            if(grabbable.gameObject.name == "Saber" || grabbable.gameObject.name == "SaberFinal")
            {
                    
                Saber saber = grabbable.gameObject.GetComponent<Saber>();
                foreach(SaberCapsule capsule in saber.capsules)
                {
                    
                    capsule.DespawnSaberCapsuleFollower();
                }
                        
            }

            
            
            
            //if gameobject has a holsteraction method, then call it
            if(grabbable.gameObject.GetComponent<HolsterAction>() != null)
            {
                HolsterAction holsterAction = grabbable.gameObject.GetComponent<HolsterAction>();
                holsterAction.DoAction();
            }

        }
    }
}