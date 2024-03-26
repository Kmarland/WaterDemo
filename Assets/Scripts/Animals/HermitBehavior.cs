using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitBehavior : MonoBehaviour
{

    #region Variables
    public float timer;
    private float intTimer;
    private float value;
    public Rigidbody2D rb;
    private float pos;
    private Vector2 start;
    private bool madePos = false;
    public float speed;
    public Animator animator;
    private bool startTime = false;
    private float distance;
    private float one;

    private bool canMove = false;
    public float timer2;
    private float intTimer2;

    public float timer3;
    private float intTimer3;

    private float startX;

    #endregion
    void Start() {
        intTimer = timer;
        intTimer2 = timer2;
        intTimer3 = timer3;
        value = Random.value * intTimer;
        startX = transform.position.x;
    }

    void Update() {
        if (!madePos && canMove) {
            makePos();
        }
    }


    void makePos() {
        float x = Random.value * 2f;
        float h = Random.value;
        if (h > 0.5f) {
            x = -x;
        }
        x += startX;
        pos = x;
        start = rb.position;
        if (pos - start.x < 0) {
            one = -1;
            transform.localScale = new Vector2(1,1);
        } else {
            one = 1;
            transform.localScale = new Vector2(-1,1);
        }
        madePos = true;
        animator.SetBool("Walk", true);
        value = Random.value * intTimer;
    }

    void FixedUpdate() {
        if (madePos && canMove) {
            distance = Mathf.Abs(pos - rb.position.x);
            if(distance > 0.01f){
                rb.MovePosition(rb.position + new Vector2(one, 0f) * Time.deltaTime);
            }else{
                startTime = true;
                animator.SetBool("Emerge", false);
            }   
        }

        //Timer controls how long crab waits before moving again during move state
        if (startTime && canMove) {
            animator.SetBool("Stop", true);
            timer -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            if (timer < value) {
                timer = intTimer;
                madePos = false;
                startTime = false;
                animator.SetBool("Stop", false);
            }
        }

        //Timer controls how long crab moves after waking up
        if (canMove) {
            timer2 -= Time.deltaTime;
            if (timer2 < 0f) {
                timer2 = intTimer2;
                canMove = false;
                animator.SetBool("Walk", false);
            }
        }

        //Timer controls eyes of crab during non move state
        if (!canMove) {
            timer3 -= Time.deltaTime;
            if (timer3 < intTimer3 / 2f) {
                animator.SetBool("Eyes", false);
            } else {
                animator.SetBool("Eyes", true);
            }
            if (timer3 < 0f) {
                timer3 = intTimer3;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!canMove && other.gameObject.tag == "Player") {
            animator.SetBool("Emerge", true);
            canMove = true;
            timer2 = intTimer2;
        }
    }
}
