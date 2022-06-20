using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float mouseSensitivetyX = 1;
    [SerializeField]
    private float mouseSensitivetyY= 1;

    private float xAxisClamp;

    	
	// Update is called once per frame
	void Update () {
        CameraRotation();
    }


    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationNumberX = mouseX * mouseSensitivetyX;
        float rotationNumberY = mouseY * mouseSensitivetyY;

        Vector3 cameraRotation = cameraTransform.rotation.eulerAngles;
        Vector3 playerRotation = playerTransform.rotation.eulerAngles;

        cameraRotation.x -= rotationNumberY;

        cameraRotation.z = 0;

        playerRotation.y += rotationNumberX;


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
        playerTransform.rotation = Quaternion.Euler(playerRotation);
    }
}
