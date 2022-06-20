using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentHUD : MonoBehaviour {

    [SerializeField]
    private WeaponClass weapon;

    [SerializeField]
    private Text ammoCountText;

	// Update is called once per frame
	void Update () {
        if(ammoCountText != null && weapon != null)
            ammoCountText.text = "ammo: " + weapon.m_currentClipAmmoCount + " / " + weapon.m_maxClipSize;
	}
}
