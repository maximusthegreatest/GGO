using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Weapons.Guns;
using UnityEngine.UI;
using UnityEngine.Rendering;
using HurricaneVR.Framework.Weapons;
using HurricaneVR.Framework.Core.Grabbers;


public class AmmoCounter : MonoBehaviour
{
    

    public HVRPistol pistol;
    public Text ammoCountText;

    [SerializeField]
    private GameObject _ammoSocket;
    private HVRAmmo _currentAmmo;   
    private bool hasAmmo = false;
    

    public void SetCurrentAmmo() 
    {
        hasAmmo = !hasAmmo;
        //get ammo (grabbable from ammo socket
        //_ammoSocket
        //look in children for object with script type of HVRAmmo

        if(hasAmmo)
        {
            _currentAmmo = _ammoSocket.GetComponentInChildren<HVRAmmo>();
        } else
        {
            _currentAmmo = null;
        }

    }
    

    // Update is called once per frame
    void Update()
    {
        
        if(_currentAmmo)
        {  
            //get ammo read from clip
            ammoCountText.text = _currentAmmo.CurrentCount.ToString();
            ammoCountText.color = Color.white;

            if (_currentAmmo.CurrentCount == 0)
            {
                ammoCountText.text = "00";
                ammoCountText.color = Color.red;
            }

        } else
        {            
            ammoCountText.text = "00";
            ammoCountText.color = Color.red;
        }

    }
}
