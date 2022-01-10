using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletContainer : MonoBehaviour
{

    public float timeAlive;
    private float birth;

    // Start is called before the first frame update
    void Start()
    {
        birth = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (birth + timeAlive < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
