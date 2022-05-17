using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RestartGame()
    {
        //hide the menu and show screen again
        Debug.Log("Restarting Game");        
        Cardinal.instance.UpdateGameMode(GameMode.Default);
        gameObject.SetActive(false);

    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");        
        Application.Quit();
    }
}
