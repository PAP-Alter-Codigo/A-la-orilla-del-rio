using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FishMarker : MonoBehaviour{
    [SerializeField] Flowchart flowchart;
    bool inBar, inBarO;

    // Update is called once per frame
    void FixedUpdate(){
        if(inBarO != inBar){
            flowchart.GetVariable<BooleanVariable>("fishInBar").Value = inBar;
        }
        inBarO = inBar;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name.Equals("Bar")) {
            inBar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.name.Equals("Bar")) {
            inBar = false;
        }
    }
}
