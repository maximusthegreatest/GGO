using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Core.Player;
using RootMotion.Demos;

public class GameMenu : MonoBehaviour
{

    public Cardinal cardinal;

    public GGOPlayerController playerController;

    public IKAutoScaler autoScaler;
    public VRIKCalibrationController calibration;
    public VRAvatarCalibrator calibrator;
    // Start is called before the first frame update
    void Start()
    {
        
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
        cardinal.currentMode = Cardinal.GameMode.GunGame;
        CloseMenu();
    }


    public void HighScore()
    {
        //autoScaler.ResizeIKCharacter();
        calibration.AutoCalibrateCharacter();
        //calibrator.CalibrateAvatar();
    }

    public void QuitGame()
    {

    }

    public void Settings()
    {

    }
}
