using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationIndicatorController : MonoBehaviour
{

	[SerializeField]
	private Transform foxTransform;

	[SerializeField]
	private Animator animationController;

	[SerializeField]
	private float range;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float distanceToFox = Vector2.Distance(foxTransform.position, transform.position);
		//CircleBreathing
		bool onCircleBreathing = animationController.GetCurrentAnimatorStateInfo(0).IsName("CircleBreathing");
        if(distanceToFox < range && onCircleBreathing){
			animationController.SetTrigger("disapearWaypoint");
		}
    }
}
