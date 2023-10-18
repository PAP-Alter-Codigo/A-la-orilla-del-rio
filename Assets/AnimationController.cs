using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    private Animator m_Animator;
	private bool NewGame = true;

    void Start()
    {
        //Get the Animator attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
		int FirstTime = PlayerPrefs.GetInt("FistTime", 0); 
		if(FirstTime == 0)
		{
			m_Animator.SetTrigger("FirstTime");
			PlayerPrefs.SetInt("FirstTime", 1);
			PlayerPrefs.Save();
		}
	}
    // Update is called once per frame
    void Update()
    {
        
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
