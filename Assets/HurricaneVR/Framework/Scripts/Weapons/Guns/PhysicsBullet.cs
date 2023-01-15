using System;
using System.Collections;
using HurricaneVR.Framework.Core.MaxUtils;
using UnityEngine;
using UnityEngine.Events;



namespace HurricaneVR.Framework.Weapons.Guns
{
    public class PhysicsBullet : AutoDestroyPoolableObject {
        
        public float raycastLength;
        public float bulletVelocity;
        public Vector3 bulletDirection;
        

        
        private Rigidbody _rb;
        private Collider _myCollider;
        
        [SerializeField]
        private Transform bulletOrigin;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            bulletOrigin = GameObject.FindWithTag("Fn_BulletOrigin").transform;
        }

        // Start is called before the first frame update
        void Start()
        {
            
            //_myCollider = GetComponent<Collider>();
        }

        public override void OnEnable()
        {
            base.OnEnable();             
            _rb.AddForce(bulletOrigin.forward * bulletVelocity, ForceMode.Impulse);            
        }

        // Update is called once per frame
        void Update()
        {
            raycastLength = bulletVelocity * Time.deltaTime;
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out var hit, raycastLength))
            {
                
            }
            
        }

        private void FixedUpdate()
        {
            //_rb.AddForce(gameObject.transform.forward * bulletVelocity * Time.deltaTime, ForceMode.Impulse);
        }


        void OnCollisionEnter(Collision collision)
        {
            //Debug.Log("bullet collision " + collision.gameObject.name);
            ContactPoint contact = collision.GetContact(0);

            if(collision.gameObject.TryGetComponent<Renderer>(out Renderer renderer))
            {
                BulletImpactManager.Instance.SpawnBulletImpact(contact.point, contact.normal, renderer.sharedMaterial);
            } else
            {
                BulletImpactManager.Instance.SpawnBulletImpact(contact.point, contact.normal, null);
            }



            OnDisable();
        }


        public override void OnDisable()
        {
            _rb.velocity = Vector3.zero;
            
            
            base.OnDisable();
            gameObject.SetActive(false);
        }

       

    }
}
