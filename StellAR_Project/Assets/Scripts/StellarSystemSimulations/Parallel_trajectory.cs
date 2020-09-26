using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Parallel_trajectory: MonoBehaviour
{
    public static Scene mainScene;
    public static PhysicsScene mainPhysicsScene;
    public static Scene parallelScene;
    public static PhysicsScene parallelPhysicsScene;

    public Vector3 initalVelocity;
    public GameObject mainObject;
    public GameObject sphere3;
    public GameObject sphere;
    public GameObject sphere2;
    public GameObject sphere4;
    private LineRenderer lineRenderer;

    public static bool mainPhysics = true;

    void Start()
    {
        Application.targetFrameRate = 30;
        Physics.autoSimulation = false;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1000;

        mainScene = SceneManager.GetActiveScene();
        mainPhysicsScene = mainScene.GetPhysicsScene();

        CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        parallelScene = SceneManager.CreateScene("ParallelScene", createSceneParameters);
        parallelPhysicsScene = parallelScene.GetPhysicsScene();

    }

    void Update()
    {
        if(PauseControl.gameIsPaused){
        if (Input.GetKeyDown(KeyCode.T)){
            //Time.timeScale = 0f;
            mainPhysics = false;
            SimulatePhysics();
            
        }

        if (Input.GetKeyDown(KeyCode.Return)){
            mainPhysics = true;
            Shoot();
            PauseControl.gameIsPaused=false;
            //Time.timeScale = 1f;
        }
        }
    }

    void FixedUpdate(){

        if (mainPhysics){
            mainScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
            }
    }

    void SimulatePhysics()
    {
        SceneManager.SetActiveScene(parallelScene);
        
        GameObject simulationObject = Instantiate(mainObject);
        GameObject simulationSphere3 = Instantiate(sphere3);
        GameObject simulationSphere = Instantiate(sphere);
        GameObject simulationSphere2 = Instantiate(sphere2);
        GameObject simulationSphere4 = Instantiate(sphere4);
        

        simulationObject.GetComponent<Rigidbody>().velocity = mainObject.GetComponent<Rigidbody>().velocity+ initalVelocity;
        simulationObject.GetComponent<Rigidbody>().angularVelocity = mainObject.GetComponent<Rigidbody>().angularVelocity;

        simulationSphere.GetComponent<Rigidbody>().velocity = sphere.GetComponent<Rigidbody>().velocity;
        simulationSphere.GetComponent<Rigidbody>().angularVelocity = sphere.GetComponent<Rigidbody>().angularVelocity;

        simulationSphere2.GetComponent<Rigidbody>().velocity = sphere2.GetComponent<Rigidbody>().velocity;
        simulationSphere2.GetComponent<Rigidbody>().angularVelocity = sphere2.GetComponent<Rigidbody>().angularVelocity;

        simulationSphere3.GetComponent<Rigidbody>().velocity = sphere3.GetComponent<Rigidbody>().velocity;
        simulationSphere3.GetComponent<Rigidbody>().angularVelocity = sphere3.GetComponent<Rigidbody>().angularVelocity;

        simulationSphere4.GetComponent<Rigidbody>().velocity = sphere4.GetComponent<Rigidbody>().velocity;
        simulationSphere4.GetComponent<Rigidbody>().angularVelocity = sphere4.GetComponent<Rigidbody>().angularVelocity;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {   
            foreach(StellarBody sb in StellarBody.StellarBodies){
                if(!(sb.gameObject.scene.name == parallelScene.name))
                    continue;
                
                NBodySimulation.SimulateStellarSystem(Parallel_trajectory.parallelScene.name,sb); }

            parallelPhysicsScene.Simulate(Time.fixedDeltaTime); 
            lineRenderer.SetPosition(i, simulationObject.transform.position);
        }

        Destroy(simulationObject);
        Destroy(simulationSphere3);
        Destroy(simulationSphere2);
        Destroy(simulationSphere);
        Destroy(simulationSphere4);

        SceneManager.SetActiveScene(mainScene);
    }

    void Shoot()
    {
        mainObject.GetComponent<Rigidbody>().velocity += initalVelocity;
    }

}
