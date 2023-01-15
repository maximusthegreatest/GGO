using System;
using System.Collections.Generic;
using UnityEngine;

namespace HurricaneVR.Framework.Core.MaxUtils
{
    
        public class PoolableObject : MonoBehaviour
        {
            public ObjectPool Parent;
            public GameObject parentObj;

            public virtual void OnDisable()
            {                               
                Parent.ReturnObjectToPool(this);
                this.transform.SetParent(parentObj.transform, false);
        }

        }
    
}
