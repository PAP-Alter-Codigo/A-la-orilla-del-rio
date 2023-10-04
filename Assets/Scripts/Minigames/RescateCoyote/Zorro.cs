using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Zorro : MonoBehaviour{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float minWait, maxWait;

	private Animator animator;
    public bool canMove = true;
    private float pOld, pNew;

    private void Start() {
		animator = GetComponent<Animator>();
        if(maxWait < 0) {
            minWait = maxWait = 0;
        }
        if(minWait >= maxWait) {
            maxWait += 1.0f;
        }
        StartCoroutine(RandomStops());
    }

    void Update() {
        if(Coyote.gameOver && canMove) {
            canMove = false;
			animator.SetBool("GameOver", true);
            for(int i=0; i<transform.childCount; i++) {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        if(!canMove) return;
        pNew += Time.deltaTime * Mathf.PI;
        float p = Mathf.Sin(Mathf.Lerp(pOld, pNew, Time.deltaTime));
        Vector2 v = p * Vector2.down;
        transform.Translate(speed * Time.deltaTime * v, Space.World);
        pOld = pNew;
    }

    private IEnumerator RandomStops() {
        yield return new WaitForSecondsRealtime(UnityEngine.Random.value * (maxWait - minWait) + minWait);
        canMove = !canMove;
        StartCoroutine(RandomStops());
    }
}
