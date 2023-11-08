using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowFps : MonoBehaviour
{
	float deltaTime = 0.0f;
	public int limitFps;
	public int showFps;
	public int fps = 60;

	public TMP_Dropdown fpsDropdow;
	public Toggle showFpsToggle, limitFpsToggle;

	void Start()
	{
		limitFps = PlayerPrefs.GetInt("limitFps", 0);
		showFps = PlayerPrefs.GetInt("showFps", 0);

		fpsDropdow.value = PlayerPrefs.GetInt("Fps", 1);
		if(fpsDropdow.value <= 0)
        {
			fps = 30;
        }

		if (limitFps >=1)
        {
			Application.targetFrameRate = fps;
			limitFpsToggle.isOn = true;
		}

        if (showFps>=1)
        {
			showFpsToggle.isOn = true;
        }
	}
	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}
	void OnGUI()
	{
        if (showFps >= 1)
        {
			int w = Screen.width, h = Screen.height;

			GUIStyle style = new GUIStyle();

			Rect rect = new Rect(0, 0, w, h * 3 / 100);
			style.alignment = TextAnchor.UpperCenter;
			style.fontSize = h * 3 / 100;
			style.normal.textColor = Color.green;
			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;
			string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
			GUI.Label(rect, text, style);
		}		
	}
	public void ChangeLimitFPS(int newfps)
    {		
		if (newfps == 0)
        {
			fps = 30;
        }
		if (newfps == 1)
		{
			fps = 60;
		}
		PlayerPrefs.SetInt("Fps", newfps);
		Debug.Log("FPS" + fps);
		Application.targetFrameRate = fps;
	}
	public void SetLimitFPS(bool limit)
    {
        if (limit)
        {
			limitFps = 1;
			Application.targetFrameRate = fps;
		}
        else
        {
			limitFps = 0;
			Application.targetFrameRate = -1;
		}
		PlayerPrefs.SetInt("limitFps", limitFps);
	}
	public void SetShowFPS(bool show)
	{
        if (show)
        {			
			showFps = 1;
        }
        else
        {
			showFps = 0;
		}
		PlayerPrefs.SetInt("showFps", showFps);
	}
}
