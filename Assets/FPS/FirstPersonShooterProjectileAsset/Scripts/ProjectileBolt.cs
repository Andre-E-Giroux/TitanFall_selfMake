using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBolt : MonoBehaviour {
    [SerializeField]
    private float projectileSpeed =2;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    public GameObject playerController;


    [SerializeField]
    private float maxDistance = 5.5f;

    [SerializeField]
    private ActorList m_aList = null;

    [SerializeField]
    private float baseDamage40mm = 100;
    [SerializeField]
    private float pushForce = 40.0f;

    [SerializeField]
    private TrailRenderer trail = null;
    
    private void Awake()
    {
        //SDebug.Log("hello");
        if(m_aList == null)
        {
            m_aList = FindObjectOfType<ActorList>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if(!trail)
        {
            trail = transform.GetChild(0).GetComponent<TrailRenderer>();
        }
        
    }
    
    private void OnEnable()
    {
        trail.Clear();
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
        //timeCurrentRadius = 0;
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        //
        GameObject obj = ParticleObjectPoolerScript_40mmBoom.current.GetPooledObject();

        if (obj != null)
        {
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);
        }
        //
        //trail.GetComponent<TrailRenderer>().emitting = false;

        rb.isKinematic = true;
        

        if (collision.gameObject.GetComponent<DummyDamageHandler_TF>())
        {
            collision.gameObject.GetComponent<DummyDamageHandler_TF>().InstaDeath();
        }


        // call function to check for all point
        m_aList.CheckForExplosionDistance(transform, maxDistance, baseDamage40mm, pushForce);

        DestroyProjectile();


        

        

        //Instantiate(explosionObject, transform.position, transform.rotation);
        //sphereCollider.enabled = true;
        //projMeshRenderer.enabled = false;


    }

    //private void FixedUpdate()
    //{
    //    if (!projMeshRenderer.enabled)
    //    {
    //        sphereCollider.radius += Time.fixedDeltaTime * exploExpansionSpeed;
    //        timeCurrentRadius += Time.fixedDeltaTime;

    //        Debug.Log("Time current: " + timeCurrentRadius);

    //        if (timeCurrentRadius >= timeUntilMaxRadius)
    //        {
    //            Debug.Log(timeCurrentRadius);
    //            DestroyProjectile();
    //        }
    //    }
    //}

    private void DestroyProjectile()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
       // sphereCollider.radius = 0.25f;
        //projMeshRenderer.enabled = true;
        rb.isKinematic = false;
        //sphereCollider.enabled = false;
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        CancelInvoke();
    }
}
