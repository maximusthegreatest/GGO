using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Weapons;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class AmmoCounter : MonoBehaviour
{
    int _ammoCount;

    public MadsonD9 pistol;
    public Text ammoCountText;

    private bool _isReloading;
    
    // Start is called before the first frame update
    void Start()
    {
        pistol.autoLoadEvent.AddListener(ShowReloadGraphic);
    }

    void ShowReloadGraphic(float reloadTimer)
    {
        ammoCountText.text = "00";
        ammoCountText.color = Color.red;
        _isReloading = true;        
        StartCoroutine(ReloadDone(reloadTimer));
    }

    IEnumerator ReloadDone(float reloadTimer)
    {
        yield return new WaitForSeconds(reloadTimer);
        _isReloading = false;

    }

    // Update is called once per frame
    void Update()
    {
                
        if(!_isReloading)
        {
            //get ammo and set the text
            _ammoCount = pistol.Ammo.GetCurrentAmmo();
            if(pistol.IsBulletChambered)
            {
                _ammoCount++;
            }
            
            ammoCountText.text = _ammoCount.ToString();
            ammoCountText.color = Color.white;

            if (_ammoCount == 0)
            {
                ammoCountText.text = "00";
                ammoCountText.color = Color.red;
            }
        }
        
    }
}
