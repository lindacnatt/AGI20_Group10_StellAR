using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/
public class SimulationPauseControl : MonoBehaviour
{
    public static bool gameIsPaused = false;
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.P) || ((gameIsPaused == false) && (Input.touchCount == 1))) //((Input.touchCount > 0) && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            gameIsPaused = !gameIsPaused;
        }
    }
}