using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Anim_Properties : MonoBehaviour
{

    public bool facingLeft;
    public bool facingFront;
    public bool isWalking;

    public Animator animator;

	
    public AudioSource audiosource; 
    public AudioClip[] dirtSteps; 
    public AudioClip[] waterSteps;
    public bool isWalkingOnWater;

    int randomSound;

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

    // Update is called once per frame
    void Update()
    {
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
	
    public void PlayAudio1() 
    {
        if (isWalkingOnWater)
        {
        	randomSound=Random.Range(0,waterSteps.Length);
			audiosource.clip = waterSteps[randomSound];
        }else 
        {            
        	randomSound=Random.Range(0,dirtSteps.Length);
			audiosource.clip = dirtSteps[randomSound];
        }
        audiosource.Play(); 
    }
    
    public void setWalkingOnWater (bool state) 
    {
        isWalkingOnWater=state;
    }
}
