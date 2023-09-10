using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMFish: MonoBehaviour{
    [SerializeField]
    private float speed, speedVariation, jumpSpeed;
    [SerializeField]
    private Vector2 swimDir;
    private float pOld, pNew;

    private void Start() {
        speed += speedVariation * (Random.value - 0.5f);
    }

    private void Update() {
        // Fuera de los límites del mapa
        if(transform.position.x < -10) {
            Destroy(gameObject);
            return;
        }

        // Mover al pez
        pNew += Time.deltaTime * Mathf.PI;
        float p = Mathf.Sin(Mathf.Lerp(pOld, pNew, Time.deltaTime));
        Vector2 v = p * new Vector2(-swimDir.y, swimDir.x);
        transform.Translate(jumpSpeed * Time.deltaTime * v, Space.World);
        transform.Translate(speed * Time.deltaTime * swimDir, Space.World);
        pOld = pNew;
    }

    // Hacer la animación de atrapado y destruir el pez
    public void FishCaught() {
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
        animator.SetBool("Caught", true);
        StartCoroutine(Caught());
    }

    IEnumerator Caught() {
        yield return new WaitForSecondsRealtime(1.0f);
        Destroy(gameObject);
    }
}
