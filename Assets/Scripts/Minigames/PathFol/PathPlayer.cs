using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class PathPlayer : MonoBehaviour{
    [SerializeField] Flowchart flowchart;
    [SerializeField] float edgeRad = 0.25f, edgeOff = 0.333f;
    Draggable2D drag;
    Vector3 startingPosition;

    private void Start() {
        drag = gameObject.GetComponent<Draggable2D>();
        startingPosition = transform.position;
        Vector2[]offsets = {new(1,1),new(-1,1),new(-1,-1),new(1,-1)};
        foreach(Vector2 offset in offsets) {
            CircleCollider2D col = gameObject.AddComponent<CircleCollider2D>();
            col.radius = edgeRad;
            col.offset = offset * edgeOff;
        }
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision) {
        if(collision.collider.CompareTag("Finish")) {
            flowchart.ExecuteBlock("End");
            //Debug.Log("End");
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision) {
        if(collision.collider.name.Contains("FloorTilemap")) {
            flowchart.ExecuteBlock("Fail");
            //Debug.Log("Fail");
        }
    }

}
