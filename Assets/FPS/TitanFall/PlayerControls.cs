using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float maxHorizontalStandardVelocity = 30.0f;
    [SerializeField]
    private float maxHorizontalSprintVelocity = 13.0f;
    private float currentMaxHorizontalVelocity = 0.0f;

    [SerializeField]
    private float maxWallMoveVelocity = 60.0f;

    [SerializeField]
    private float acceleration = 40;
    [SerializeField]
    private float midAirAccelerationPenalty = 2;

    [SerializeField]
    private float decceleration = 200;

    // jump
    [SerializeField]
    private float groundJumpForce = 4;
    [SerializeField]
    private float airJumpForce = 2;

    [SerializeField]
    private float wallJumpVerticalForce = 3;
    [SerializeField]
    private float wallJumpHorizontalForce = 3;
    [SerializeField]
    private float wallJumpForwardForce = 5;

    private bool grounded = false;

    [SerializeField]
    private bool allowDoubleJump = false;
    //
    

    [HideInInspector]
    public bool isOnWall = false;

    [SerializeField]
    private float wallVerticalForceIncrease = 2;


    [SerializeField]
    private float minDistnaceToWall = 1.2f;

    [SerializeField]
    private float minVelocityToWallRun = 8.0f;

    private bool recentOnWall;
    private float maxReecentOnWallTime = 1;
    private float recentOnWallTime = 0;

    [SerializeField]
    private CameraControl_TF CCTF = null;

    [SerializeField]
    private float connectionToWallAcceleration = 45;

    [SerializeField]
    private bool isHardLockOn = false;



    // grab ledges
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float distanceOfLedGrabCheck = 1.90f;
    [SerializeField]
    private Transform pointOfLedgeGrabNode = null;


    private bool isHoisting = false;
    [SerializeField]
    private float m_hoistSpeed = 1.5f;
    private Vector3 m_pointOfReference = Vector3.zero;
    //
    [SerializeField]
    private float m_breakSpeed = 0.5f;


    // Use this for initialization
    void Start () {
		if(!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
        if(!CCTF)
        {
            CCTF = GetComponent<CameraControl_TF>();
        }
        currentMaxHorizontalVelocity = maxHorizontalStandardVelocity;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isHoisting)
        {
            if (!isOnWall && Input.GetKeyDown(KeyCode.Space))
            {
                // Debug.Log("Jump");
                Jump();
            }

            CCTF.CameraRotationY();

            CheckForGround();

            if (!grounded && Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) > minVelocityToWallRun)
            {
                CheckForWall();
            }
            else
            {
                if (isOnWall)
                {
                    isOnWall = false;
                    isHardLockOn = false;
                }
            }
            if (recentOnWall)
            {
                recentOnWallTime += Time.deltaTime;
                if (recentOnWallTime >= maxReecentOnWallTime)
                {
                    recentOnWallTime = 0;
                    recentOnWall = false;
                }
            }


            if (!isOnWall)
            {
                if (isHardLockOn)
                {
                    isHardLockOn = false;
                }
                CCTF.CameraStandardRotationX();
                if (grounded)
                {
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                    {


                        /*
                        if (transform.InverseTransformDirection(rb.velocity).x < -0.05f)
                        {
                            HaltOneSided(true);
                        }

                        rb.AddForce(transform.right * acceleration, ForceMode.Force);


                        */
                        //forward
                        if (Input.GetKey(KeyCode.W))
                        {
                            if (transform.InverseTransformDirection(rb.velocity).z < -0.5f)
                            {
                                //    //rb.velocity = transform.TransformDirection(new Vector3(rb.velocity.x / decceleration, rb.velocity.y, rb.velocity.z));
                                //    //Vector3 temp = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z / decceleration);
                                //    //rb.velocity = transform.InverseTransformDirection(temp);
                                rb.AddForce(transform.forward * acceleration * m_breakSpeed, ForceMode.Force);

                            }
                            else
                            rb.AddForce(transform.forward * acceleration, ForceMode.Force);
                        }
                        // left
                        if (Input.GetKey(KeyCode.A))
                        {
                            if (transform.InverseTransformDirection(rb.velocity).x > 0.5f)
                                rb.AddForce(-transform.right * acceleration * m_breakSpeed, ForceMode.Force);
                            //{
                            //    //rb.velocity = transform.TransformDirection(new Vector3(rb.velocity.x / decceleration, rb.velocity.y, rb.velocity.z));
                            //    //Vector3 temp = new Vector3(rb.velocity.x / decceleration, rb.velocity.y, rb.velocity.z);
                            //   // rb.velocity = transform.InverseTransformDirection(temp);
                            //}
                            else
                            rb.AddForce(-transform.right * acceleration, ForceMode.Force);
                        }
                        // back
                        if (Input.GetKey(KeyCode.S))
                        {
                            if (transform.InverseTransformDirection(rb.velocity).z > 0.5f)//
                                rb.AddForce(-transform.forward * acceleration * m_breakSpeed, ForceMode.Force);
                            //{
                            //    // rb.velocity = transform.TransformDirection(new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z / decceleration));
                            //    //Vector3 temp = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z / decceleration);
                            //    //rb.velocity = transform.InverseTransformDirection(temp);
                            //}
                            else
                            rb.AddForce(-transform.forward * acceleration, ForceMode.Force);
                        }
                        // right
                        if (Input.GetKey(KeyCode.D))
                        {
                            if (transform.InverseTransformDirection(rb.velocity).x < -0.5f)//
                                rb.AddForce(transform.right * acceleration * m_breakSpeed, ForceMode.Force);
                            //{
                            //    //rb.velocity = transform.TransformDirection(new Vector3(rb.velocity.x / decceleration, rb.velocity.y, rb.velocity.z));
                            //   // Vector3 temp = new Vector3(rb.velocity.x / decceleration, rb.velocity.y, rb.velocity.z);
                            //   // rb.velocity = transform.InverseTransformDirection(temp);
                            //}
                            else
                            rb.AddForce(transform.right * acceleration, ForceMode.Force);
                        }

                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            if (currentMaxHorizontalVelocity != maxHorizontalSprintVelocity)
                                currentMaxHorizontalVelocity = maxHorizontalSprintVelocity;
                        }
                        else
                        {
                            if (currentMaxHorizontalVelocity != maxHorizontalStandardVelocity)
                                currentMaxHorizontalVelocity = maxHorizontalStandardVelocity;
                        }

                    }
                    else
                    {
                        Halt();
                    }
                }
                else
                {
                    LedgeGrab();

                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                    {
                        //forward
                        if (Input.GetKey(KeyCode.W))
                        {
                            rb.AddForce(transform.forward * midAirAccelerationPenalty, ForceMode.Force);
                        }
                        // left
                        if (Input.GetKey(KeyCode.A))
                        {
                            rb.AddForce(-transform.right * midAirAccelerationPenalty, ForceMode.Force);
                        }
                        // back
                        if (Input.GetKey(KeyCode.S))
                        {
                            rb.AddForce(-transform.forward * midAirAccelerationPenalty, ForceMode.Force);
                        }
                        // right
                        if (Input.GetKey(KeyCode.D))
                        {
                            rb.AddForce(transform.right * midAirAccelerationPenalty, ForceMode.Force);
                        }
                    }
                }
                if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) > currentMaxHorizontalVelocity)
                {
                    //Debug.Log("it is greater!");
                    Vector3 hold = rb.velocity.normalized * currentMaxHorizontalVelocity;
                    rb.velocity = new Vector3(hold.x, rb.velocity.y, hold.z);
                }

            }
            else
            {
                if (!allowDoubleJump)
                {
                    allowDoubleJump = true;
                }
                //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * wallVerticalForceIncrease, ForceMode.Force);
            }
        }
        else
        {
            Hoist();
        }
    }

    private void Halt()
    {
        
        if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) > 1.2f)
        {
            rb.velocity = new Vector3(rb.velocity.x / decceleration, rb.velocity.y, rb.velocity.z / decceleration);
        }
    }
    private void Jump()
    {
        //Debug.Log("standard jump");
        if(grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, groundJumpForce, rb.velocity.z);
        }
        else
        {
            if(allowDoubleJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, airJumpForce, rb.velocity.z);
                allowDoubleJump = false;
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    //forward
                    if (Input.GetKey(KeyCode.W))
                    {
                            rb.AddForce(transform.forward * acceleration, ForceMode.Impulse);
                    }
                    // left
                    if (Input.GetKey(KeyCode.A))
                    {
                            rb.AddForce(-transform.right * acceleration, ForceMode.Impulse);
                    }
                    // back
                    if (Input.GetKey(KeyCode.S))
                    {

                            rb.AddForce(-transform.forward * acceleration, ForceMode.Impulse);
                    }
                    // right
                    if (Input.GetKey(KeyCode.D))
                    {

                            rb.AddForce(transform.right * acceleration, ForceMode.Impulse);
                    }
                }
            }
        }
    }


    //
    [SerializeField]
    private Transform[] GroundSensors = null;

    [SerializeField]
    private float groundDetectionLength = 1;
    
    private void CheckForGround()
    {
        for (int i = GroundSensors.Length - 1; i >= 0; i--)
        {
            RaycastHit hit;
            if (Physics.Raycast(GroundSensors[i].position, GroundSensors[i].TransformDirection(-Vector3.up), out hit, groundDetectionLength))
            {
                SetGround(true);
                Debug.DrawRay(GroundSensors[i].position, GroundSensors[i].TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
                return;
            }

        }
            SetGround(false);
    }
    
    private void SetGround(bool isGroundDetected)
    {
        if (isGroundDetected)
        {
            if (!grounded)
            {
                grounded = true;
            }
            if (!allowDoubleJump)
            {
                allowDoubleJump = true;
            }

            isOnWall = false;
        }
        else
        {
            if (grounded)
            {
                grounded = false;
            }
        }
        
    }
    
    
    
    //
    [SerializeField]
    private float wallDetectionLength;

    [SerializeField]
    private Transform[] wallTriggerSensors_Right;
    [SerializeField]
    private Transform[] wallTriggerSensors_Left;


    private void CheckForWall()
    {
        if (!grounded)
        {
            for (int i = wallTriggerSensors_Right.Length - 1; i >= 0; i--)
            {

                RaycastHit hit;
                Debug.DrawRay(wallTriggerSensors_Right[i].position, wallTriggerSensors_Right[i].TransformDirection(Vector3.forward) * wallDetectionLength, Color.green);
                if (Physics.Raycast(wallTriggerSensors_Right[i].position, wallTriggerSensors_Right[i].TransformDirection(Vector3.forward), out hit, wallDetectionLength))
                {
                    if (!recentOnWall)
                    {

                        if (!isOnWall)
                        {
                            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                            isOnWall = true;
                        }

                        Vector3 direction = (transform.position - hit.point).normalized;
                        Vector3 playerPosition = hit.point + (direction * minDistnaceToWall);
                        if (!isHardLockOn)
                        {
                            if (hit.distance < minDistnaceToWall - 0.5f || hit.distance < minDistnaceToWall + 0.5f)
                            {
                                Debug.Log("Alert");
                                transform.position += new Vector3((transform.position.x - playerPosition.x), 0, (transform.position.z - playerPosition.z)) / connectionToWallAcceleration;

                                //transform.position += (transform.position - playerPosition) / connectionToWallAcceleration;
                            }
                            if (hit.distance < minDistnaceToWall - 0.5f && hit.distance < minDistnaceToWall + 0.5f)
                            {
                                Debug.Log("Accepted");
                                isHardLockOn = true;
                            }
                        }
                        else
                        {
                            transform.position = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
                        }
                        //else
                        //{
                        //    Debug.Log("Else");
                        //   // transform.position = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);

                        //}
                        rb.AddForce(transform.forward * acceleration, ForceMode.Force);
                        if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) > maxWallMoveVelocity)
                        {
                            Vector3 hold = rb.velocity.normalized * maxWallMoveVelocity;
                            rb.velocity = new Vector3(hold.x, rb.velocity.y, hold.z);
                        }

                       
                       

                        ////////////////////////////
                        CCTF.CameraWallRotationX(false);
                        Quaternion temp = CCTF.cameraTransform.rotation;
                        transform.eulerAngles = Quaternion.FromToRotation(-Vector3.right, hit.normal).eulerAngles;
                        //transform.eulerAngles = hit.normal;
                        CCTF.cameraTransform.rotation = temp;
                        ////////////////////////////

                        //rb.velocity = transform.TransformDirection(rb.velocity.);

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            recentOnWall = true;
                            isOnWall = false;
                            recentOnWallTime = 0;
                            WallJump(hit.normal);
                        }
                    }
                    return;
                }
            }
            for (int i = wallTriggerSensors_Left.Length - 1; i >= 0; i--)
            {
                RaycastHit hit;
                Debug.DrawRay(wallTriggerSensors_Right[i].position, wallTriggerSensors_Right[i].TransformDirection(Vector3.forward) * wallDetectionLength, Color.green);
                if (Physics.Raycast(wallTriggerSensors_Left[i].position, wallTriggerSensors_Left[i].TransformDirection(Vector3.forward), out hit, wallDetectionLength))
                {
                    if (!recentOnWall)
                    {

                        if (!isOnWall)
                        {
                            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                            isOnWall = true;
                        }
                        Vector3 direction = (transform.position - hit.point).normalized;
                        Vector3 playerPosition = hit.point + (direction * minDistnaceToWall);

                        if (!isHardLockOn)
                        {
                            if (hit.distance < minDistnaceToWall - 0.5f || hit.distance < minDistnaceToWall + 0.5f)
                            {
                                Debug.Log("Alert");
                                transform.position += new Vector3((transform.position.x - playerPosition.x), 0, (transform.position.z - playerPosition.z)) / connectionToWallAcceleration;

                                // transform.position += (transform.position - playerPosition) / connectionToWallAcceleration;
                            }
                            if (hit.distance < minDistnaceToWall - 0.5f && hit.distance < minDistnaceToWall + 0.5f)
                            {
                                isHardLockOn = true;
                                Debug.Log("Accepted");
                            }
                        }
                        else
                        {
                            transform.position = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
                        }
                            //else
                            //{
                            //    Debug.Log("Else");
                            //    //transform.position = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
                            //}
                            rb.AddForce(transform.forward * acceleration, ForceMode.Force);
                        if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) > maxWallMoveVelocity)
                        {
                            Vector3 hold = rb.velocity.normalized * maxWallMoveVelocity;
                            rb.velocity = new Vector3(hold.x, rb.velocity.y, hold.z);
                        }

                        
                        /////////////////////////
                        CCTF.CameraWallRotationX(true);
                        Quaternion temp = CCTF.cameraTransform.rotation;
                        transform.eulerAngles = Quaternion.FromToRotation(Vector3.right, hit.normal).eulerAngles;
                        //transform.eulerAngles = hit.normal;
                        CCTF.cameraTransform.rotation = temp;
                        /////////////////////////

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            isHardLockOn = false;
                            recentOnWall = true;
                            isOnWall = false;
                            recentOnWallTime = 0;
                            WallJump(hit.normal);
                        }
                    }
                    return;
                }
            }
            if (isOnWall)
            {
                isOnWall = false;
            }
            if(recentOnWall)
            {
                recentOnWall = false;
                recentOnWallTime = 0;
            }
        }
        else
        {
            if (isOnWall)
            {
                isOnWall = false;
            }
            if(recentOnWall)
            {
                recentOnWall = false;
            }
        }

    }
    private void WallJump(Vector3 wallNormalVector)
    {
        recentOnWall = true;
        isOnWall = false;
        rb.AddForce(((wallNormalVector * wallJumpHorizontalForce)), ForceMode.Impulse);
        rb.AddForce(((transform.forward * wallJumpForwardForce)), ForceMode.Impulse);
        rb.velocity = new Vector3(rb.velocity.x, (wallJumpVerticalForce), rb.velocity.z);
    }

    private void LedgeGrab()
    {

        Debug.DrawRay(pointOfLedgeGrabNode.position, pointOfLedgeGrabNode.TransformDirection(Vector3.forward) * distanceOfLedGrabCheck, Color.cyan);
        RaycastHit hit;
        if (Physics.Raycast(pointOfLedgeGrabNode.position, pointOfLedgeGrabNode.TransformDirection(Vector3.forward), out hit, distanceOfLedGrabCheck, layerMask))
        {
            // there is a ledge!
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Grab");
                m_pointOfReference = hit.point;
                isHoisting = true;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                
                //transform.position = hit.point;
            }
           // Debug.Log("Did Hit Ledge");
        }
    }
    
    
    private void Hoist()
    {
        //////
        CCTF.CameraStandardRotationX();
        CCTF.CameraRotationY();
        //////
        ///

        // Move our position a step closer to the target.
        float step = m_hoistSpeed * Time.deltaTime; // calculate distance to move

        if (transform.position.y < m_pointOfReference.y)
        {
            Vector3 vecVert = new Vector3(transform.position.x, m_pointOfReference.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, vecVert, step);
            Debug.Log("Move");
        }
        else
        {
            //if(transform.position.y != m_pointOfReference.y)
            //{
            //    transform.position = new Vector3(transform.position.x, m_pointOfReference.y, transform.position.z);
            //}

            Vector3 vecVert = new Vector3(m_pointOfReference.x, transform.position.y, m_pointOfReference.z);

            transform.position = Vector3.MoveTowards(transform.position, vecVert, step);

            Debug.Log("move : velo = " + rb.velocity);
            
        }
        if(transform.position == m_pointOfReference)
        {
            //end
            isHoisting = false;
            rb.useGravity = true;
            rb.velocity = transform.forward * step * 50;
        }
    }
}
