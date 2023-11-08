using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    private Animator m_Animator;
	private bool NewGame = true;


	[SerializeField] private bool ResetFirstTime = false;

    void Awake()
    {
        //Get the Animator attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
		int FirstTime = PlayerPrefs.GetInt("playintro", 0);
		if(FirstTime == 0)
		{
			PlayerPrefs.SetInt("playintro", 1);
			PlayerPrefs.Save();
			m_Animator.SetTrigger("FirstTime");
		} else {
		}
	}
    // Update is called once per frame
    void Update()
    {
        if(ResetFirstTime)
		{
			PlayerPrefs.SetInt("playintro", 0);
			PlayerPrefs.Save();
			ResetFirstTime = false;
		}
    }

	public void StartGame() 
	{
		m_Animator.SetTrigger("StartGame");

	}
	public void ShowMenu() 
	{
		m_Animator.SetTrigger("ShowMenu");

	}
	public void LoadGame()
	{
		m_Animator.SetTrigger("StartGame");
		NewGame = false;
	}
	public void QuitGame() {
		Application.Quit();
	}

	public void ChangeScene(){
		if(NewGame)
		{
			//Iniciar en la scena del tutorial
            SceneManager.LoadScene("L1 Scene01");
		}else
		{
			//Cargar Partida
			DataManager.CargarDatos();
		}
	}
}
