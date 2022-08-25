using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.ControllerInput;
using UnityEngine;
using System.Collections;


public class PauseClickListener : MonoBehaviour
{
    // Start is called before the first frame update
    public HVRHandGrabber hand;
    [SerializeField]
    private int clicksToPause;
    [SerializeField]
    private int currentClicks;
    [SerializeField]
    private float pauseTimeInterval;    
    private HVRController controller;

    void Start()
    {
        //get hand         
        controller = HVRInputManager.Instance.GetController(hand.HandSide);
        StartCoroutine(StartPauseRoutine());

    }

    // Update is called once per frame
    void Update()
    {

        //wait here for time window
        //stop coroutine
        //set pause to none
        //

    }

    IEnumerator StartPauseRoutine()
    {
        while(true)
        {
            StartCoroutine(CheckPause());

            yield return new WaitForSeconds(pauseTimeInterval);
            //reset currentclicks
            currentClicks = 0;
        }

    }

    IEnumerator CheckPause()
    {
        while(true)
        {
            //check for button state 

            bool clickedB = controller.SecondaryButtonState.JustActivated;
            Debug.Log("clicked B" + clickedB);
            if (clickedB)
            {
                currentClicks++;
                if (currentClicks >= clicksToPause)
                {
                    Pause();
                }
            }
            yield return new WaitForSeconds(.1f);
            //wait for super small time then check again
        }


    }

    private void Pause()
    {
        //pause game
        Debug.Break();
        StopAllCoroutines();
    }
}
