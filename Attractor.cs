using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Rigidbody rigidBody;
    const float gravityConstant = 667.408f;

    public bool staticBody = false;
    
    public static List<Attractor> Attractors;
    private TrailRenderer trail; 

    // Update call for the attractor. Runs through the static attractors list.
    void Awake(){
        if (staticBody)
           { rigidBody.isKinematic = true;
            trail = null; }
        else {
            trail = gameObject.AddComponent(typeof(TrailRenderer)) as TrailRenderer;
            trail.time=0.5f;
            trail.material= new Material(Shader.Find("Sprites/Default"));
            trail.material.SetColor("_TintColor",Color.white);
            float alpha = 0.5f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.gray, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });
            trail.colorGradient=gradient;
            trail.startWidth=0.05f;
            trail.emitting=true;
        }
            

    }

    void FixedUpdate(){
        SimulateStellarSystem();
    }

    // For adding attractors to the attractors list
    void OnEnable(){  
        if(Attractors == null){
            Attractors = new List<Attractor>();
        }
        Attractors.Add(this);
    }

    // For removing attractors from the attractors list
    void OnDisable(){
        Attractors.Remove(this);
    }

    void Attract(Attractor attractedObj){
        

        Rigidbody rigidBodyToAttract = attractedObj.rigidBody;
        Vector3 distanceDirection = rigidBody.position - rigidBodyToAttract.position;
        float distance = distanceDirection.magnitude;

        //If two attractors are at the exact same place we just return out of it
        if(distance == 0f){
            return;
        }

        float forceMag = (gravityConstant * rigidBody.mass * rigidBodyToAttract.mass) / Mathf.Pow(distance,2);
        Vector3 forceDirection = distanceDirection.normalized * forceMag;

        rigidBodyToAttract.AddForce(forceDirection);
  }

  public void SimulateStellarSystem(){
      if(Attractors != null)
        foreach(Attractor attractor in Attractors){
                if ((attractor != this) && (attractor.gameObject.scene == this.gameObject.scene)&&(!attractor.staticBody)){
                    Attract(attractor);
                }
            }
  }

}
