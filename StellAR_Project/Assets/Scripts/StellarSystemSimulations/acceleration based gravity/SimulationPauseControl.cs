using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/
public class SimulationPauseControl : MonoBehaviour
{
    public static bool gameIsPaused = false;
    /*void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.P) || ((gameIsPaused == false) && (Input.touchCount == 1))) //((Input.touchCount > 0) && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            gameIsPaused = !gameIsPaused;
        }
    }*/

    public void PauseToggle(){
        gameIsPaused = !gameIsPaused;
    }

    public void RestartGame(){
        TrajectoryVelocity.startSlingshot = false;
        TrajectoryVelocity.start = new Vector3(0f,0f,0f);
        gameIsPaused = false;
        TrajectorySimulation.drawLine = false;
        TrajectorySimulation.destroyLine = false;
        TrajectorySimulation.freeze = false;
        TrajectorySimulation.shoot = false;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        ToggleGravityMode.nBodyGravity=true;

    }

    public void Pause(){
        gameIsPaused = true;
    }

    public void UnPause(){
        gameIsPaused = false;
    }

}