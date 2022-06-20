using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControls : MonoBehaviour
{



    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetKeyDown(KeyCode.Space))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);



    }











    /*
    private float movementSpeed;

    private float jumpStrength;


    //private Rigidbody rb;
    

    public bool isGrounded;
    private CharacterController cController;

    private void Awake()
    {
        movementSpeed = 10;
        jumpStrength = 5;

        //rb = GetComponent<Rigidbody>();

        cController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }


    private void Move()
    {
        float vertic = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");

        //transform.Translate(Vector3.forward * vertic * movementSpeed);
        //transform.Translate(Vector3.right * horiz * movementSpeed);

        //rb.AddForce(transform.forward * vertic * movementSpeed, ForceMode.Force);
        //rb.AddForce(transform.right * horiz * movementSpeed, ForceMode.Force);

        //rb.AddForce(transform.right * movementSpeed, ForceMode.Force);




    }


    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(transform.up * jumpStrength, ForceMode.Impulse);
        }
    }


    */












}
