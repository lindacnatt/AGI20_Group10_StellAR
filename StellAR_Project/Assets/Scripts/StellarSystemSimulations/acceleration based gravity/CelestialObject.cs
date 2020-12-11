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

    public GameObject explosionEffect;
    bool hasExploded = false;
    public float weightMultiplier = 1f;
    [HideInInspector]
    public float mass=1f;
    public string name;
    public GameObject txt = null;
    [HideInInspector]
    public float textTranslation = 0.7f;







    public static List<CelestialObject> Objects;

    void Awake(){
        if (staticBody){
            name=null;
        }

    }


    // Update call for the attractor. Runs through the static attractors list.
    void Start(){
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            rigidBody = this.gameObject.AddComponent<Rigidbody>();
        }

        //rigidBody.mass = this.gameObject.transform.localScale.x *
        //    this.GetComponent<SphereCollider>().radius * weightMultiplier;
        rigidBody.useGravity = false;
        mass = rigidBody.mass;
        
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
            if (this.gameObject.tag != "GasPlanet")
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
                    Debug.Log("Explode");
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

    public void SetState(_celestialObject data){
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            rigidBody = this.gameObject.AddComponent<Rigidbody>();
        }
        staticBody = data.staticBody;
        velocity = data.velocity;
        rigidBody.position = data.position;
        
        RotationSim rot = this.gameObject.GetComponent<RotationSim>();
        if(rot != null){
            this.gameObject.GetComponent<RotationSim>().SetState(true);
            this.gameObject.GetComponent<RotationSim>().SetRotation(data.rotation[0],data.rotation[1]);
            this.gameObject.GetComponent<RotationSim>().StartRotation(true);
            this.gameObject.GetComponent<RotationSim>().Deploy();
        }
        else{
            this.gameObject.AddComponent<RotationSim>();
            this.gameObject.GetComponent<RotationSim>().SetState(true);
            this.gameObject.GetComponent<RotationSim>().SetRotation(data.rotation[0],data.rotation[1]);
            this.gameObject.GetComponent<RotationSim>().StartRotation(true);
            this.gameObject.GetComponent<RotationSim>().Deploy();
        }

        mass = data.mass;
        rigidBody.mass=mass;

        name = data.name;
       if(name != null){
        txt = Resources.Load("PlanetNameText/PlanetName") as GameObject;
        Vector3 newpos= this.gameObject.transform.position;
        newpos = newpos+ new Vector3(0f,0.2f,0f);
        txt = Instantiate(txt, newpos,this.gameObject.transform.rotation);
        //txt.transform.SetParent(this.gameObject.transform, false);
        txt.AddComponent<PlanetNameMovement>();
        textTranslation= data.textTranslation;
        txt.GetComponent<PlanetNameMovement>().SetPlanet(this.gameObject, data.textTranslation);
        txt.GetComponent<TextMesh>().text = name;
        }

        if (staticBody)
        {
            rigidBody.isKinematic = true;
            this.gameObject.transform.position = data.position;

        }
        else{
            rigidBody.isKinematic = false;
        }
    }

    public Vector3 GetPosition(){
        return rigidBody.position;
    }

    public static void DestroyAll(){
        if(Objects != null)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if(!Objects[i].staticBody){
                Destroy(Objects[i].gameObject);
                }
            }
            //Objects.Clear(); //clearing the old planets
            Objects.TrimExcess();
        }

    }

    public string GetName(){
        return name;
    }

    public void SetName(string input){
        IsTypeOfPlanet type =this.gameObject.GetComponent<IsTypeOfPlanet>();
        
        if(type != null){
            if(type.IsRocky){
                var RockSetting = this.gameObject.GetComponent<MotherPlanet>().shapeGenerator.settings;
                textTranslation = RockSetting.radius+0.1f;
            }

            if(type.IsGassy){
                float GasValue = this.gameObject.transform.localScale.x;
                textTranslation = GasValue/1.7f;

            }
        }


        name = input;
        if(name != null){
        txt = Resources.Load("PlanetNameText/PlanetName") as GameObject;
        Vector3 newpos= this.gameObject.transform.position;
        //newpos = newpos + new Vector3(0f,0.2f,0f);
        txt = Instantiate(txt, newpos,this.gameObject.transform.rotation);
        //txt.transform.SetParent(this.gameObject.transform, false);
        txt.AddComponent<PlanetNameMovement>();
        txt.GetComponent<PlanetNameMovement>().SetPlanet(this.gameObject, textTranslation);
        txt.GetComponent<TextMesh>().text = name;
        }
    }

    public void SetMass(){

        IsTypeOfPlanet type =this.gameObject.GetComponent<IsTypeOfPlanet>();
        
        if(type != null){
            if(type.IsRocky){
                weightMultiplier = 18f;
                var RockSetting = this.gameObject.GetComponent<MotherPlanet>().shapeGenerator.settings;
                //float interval = (0.599485f-0.3692803f);
                float scaling = 2f -(0.5f-RockSetting.radius)*4f;
                scaling = scaling < 0f ? 0.1f : scaling;
                weightMultiplier *= scaling;
            }

            if(type.IsGassy){
                weightMultiplier = 5f;
                float GasInterval = 1.573064f-1.19897f;
                float GasValue = this.gameObject.transform.localScale.x;
                print(GasValue);
                float scaling = Mathf.Exp((GasValue-GasInterval)/GasInterval)/3f;
                weightMultiplier *= scaling;

            }
        }

       
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        if (rigidBody == null)
        {
            rigidBody = this.gameObject.AddComponent<Rigidbody>();
        }
        rigidBody.useGravity = false;
        mass = rigidBody.mass;
        rigidBody.mass *= weightMultiplier;
        mass *= weightMultiplier;
        print("Setmass:");
        print(mass);
        print(rigidBody.mass);
        print(weightMultiplier);

        

    }

}
