using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core.Bags;
using HurricaneVR.Framework.Core.Grabbers;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public Vector3[] hudSlots = new Vector3[2];
    public Dictionary<string, GameObject> weaponHuds;
    public GameObject saberUI;
    public GameObject pistolUI;

    public HVRHandGrabber leftHandGrabber;
    public HVRHandGrabber rightHandGrabber;
    public VRGrabberEvent grabbed;
    public VRGrabberEvent released;


    private PlayerHUD playerHUd;
    [SerializeField]
    private List<string> _weaponSlots;
    private HVRTriggerGrabbableBag _leftGrabBag;
    private HVRTriggerGrabbableBag _rightGrabBag;

    

    // Start is called before the first frame update
    void Start()
    {
        _weaponSlots = new List<string>(); 


        hudSlots[0] = new Vector3(-172.2f, -154.7f, 0);
        hudSlots[1] = new Vector3(-172.2f, -215.9f, 0);

        //get the event from handGrabber
        leftHandGrabber.Grabbed.AddListener(GrabbingItem);
        rightHandGrabber.Grabbed.AddListener(GrabbingItem);

        leftHandGrabber.Released.AddListener(ReleasingItem);
        rightHandGrabber.Released.AddListener(ReleasingItem);

        

        //grabbed.AddListener(GrabbingItem);

        //_leftGrabBag = GameObject.Find("LeftHand").GetComponentInChildren<HVRTriggerGrabbableBag>();
        //_rightGrabBag = GameObject.Find("RightHand").GetComponentInChildren<HVRTriggerGrabbableBag>();

        weaponHuds = new Dictionary<string, GameObject>();
        weaponHuds.Add("PhotonSwordParent", saberUI);
        weaponHuds.Add("FN", pistolUI);

    }

    

    // Update is called once per frame
    void Update()
    {

        /*
        if (_leftGrabBag.ClosestGrabbable)
        {

            string leftGrabbed = _leftGrabBag.ClosestGrabbable.gameObject.name;

            CheckWeaponsEquipped(leftGrabbed);

            //if that exists in list of weapon names 
            //then add the ui for that weapon



            Debug.Log("left grabbed " + leftGrabbed);
        }
        */

        //throw this in a loop for both controllers
        
    }

    public void ReleasingItem(HVRGrabberBase grabber, HVRGrabbable grabbable)
    {
        //Debug.Log("RELEASING ITEM FROM THE HUD MAYNE");
        //Debug.Log("grabber " + grabber.gameObject);
        //Debug.Log("grabable " + grabbable.gameObject);

        if (grabbable)
        {
            //string item = grabbable.gameObject.name;
            bool remove = true;
            GameObject item = grabbable.gameObject;
            CheckWeaponsEquipped(item, remove);
        }

    }
    
    public void GrabbingItem(HVRGrabberBase grabber, HVRGrabbable grabbable)
    {
        //Debug.Log("grabber " + grabber.gameObject);
        //Debug.Log("grabable " + grabbable.gameObject);
        if (grabbable)
        {
            //string item = grabbable.gameObject.name;
            bool remove = false;
            GameObject item = grabbable.gameObject;
            CheckWeaponsEquipped(item, remove);
        }
        
    }

    void CheckWeaponsEquipped(GameObject item, bool remove)
    {
        string itemName = item.name;

        if (weaponHuds.ContainsKey(itemName))
        {
            //get the game HUD
            GameObject wepHUD = weaponHuds[itemName];
            RectTransform hudTransform = wepHUD.GetComponent<RectTransform>();
            //Its a weapon with a HUD
            if (remove)
            {                
                _weaponSlots.Remove(itemName);
                wepHUD.SetActive(false);
                MoveUIUp();
            } else
            {
                //check which position it can go in, so we need another dicitionary/enum that associates 0 with coordinates                           
                if (_weaponSlots.ElementAtOrDefault(0) != null) //if first item is full
                {
                    //Debug.Log("throwing into second slot");
                    _weaponSlots.Insert(1, itemName);
                    hudTransform.anchoredPosition = new Vector2(hudSlots[1].x, hudSlots[1].y);
                    wepHUD.SetActive(true);

                }
                else
                {
                    //Debug.Log("throwing into first slot");
                    _weaponSlots.Add(itemName); //set first item
                    //give it coords
                    hudTransform.anchoredPosition = new Vector2(hudSlots[0].x, hudSlots[0].y);
                    wepHUD.SetActive(true);
                }
            }

            
            
        } else
        {
            Debug.Log("nope bro");
        }
    }

    void MoveUIUp()
    {        
        if(_weaponSlots.Count > 0)
        {            
                //Debug.Log("moving on up inside");                                                
                //get hud of item thats in there
                GameObject otherWepHUD = weaponHuds[_weaponSlots[0]];
                //Debug.Log("other wep hud" + otherWepHUD.name);
                RectTransform otherHudTransform = otherWepHUD.GetComponent<RectTransform>();
                otherHudTransform.anchoredPosition = new Vector2(hudSlots[0].x, hudSlots[0].y);
            
        }
        
    }
}
