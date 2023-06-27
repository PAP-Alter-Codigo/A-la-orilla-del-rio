using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Anim_Properties : MonoBehaviour
{

    public bool facingLeft;
    public bool facingFront;
    public bool isWalking;

    public Animator animator;
    public Animator path_indicator_animator;

    // Referencia al  navmesh
    private UnityEngine.AI.NavMeshAgent nav;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
      isWalking = false;
      facingFront = true;
      facingLeft = true;
      nav = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
      renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }
	void FixedUpdate()
	{
		
	}

    // Update is called once per frame
    void Update()
    {
		
       	if (Input.GetMouseButtonDown(0))
       	{
			path_indicator_animator.SetTrigger("mouseClicked");
		}	
        
		//print(nav.steeringTarget);
        if (nav.velocity != Vector3.zero){
            isWalking = true;
        }else{
            isWalking = false;
        }
        //X
        if(nav.velocity[0] > 0) facingLeft = false;
        else if(nav.velocity[0] < 0) facingLeft = true;
        //Y
        if(nav.velocity[1] > 0) facingFront = false;
        else if(nav.velocity[1] < 0) facingFront = true;

        renderer.flipX = !facingLeft;

        animator.SetBool("facingFront", facingFront);
        animator.SetBool("facingLeft", facingLeft);
        animator.SetBool("isMoving", isWalking);

    }
}
