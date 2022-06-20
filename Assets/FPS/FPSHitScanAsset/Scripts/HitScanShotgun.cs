using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanShotgun : MonoBehaviour {

   // private Vector3 tempForward = new Vector3(0, 0, 0);

    private int numberOfPellets;

    RaycastHit hit;

    [SerializeField]
    LayerMask layerMask;

    private float pelletDamage;


    private float fireRate;

    private bool allowShoot = true;

    [SerializeField]
    private float maxTimeUntilNextShot = 1.0f;

    private float timeUntilNextShot;

    [SerializeField]
    private Transform shootFromPoint;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
		if (numberOfPellets <= 0)
        {
            numberOfPellets = 8;
        }

        if (pelletDamage <= 0)
        {
            pelletDamage = 20;
        }

        if (fireRate <= 0)
        {
            fireRate = 1f;
        }
        allowShoot = true;
	}
	
	// Update is called once per frame
	void Update () {




        if (allowShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (timeUntilNextShot <= 0)
                {
                    FireRaycast();
                    timeUntilNextShot = maxTimeUntilNextShot;
                }
                

                //StartCoroutine(FireRateWait());

            }
        }

        
        if(timeUntilNextShot > 0)
            timeUntilNextShot -= Time.deltaTime;


  
    }
  
    private void FireRaycast()
    {
        //bodyParts = new List<GameObject>();
        for (int i = numberOfPellets - 1; i >= 0; --i)
        {
            Vector3 pelletDirection = new Vector3(shootFromPoint.rotation.x + Random.Range(-3, 3), shootFromPoint.rotation.y +Random.Range(-2, 2), 10);
            Debug.DrawRay(shootFromPoint.position, shootFromPoint.TransformDirection(pelletDirection.x, pelletDirection.y, pelletDirection.z) * hit.distance, Color.yellow);

            if (Physics.Raycast(shootFromPoint.position, shootFromPoint.TransformDirection(pelletDirection), out hit, Mathf.Infinity, layerMask))
            {
                
                Debug.Log("Did Hit: " + hit.transform.name);

                if (hit.transform.root.GetComponent<DummyDamageHandler>() != null)
                {
                    Debug.Log("DummyHit!");

                    DummyDamageHandler tempDamageVar = hit.transform.root.GetComponent<DummyDamageHandler>();

                    float distance = (tempDamageVar.transform.position - transform.position).magnitude;

                    string bodyPartHit = hit.transform.gameObject.tag;

                    tempDamageVar.TakeDamage(pelletDamage, distance, bodyPartHit);

                   
                }

            }
        }
    }


    //IEnumerator FireRateWait()
    //{
    //    allowShoot = false;
    //    yield return new WaitForSeconds(fireRate);
    //    allowShoot = true;
    //}


}
