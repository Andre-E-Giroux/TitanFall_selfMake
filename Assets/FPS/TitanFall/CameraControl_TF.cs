using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl_TF : MonoBehaviour {

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    public Transform cameraTransform;


    [SerializeField]
    private Transform cameraNode;

    [SerializeField]
    private float mouseSensitivetyX = 1;
    [SerializeField]
    private float mouseSensitivetyY = 1;

    private float xAxisClamp;
    [SerializeField]
    private PlayerControls pC;

    public bool hasBeenOnWall = false;
   
    public Transform cameraParent = null;
    [SerializeField]
    private float rotAcc = 5;

    private float repoStartTime = 0.0f;
    [SerializeField]
    private float repoCamSpeed = 5.0f;
    [SerializeField]
    private float repoBodySpeed = 7.0f;

    private float repoLength = 0.0f;
    private Vector3 repoStartRot = new Vector3(); 

    private void Start()
    {
        if(pC == null)
        {
            pC = GetComponent<PlayerControls>();
        }
    }

   

    public void CameraStandardRotationX()
    {

       
        if (cameraParent.localEulerAngles.z < 180 && cameraParent.localEulerAngles.z != 0)
        {
            // optimize screen -> shake
            if (cameraParent.localEulerAngles.z > 0.5f && cameraParent.localEulerAngles.z < 359.5f)
            {
                cameraParent.Rotate(Vector3.forward * ((-rotAcc * Time.deltaTime)));
            }
            else
            {
                cameraParent.localEulerAngles = new Vector3(cameraParent.localEulerAngles.x, cameraParent.localEulerAngles.y, 0);
            }
        }

        if (cameraParent.localEulerAngles.z > 180 && cameraParent.localEulerAngles.z != 0)
        {
            // optimize screen -> shake
            if (cameraParent.localEulerAngles.z > 0.5f && cameraParent.localEulerAngles.z < 359.5f)
            {
                cameraParent.Rotate(Vector3.forward * ((rotAcc * Time.deltaTime)));
            }
            else
            {
                cameraParent.localEulerAngles = new Vector3(cameraParent.localEulerAngles.x, cameraParent.localEulerAngles.y, 0);
            }
        }


        if (hasBeenOnWall)
        {
            repoStartRot = cameraTransform.eulerAngles;
            playerTransform.eulerAngles = new Vector3(playerTransform.eulerAngles.x, cameraTransform.eulerAngles.y, playerTransform.eulerAngles.z);
            cameraTransform.localEulerAngles = new Vector3(cameraTransform.localEulerAngles.x, 0, 0);
            cameraTransform.eulerAngles = new Vector3(repoStartRot.x, cameraTransform.eulerAngles.y, cameraTransform.eulerAngles.z);
            hasBeenOnWall = false;
        }









        float mouseX = Input.GetAxis("Mouse X");

        float rotationNumberX = mouseX * mouseSensitivetyX;
        // + new Vector3(0, 0, cameraParent.localEulerAngles.z)
        Vector3 cameraRotation = cameraTransform.localEulerAngles + new Vector3(0, 0, cameraParent.localEulerAngles.z);
        Vector3 playerRotation = playerTransform.rotation.eulerAngles;

        //cameraRotation.x -= rotationNumberY;

        cameraRotation.z = 0;

        playerRotation.y += rotationNumberX;

        cameraTransform.localRotation = Quaternion.Euler(cameraRotation);
        playerTransform.rotation = Quaternion.Euler(playerRotation);


        //if(cameraTransform.rotation != Quaternion.Euler(new Vector3(cameraTransform.eulerAngles.x, 0, 0)))
        //   cameraTransform.rotation = Quaternion.RotateTowards(cameraTransform.rotation, Quaternion.Euler(new Vector3(cameraTransform.eulerAngles.x, 0, 0)), repoCamSpeed);


        //if (playerTransform.rotation != Quaternion.Euler(new Vector3(playerTransform.eulerAngles.x, cameraTransform.eulerAngles.y, playerTransform.eulerAngles.z)))
        //    playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, Quaternion.Euler(new Vector3(playerTransform.eulerAngles.x, cameraTransform.eulerAngles.y, playerTransform.eulerAngles.z)), repoBodySpeed);


        //if (cameraTransform.localRotation != Quaternion.Euler(new Vector3(cameraTransform.localEulerAngles.x, 0, 0)))
        //    cameraTransform.localRotation = Quaternion.RotateTowards(cameraTransform.localRotation, Quaternion.Euler(new Vector3(cameraTransform.localEulerAngles.x, 0, 0)), repoCamSpeed);

    }


    // askew the screen based on which side the wall is, corresponding to the player
    public void CameraWallRotationX(bool leftSide)
    {

        if (leftSide)
        {
            cameraParent.Rotate(Vector3.forward * ((-rotAcc * Time.deltaTime)));
            if (cameraParent.localEulerAngles.z < 340 && cameraParent.localEulerAngles.z != 0)
            {
                cameraParent.localEulerAngles = new Vector3(cameraParent.localEulerAngles.x, cameraParent.localEulerAngles.y, -20);
            }
        }
        else
        {
            cameraParent.Rotate(Vector3.forward * (rotAcc * Time.deltaTime));
            if (cameraParent.localEulerAngles.z > 20)
            {
                cameraParent.localEulerAngles = new Vector3(cameraParent.localEulerAngles.x, cameraParent.localEulerAngles.y, 20);
            }
        }


        if (!hasBeenOnWall)
        {
            hasBeenOnWall = true;
        }

        float mouseX = Input.GetAxis("Mouse X");

        float rotationNumberX = mouseX * mouseSensitivetyX;

        Vector3 cameraRotation = cameraTransform.localEulerAngles /*+ playerTransform.localEulerAngles*/ + cameraParent.localEulerAngles;

        cameraRotation.y += rotationNumberX;

        cameraTransform.localRotation = Quaternion.Euler(cameraRotation);
    }

    public void CameraRotationY()
    {
        //float mouseX = Input.GetAxis("Mouse X");
         float mouseY = Input.GetAxis("Mouse Y");

        //float rotationNumberX = mouseX * mouseSensitivetyX;
        float rotationNumberY = mouseY * mouseSensitivetyY;

        Vector3 cameraRotation = cameraTransform.rotation.eulerAngles;
        //Vector3 playerRotation = playerTransform.rotation.eulerAngles;

        cameraRotation.x -= rotationNumberY;

        cameraRotation.z = 0;

        //playerRotation.y += rotationNumberX;


        xAxisClamp -= rotationNumberY;


        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            cameraRotation.x = 90;
        }

        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            cameraRotation.x = 270;
        }


        cameraTransform.rotation = Quaternion.Euler(cameraRotation);
        //playerTransform.rotation = Quaternion.Euler(playerRotation);
    }
}
