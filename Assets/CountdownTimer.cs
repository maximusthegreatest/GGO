using System;
using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core.Utils;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class CountdownTimer : MonoBehaviour
{
    public int time;

    public Cardinal cardinal;

    public Text timerText;

    public AudioClip tickingSound;
    public AudioClip dingSound;
    public AudioClip goSound;

    

    [SerializeField]
    private CountdownIconImage startingIcon;

    [SerializeField]
    private Image endingIcon;

    [SerializeField]    
    private List<CountdownIconImage> countdownIcons = new List<CountdownIconImage>();

    [SerializeField]
    private float iconsPerSecond;

    [SerializeField]
    private float ticksPerSecond;

    private Guid tickingSoundGuid;

    private Coroutine countdownTextRoutine;
    private Coroutine countdownRoutine;
    private Coroutine flashIconsRoutine;
    private Coroutine tickingSoundRoutine;      
    private Coroutine tickingRoutine;


    private bool countdownFinished;
    private bool fadeText;
    private bool doTicking;
    private bool tickingRoutineRunning;

    [SerializeField]
    private CanvasGroup uiGroup;


    // Start is called before the first frame update
    void Start()
    {
        //get all children of type 
        CountdownIconImage[] imageIcons = GetComponentsInChildren<CountdownIconImage>();

        List<CountdownIconImage> startingList = new List<CountdownIconImage>();

        foreach (CountdownIconImage icon in imageIcons)
        {
            startingList.Add(icon);
        }
        
        iconsPerSecond = 1f / startingList.Count;

        
        
        //shuffle the list        
        for(int i = startingList.IndexOf(startingIcon); i < startingList.Count; i++)
        {
            countdownIcons.Add(startingList[i]);
        }

        for (int i = 0; i < startingList.IndexOf(startingIcon); i++)
        {
            countdownIcons.Add(startingList[i]);
        }

        Debug.Log("iconsLength * iconspersecond = " + countdownIcons.Count * iconsPerSecond);

        tickingSoundGuid = System.Guid.NewGuid();

    }

    private void Update()
    {        
        if(fadeText)
        {
           if(uiGroup.alpha > 0)
            {
                uiGroup.alpha -= Time.deltaTime;
            } else
            {
                fadeText = false;
                uiGroup.alpha = 1;
            }


        }

        if(doTicking && !tickingRoutineRunning)
        {
            //check if routine is already running
            Debug.Log("Starting routine from update");
            tickingRoutineRunning = true;
            tickingSoundRoutine = StartCoroutine(TickingSound());
            
        }
    }

    public void StartGunGameCountDown()
    {
        transform.parent.gameObject.SetActive(true);
        RadialLayout rLayout = GetComponent<RadialLayout>();
        rLayout.CalculateRadial();
        countdownRoutine = StartCoroutine(Countdown());
    }


  

    public IEnumerator TickingSound()
    {          
        for (int i = 0; i < ticksPerSecond; i++)
        {                
            SFXPlayer.Instance.PlaySFXCooldown(tickingSound, transform.position, tickingSoundGuid, 1, 1, (1f / ticksPerSecond));
            yield return new WaitForSecondsRealtime(1.25f / ticksPerSecond);
        }
    }

    public void DingSound()
    {
        SFXPlayer.Instance.PlaySFX(dingSound, transform.position);
    }

    public IEnumerator CountdownText()
    {
        for(int i = 0; i <= time; i++)
        {
            if(time == i)
            {
                timerText.text = "GO!";
                SFXPlayer.Instance.PlaySFX(goSound, transform.position);
                foreach (CountdownIconImage icon in countdownIcons)
                {
                    icon.gameObject.SetActive(false);
                }
                fadeText = true;
            } else
            {
                int j = time - i;
                timerText.text = j.ToString();
            }
            
            
            yield return new WaitForSeconds(1);
        }
        //hide radial menu
        fadeText = false;
        transform.parent.gameObject.SetActive(false);
        cardinal.currentMode = Cardinal.GameMode.GunGame;
        //StopCoroutine(countdownTextRoutine);
        foreach (CountdownIconImage icon in countdownIcons)
        {
            icon.gameObject.SetActive(true);
        }
    }

    public IEnumerator Countdown()
    {        
        uiGroup.alpha = 1;
        transform.parent.gameObject.SetActive(true);
        for (int i = 0; i <= time; i++)
        {

            if (time == i)
            {
                timerText.text = "GO!";
                SFXPlayer.Instance.PlaySFX(goSound, transform.position);
                foreach (CountdownIconImage icon in countdownIcons)
                {
                    icon.gameObject.SetActive(false);
                }
                fadeText = true;
                yield return new WaitForSeconds(1);

                //fades then waits a second
                //hide radial menu
                fadeText = false;
                transform.parent.gameObject.SetActive(false);
                cardinal.currentMode = Cardinal.GameMode.GunGame;
                foreach (CountdownIconImage icon in countdownIcons)
                {
                    icon.gameObject.SetActive(true);
                }                
            }
            else
            {
                doTicking = true;
                
                int j = time - i;
                timerText.text = j.ToString();

                yield return StartCoroutine(FlashIcons()); //this will take a second
                
                
                if (i != time - 1)
                {
                    DingSound();
                }

                
                doTicking = false;
                tickingRoutineRunning = false;
                StopCoroutine(tickingSoundRoutine);                               
            }
        }
        
        StopCoroutine(countdownRoutine);
    }

    private IEnumerator FlashIcons()
    {        
        //loop this loop until time is done
        foreach(CountdownIconImage icon in countdownIcons)
        {
            icon.FlashIcon();            
            yield return new WaitForSecondsRealtime(iconsPerSecond);            
        }        
    }
}


