using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanFallPointPicker : MonoBehaviour
{
    private GameObject titan;
    [SerializeField]
    private InteractWithTitan IWT;

    [SerializeField]
    private Transform titanSpawnPoint = null;

    [SerializeField]
    private Transform playerCamera = null;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float heightCheck = 10.0f;
    [SerializeField]
    private float maxDistance = 10.0f;
    [SerializeField]
    private TitanSpawn TS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            RaycastHit hit;
            Debug.DrawRay(playerCamera.position, playerCamera.forward * maxDistance, Color.green);
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, layerMask))
            {
                titanSpawnPoint.position = new Vector3(hit.point.x, heightCheck, hit.point.z);
                Debug.Log("AlertWithin Range");
            }
            else
            {
                Vector3 newRefPoint = playerCamera.forward * maxDistance;
                titanSpawnPoint.position = new Vector3(newRefPoint.x, heightCheck, newRefPoint.z);
                Debug.Log("OutOfRange");
            }

            TS.SendNavRays();
        }
    }

    public void SummonTitan(Vector3 pointOfReference)
    {

    }
}
