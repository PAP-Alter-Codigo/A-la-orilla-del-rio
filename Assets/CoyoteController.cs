using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using Fungus;

public class CoyoteController : MonoBehaviour
{
	[SerializeField]
	private float speed;
	[SerializeField]
	private float speed2;

	[SerializeField]
	private float range;

	[SerializeField]
	private float maxDistance;

	private Vector2 wayPoint;

	private Vector2 wayPoint2;

	[SerializeField]
	private Collider2D bounds;

	[SerializeField]
	private Transform ZorroTransform;

	public bool method;

	private int directionRD = 1;
	private int directionUD = 1;

	public static bool gameComplete = false;

	public Flowchart DialogFlowchart;

	void Start()
	{
		if(method)
			SetNewRandomDestination();
	}

	void Update()
	{
		if(gameComplete)
		{
			if(Vector2.Distance(transform.position, ZorroTransform.position) < .5f)
			{
				DialogFlowchart.ExecuteBlock("FinalDialogs");
				this.enabled = false;	
			}
			transform.position = Vector2.MoveTowards(transform.position, ZorroTransform.position, speed * Time.deltaTime);
		} else if(method)
		{
			RandomMovement();
		} else
		{
			UpAndDownMovement();
		}
		
	}

	void UpAndDownMovement()
	{
		wayPoint = Vector2.MoveTowards(wayPoint, new Vector2( directionRD * 10f, directionUD * 10f), speed2 * Time.deltaTime);
		transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);

		//prob de cambiar de direccion 
		if(Random.Range(0f,1f) > .9f){
			directionRD *= -1;
		}
		//-3 a 8 es el limite de izquierda a derecha
		if(transform.position.x < -3f || transform.position.x > 8f)
		{
			directionRD *= -1;
		}
		//+-7 es el limite de arriba y abajo
		if(transform.position.y < -7f || transform.position.y > 7f)
		{
			directionUD *= -1;
		}
	}

	void RandomMovement()
	{
		wayPoint2 = Vector2.MoveTowards(wayPoint2, wayPoint, speed2 * Time.deltaTime);

		transform.position = Vector2.MoveTowards(transform.position, wayPoint2, speed * Time.deltaTime);
		if(Vector2.Distance(wayPoint2, wayPoint) < range)
		{
			SetNewRandomDestination();
		}
	}

	void SetNewRandomDestination()
	{
		do{
			wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
		}while(!bounds.OverlapPoint(wayPoint));
	}
}
