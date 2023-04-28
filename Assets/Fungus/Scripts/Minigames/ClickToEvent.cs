using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.Events;

public class ClickToEvent : MonoBehaviour{
    [SerializeField] UnityEvent onClick;
    Collider2D col;

    private void Start() {
        col = GetComponent<Collider2D>();
    }

    private void Update() {
        if(!IsTouchDown()) return;
        Vector2 wp = Camera.main.ScreenToWorldPoint(Input.GetMouseButtonDown(0)? Input.mousePosition: Input.GetTouch(0).position);
        if(col == Physics2D.OverlapPoint(wp))
            onClick.Invoke();
    }
    bool IsTouchDown()=>Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0);
}
