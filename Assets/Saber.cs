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
    public Transform convergencePoint;

    public AudioClip saberStartSound;
    public AudioClip saberRetractSound;
    public AudioClip saberSwingSound;
    public AudioClip saberHumSound;


    public GameObject decalPrefab;

    [SerializeField]
    private Animator saberAnimator;

    [SerializeField]
    private Animator saberBladeAnimator;
    [SerializeField]
    private GameObject _laser;
    private int fullSize = 1;
    private float _colliderFullSize;
    private bool _active;
    private bool _pressedA;
    private bool btnAnimRunning;
    [SerializeField]
    private CapsuleCollider _bladeCollider;
    private Rigidbody _rb;
    private float elapsedTime;
    [SerializeField]
    private float desiredDurationUp;
    [SerializeField]
    private float desiredDurationDown;
    [SerializeField]
    private float elapsedDownTime;
    [SerializeField]
    private float btnLastPressed;
    [SerializeField]
    private float btnUpAnimLength = .65f;
    [SerializeField]
    private AudioSource source;
    private Rigidbody rb;


    private void Awake()
    {
        capsules = new List<SaberCapsule>();
    }

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>(); 
        source.spatialBlend = 1;

        rb = gameObject.GetComponent<Rigidbody>();

        _laser.SetActive(false);
        //_laser = transform.Find("Blade").gameObject;
        //fullSize = _laser.transform.localScale;
        _bladeCollider = _laser.GetComponent<CapsuleCollider>();
        _colliderFullSize = _bladeCollider.height;
        _bladeCollider.height = 0;
        _laser.transform.localScale = new Vector3(0.2f, 0.2f, 0);
        _rb = GetComponent<Rigidbody>();

        //_rb.centerOfMass = hilt.transform.position;
        elapsedTime = btnUpAnimLength;

    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("saber velocity" + _rb.velocity);
        ControlLaser();
        if(btnAnimRunning)
        {            
            elapsedTime += Time.deltaTime;
        }
        ControlSound();

    }


    private void FixedUpdate()
    {
        //raycast out from certain point a certain distance
        if(_pressedA)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(bladeTip.transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.1f))
            {
                Instantiate(decalPrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
               
                Debug.Log("Did Hit");
            }
        }
    }


    public void SetTrail(bool setStart)
    {
        trail.SetStartPos(setStart);        
        trail.hasTrail = !trail.hasTrail;
    }

    public void SetPressed()
    {

        btnLastPressed = Time.time;


        if (btnLastPressed + elapsedTime >= btnLastPressed + btnUpAnimLength)
        {
            _pressedA = !_pressedA;
            elapsedTime = 0;
            
            btnAnimRunning = true;

            if (_pressedA)
            {
                source.PlayOneShot(saberStartSound);
                saberAnimator.SetBool("SaberOn", true);
            }
            else
            {
                source.PlayOneShot(saberRetractSound);
                saberAnimator.SetBool("SaberOn", false);
            }
        }
       



    }

    void ControlSound()
    {        
        Debug.Log("Saber playing " + source.isPlaying);
        if(_pressedA && rb.velocity.magnitude > 1)
        {
            source.PlayOneShot(saberSwingSound); 
        } else if(_pressedA && source.isPlaying == false)
        {                        
            source.PlayOneShot(saberHumSound, 2.5f);
        }
        
    }

    void ControlLaser()
    {
        
        //Grow Saber
        if (_pressedA && _laser.transform.localScale.z < fullSize)
        {
            if(!_laser.activeSelf) _laser.SetActive(true);
            


            elapsedTime += Time.deltaTime;
            
            

            float percentageComplete = elapsedTime / desiredDuration;
            float percentageCompleteUp = elapsedTime / desiredDurationUp;

            if (percentageComplete > 1)
            {
                percentageComplete = 1;
            }

            //z scale
            float newScale = Mathf.Lerp(_laser.transform.localScale.z, 1, percentageComplete);
            _laser.transform.localScale = new Vector3(_laser.transform.localScale.x, _laser.transform.localScale.y, Mathf.Clamp(newScale, 0, 1));


            //x,y scale
            float otherScale = Mathf.Lerp(_laser.transform.localScale.x, 1, percentageCompleteUp);
            _laser.transform.localScale = new Vector3(otherScale, otherScale, _laser.transform.localScale.z);
            
            
            //Change the collider size   

            /*
            This was for the follower method, which we aren't doing anymore

            //as we increase, we need to activate the followers that we are passing through
            foreach (SaberCapsule capsule in capsules)
            {
                if (bladeTip.position.y > capsule.transform.position.y)
                {
                    //spawn the follower
                    capsule.SpawnSaberCapsuleFollower();
                    
                }

            }
            */

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

            //float percentageCompleteDown = elapsedTime / desiredDurationDown;

            if (percentageComplete > 1)
            {
                percentageComplete = 1;
            }

            float newScale = Mathf.Lerp(_laser.transform.localScale.z, 0, percentageComplete);
            _laser.transform.localScale = new Vector3(_laser.transform.localScale.x, _laser.transform.localScale.y, Mathf.Clamp(newScale, 0, 1));

            if(elapsedTime >= desiredDurationDown)
            {
                elapsedDownTime += Time.deltaTime;
                float percentageCompleteDown = elapsedDownTime / (desiredDuration - desiredDurationDown);

                //x,y scale
                float otherScale = Mathf.Lerp(_laser.transform.localScale.x, 0.2f, percentageCompleteDown);                
                _laser.transform.localScale = new Vector3(otherScale, otherScale, _laser.transform.localScale.z);                
            }

            


            if (_laser.transform.localScale.z < 0.001f)
            {
                _laser.transform.localScale = new Vector3(0.2f, 0.2f, 0);
            }
            


            //_laser.transform.localScale += new Vector3(0, 0, -saberGrowSpeed);

            
            /*
            //as we increase, we need to activate the followers that we are passing through
            foreach (SaberCapsule capsule in capsules)
            {
                if (bladeTip.position.z < capsule.transform.position.y)
                {
                    capsule.DespawnSaberCapsuleFollower();
                }

            }
            */


            //_cc.height -= saberGrowSpeed;
        } else if (_pressedA == false)
        {            
            _laser.SetActive(false);
        }
    }


    public void RetractBlade()
    {
        _pressedA = false;
        _laser.transform.localScale = new Vector3(0.2f, 0.2f, 0);
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
        //print("Collision Out: " + gameObject.name);
    }


    public void HolsterAction()
    {
        RetractBlade();
    }


}

