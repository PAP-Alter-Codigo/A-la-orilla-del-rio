using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change_Trigger : MonoBehaviour
{

    private Game_Master gm;
    public int nextSceneId;
    public string nextCheckPointName;
    

    void Start(){
        gm = GameObject.FindGameObjectWithTag("Game_Master").GetComponent<Game_Master>();
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Player")){
            gm.lastSpawnpointName = nextCheckPointName;
            // Aqui podemos poner alguna animacion de transicion
            SceneManager.LoadScene(nextSceneId);
        }

    }

}
