using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


public class RadialTest : MonoBehaviour
{

    public GameObject OtherPanel;
    private RadialLayout myRL;
    void Start()
    {
        myRL = OtherPanel.GetComponent<RadialLayout>();
        myRL.StartAngle = 44;
    }
}