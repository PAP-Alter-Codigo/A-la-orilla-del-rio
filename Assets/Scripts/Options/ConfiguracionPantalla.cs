using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfiguracionPantalla : MonoBehaviour
{
    public Toggle toggle;
    bool control;

    public TMP_Dropdown resolutionDropdow;
    Resolution[] resolutions;

    public GameObject canvasManager;

    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        RevisarResolucion();
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
    public void ActivarControl(bool usarControl)
    {
        control = usarControl;
        canvasManager.SendMessage("ActivarControlManager", usarControl);
    }

    public void RevisarResolucion()
    {
        resolutions = Screen.resolutions;
        resolutionDropdow.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdow.AddOptions(options);
        resolutionDropdow.value = currentResolutionIndex;
        resolutionDropdow.RefreshShownValue();

        resolutionDropdow.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }

    public void CambiarResolucion(int resolutionIndex)
    {
        PlayerPrefs.SetInt("numeroResolucion", resolutionDropdow.value);

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
