using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class VRAvatarCalibrator : MonoBehaviour
{

    

    public VRIK ik;
    public float scaleMlp = 1f;

    void Start()
    {
        

    }

    public void CalibrateAvatar()
    {
        Debug.Log("avatar Calibrator");
        //Compare the height of the head target to the height of the head bone, multiply scale by that value.
        float sizeF = (ik.solver.spine.headTarget.position.y - ik.references.root.position.y) / (ik.references.head.position.y - ik.references.root.position.y);
        ik.references.root.localScale *= sizeF * scaleMlp;
    }
}
