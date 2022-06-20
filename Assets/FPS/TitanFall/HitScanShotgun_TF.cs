using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanShotgun_TF : MonoBehaviour {

    // private Vector3 tempForward = new Vector3(0, 0, 0);

    [SerializeField]
    private WeaponClass m_WC = null;

    //[SerializeField]
    //private int numberOfPellets;


    //public float maxNumberOfShots = 8;

    //[HideInInspector]
    //public float numberOfShots = 0;

    public bool reloading;

    //[SerializeField]
    //private float maxTimeUntilReload = 1.0f;

   
    private float currentReloadTimerRemaining = 0.0f;

    RaycastHit hit;

   // [SerializeField]
    //LayerMask layerMask;

   // [SerializeField]
    //private float pelletDamage;


   // private float fireRate;

    private bool allowShoot = true;

    //[SerializeField]
    //private float maxTimeUntilNextShot = 1.0f;
    [SerializeField]
    private float timeUntilNextShot;

    //[SerializeField]
    //private Transform shootFromPoint;

    //[SerializeField]
    //private float shotForce = 50;

    [SerializeField]
    private Animator shotgunAnimator;

    //[SerializeField]
    //private GameObject gunShotParticles;

    [SerializeField]
    private Transform muzzleNode;

    [SerializeField]
    private ParticleObjectPoolerScript pooledObjectParticle;


    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
		if (m_WC.m_numberOfPelletsPerRound <= 0)
        {
            m_WC.m_numberOfPelletsPerRound = 8;
        }

        if (m_WC.m_damagePerProjectile <= 0)
        {
            m_WC.m_damagePerProjectile = 20;
        }
        if(m_WC.m_currentBackupAmmo != m_WC.m_maxAmmoCount)
        {
            m_WC.m_currentBackupAmmo = m_WC.m_maxAmmoCount;
        }

        //if (m_WC.m_fireRate <= 0)
        //{
        //    m_WC.m_fireRate = 1f;
        //}
        if(m_WC.m_shotForce <= 0)
        {
            m_WC.m_shotForce = 50.0f;
        }

        m_WC.m_currentClipAmmoCount = m_WC.m_maxClipSize;

        allowShoot = true;


        if(!pooledObjectParticle)
        {
            pooledObjectParticle = GameObject.FindGameObjectWithTag("MasterPool").transform.GetChild(0).GetComponent<ParticleObjectPoolerScript>();
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (allowShoot && m_WC.m_currentClipAmmoCount > 0 && timeUntilNextShot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                shotgunAnimator.SetTrigger("ShotGunTrigger");
                //Instantiate(gunShotParticles, muzzleNode.position, muzzleNode.rotation);

                //
                

                if (pooledObjectParticle != null)
                {
                    pooledObjectParticle.ActivateParticleAtPositionAndRotation(muzzleNode.position, muzzleNode.rotation);
                }
                //

                if (reloading)
                {
                    currentReloadTimerRemaining = 0;
                    reloading = false;
                }

                if (timeUntilNextShot <= 0)
                {
                    FireRaycast();
                    timeUntilNextShot = m_WC.m_fireRate;
                }

                m_WC.m_currentClipAmmoCount--;
            }
        }

        
        if(timeUntilNextShot > 0)
            timeUntilNextShot -= Time.deltaTime;


        // allow reload
        if (m_WC.m_currentClipAmmoCount < m_WC.m_maxClipSize)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                shotgunAnimator.SetTrigger("Reload");
                reloading = true;
                currentReloadTimerRemaining = 0.0f;
            }

            if (reloading)
            {
                
                currentReloadTimerRemaining += Time.deltaTime;
                
                //Debug.Log("Reloading");
                if (currentReloadTimerRemaining >= m_WC.m_realoadSpeed)
                {
                    if (m_WC.m_currentBackupAmmo > 0 && m_WC.m_currentClipAmmoCount < m_WC.m_maxClipSize)
                    {
                        
                        m_WC.m_currentBackupAmmo -= 1;
                        m_WC.m_currentClipAmmoCount += 1;

                        Debug.Log("curr: " + m_WC.m_currentClipAmmoCount + "maxclip: " + (m_WC.m_maxClipSize - 1));

                        if(m_WC.m_currentClipAmmoCount <= m_WC.m_maxClipSize - 1)
                        {
                            Debug.Log("reload");
                            shotgunAnimator.SetTrigger("Reload");
                        }
                    }

                    else
                    {
                        reloading = false;
                    }
                    //if(reloading != false)
                    //{
                    //    shotgunAnimator.SetTrigger("Reload");
                    //}
                    currentReloadTimerRemaining = 0;
                    //Debug.Log("current: " + m_WC.m_maxAmmoCount);
                    
                }
            }
        }
        




  
    }
  
    private void FireRaycast()
    {
        List<Transform> docmentedHits = new List<Transform>();
        List<Vector3> docmentedHitPoints = new List<Vector3>();
        //bodyParts = new List<GameObject>();
        for (int i = m_WC.m_numberOfPelletsPerRound - 1; i >= 0; --i)
        {
            Vector3 pelletDirection = new Vector3(m_WC.m_pointOfShot.rotation.x + Random.Range(-3, 3), m_WC.m_pointOfShot.rotation.y +Random.Range(-2, 2), 10);
            Debug.DrawRay(m_WC.m_pointOfShot.position, m_WC.m_pointOfShot.TransformDirection(pelletDirection.x, pelletDirection.y, pelletDirection.z) * hit.distance, Color.yellow);

            if (Physics.Raycast(m_WC.m_pointOfShot.position, m_WC.m_pointOfShot.TransformDirection(pelletDirection), out hit, Mathf.Infinity, m_WC.m_layerMask))
            {
                //Debug.Log("hit.transform: " + hit.transform);
                if (hit.transform != null)
                {
                  //  Debug.Log("Did Hit: " + hit.transform.name);

                    if (hit.transform.root.GetComponent<DummyDamageHandler_TF>() != null)
                    {
                        if (hit.transform.root.GetComponent<DummyDamageHandler_TF>().health > 0)
                        {
                            docmentedHits.Add(hit.transform);
                            docmentedHitPoints.Add(hit.point);
                            
                            DummyDamageHandler_TF tempDamageVar = hit.transform.root.GetComponent<DummyDamageHandler_TF>();

                            float distance = (tempDamageVar.transform.position - transform.position).magnitude;

                            string bodyPartHit = hit.transform.gameObject.tag;

                            tempDamageVar.TakeDamage(m_WC.m_damagePerProjectile, distance, bodyPartHit);

                            if (hit.transform.root.GetComponent<DummyDamageHandler_TF>().health <= 0)
                            {
                                Debug.Log("Added: " + docmentedHits.Count);
                                for (int j = docmentedHits.Count - 1; j >= 0; j--)
                                {
                                    docmentedHits[j].GetComponent<Rigidbody>().AddForceAtPosition(((docmentedHitPoints[j] - transform.position).normalized) * (m_WC.m_shotForce / hit.distance), hit.point, ForceMode.Impulse);
                                }
                                Debug.Log("SameFrame Dead");
                                //hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(((hit.point - transform.position).normalized) * (shotForce / hit.distance), hit.point, ForceMode.Impulse);
                                //hit.transform.GetComponent<Rigidbody>().AddForce(((hit.point - transform.position).normalized) * (pelletDamage / hit.distance), ForceMode.Impulse);
                            }
                        }
                        else
                        {
                            if (hit.transform.GetComponent<Rigidbody>() != null)
                            {
                                Debug.Log("Dead");
                                hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(((hit.point - transform.position).normalized) * (m_WC.m_shotForce / hit.distance), hit.point, ForceMode.Impulse);
                                //hit.transform.GetComponent<Rigidbody>().AddForce(((hit.point - transform.position).normalized) * (pelletDamage / hit.distance), ForceMode.Impulse);
                            }
                        }

                    }
                    
                    else
                    {
                        if (hit.transform.GetComponent<Rigidbody>() != null)
                        {
                            if (hit.transform.GetComponent<Rigidbody>().mass <= 2)
                            {
                                //Debug.Log("Hit rigid");
                                hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(((hit.point - transform.position).normalized) * (m_WC.m_shotForce / hit.distance), hit.point, ForceMode.Impulse);
                            }
                        }
                    }
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
