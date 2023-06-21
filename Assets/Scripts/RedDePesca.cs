using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDePesca : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(mousePosition.x, mousePosition.y, 0.0f);
			
        }
    }


	void OnCollisionEnter2D(Collision2D col)
	{
		ProgressBar.MakingProgress = true;
	}
	void OnCollisionExit2D(Collision2D col)
	{
		ProgressBar.MakingProgress = false;	
	}
}
