using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Titan_HUD : MonoBehaviour
{
    [SerializeField]
    private Text textField;

    [SerializeField]
    private TitanControls tControls;

    // Update is called once per frame
    void Update()
    {
        if (textField != null && tControls != null)
            textField.text = "boost: " + tControls.m_currentNumberOfBoosts + " / " + tControls.m_maxNumberOfBoosts;
    }
}
