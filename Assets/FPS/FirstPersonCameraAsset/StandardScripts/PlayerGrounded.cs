using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour {
    /*

    [SerializeField]
    private float maxGroundDistance;
    [SerializeField]
    private LayerMask groundLayers;
    [SerializeField]
    private RaycastHit hit;
    [SerializeField]
    private PlayerMovementControls pMCScript;

    // Use this for initialization
    void Awake () {
        maxGroundDistance = 0.1f;
        groundLayers = LayerMask.GetMask("GroundLayers");
        pMCScript = GetComponent<PlayerMovementControls>();
    }
	
	// Update is called once per frame
	void Update () {
        
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), -transform.up * maxGroundDistance, Color.red);

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), -transform.up, out hit, maxGroundDistance, groundLayers))
        {
            Debug.Log("fart");
            pMCScript.isGrounded = true;
        }

        else
        {
            pMCScript.isGrounded = false;
        }

    }
    */
   

  


}
