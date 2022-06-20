using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLob : MonoBehaviour {

    private float projectileSpeed =10;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    public GameObject playerController;

    private void Awake()
    {
        //SDebug.Log("hello");
        rb = GetComponent<Rigidbody>();




    }
    
    private void OnEnable()
    {
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
       
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        
        //if (collision.gameObject != playerController)
        //{
            DestroyProjectile();
        //}
        
    }

    private void DestroyProjectile()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        CancelInvoke();
    }
}
