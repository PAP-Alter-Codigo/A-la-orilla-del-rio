using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStuff : MonoBehaviour{
    public float speed = 1f;
    public float returnX = 0.0f, finalX = 0.0f;

    // Update is called once per frame
    void Update(){
        transform.Translate(speed * Time.deltaTime * Vector2.left, Space.World);
        if(transform.position.x < finalX) DestroyOrReturn();
    }

    private void DestroyOrReturn() {
        if(returnX == 0.0f) {
            Destroy(gameObject);
            return;
        }
        transform.position = new(returnX, transform.position.y);
    }
}
