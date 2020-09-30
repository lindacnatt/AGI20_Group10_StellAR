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
        /*if (Input.GetKeyDown(KeyCode.P)) //this is for testing on computer
        {
            gameIsPaused = !gameIsPaused;
        }*/

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                //When a touch has first been detected
                case TouchPhase.Began:
                    gameIsPaused = true;
                    Debug.Log("paused");
            
                break;
                //When a touch ends
                case TouchPhase.Ended:
                    TrajectorySimulation.isDone = true; //if a trajectorysimulation is being conducted it enters it's finished state and displays
                    gameIsPaused = false;
                    Debug.Log("play");
                break;
            }
        }
    }
}