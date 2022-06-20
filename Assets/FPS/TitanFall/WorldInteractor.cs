using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractor : MonoBehaviour
{
    [SerializeField]
    private Transform m_camera = null;
    [SerializeField]
    private float m_rangeOfRaycast = 5.0f;

    [SerializeField]
    private float m_maxTimeToInteract = 0.5f;
    [SerializeField]
    private float m_currentTimeFromInteract = 0.0f;

    [SerializeField]
    private ComponentEnabler m_playerEnabler = null;

    [SerializeField]
    private Transform m_parentCameraTransform = null;
    [SerializeField]
    private Transform m_cameraTransform = null;

    [SerializeField]
    private GameObject m_weaponGameObject = null;

    [SerializeField]
    private InteractWithTitan m_IWT = null;
    

    private bool m_isInTitan = false;
    private void Start()
    {
        if(m_playerEnabler == null)
        {
            transform.GetComponent<ComponentEnabler>();
        }
        m_currentTimeFromInteract = m_maxTimeToInteract;
        if(m_IWT == null)
        {
            m_IWT = GetComponent<InteractWithTitan>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!m_isInTitan)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            Debug.DrawRay(m_camera.position, m_camera.TransformDirection(Vector3.forward) * m_rangeOfRaycast, Color.red);
            if (Physics.Raycast(m_camera.position, m_camera.TransformDirection(Vector3.forward), out hit, m_rangeOfRaycast))
            {
                //Debug.DrawRay(m_camera.position, m_camera.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
                if (hit.transform.tag == "LocalPlayerTitan")
                {
                    //Debug.Log("isLocalTitan");
                    if (Input.GetKey(KeyCode.E))
                    {
                        m_currentTimeFromInteract -= Time.deltaTime;
                        if (m_currentTimeFromInteract <= 0)
                        {
                            m_IWT.EnterTitan();
                            m_isInTitan = true;
                            m_currentTimeFromInteract = m_maxTimeToInteract;
                        }
                    }
                    else
                    {
                        if (m_currentTimeFromInteract < m_maxTimeToInteract)
                        {
                            m_currentTimeFromInteract = m_maxTimeToInteract;
                        }
                    }
                }
                else
                {
                    if (m_currentTimeFromInteract < m_maxTimeToInteract)
                    {
                        m_currentTimeFromInteract = m_maxTimeToInteract;
                    }
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.E))
            {
                m_currentTimeFromInteract -= Time.deltaTime;
                if (m_currentTimeFromInteract <= 0)
                {
                    m_IWT.LeaveTitan();
                    m_isInTitan = false;
                    m_currentTimeFromInteract = m_maxTimeToInteract;
                }
            }
            else
            {
                if (m_currentTimeFromInteract < m_maxTimeToInteract)
                {
                    m_currentTimeFromInteract = m_maxTimeToInteract;
                }
            }
        }
    }

   
}
