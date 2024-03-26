using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate() {

        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

        if(Input.GetKey("a")){

            animator.SetBool("Left", true);
            animator.SetBool("Right", false);

        }
        
        if(Input.GetKey("d")){

            animator.SetBool("Left", false);
            animator.SetBool("Right", true);

        }
        
    }
}
