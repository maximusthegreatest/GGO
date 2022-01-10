using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saber : MonoBehaviour
{
    
    
    public float saberGrowSpeed = 0.00001f;
    public List<SaberCapsule> capsules;
    public Transform bladeTip;
    public Transform bladeBase;
    public Transform hilt;
    public Collider saberCollider;
    public SaberTrail trail;
    public float desiredDuration;

    [SerializeField]
    private Animator saberAnimator;

    [SerializeField]
    private Animator saberBladeAnimator;

    private GameObject _laser;
    private Vector3 fullSize;
    private float _colliderFullSize;
    private bool _active;
    private bool _pressedA;
    private CapsuleCollider _cc;
    private Rigidbody _rb;
    private float elapsedTime;


    private void Awake()
    {
        capsules = new List<SaberCapsule>();
    }

    // Start is called before the first frame update
    void Start()
    {

        _laser = transform.Find("Blade").gameObject;
        fullSize = _laser.transform.localScale;
        _cc = _laser.GetComponent<CapsuleCollider>();
        _colliderFullSize = _cc.height;
        //_cc.height = 0;
        //_laser.transform.localScale = new Vector3(fullSize.x, fullSize.y, 0);
        _rb = GetComponent<Rigidbody>();

        //_rb.centerOfMass = hilt.transform.position;

        

    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("saber velocity" + _rb.velocity);
        //ControlLaser();

    }


    public void SetTrail(bool setStart)
    {
        trail.SetStartPos(setStart);        
        trail.hasTrail = !trail.hasTrail;
    }

    public void SetPressed()
    {
        
        _pressedA = !_pressedA;
        elapsedTime = 0;

        if(_pressedA)
        {
            saberAnimator.SetBool("SaberOn", true);
            saberBladeAnimator.SetBool("BladeUp", true);
        } else
        {
            saberAnimator.SetBool("SaberOn", false);
            saberBladeAnimator.SetBool("BladeUp", false);
        }

    }


    void ControlLaser()
    {
        
        //Grow Saber
        if (_pressedA && _laser.transform.localScale.z < fullSize.z)
        {
            elapsedTime += Time.deltaTime;

            

            Debug.Log("elapsed Time " + elapsedTime + " desiredDur" + desiredDuration);

            float percentageComplete = elapsedTime / desiredDuration;
            percentageComplete = Mathf.Clamp(percentageComplete, 0, 1);
            Debug.Log("Percent complete rising" + percentageComplete);
            

            _laser.SetActive(true);


                //Debug.Log("Lerp Value: " + Mathf.Lerp(_laser.transform.localScale.z, fullSize.z, percentageComplete));

                _laser.transform.localScale = new Vector3(fullSize.x, fullSize.y, Mathf.Lerp(_laser.transform.localScale.z, fullSize.z, percentageComplete));
                //_laser.transform.localScale += new Vector3(0, 0, saberGrowSpeed);
            

            

            //as we increase, we need to activate the followers that we are passing through
            foreach (SaberCapsule capsule in capsules)
            {
                if (bladeTip.position.y > capsule.transform.position.y)
                {
                    //spawn the follower
                    capsule.SpawnSaberCapsuleFollower();
                    
                }

            }


            //_cc.height += saberGrowSpeed;
        } else if (_pressedA == false && _laser.transform.localScale.z > 0)
        {

           
            //All this logic is for changing the scale of the blade

            //we're not using the capsule followers anymore so we don't need to worry about the blade tip
            //well we need it for the saber trail

            //somehow we need to match the blade tip location with the animation

            //alternativly we could have a check system to only activate the blade tip on full extension


            
            elapsedTime += Time.deltaTime;

            float percentageComplete = elapsedTime / desiredDuration;
            if(percentageComplete > 1)
            {
                percentageComplete = 1;
            }
            //percentageComplete = Mathf.Clamp(percentageComplete, 0, 1);


            Debug.Log("Percent complete falling" + percentageComplete);

            Debug.Log("Laser scale " + _laser.transform.localScale.z);
            _laser.transform.localScale = new Vector3(fullSize.x, fullSize.y, Mathf.Lerp(_laser.transform.localScale.z, 0, percentageComplete));

            if(_laser.transform.localScale.z < 0.001f)
            {
                _laser.transform.localScale = new Vector3(fullSize.x, fullSize.y, 0);
            }


            //_laser.transform.localScale += new Vector3(0, 0, -saberGrowSpeed);

            //as we increase, we need to activate the followers that we are passing through
            foreach (SaberCapsule capsule in capsules)
            {
                if (bladeTip.position.z < capsule.transform.position.y)
                {
                    capsule.DespawnSaberCapsuleFollower();
                }

            }
            //_cc.height -= saberGrowSpeed;
        } else if (_pressedA == false)
        {
            
            _laser.SetActive(false);
        }
    }


    public void RetractBlade()
    {
        _pressedA = false;
        _laser.transform.localScale = new Vector3(fullSize.x, fullSize.y, 0);
    }


    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.name == "SaberFollower(Clone)")
        {
            Physics.IgnoreCollision(collision.collider, saberCollider);
        }

            if (collision.gameObject.name == "mine(Clone)")
        {
            Debug.Log("saber destroy bomb");
            collision.gameObject.GetComponent<Bomb>().DestroyBomb();
        }

        if (collision.gameObject.name == "LaserBulletNew(Clone)")
        {
            Debug.Log("wrong lazer");
            Physics.IgnoreCollision(collision.collider, saberCollider);
            return;

            // Time.timeScale = 0;

            Debug.Log("laser hit blade");
            Vector3 point = collision.contacts[0].point;
            /*
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(cube.GetComponent<BoxCollider>());
            cube.transform.position = point;
            */
            //Debug.DrawRay(point, collision.contacts[0].normal, Color.red, 100);
            //Time.timeScale = 0;
            //Vector3 direction = Vector3.Reflect(collision.gameObject.transform.forward, collision.contacts[0].normal);
            Vector3 direction = transform.forward;

            collision.gameObject.GetComponent<LaserBullet>().SaberDestroyLaser(_rb.velocity, direction, point); 
            
            //if the saber is moving fast enough 
                
                //then calculate the direction and fire it towards that direction at deflect speed
                //Vector3 direction = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
                //Vector3 point = collision.contacts[0].point;
                //collision.gameObject.GetComponent<Laser>().DestroyLaser();
            //else

            //collision.gameObject.GetComponent<Laser>().DestroyLaser();
        }
    }


    void OnCollisionExit(Collision collisionInfo)
    {
        print("Collision Out: " + gameObject.name);
    }


    public void HolsterAction()
    {
        RetractBlade();
    }


}

