using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	void Start()
	{
		SetNewDestination();
	}

	void Update()
	{
		wayPoint2 = Vector2.MoveTowards(wayPoint2, wayPoint, speed2 * Time.deltaTime);

		transform.position = Vector2.MoveTowards(transform.position, wayPoint2, speed * Time.deltaTime);
		if(Vector2.Distance(wayPoint2, wayPoint) < range)
		{
			SetNewDestination();
		}
	}

	void SetNewDestination()
	{
		do{
			wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
		}while(!bounds.OverlapPoint(wayPoint));
	}
	
}
