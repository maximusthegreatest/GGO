using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BombSetting",menuName ="Bomb Setting")]
public class BombSetting : ScriptableObject
{    
    public int bombCount;    
    public float minSpawnTime;    
    public float maxSpawnTime;
}
