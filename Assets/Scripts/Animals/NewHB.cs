using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHB : MonoBehaviour
{
    #region Variables
    public float timer;
    private float intTimer;
    private float value;
    public Rigidbody2D rb;
    private Vector2 pos;
    private Vector2 start;
    private bool madePos = false;
    public float speed;
    public Animator animator;
    private bool startTime = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        intTimer = timer;
        value = Random.value * intTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(madePos){

            transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);

        }

        if(!madePos){

            makePos();

        }

        if(transform.position.x == pos.x){

            startTime = true;

        }

    }

    void makePos(){

        float x = Random.value * 2f;
        float h = Random.value;
        if(h > 0.5f){

            x = -x;

        }
        
        pos = new Vector2(x , transform.position.y);
        madePos = true;
        animator.SetBool("Walk", true);
        value = Random.value * intTimer;
        
 
    }

    void FixedUpdate() {

        if(startTime){

            //Debug.Log(value);
            animator.SetBool("Walk", false);

            timer -= Time.deltaTime;

            if(timer < value){

                timer = intTimer;
                madePos = false;
                startTime = false;

            }

        }

    }
}
