using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool _isGamePaused = false;


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            PauseController();
            //show the Pause Menu UI

        }

    }

    public void PauseController()
    {

        _isGamePaused = !_isGamePaused;
        PauseGame();

    }

    void PauseGame()
    {
        
        if (_isGamePaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

}
