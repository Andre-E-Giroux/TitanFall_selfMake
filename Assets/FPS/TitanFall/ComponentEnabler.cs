using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentEnabler : MonoBehaviour
{
    public Behaviour[] m_behavioursOnActivate_Enable = null;
    public Rigidbody[] m_RigidBody_Enable = null;
    public GameObject[] m_gameObjectsOnActivate_Enable = null;
    public Collider[] m_collidersOnActivate_Enable = null;

    public Behaviour[] m_behavioursOnActivate_Disable = null;
    public Rigidbody[] m_RigidBody_Disable = null;
    public GameObject[] m_gameObjectsOnActivate_Disable = null;
    public Collider[] m_collidersOnActivate_Disable = null;

    public void Activate(bool yes)
    {
        if(yes)
        {
            //Debug.Log("yes");
            foreach (Behaviour behaviour in m_behavioursOnActivate_Enable)
            {
                behaviour.enabled = true;
            }
            foreach (GameObject go in m_gameObjectsOnActivate_Enable)
            {
                go.SetActive(true);
            }
            foreach (Collider coll in m_collidersOnActivate_Enable)
            {
                coll.enabled = true;
            }
            foreach (Rigidbody rb in m_RigidBody_Enable)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }

            foreach (Behaviour behaviour in m_behavioursOnActivate_Disable)
            {
                behaviour.enabled = false;
            }
            foreach (GameObject go in m_gameObjectsOnActivate_Disable)
            {
                go.SetActive(false);
            }
            foreach (Collider coll in m_collidersOnActivate_Disable)
            {
                coll.enabled = false;
            }
            foreach (Rigidbody rb in m_RigidBody_Disable)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
        }
        else
        {
            foreach (Behaviour behaviour in m_behavioursOnActivate_Enable)
            {
                behaviour.enabled = false;
            }
            foreach (GameObject go in m_gameObjectsOnActivate_Enable)
            {
                go.SetActive(false);
            }
            foreach (Rigidbody rb in m_RigidBody_Enable)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
            foreach (Collider coll in m_collidersOnActivate_Enable)
            {
                coll.enabled = false;
            }

            foreach (Behaviour behaviour in m_behavioursOnActivate_Disable)
            {
                behaviour.enabled = true;
            }
            foreach (GameObject go in m_gameObjectsOnActivate_Disable)
            {
                go.SetActive(true);
            }
            foreach (Rigidbody rb in m_RigidBody_Disable)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            foreach (Collider coll in m_collidersOnActivate_Disable)
            {
                coll.enabled = true;
            }
        }
    }
}
