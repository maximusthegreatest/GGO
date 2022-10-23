using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class AutoloadEvent : UnityEvent<float> { }

namespace HurricaneVR.Framework.Weapons.Guns
{
    
    public class HVRPistol : HVRGunBase
    {

        public AutoloadEvent autoLoadEvent = new AutoloadEvent();

        protected override void Awake()
        {
            base.Awake();
        }


    }
}