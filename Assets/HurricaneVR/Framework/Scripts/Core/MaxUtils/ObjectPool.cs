using System;
using System.Collections.Generic;
using UnityEngine;

namespace HurricaneVR.Framework.Core.MaxUtils
{
    public class ObjectPool : MonoBehaviour
    {
        private PoolableObject _prefab;
        private List<PoolableObject> _availableObjects;
        private GameObject _objectPoolParent;
        

        private ObjectPool(PoolableObject prefab, int size)
        {
            _prefab = prefab;
            _availableObjects = new List<PoolableObject>(size);            
        }

        public static ObjectPool CreateInstance(PoolableObject prefab, int size)
        {
            ObjectPool pool = new ObjectPool(prefab, size);
            GameObject objectPoolParent = new GameObject(prefab.name + " pool");
            
            pool.CreateObjects(objectPoolParent, size, pool);
            pool._objectPoolParent = objectPoolParent;


            return pool;

        }

        public void ReturnObjectToPool(PoolableObject poolableObject)
        {
            _availableObjects.Add(poolableObject);
            
            //add as child of parent go
            //instance.transform.SetParent(transform, false);
        }

        public PoolableObject GetObject()
        {

            if (_availableObjects.Count == 0) // auto expand pool size if out of objects
            {
                CreateObject();
            }

            if (_availableObjects.Count > 0)
            {
                PoolableObject instance = _availableObjects[0];
                _availableObjects.RemoveAt(0);
                instance.gameObject.SetActive(true);
                return instance;
            }
            else
            {
                return null;
            }
        }


        private void CreateObject()
        {
            PoolableObject poolableObject = Instantiate(_prefab, Vector3.zero, Quaternion.identity, this._objectPoolParent.transform);
            poolableObject.parentObj = this._objectPoolParent;
            poolableObject.Parent = this;
            poolableObject.gameObject.SetActive(false);
        }


        private void CreateObjects(GameObject parent, int size, ObjectPool poolInstance)
        {            
            for (int i = 0; i < size; i++)
            {                
                PoolableObject poolableObject = Instantiate(_prefab, Vector3.zero, Quaternion.identity, parent.transform);
                poolableObject.parentObj = parent;
                poolableObject.Parent = poolInstance;                
                poolableObject.gameObject.SetActive(false);

            }
        }

    
    }
}
