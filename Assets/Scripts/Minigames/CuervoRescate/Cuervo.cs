using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuervo : MonoBehaviour {
    [SerializeField]
    private CuervoMinigame cuervoMinigame;

    private bool ded = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(ded) return;
        cuervoMinigame.UnAlive();
        ded = true;
    }
}
