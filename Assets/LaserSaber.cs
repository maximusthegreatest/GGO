using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSaber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Saber colliding with:" + collision.gameObject.name);
        //if bullet
        if (collision.gameObject.name == "Laser(clone)")
        {
            Debug.Log("laser");
            collision.gameObject.GetComponent<Laser>().DestroyLaser();
        }

        if (collision.gameObject.name == "Bomb(clone)")
        {
            Debug.Log("bomb");
            collision.gameObject.GetComponent<Bomb>().DestroyBomb();
        }

        //if bomb call destroy on the bomb
    }
}
