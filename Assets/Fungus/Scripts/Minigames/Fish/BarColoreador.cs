using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class BarColoreador : MonoBehaviour{
    [SerializeField] Flowchart flowchart;
    SpriteRenderer sprite;
    Collider2D col;
    FloatVariable hueVar;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        hueVar = flowchart.GetVariable<FloatVariable>("fishingTiem");
    }

    // Change the bar color based on the remaining time to catch the fish
    void FixedUpdate(){
        sprite.color = Color.HSVToRGB(hueVar.Value/360.0f,0.73f,0.97f);
    }

    // Scene compatibility assertion
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision) {
        if(!collision.gameObject.CompareTag("Laser"))
            Physics2D.IgnoreCollision(collision.collider, col);
    }
}
