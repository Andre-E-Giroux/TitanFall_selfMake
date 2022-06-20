using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDamageHandler_TF : MonoBehaviour {

    [SerializeField]
    private float maxHealth = 100;

    public float health;

    [SerializeField]
    private Ragdoll ragdoll;

    private bool isDead = false;

    public Rigidbody mainBodyRB = null;

    [SerializeField]
    ParticleObjectPoolerScript pops = null;

    //private float weaknessModifier;
   
    // Use this for initialization
    void Start () {
       // maxHealth = 100;
        health = maxHealth;

        if(!ragdoll)
        {
            ragdoll = GetComponent<Ragdoll>();
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (!isDead)
        {
            if (health <= 0)
            {
                isDead = true;
                ragdoll.EnableRagdoll();
            }
        }
    }


    public void InstaDeath()
    {
        health = 0;
        isDead = false;
        pops.ActivateParticleAtPosition(transform.position);

        gameObject.SetActive(false);
    }


    public void TakeExploDistanceDamage(float distance, float deffaultDamage)
    {
        health -= (deffaultDamage - distance);

        if (!isDead)
        {
            if (health <= 0)
            {
                isDead = true;
                ragdoll.EnableRagdoll();
                // add force
            }
        }
    }

    //public void TakeDamage(float deffaultBulletdamage, float distance, GameObject hitBodyPart)
    public void TakeDamage(float deffaultBulletdamage, float distance, string hitBodyPart)
    {
        // 1. find what type of body part
        // 2. fin algorithm on how to deal damage

        //Debug.Log("TakeDamage called.");
        //Debug.Log("damage: " + deffaultBulletdamage);
        //Debug.Log("distance: " + distance);
        //Debug.Log("hitBodyPart: " + hitBodyPart);


        if (hitBodyPart == "CritPart")
        {                                              
            health -= (deffaultBulletdamage / distance) * 2;
        }

        if(hitBodyPart == "WeakPart")
        {
            health -= (deffaultBulletdamage / distance) * 1.5f;
        }

        if (hitBodyPart == "StandardPart")
        {
            health -= (deffaultBulletdamage / distance);
        }

        if (hitBodyPart == "StrongPart")
        {
            health -= (deffaultBulletdamage / distance) * 0.5f;
        }


        if (!isDead)
        {
            if (health <= 0)
            {
                isDead = true;
                ragdoll.EnableRagdoll();
            }
        }
        //Debug.Log("Dummy health: " + health);
    }

}
