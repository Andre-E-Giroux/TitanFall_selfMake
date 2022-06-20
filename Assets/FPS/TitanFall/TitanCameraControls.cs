using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanCameraControls : MonoBehaviour
{
    [SerializeField]
    private Transform titanTransform;

    public Transform cameraParent = null;
    public Transform cameraTransform;
   

    [SerializeField]
    private float mouseSensitivetyX = 1;
    [SerializeField]
    private float mouseSensitivetyY = 1;

    private float xAxisClamp;
   
    
    


    [SerializeField]
    private float rotAcc = 5;

    

    private void Start()
    {
      
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






        float mouseX = Input.GetAxis("Mouse X");

        float rotationNumberX = mouseX * mouseSensitivetyX;
        // + new Vector3(0, 0, cameraParent.localEulerAngles.z)
        Vector3 cameraRotation = cameraTransform.localEulerAngles + new Vector3(0, 0, cameraParent.localEulerAngles.z);
        Vector3 playerRotation = titanTransform.rotation.eulerAngles;

        //cameraRotation.x -= rotationNumberY;

        cameraRotation.z = 0;

        playerRotation.y += rotationNumberX;

        cameraTransform.localRotation = Quaternion.Euler(cameraRotation);
        titanTransform.rotation = Quaternion.Euler(playerRotation);




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
