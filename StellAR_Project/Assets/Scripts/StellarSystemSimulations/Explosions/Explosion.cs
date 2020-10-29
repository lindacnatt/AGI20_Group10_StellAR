using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public GameObject explosionEffect;
    public GameObject explosionEffectSmall;
    bool hasExploded = false;
    bool bigExplopsion = false;
    Vector3 startpos;
    // Start is called before the first frame update
    private void Start()
    {
        this.tag = "Asteroid";
        if (this.transform.localScale.x >= 1)
        {
            bigExplopsion = true;
            explosionEffect = Resources.Load<GameObject>("Explosions/BigExplosionEffect");
        }
        else
        {
            explosionEffect = Resources.Load<GameObject>("Explosions/SmallExplosionEffect");
        }
        startpos = this.transform.position;
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded)
        {
            if (bigExplopsion) {
                ExplodeBig();
            }
            else
            {
                ExplodeSmall();
            }
            hasExploded = true;
        }
    }

    void ExplodeBig()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
        Respawn();
    }

    void ExplodeSmall()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
        Respawn();
    }

    void Respawn()
    {
        GameObject objToSpawn = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        objToSpawn.AddComponent<Rigidbody>();
        objToSpawn.AddComponent<SphereCollider>();
        objToSpawn.AddComponent<Explosion>();
        objToSpawn.AddComponent<TestController>();
        objToSpawn.transform.position = startpos;
        Rigidbody rb = objToSpawn.GetComponent<Rigidbody>();
        rb.useGravity = false;
        //Debug.Log(startpos);

    }
}
