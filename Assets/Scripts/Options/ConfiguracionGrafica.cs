using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfiguracionGrafica : MonoBehaviour
{
    public TMP_Dropdown graficsDropdow;
    public int quality;

    void Start()
    {
        quality = PlayerPrefs.GetInt("numeroDeCalidad", 3);
        graficsDropdow.value = quality;
        SetQuality();
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(graficsDropdow.value);
        PlayerPrefs.SetInt("numeroDeCalidad", graficsDropdow.value);
        quality = graficsDropdow.value;
    }
}
