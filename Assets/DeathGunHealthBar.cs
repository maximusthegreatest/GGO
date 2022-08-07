using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))] // without a LineRenderer, this script is pointless
[ExecuteAlways] // makes it work in the editor
public class DeathGunHealthBar : MonoBehaviour
{
    public LineRenderer line;
    public int sortOrder;

    public float healthLerpDuration;
    public float showContainerDuration;


    [SerializeField]
    private GameObject healthBarContainer;

    [SerializeField]
    private Material healthBarMat;

    [SerializeField]
    private Color whiteColor;

    [SerializeField]
    private Color yellowColor;

    [SerializeField]
    private Color redColor;

    [SerializeField]
    private int numSegments = 32; // quality setting - the higher the better it looks in close-ups
    [SerializeField]
    [Range(0f, 1f)]
    private float fillState = 0.86f; // how full the bar is

    [SerializeField]
    private float deadState = 0.13f;

    [SerializeField]
    private int deathGunHealth;


    private float valueToLerp;

    public void Start()
    {
        healthBarMat.SetColor("_EmissionColor", whiteColor);
        //average out the starting fill state to the ending fill state
        //then get that proportional to deathguns health where 0 is whatever the end fill state is and .86 is the starting health



    }

    public void Update()
    {
       
        if(FillState < 0.462f && FillState > 0.23)
        {
            //set the material to yellow color
            //healthBarMat.color = yellowColor;
            healthBarMat.SetColor("_EmissionColor", yellowColor);

        } else if(FillState < 0.23f )
        {
            //set mat to red
            //healthBarMat.color = redColor;
            healthBarMat.SetColor("_EmissionColor", redColor);
        }

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
        healthBarContainer.SetActive(true);
        //start show coroutine
        StartCoroutine(ShowHealthBarContainer());
        
        //subtract health from the health bar
        Debug.Log("new health " + health);
        float healthBarVal = 0.13f + ((0.86f - 0.13f) / 100f) * (health);
        Debug.Log("healthbar val " + healthBarVal);
        StartCoroutine(LerpHealthBar(healthBarVal));

        //you want to lerp from start_val(current fill state), to healthBarVal  
        //over a period of time


        //Lerp fillState to new value

    }

    IEnumerator ShowHealthBarContainer()
    {
        //wait for the lerp to finish then hide the container
        float timeElapsed = 0;
        while (timeElapsed < showContainerDuration)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        healthBarContainer.SetActive(false);
    }

        IEnumerator LerpHealthBar(float endValue)
    {
        float timeElapsed = 0;
        while (timeElapsed < healthLerpDuration)
        {
            valueToLerp = Mathf.Lerp(FillState, endValue, timeElapsed / healthLerpDuration);
            //set the value of the bar, might not want to do this
            FillState = valueToLerp;

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        valueToLerp = endValue;
        FillState = valueToLerp;
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