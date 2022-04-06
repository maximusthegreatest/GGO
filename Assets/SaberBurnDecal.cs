using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SaberBurnDecal : MonoBehaviour
{
    // Start is called before the first frame update    
    DecalProjector decalProjector;    
    void Awake()
    {

        //start fade
        decalProjector = GetComponent<DecalProjector>();        
        StartCoroutine("StartFade");
    }

    IEnumerator StartFade()
    {        
        for (float t = 1; t >= -0.05f; t -= 0.05f)
        {
            decalProjector.fadeFactor = t;        
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(gameObject);
    }
}
