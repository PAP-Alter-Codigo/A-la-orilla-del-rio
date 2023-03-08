using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class BarColoreador : MonoBehaviour{
    [SerializeField] Flowchart flowchart;
    SpriteRenderer sprite;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        float hue = flowchart.GetVariable<FloatVariable>("fishingTiem").Value;
        sprite.color = Color.HSVToRGB(hue/360.0f,0.73f,0.97f);
    }
}
