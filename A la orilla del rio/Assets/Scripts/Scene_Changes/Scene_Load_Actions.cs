using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

/*
    Este codigo es para hacer setup de propiedades cada vez que la escena carga

    Yo debo estar en un objeto llamado Level Loader, corro automaticamente cuando carga la escena
*/

public class Scene_Load_Actions : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded; //You add your method to the delegate
    }
     
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }
    
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Set cursor properties
        SetCursorProperties();
        //Set Player Coordinates
        SetPlayerCoordinates();

        SetCameraFollow();
    }

    private void SetCameraFollow(){
        GameObject player = Player_Properties.Instance.gameObject;
        CinemachineVirtualCamera vcam = GameObject.FindGameObjectWithTag("CinemachineCam").GetComponent<CinemachineVirtualCamera>();
        vcam.LookAt = Player_Properties.Instance.gameObject.transform;
        vcam.Follow = Player_Properties.Instance.gameObject.transform;        
    }

    private void SetPlayerCoordinates(){
        Player_Properties player = Player_Properties.Instance;
        
        player.gameObject.transform.position = GameObject.Find("Map/SpawnPoints/" +player.lastCheckPoint).transform.position;
    }

    private void SetCursorProperties(){
        MouseClickPosition cursor = MouseClickPosition.Instance;
        GameObject cursorObj = cursor.gameObject;
        Player_Properties player = Player_Properties.Instance;
        cursor.cam =  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cursorObj.transform.position = GameObject.Find("Map/SpawnPoints/" +player.lastCheckPoint).transform.position;
        player.currentState = Player_Properties.PlayerStates.AVAILABLE;
    }

}
