using HurricaneVR.Framework.Weapons;
using System.Collections;
using UnityEngine;
using HurricaneVR.Framework.Weapons.Guns;
using HurricaneVR.Framework.Core.MaxUtils;

public class PhysicBullet : PoolableObject
{

    public float raycastLength;
    public float bulletVelocity;
    
    public float bulletAliveTime;
    
    private float _elapsed;
    private Rigidbody _rb;
    private Collider _myCollider;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();        
        //_myCollider = GetComponent<Collider>();
        
       
    }

    private void OnEnable()
    {
        _elapsed = 0f;
        _rb.velocity = gameObject.transform.forward * bulletVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        
        _elapsed += Time.deltaTime;                    
        if(_elapsed > bulletAliveTime)
        {
            //Destroy(gameObject); converting to pool
            gameObject.SetActive(false);
        }

        raycastLength = bulletVelocity * Time.deltaTime;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out var hit, raycastLength))
        {
            var bomb = hit.collider.GetComponent<Bomb>();
            if (bomb)
            {
                //Debug.Log("Has bomb");
                //Destroy(bomb.gameObject);
            }
            else
            {
                //Debug.Log("No bomb");
            }
        }
        */
    }

    private void FixedUpdate()
    {
        //_rb.AddForce(gameObject.transform.forward * bulletVelocity * Time.deltaTime, ForceMode.Impulse);
    }




    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("bullet collision " + collision.gameObject.name);
        Destroy(gameObject);
    }
       

    public override void OnDisable()
    {
        _rb.velocity = Vector3.zero;
        base.OnDisable();
    }
}
