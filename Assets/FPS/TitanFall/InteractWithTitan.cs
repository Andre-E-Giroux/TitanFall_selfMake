using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithTitan : MonoBehaviour
{
 
    public ComponentEnabler m_titanComponentEnabler = null;
    public Transform m_titanTransform = null;
    public ComponentEnabler m_pilotComponentEnabler = null;

    [SerializeField]
    private Transform m_pilotNode = null;

    [SerializeField]
    private Transform m_pilotDisembarkNode = null;

    public Transform m_pilotTransform = null;

    

    public Transform m_cameraParentTransform = null;
    public Transform m_cameraTransform = null;
    public GameObject m_weapon = null;

    public Transform m_titanCamerTransform;


    // Start is called before the first frame update
    void Start()
    {
        if(m_titanComponentEnabler == null)
        {
            transform.GetComponent<ComponentEnabler>();
        }
    }




    public void EnterTitan()
    {
        //Debug.Log(m_pilotComponentEnabler);
        m_pilotComponentEnabler.Activate(false);
        m_titanComponentEnabler.Activate(true);
        m_pilotTransform.parent = m_titanTransform;
        m_pilotTransform.position = m_pilotNode.position;
        m_pilotTransform.localEulerAngles = Vector3.zero;
        m_cameraParentTransform.localEulerAngles = Vector3.zero;
        m_cameraTransform.localEulerAngles = Vector3.zero;
        m_weapon.SetActive(false);
    }

    public void LeaveTitan()
    {
        //Debug.Log(m_pilotComponentEnabler);
        m_pilotComponentEnabler.Activate(true);
        m_titanComponentEnabler.Activate(false);
        m_pilotTransform.parent = null;
        m_pilotTransform.position = m_pilotDisembarkNode.position;
        m_pilotTransform.eulerAngles = new Vector3 (0, m_titanCamerTransform.eulerAngles.y, m_titanCamerTransform.eulerAngles.z);
        m_cameraParentTransform.localEulerAngles = Vector3.zero;
        m_cameraTransform.localEulerAngles = Vector3.zero;
        m_weapon.SetActive(true);
    }


}
