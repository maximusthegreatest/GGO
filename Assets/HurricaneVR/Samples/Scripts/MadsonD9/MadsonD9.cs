using HurricaneVR.Framework.Weapons;
using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class AutoloadEvent : UnityEvent<float> { }

public class MadsonD9 : HVRPistol
{

    public HVRSocket magSocket;
    public AutoloadEvent autoLoadEvent = new AutoloadEvent();


    private SocketSpawnEvent SpawnedPrefab;
    private bool _isFirstLoad;
    


    protected override void Start()
    {
        base.Start();
        magSpawnTime = Time.time;
        _isFirstLoad = true;
        SpawnedPrefab = magSocket.SpawnedPrefab;
        SpawnedPrefab.AddListener(AutoReload);


    }

    public void AutoReload(HVRSocket socket, GameObject gun)
    {
        magSpawnTime = Time.time;
        
        if(_isFirstLoad)
        {
            //Debug.Log("first load");
            //auto slide it
            StartCoroutine(AutoSlide(_isFirstLoad));
            _isFirstLoad = false;
        } else
        {
            Debug.Log("second load");            
            StartCoroutine(AutoSlide(_isFirstLoad));
            //wait for time then autoslide
            //AutoSlide(true);
            //send event to ammo counter 
        }
    }

    IEnumerator AutoSlide(bool isFirstLoad)
    {
        if(!isFirstLoad)
        {
            Debug.Log("reloading not first" + Time.time);
            autoLoadEvent.Invoke(autoReloadTime);
            yield return new WaitForSeconds(autoReloadTime);
            Debug.Log("reloading not first actual reload" + Time.time);
            Slide.FullRelease.Invoke();
            Slide.Close();
        } else
        {            
            //rack the slide
            Slide.FullRelease.Invoke();
            Slide.Close();            
        }
        
        
    }

    void OnReleased()
    {
        Slide.PushBack();
        EjectBullet();
        //EjectCasing();
    }


    void OnGrabbed()
    {
        if (autoReloadTime + magSpawnTime > Time.time)
        {
            //return true;
        }
        else
        {
            //return false;
        }
    }

    

}