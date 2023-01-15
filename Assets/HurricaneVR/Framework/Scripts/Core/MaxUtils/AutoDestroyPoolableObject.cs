using System;
using System.Collections;
using UnityEngine;

namespace HurricaneVR.Framework.Core.MaxUtils
{
    public class AutoDestroyPoolableObject : PoolableObject
    {

        public float autoDestroyTime = 5;


        private const string DisableMethodName = "Disable";

        public virtual void Disable()
        {            
            base.OnDisable();
            gameObject.SetActive(false);                        
        }

        public virtual void OnEnable()
        {
            CancelInvoke(DisableMethodName);
            Invoke(DisableMethodName, autoDestroyTime);
        }

    }
}
