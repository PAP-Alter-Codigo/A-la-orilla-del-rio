using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDePesca : MonoBehaviour
{
	private Collider2D redColider;
	[SerializeField]
	private Collider2D coyoteColider;

	[SerializeField]
	private float recoverySpeed;
	[SerializeField]
	private Transform recoveryPoint;

	private void Start() {
		redColider = GetComponent<Collider2D>();
	}

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(mousePosition.x, mousePosition.y, 0.0f);
        }else 
		{
			transform.position = Vector2.MoveTowards(transform.position, recoveryPoint.position, recoverySpeed * Time.deltaTime);
		}
		ProgressBar.MakingProgress = redColider.IsTouching(coyoteColider);

    }
}