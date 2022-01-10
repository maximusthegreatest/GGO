using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibratorTransformTracker : MonoBehaviour
{
    public Transform headTracker;
    public Transform bodyTracker;
    /*
    public Transform lHTracker;
    public Transform rHTracker;
    public Transform lFTracker;
    public Transform RFTracker;
    */

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting Head Tracker pos" + headTracker.position);
        Debug.Log("Starting body Tracker pos" + bodyTracker.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Head Tracker pos" + headTracker.position);
        Debug.Log("body Tracker pos" + bodyTracker.position);
    }
}
