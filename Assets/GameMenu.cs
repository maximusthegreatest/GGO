using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Core.Player;
using RootMotion.Demos;

public class GameMenu : MonoBehaviour
{

    public Cardinal cardinal;
    public CountdownTimer countdownTimer;

    public GGOPlayerController playerController;

    public IKAutoScaler autoScaler;
    public VRIKCalibrationController calibration;
    public VRAvatarCalibrator calibrator;

    public GameObject laserSpawner;

    private LaserSpawner laserSpawnerScript;

    // Start is called before the first frame update
    void Start()
    {
        laserSpawnerScript = laserSpawner.GetComponent<LaserSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseMenu()
    {
        Debug.Log("Closing Menu");
        
        playerController.isMenuOpen = false;
        //animate the menu upwards, for now just set inactive
        gameObject.SetActive(false);
    }

    //put the button methods here, then the sub buttons handle the onclick

    public void StartGame()
    {
        countdownTimer.StartGunGameCountDown();
        //cardinal.currentMode = Cardinal.GameMode.GunGame;
        CloseMenu();
    }


    public void HighScore()
    {
        //autoScaler.ResizeIKCharacter();
        calibration.AutoCalibrateCharacter();
        //scale laser spawner        

        float calHeight = laserSpawnerScript.ik.solver.spine.headTarget.position.y - laserSpawnerScript.ik.references.root.position.y;
        Debug.Log("Cal height in gamemenu " + calHeight);
        float yScaler = (calHeight * 0.25f) + 0.635f;
        Debug.Log(yScaler);
        laserSpawner.transform.localScale = new Vector3(laserSpawner.transform.localScale.x, laserSpawner.transform.localScale.y * yScaler, laserSpawner.transform.localScale.z);

        //calibrator.CalibrateAvatar();
    }

    public void QuitGame()
    {
        Debug.Break();
    }

    public void Settings()
    {
        //pause the game
        Debug.Log("Pausing game");
        
    }
}
