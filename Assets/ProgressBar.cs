using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
	[SerializeField]
	private Image ProgressImage;

	[SerializeField]
	[Range(0.01f, 1.0f)]
	private float ProgressSpeed = 1f;

	[SerializeField]
	[Range(0.0f, 1.0f)]
	private float Progress = 0.0f;

	[SerializeField]
	public static bool MakingProgress = false;

	



	public void Update()
	{
		if(Progress >= 1.0f){
			//Aqui hacer la rutina de juego ganado.
			this.enabled = false;
		}
		if(MakingProgress && Progress < 1.0f)
		{
			Progress += Time.deltaTime * ProgressSpeed;
		} else if(!MakingProgress && Progress >= 0.0f)
		{
			Progress -= Time.deltaTime * ProgressSpeed;
		}
		ProgressImage.fillAmount = Progress;
	}

}