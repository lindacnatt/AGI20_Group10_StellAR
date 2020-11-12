using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour
{

    [HideInInspector]
    public Rigidbody rigidBody;

    [HideInInspector]
    public Vector3 acceleration = new Vector3(0f,0f,0f);

    [HideInInspector]
    public Vector3 velocity = new Vector3(0f,0f,0f);

    public bool staticBody;

    [HideInInspector]
    public Vector3 pausedVelocity;
    private bool hasBeenPaused;

    public GameObject explosionEffect;
    bool hasExploded = false;
    public float weightMultiplier = 8;




    public static List<CelestialObject> Objects;

    // Update call for the attractor. Runs through the static attractors list.
    void Start(){
        /*
        rigidBody = gameObject.GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            rigidBody = this.gameObject.AddComponent<Rigidbody>();
        }
        else
        {
            rigidBody = GetComponent<Rigidbody>();
        }
        rigidBody.mass = this.gameObject.transform.localScale.x *
            this.GetComponent<SphereCollider>().radius * weightMultiplier;
        rigidBody.useGravity = false;
        */
        if (staticBody)
        {
            rigidBody.isKinematic = true;
        }


    }

    // For adding attractors to the attractors list
    void OnEnable(){
        if(Objects == null){
            Objects = new List<CelestialObject>();
        }
        Objects.Add(this);
    }

    // For removing attractors from the attractors list
    void OnDisable(){
        Objects.Remove(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        float otherRadius = collision.gameObject.GetComponent<SphereCollider>().radius;
        float otherX = collision.gameObject.transform.localScale.x;
        float thisX = this.gameObject.transform.localScale.x;
        float thisRadius = this.GetComponent<SphereCollider>().radius;

        if (otherRadius * otherX <= thisRadius * thisX / 2)
        {
            Vector3 testvar = collision.contacts[0].point - this.transform.localPosition;
            if (this.gameObject.tag != "gasgiant")
            {
                IcoPlanet planet = this.GetComponent<IcoPlanet>();
                planet.MakeCrater(collision, otherRadius* otherX);
            }
        }
        else
        {
            if (!staticBody)
            {
                if (!hasExploded)
                {
                    if (this.transform.localScale.x >= 1)
                    {
                        explosionEffect = Resources.Load<GameObject>("Explosions/BigExplosionEffect");
                        ExplodeBig();
                    }
                    else
                    {
                        explosionEffect = Resources.Load<GameObject>("Explosions/SmallExplosionEffect");
                        ExplodeSmall();
                    }
                    hasExploded = true;
                }
            }
        }

    }

    void ExplodeBig()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    void ExplodeSmall()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }

}
