using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    [SerializeField]
    public float m_fireRate = 1.0f;
    [SerializeField]
    public float m_realoadSpeed = 1.5f;
    [SerializeField]
    public int m_maxAmmoCount = 100;
    [SerializeField]
    public int m_currentBackupAmmo = 100;
    [SerializeField]
    public int m_maxClipSize = 10;
    [SerializeField]
    public int m_currentClipAmmoCount = 0;
    [SerializeField]
    public float m_damagePerProjectile = 50.0f;
    [SerializeField]
    public int m_numberOfRoundPerShot = 1;
    [SerializeField]
    public int m_numberOfPelletsPerRound = 1;
    [SerializeField]
    public float m_accuracy = 100.0f;
    [SerializeField]
    public float m_shotForce = 50.0f;
    [SerializeField]
    public Transform m_pointOfShot = null;
    [SerializeField]
    public GameObject m_gunShotParticle = null;
    public LayerMask m_layerMask;

    public float m_projSpeed = 1.0f;

}
