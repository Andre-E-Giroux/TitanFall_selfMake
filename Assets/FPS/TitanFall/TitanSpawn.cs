using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitanSpawn : MonoBehaviour
{
    [SerializeField]
    private List<Transform> navmeshSensors = new List<Transform>();
    [SerializeField]
    private float movePointDistance = 0.5f;
    [SerializeField]
    private Transform titanFallNodesParent;
    [SerializeField]
    private List<Transform> titanFallNodes = new List<Transform>();
    public Transform titan = null;
    [SerializeField]
    private Rigidbody titanRB = null;

    private bool titanIsDropping = false;


    private Vector3 pointOfLanding = Vector3.zero;
    [SerializeField]
    private float dropSpeed = 15.0f;


    //[SerializeField]
    //private ParticleTitanLandObjectPoolerScript titanFallParticleLandSystem = null;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform sensor in transform.GetComponentInChildren<Transform>()) 
        {
            navmeshSensors.Add(sensor);
        }

        foreach (Transform node in titanFallNodesParent.GetComponentInChildren<Transform>())
        {
            titanFallNodes.Add(node);
        }

        if(titanRB == null)
        {
            titanRB = titan.GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dropStep = dropSpeed * Time.deltaTime;
        if (titanIsDropping)
        {
            Debug.Log("dropping");
            titan.position = Vector3.MoveTowards(titan.position, pointOfLanding, dropStep);
            if((titan.position.y - pointOfLanding.y) == 0)
            {
                titanRB.useGravity = true;


                //
                GameObject obj = ParticleTitanLandObjectPoolerScript.current.GetPooledObject();

                if (obj == null)
                {
                    return;
                }


                //if (obj.GetComponent<ProjectileBolt>().playerController != transform.parent.parent.gameObject)
                //{
                //    obj.GetComponent<ProjectileBolt>().playerController = transform.parent.parent.gameObject;
                //    Physics.IgnoreCollision(obj.GetComponent<Collider>(), transform.parent.parent.GetComponent<Collider>());
                //}
                obj.transform.position = titan.position;
                //obj.transform.rotation = titan.rotation;
                obj.SetActive(true);
                //




                titanIsDropping = false;
            }

        }

    }

    public void SendNavRays()
    {
        List<int> pointsTrue = new List<int>();
        for (int i = 0; i < navmeshSensors.Count; i++)
        {

        
            //NavMeshHit navHit;
            //NavMesh.Raycast(ray.position, )



            RaycastHit hit;
            
            if (Physics.Raycast(navmeshSensors[i].position, navmeshSensors[i].forward, out hit))
            {
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(hit.point, out navHit, 1, NavMesh.AllAreas))
                {
                    Debug.Log("In nav");
                    pointsTrue.Add(i);
                    if(i == 4)
                    {
                        Debug.Log("hit point is the target landing");
                        pointOfLanding = hit.point;
                    }
                }
                else
                {
                    Debug.Log("Not nav");
                }
            }
        }

        if(pointsTrue.Count == navmeshSensors.Count)
        {
            Debug.Log("SuitablePoint found spawn titan");
            // spawn titan
            
            ActivateTitanFall(transform.position);
        }
        else
        {
            Debug.Log("SuitablePoint not found, searching for optimal TitanFall point...");
            // The child ray points config
            // 0 1 2
            // 3 4 5
            // 6 7 8
            //

            // Get spawn point cloesest to middle
            float closestDistance = -1;
            Transform closestNode = transform;
            foreach(Transform node in titanFallNodes)
            {
                if(closestDistance < 0)
                {
                    closestDistance = (navmeshSensors[4].position - node.position).magnitude;
                    closestNode = node;
                    
                }
                else
                {
                    float tempDis = (navmeshSensors[4].position - node.position).magnitude;
                    if (tempDis < closestDistance)
                    {
                        closestDistance = tempDis;
                        closestNode = node;
                    }
                }
            }

            transform.position = new Vector3(closestNode.position.x, transform.position.y, closestNode.position.z);
            Debug.Log("node is the point of lading");
            pointOfLanding = closestNode.position;
            ActivateTitanFall(transform.position);
        }
    }




    private void ActivateTitanFall(Vector3 TitanPointOfEntry)
    {
        titan.position = TitanPointOfEntry;
        //titan.GetComponent<Rigidbody>().velocity = new Vector3(0, -100, 0);
        titanRB.useGravity = false;
        titanIsDropping = true;

    }
}
