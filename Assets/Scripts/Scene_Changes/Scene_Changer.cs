using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*

    Este script se encarga de disparar el evento para cambiar la escena

    Debo estar en un objeto aparte que me contenga "Scene Changer"
    Dependo de que el trigger zone me mande mensaje para correr mi codigo


    El codigo comentado es para iniciar animacion de fade in, lo desactive porque no tenemos una por ahora

*/

public class Scene_Changer : MonoBehaviour
{
    //public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel(int index){
        StartCoroutine(LoadLevel(index));
    }

    IEnumerator LoadLevel(int levelIndex){

        //Estas weas son para animacion de fade in and out
        //transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

    }
}
