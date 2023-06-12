/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    
    public float moveSpeed = 2.5f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    private Vector2 moveInput;

    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private Rigidbody2D rb;

    private Player_Properties playerProperties;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerProperties = Player_Properties.Instance;
    }

    void FixedUpdate() {
        bool success = MovePlayer(moveInput);


        if(!success){
            success = MovePlayer(new Vector2(moveInput.x, 0));

            if(!success){
                success = MovePlayer(new Vector2(0, moveInput.y));
                }
            }


            if(moveInput.x != 0){
                if(moveInput.x > 0){
                    //print("Show Right");
                }else{
                    //print("Show Left");
                }
            }


        }

    public bool MovePlayer(Vector2 direction){
        if(playerProperties.currentState == Player_Properties.PlayerStates.LOCKED) return false;
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );
        if(count == 0){
            Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;


            // No collisions
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        else{
            // Print Collissions
            foreach (RaycastHit2D hit in castCollisions){
                print(hit.ToString());
            }

            return false;
        }
    }


    void PhysicsUpdate(){
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }
}
*/