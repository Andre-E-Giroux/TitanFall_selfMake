using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDamageHandler : MonoBehaviour {

    [SerializeField]
    private float maxHealth = 100;
    private float health;


    //private float weaknessModifier;
   
    // Use this for initialization
    void Start () {
       // maxHealth = 100;
        health = maxHealth;

	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
        {
            Destroy(gameObject);
        }
	}


    //public void TakeDamage(float deffaultBulletdamage, float distance, GameObject hitBodyPart)
    public void TakeDamage(float deffaultBulletdamage, float distance, string hitBodyPart)
    {
        // 1. find what type of body part
        // 2. fin algorithm on how to deal damage

        Debug.Log("TakeDamage called.");
        Debug.Log("damage: " + deffaultBulletdamage);
        Debug.Log("distance: " + distance);
        Debug.Log("hitBodyPart: " + hitBodyPart);


        if (hitBodyPart == "CritPart")
        {                                               //crit
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

        Debug.Log("Dummy health: " + health);
    }

}
