using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathGunHealhBarBackground : MonoBehaviour
{
    public LineRenderer line;
    public int sortOrder;

    [SerializeField]
    private int numSegments = 32; // quality setting - the higher the better it looks in close-ups
    [SerializeField]
    [Range(0f, 1f)]
    private float fillState = 1f; // how full the bar is

    [SerializeField]
    private int deathGunHealth;

    public void Start()
    {
        //average out the starting fill state to the ending fill state
        //then get that proportional to deathguns health where 0 is whatever the end fill state is and .86 is the starting health


    }

    public float FillState
    {
        get => fillState;
        set
        {
            fillState = value;
            RecalculatePoints();
        }
    }

    public void TookDamage(int health)
    {
        //subtract health from the health bar

    }

    private void OnValidate() => RecalculatePoints();

    // Called when you change something in the inspector 
    // or change the FillState via another script
    private void RecalculatePoints()
    {
        // calculate the positions of the points
        float angleIncrement = Mathf.PI * fillState / numSegments;

        float angle = 0.0f;
        Vector3[] positions = new Vector3[numSegments + 1];
        for (var i = 0; i <= numSegments; i++)
        {
            positions[i] = new Vector3(
                Mathf.Cos(angle),
                0.0f,
                Mathf.Sin(angle)
            );
            angle += angleIncrement;
        }
        // apply the new points to the LineRenderer
        LineRenderer myLineRenderer = GetComponent<LineRenderer>();
        myLineRenderer.positionCount = numSegments + 1;
        myLineRenderer.SetPositions(positions);
    }
}
