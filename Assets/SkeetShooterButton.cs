using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeetShooterButton : MonoBehaviour
{

    [SerializeField]
    Material redMat;

    [SerializeField]
    Material greenMat;

    [SerializeField]
    MeshRenderer btnMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeBtnColor()
    {
        if(btnMesh.sharedMaterial == redMat)
        {
            btnMesh.sharedMaterial = greenMat;
        } else
        {
            btnMesh.sharedMaterial = redMat;
        }
    }
}
