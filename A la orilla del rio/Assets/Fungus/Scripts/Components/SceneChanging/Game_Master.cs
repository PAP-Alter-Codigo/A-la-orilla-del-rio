using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Master : MonoBehaviour
{
    public static Game_Master instance;

    public string lastSpawnpointName;
    public Vector2 lastSpawnpointPos;

    private void Awake() {
        lastSpawnpointName = "Default_Point";
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }else{
            Destroy(gameObject);
        }
    }

}
