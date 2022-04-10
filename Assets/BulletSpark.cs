using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletSpark : MonoBehaviour
{
    private void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public void OnParticleSystemStopped()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
