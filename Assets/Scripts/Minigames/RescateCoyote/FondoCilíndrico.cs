using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoCilíndrico : MonoBehaviour{
    [SerializeField, Range(0.0f, 10.0f)]
    private float speed = 1.0f;
    private bool ranRoutine;

    void Update() {
        if(Coyote.gameOver && !ranRoutine) {
            ranRoutine = true;
            StartCoroutine(DecreaseSpeed());
        }
        transform.Rotate(Vector3.right, 10.0f * speed * Time.deltaTime, Space.World);
    }

    private IEnumerator DecreaseSpeed() {
        speed -= 10.0f * speed * Time.deltaTime / 0.3f;
        yield return new WaitForSeconds(0.3f);
        if(speed < 0.0f) {
            speed = 0.0f;
        } else {
            StartCoroutine(DecreaseSpeed());
        }
    }
}
