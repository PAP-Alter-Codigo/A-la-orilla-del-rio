using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //Set Player Coordinates
        SetPlayerCoordinates();
        //Set cursor properties
        SetCursorProperties();
    }

    private void SetPlayerCoordinates(){
        GameObject player = Player_Properties.Instance.gameObject;
        player.transform.position = new Vector2(Player_Properties.Instance.checkpointPosX, Player_Properties.Instance.checkpointPosY);
    }

    private void SetCursorProperties(){
        MouseClickPosition cursor = MouseClickPosition.Instance;
        GameObject cursorObj = cursor.gameObject;
        GameObject player = Player_Properties.Instance.gameObject;
        cursorObj.transform.position = player.transform.position;
        cursor.cam =  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

}
