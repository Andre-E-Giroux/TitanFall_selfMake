using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObjectPoolerScript : MonoBehaviour {


    public static ParticleObjectPoolerScript current;
    [SerializeField]
    private GameObject pooledObject;
    [SerializeField]
    private int poolAmount = 10;
    [SerializeField]
    private bool willGrow = true;

    List<GameObject> pooledObjects = new List<GameObject>();

	// Use this for initialization
	void Awake() {
        current = this;
        
        // instantiate game objects equal to the poolAmount variable and pool them.
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);

        }

    }

    /// <summary>
    /// Activate pooled objects
    /// </summary>
    /// <returns></returns>
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if an object is not set to active set to active
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // if there are no available objects in pool, Instantiate more
        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObject);
            pooledObjects.Add(obj);
            obj.SetActive(false);
            return obj;
        }

        return null;

    }
    
    public void ActivateParticleAtPosition(Vector3 position)
    {
        GameObject temp = GetPooledObject();
        temp.transform.position = position;
        temp.SetActive(true);
    }

    public void ActivateParticleAtPositionAndRotation(Vector3 position, Quaternion quat)
    {
        GameObject temp = GetPooledObject();
        temp.transform.position = position;
        temp.transform.rotation = quat;
        temp.SetActive(true);
    }


    // how to accces it in other scripts
    //GameObject obj = ObjectPoolerScript.current.GetPooledObject();

}
