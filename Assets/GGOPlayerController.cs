using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Core.Player;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Shared;
public class GGOPlayerController : MonoBehaviour
{
    public bool isMenuOpen;
    public float drawDistance;
    public GameObject gameMenu;
    public Transform mainCamera;
    public Transform RightController;
    public HVRController RightControllerInput => HVRInputManager.Instance.RightController;

    private Vector3 startDraw;
    private RectTransform gameMenuPosition;

    // Start is called before the first frame update
    void Start()
    {
        startDraw = Vector3.zero;
        gameMenuPosition = gameMenu.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMenu();   
    }

    void CheckMenu()
    {
        //check if menu is open
        if (GetAButtonJustPressed())
        {
            //set start draw
            startDraw = RightController.position;
        }

        if (GetAButtonJustReleased())
        {
            startDraw = Vector3.zero;
        }

        if (!isMenuOpen)
        {
            if (startDraw != Vector3.zero)
            {
                if (startDraw.y - RightController.position.y > drawDistance)
                {
                    //move menu to position 
                    gameMenuPosition.transform.position = RightController.position;
                    gameMenuPosition.rotation = Quaternion.LookRotation(mainCamera.forward);
                    gameMenuPosition.transform.rotation = Quaternion.Euler(0, gameMenuPosition.transform.eulerAngles.y, gameMenuPosition.transform.eulerAngles.z);
                    //angle towards player
                    gameMenu.SetActive(true);
                    isMenuOpen = true;
                }
            }
        }
    }

    private bool GetAButtonJustPressed()
    {
        //make sure player isnt holding anything in there hand, if they are make it false
        return RightControllerInput.PrimaryButtonState.JustActivated;
    }

    private bool GetAButtonJustReleased()
    {
        return RightControllerInput.PrimaryButtonState.JustDeactivated;
    }





}
