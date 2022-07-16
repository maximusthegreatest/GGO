using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LaserSetting", menuName ="Laser Setting")]
public class LaserSetting : ScriptableObject
{    
    public int laserCount;    
    public float minSpawnTime;    
    public float maxSpawnTime;
}
