using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour
{
    public Transform leftShoulder;
    public Transform rightShoulder;

    public AudioClip[] bulletWhizSounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public AudioClip GetWhizSound()
    {
        //get random clip in array
        return bulletWhizSounds[Random.Range(0, bulletWhizSounds.Length)];
    }
}
