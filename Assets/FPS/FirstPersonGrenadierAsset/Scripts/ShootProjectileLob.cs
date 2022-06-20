using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileLob : MonoBehaviour {

    
    private float fireRate = 0.2f;

    
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;






        
            GameObject obj = ProjObjectPoolerScript.current.GetPooledObject();

            if (obj == null)
            {
                return;
            }


            if (obj.GetComponent<ProjectileLob>().playerController != transform.parent.parent.gameObject)
            {
                Debug.Log("fffff");
                //obj.GetComponent<ProjectileBolt>().playerController = gameObject/*transform.parent.parent.gameObject*/;
                Physics.IgnoreCollision(obj.GetComponent<Collider>(), transform.parent.parent.GetComponent<Collider>());
            }
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;


           
        

    }
    



    private void Update()
    {
        


        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject obj = ProjObjectPoolerScript.current.GetPooledObject();

        if (obj == null)
        {
            return;
        }

        
        if (obj.GetComponent<ProjectileLob>().playerController != transform.parent.parent.gameObject)
        {
            obj.GetComponent<ProjectileLob>().playerController = transform.parent.parent.gameObject;
            Physics.IgnoreCollision(obj.GetComponent<Collider>(), transform.parent.parent.GetComponent<Collider>());
        }
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

    }


    












        /*
    
    [SerializeField]
    private GameObject projectile;
    

    private float fireRate;

    private int pooledProjectileAmount = 10;
    private List<GameObject> projectilePool = new List<GameObject>();


    private bool willGrow = true;



	// Use this for initialization
	private void Start () {

        for (int i = 0; i < pooledProjectileAmount; i++)
        {
            GameObject obj = Instantiate(projectile);
            obj.SetActive(false);
            projectilePool.Add(obj);
        }



        fireRate = 0.5f;
        InvokeRepeating("Shoot", fireRate, fireRate);
	}
	
	private void Shoot()
    {
        for (int i = 0; i < projectilePool.Count; i++)
        {
            if (!projectilePool[i].activeInHierarchy)
            {
                projectilePool[i].transform.position = transform.position;
                projectilePool[i].transform.rotation = transform.rotation;
                projectilePool[i].SetActive(true);
                break;
            }
        }


        if (willGrow)
        {
            GameObject obj = Instantiate(projectile);
            projectilePool.Add(obj);
            obj.SetActive(false);
        }

    }
    */
}