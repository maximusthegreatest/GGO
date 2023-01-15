using System;
using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core.MaxUtils;
using UnityEngine;
using UnityEngine.Events;

namespace HurricaneVR.Framework.Weapons.Guns
{

    class BulletImpactManager : MonoBehaviour
    {

        public List<BulletImpact> bulletImpacts = new List<BulletImpact>();
        public Dictionary<Material, ObjectPool> bulletImpactDictionary = new Dictionary<Material, ObjectPool>();

        [SerializeField]
        private int particleSystemBuffer = 5;


        private static BulletImpactManager _instance;


        public static BulletImpactManager Instance
        {
            get
            {
                return _instance;
            }
            private set
            {
                _instance = value;
            }

        }


        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple bullet impact managers!. Destroying the newest one " + this.name);
                Destroy(this.gameObject);
                return;
            }

            Instance = this;

            foreach (BulletImpact bulletImpact in bulletImpacts)
            {
                bulletImpactDictionary.Add(bulletImpact.material, ObjectPool.CreateInstance(bulletImpact.collisionParticleSystem, particleSystemBuffer));
            }


        }

        public void SpawnBulletImpact(Vector3 position, Vector3 forward, Material hitMaterial)
        {
            if (bulletImpactDictionary.ContainsKey(hitMaterial))
            {
                DoSpawnBulletImpact(position, forward, bulletImpactDictionary[hitMaterial].GetObject());
            } else
            {
                //could do a default material but hold off for now
            }
        }


        private void DoSpawnBulletImpact(Vector3 position, Vector3 forward, PoolableObject bulletImpact)
        {
            bulletImpact.transform.position = position;
            bulletImpact.transform.forward = forward;
        }
    

        [System.Serializable]
        public class BulletImpact
        {
            public Material material;
            public PoolableObject collisionParticleSystem;
        }
        


    }
}
