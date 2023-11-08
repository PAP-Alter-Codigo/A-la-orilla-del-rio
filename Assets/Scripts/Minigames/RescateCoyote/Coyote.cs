using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coyote: MonoBehaviour{
    public static bool gameOver = false;

    [SerializeField]
    private float speed;
    [SerializeField]
    private ParticleSystem sploosh;
    [SerializeField]
    public Transform foxTransform;
    [SerializeField]
    Flowchart flowchart;

	private Animator animator;
    private Vector2 minPos = new(-2.5f, -4.5f);
    private Vector2 targetPos = new(-6, 0);
    private Vector2 maxPos = new(5.5f, 4.0f);
    private float actualSpeed;

    private void Start() {
        gameOver = false;
		animator = GetComponent<Animator>();
    }

    private void Update() {
        if(gameOver && Vector2.Distance(transform.position, foxTransform.position) < 0.25f) {
			flowchart.ExecuteBlock("FinalDialogs");
			animator.SetBool("GameOver", true);
			enabled = false;
            return;
        }
        // Moverse
        GetTargetPos();
        transform.position = Vector2.MoveTowards(transform.position, targetPos, actualSpeed * Time.deltaTime);
    }

    private void GetTargetPos() {
        if(gameOver) {
            targetPos = foxTransform.position;
            actualSpeed = 2.0f * speed;
            return;
        }
        if(targetPos.x != -6 && Vector3.Distance(targetPos, transform.position) > 0.75f) {
            return;
        }
        float xPos = (maxPos.x - minPos.x) * UnityEngine.Random.value + minPos.x;
        float yPos = (maxPos.y - minPos.y) * UnityEngine.Random.value + minPos.y;
        targetPos = new(xPos, yPos);
        actualSpeed = speed + UnityEngine.Random.value * speed / 10.0f;
        sploosh.Play();
    }
}
