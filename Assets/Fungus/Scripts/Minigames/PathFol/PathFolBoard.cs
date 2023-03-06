using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Fungus;

public class PathFolBoard : MonoBehaviour{
    [SerializeField] Tilemap[] boards;
    [SerializeField] Flowchart flowchart;
    
    public void GameSetup(){
        foreach(Tilemap b in boards)
            b.gameObject.SetActive(false);
        Tilemap board = boards[Random.Range(0, boards.Length)];
        board.gameObject.SetActive(true);
        flowchart.GetVariable<Vector3Variable>("startPos").Value = board.transform.GetChild(0).position;
        flowchart.GetVariable<Vector3Variable>("endPos").Value = board.transform.GetChild(1).position;
    }
}
