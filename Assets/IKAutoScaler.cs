using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKAutoScaler : MonoBehaviour
{
    public float defaultHeight;
    public Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResizeIKCharacter()
    {
        Debug.Log("Auto scaling");
        float headHeight = camera.transform.localPosition.y;
        float scale = headHeight / defaultHeight;
        //float scale = defaultHeight / headHeight;
        transform.localScale = Vector3.one * scale;
    }
}
