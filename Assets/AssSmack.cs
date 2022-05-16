using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Core.Utils;

public class AssSmack : MonoBehaviour
{
    public AudioClip smackSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "GrabbableBag")
        {
            SFXPlayer.Instance.PlaySFX(smackSound, transform.position, 1f, 10f);
        }
        
        
    }
}
