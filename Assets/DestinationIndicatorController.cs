using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestinationIndicatorController : MonoBehaviour
{

	[SerializeField]
	private Transform foxTransform;
	[SerializeField]
	private Animator animationController;
	[SerializeField]
	private float range;

	private SpriteRenderer sp;

	[SerializeField]
    private NavMeshAgent agent;

	[SerializeField]
	private LineRenderer Path;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		if(agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete)
		{
			transform.position = agent.destination;
			Path.positionCount = agent.path.corners.Length;
			for (int i = 0; i < agent.path.corners.Length; i++)
			{
				Path.SetPosition(i, agent.path.corners[agent.path.corners.Length - 1 - i]);
			}
		}

		float distanceToFox = Vector2.Distance(foxTransform.position, transform.position);
		//CircleBreathing
		bool onCircleBreathing = animationController.GetCurrentAnimatorStateInfo(0).IsName("CircleBreathing");
        if(distanceToFox < range && onCircleBreathing){
			animationController.SetTrigger("disapearWaypoint");
		}else{
			animationController.ResetTrigger("disapearWaypoint");
		}

		/// Aqui mostramos el objetivo, maybe ver como hacer que desaparezca cuando nos acercamos
		/// Aqui hacemos el path
    }
}
