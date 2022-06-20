using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanControls : MonoBehaviour
{
    [SerializeField]
    private float maxHorizontalStandardVelocity = 8.0f;

    [SerializeField]
    private float maxHorizontalSprintVelocity = 13.0f;

    private float currentMaxHorizontalVelocity = 0.0f;

    [SerializeField]
    private float acceleration = 40;
    

    [SerializeField]
    private float decceleration = 200;


    private bool grounded = false;


    //
    [SerializeField]
    private Transform[] GroundSensors = null;

    [SerializeField]
    private float groundDetectionLength = 1;


    [SerializeField]
    private TitanCameraControls TCC = null;

    [SerializeField]
    private Rigidbody rb;


    // boos
    [SerializeField]
    public int m_maxNumberOfBoosts = 2;

    public int m_currentNumberOfBoosts = 0;

    [SerializeField]
    private float m_maxTimeOfBoostRecharge = 3.0f;

    private float m_timeOfBoostRecharge = 0.0f;

    [SerializeField]
    private float m_titanBoostVelocity = 30.0f;

    [SerializeField]
    private float m_maxTimeUntilBoostDecrease = 3.0f;

    private float m_timeUntilBoostDecrease = 0.0f;

    [SerializeField]
    private float m_breakSpeed = 0.5f;

    // Use this for initialization
    void Start()
    {       
        if(TCC == null)
        {
            TCC = GetComponent<TitanCameraControls>();
        }

        currentMaxHorizontalVelocity = maxHorizontalStandardVelocity;

        m_currentNumberOfBoosts = m_maxNumberOfBoosts;
        m_timeOfBoostRecharge = m_maxTimeOfBoostRecharge;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        TCC.CameraRotationY();
        TCC.CameraStandardRotationX();
        CheckForGround();

       

        // CCTF.CameraStandardRotationX();
        if (grounded)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {

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

                /*
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    //forward
                    if (Input.GetKey(KeyCode.W))
                    {
                        //move forward
                        if (transform.InverseTransformDirection(rb.velocity).z < -0.5f)
                        {
                            HaltOneSided(false);
                        }

                        rb.AddForce(transform.forward * acceleration, ForceMode.Force);
                    }
                    // back
                    if (Input.GetKey(KeyCode.S))
                    {
                        //move backward
                        if (transform.InverseTransformDirection(rb.velocity).z > 0.5f)
                        {
                            HaltOneSided(false);
                        }

                        rb.AddForce(-transform.forward * acceleration, ForceMode.Force);
                    }
                }
                

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    // left
                    if (Input.GetKey(KeyCode.A))
                    {
                        //move left
                        if (transform.InverseTransformDirection(rb.velocity).x > 0.5f)
                        {
                            HaltOneSided(true);
                        }

                        rb.AddForce(-transform.right * acceleration, ForceMode.Force);
                    }
                    // right
                    if (Input.GetKey(KeyCode.D))
                    {
                        //move right
                        if (transform.InverseTransformDirection(rb.velocity).x < -0.5f)
                        {
                            HaltOneSided(true);
                        }

                        rb.AddForce(transform.right * acceleration, ForceMode.Force);
                    }
                }
                */
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if(currentMaxHorizontalVelocity != maxHorizontalSprintVelocity)
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
                if(m_timeUntilBoostDecrease <= 0)
                    Halt();
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(rb.velocity.x != 0 && rb.velocity.z != 0)
            {
                if(m_currentNumberOfBoosts > 0)
                {
                    Vector3 tempNormal = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z).normalized;
                    rb.velocity = new Vector3(tempNormal.x * m_titanBoostVelocity, rb.velocity.y, tempNormal.z * m_titanBoostVelocity);

                    m_timeUntilBoostDecrease = m_maxTimeUntilBoostDecrease;
                    m_currentNumberOfBoosts--;
                }
            }
        }

        if (m_timeUntilBoostDecrease <= 0)
        {
            if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) > currentMaxHorizontalVelocity)
            {
                Vector3 hold = rb.velocity.normalized * currentMaxHorizontalVelocity;
                rb.velocity = new Vector3(hold.x, rb.velocity.y, hold.z);
            }
        }
        else
        {
            m_timeUntilBoostDecrease -= Time.deltaTime;
        }


        if(m_currentNumberOfBoosts < m_maxNumberOfBoosts)
        {
            m_timeOfBoostRecharge -= Time.deltaTime;
            if(m_timeOfBoostRecharge <= 0)
            {
                m_currentNumberOfBoosts++;
                m_timeOfBoostRecharge = m_maxTimeOfBoostRecharge;
            }
        }
        //Debug.Log("number of boost: "+m_currentNumberOfBoosts);
    }

    private void Halt()
    {

        if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) > 1.2f)
        {
            rb.velocity = new Vector3(rb.velocity.x / decceleration, rb.velocity.y, rb.velocity.z / decceleration);
        }
    }

    private void HaltOneSided(bool isXSided)
    {
        if (isXSided)
        {
            Vector3 temp = transform.InverseTransformDirection(rb.velocity);
            temp.x = temp.x / decceleration;
            rb.velocity = transform.TransformDirection(temp);

            //rb.velocity = transform.right / decceleration;
        }
        else
        {
            Vector3 temp = transform.InverseTransformDirection(rb.velocity);
            temp.z = temp.z / decceleration;
            rb.velocity = transform.TransformDirection(temp);
            //rb.velocity = transform.forward / decceleration;
        }
    }





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
           
        }
        else
        {
            if (grounded)
            {
                grounded = false;
            }
        }

    }




   
   
}
