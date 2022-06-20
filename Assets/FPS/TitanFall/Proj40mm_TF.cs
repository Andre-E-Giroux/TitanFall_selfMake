using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj40mm_TF : MonoBehaviour {

    [SerializeField]
    private WeaponClass m_WC = null;

    public bool reloading;

    private float currentReloadTimerRemaining = 0.0f;

    RaycastHit hit;

    private bool allowShoot = true;

    [SerializeField]
    private float timeUntilNextShot;

    [SerializeField]
    private Animator cannonAnimator;

    [SerializeField]
    private Transform muzzleNode;

    [SerializeField]
    private Transform cameraTransform = null;
    [SerializeField]
    private bool allowReloadCancel = false;

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
        if (m_WC.m_currentBackupAmmo != m_WC.m_maxAmmoCount)
        {
            m_WC.m_currentBackupAmmo = m_WC.m_maxAmmoCount;
        }

        if (m_WC.m_shotForce <= 0)
        {
            m_WC.m_shotForce = 50.0f;
        }

        m_WC.m_currentClipAmmoCount = m_WC.m_maxClipSize;

        allowShoot = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (allowShoot && m_WC.m_currentClipAmmoCount > 0 && timeUntilNextShot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cannonAnimator.SetTrigger("ShotGunTrigger");
                //Instantiate(gunShotParticles, muzzleNode.position, muzzleNode.rotation);

                //

                //Vector3 targetPoint = SendRaycast();
                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.red, 2);

                    //Debug.Log("point: " + hit.point);

                    GameObject obj = ProjObjectPoolerScript.current.GetPooledObject();

                    //float distanceFromCamAndNode = (muzzleNode.position - cameraTransformers.position).magnitude;

                    if (obj != null)
                    {
                        //Debug.Log("There is obj proj");
                        //float angle = (Mathf.Rad2Deg * Mathf.Atan(hit.distance / distanceFromCamAndNode));
    
                        // opp / adj = angle tan -1
                        obj.transform.position = muzzleNode.position;
                        obj.transform.LookAt(hit.point);
                        obj.SetActive(true);
                    }
                    //

                    if (reloading && allowReloadCancel)
                    {
                        currentReloadTimerRemaining = 0;
                        reloading = false;
                    }

                    if (timeUntilNextShot <= 0)
                    {
                        FireProj();
                        timeUntilNextShot = m_WC.m_fireRate;
                    }

                    m_WC.m_currentClipAmmoCount--;
                }
            }
        }

        
        if(timeUntilNextShot > 0)
            timeUntilNextShot -= Time.deltaTime;


        // allow reload
        if (m_WC.m_currentClipAmmoCount < m_WC.m_maxClipSize)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                cannonAnimator.SetTrigger("Reload");
                reloading = true;
                currentReloadTimerRemaining = 0.0f;
            }

            if (reloading)
            {
                currentReloadTimerRemaining += Time.deltaTime;

                //Debug.Log("Reloading");
                if (currentReloadTimerRemaining >= m_WC.m_realoadSpeed)
                {
                    if (m_WC.m_currentBackupAmmo >= (m_WC.m_maxClipSize - m_WC.m_currentClipAmmoCount))
                    {
                        
                        m_WC.m_currentBackupAmmo += (m_WC.m_currentClipAmmoCount - m_WC.m_maxClipSize);
                        m_WC.m_currentClipAmmoCount = m_WC.m_maxClipSize;
      
                    }

                    else
                    {
                        m_WC.m_currentClipAmmoCount += m_WC.m_currentBackupAmmo;
                        m_WC.m_currentBackupAmmo = 0;
                    }
                    currentReloadTimerRemaining = 0;
                    //Debug.Log("current: " + m_WC.m_maxAmmoCount);
                    reloading = false;
                }
            }
        }
    }
    //private Vector3 SendRaycast()
    //{
    //    if (Physics.Raycast(m_WC.m_pointOfShot.position, Vector3.forward, out hit, Mathf.Infinity, m_WC.m_layerMask))
    //    {
    //        return hit.point;
    //    }
    //    Debug.LogError("NO HIT");
    //    return Vector3.zero;
        
    //}
    public void FireProj()
    {
        Debug.Log("40mm fired");
    }



    //IEnumerator FireRateWait()
    //{
    //    allowShoot = false;
    //    yield return new WaitForSeconds(fireRate);
    //    allowShoot = true;
    //}


}
