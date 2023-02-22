using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoTopDown : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Vector2 direccion;
    private Rigidbody2D rb;
    private float X;
    private float Y;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        X = Input.GetAxisRaw("Horizontal");
        Y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("X", X);
        animator.SetFloat("Y", Y);
        if (X != 0 || Y != 0) {
            animator.SetFloat("LastX", X);
            animator.SetFloat("LastY", Y);
        }
        direccion = new Vector2(X, Y).normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direccion * velocidadMovimiento * Time.fixedDeltaTime);
    }
}
