using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    Este script se encarga de cargar la siguiente escena
    Aseguranse de que yo esté en el trigger zone que cambie el mapa

*/
public class Map_Update : MonoBehaviour
{
    [SerializeField]
    public Scene_Changer sceneChanger;

    [Header("Set coordinates for next map")]
    [SerializeField]
    int index;
    [SerializeField]
    float checkpointX;
    [SerializeField]
    float checkpointY;

    private GameObject playerObject;
    private Player_Properties playerProperties;

    private void Start() {

    }

    private void SetPlayerCheckpoint(){
        playerProperties = Player_Properties.Instance;
        playerProperties.checkpointPosX = checkpointX;
        playerProperties.checkpointPosY = checkpointY; 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            print("Detected Player Collission");
            SetPlayerCheckpoint();
            sceneChanger.LoadNextLevel(index);
        }
    }

}
