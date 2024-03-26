using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBehavior : MonoBehaviour
{
    //Variables to determine which shrimp to go to
    #region Variables
    private GameObject shrimp;
    private float prevDist;
    private float dist = 1000f;

    private float distance;
    private float xD;
    private float yD;

    public Rigidbody2D rb;
    public float speed;
    private float dirX = 1f;
    private float dirY = 1f;
    private float startX;
    private float startY;
    private bool startMoving = true;

    private Vector2 pos;
    private Vector2 standInForDirCalc;

    private bool moveToShrimp = true;

    public float timer;
    private float intTimer;
    private bool startTime = false;

    private bool moving = true;

    public Animator animator;
    #endregion

    // Start is called before the first frame update
    void Start() {
        //Determines which shrimp is closer and stores it in shrimp gameobject
        findClosestShrimp();
        startX = transform.position.x;
        startY = transform.position.y;
        intTimer = timer;
    }

    // Update is called once per frame
    void Update() {
        if (startMoving && moveToShrimp) {
            findDir(shrimp.transform.position);
            moving = true;
            animator.SetBool("Move", true);
            animator.SetBool("ChaseShrimp", true);
        }

        if (startMoving && !moveToShrimp) {
            findDir(pos);
            moving = true;
            animator.SetBool("Move", true);
        }
        startX = transform.position.x;
        startY = transform.position.y;
    }

    void FixedUpdate() {
        if (moving) {
            if (moveToShrimp) {
                moveShrimp(shrimp.transform.position);
            } else {
                moveShrimp(pos);
            }
        }
        if (startTime) {
            timer -= Time.deltaTime;
            if (timer < 0f) {
                if (moveToShrimp) {
                    findClosestShrimp();
                } else {
                    findNewPos();
                }
                startTime = false;
                animator.SetBool("ChaseShrimp", false);
                timer = intTimer;
            }
        }
    }

    void findNewPos() {
        startX = transform.position.x;
        startY = transform.position.y;
        float x = Random.value * 5f;
        float h = Random.value;
        if (h > 0.5f) {
            x = -x;
        }
        float y = Random.value * 2f;
        float i = Random.value;
        if (i > 0.5f) {
            y = -y;
        }
        float j = Random.value;
        if (j > 0.5f) {
            animator.SetBool("Spin", true);
        } else {
            animator.SetBool("Spin", false);
        }
        y += 2;
        pos = new Vector2(x, y);
        startMoving = true;
    }

    void findClosestShrimp() {
        GameObject[] sG = GameObject.FindGameObjectsWithTag("Shrimp");
        dist = 1000f;
        foreach (GameObject seaShrimp in sG) {
            if (Mathf.Abs(transform.position.x - seaShrimp.transform.position.x) < dist) {
                dist = Mathf.Abs(transform.position.x - seaShrimp.transform.position.x);
                shrimp = seaShrimp;
            }
        }
        startMoving = true;
    }

    void findDir (Vector2 standIn) {
        if (standIn.x - startX < 0) {
            dirX = -1;
            transform.localScale = new Vector2(1, 1);
        } else {
            dirX = 1;
            transform.localScale = new Vector2(-1, 1);
        }
        if (standIn.y - startY < 0) {
            dirY = -1;
        } else {
            dirY = 1;
        }
        //float AngleRad = Mathf.Atan2 (standIn.y - startY, standIn.x - startX);
        //float angle = (180 / Mathf.PI) * AngleRad;
        //rb.rotation = angle;
    }

    void moveShrimp (Vector2 standIn) {
        xD = Mathf.Abs(transform.position.x - standIn.x); 
        yD = Mathf.Abs(transform.position.y - standIn.y);
        distance = Mathf.Sqrt(xD * xD + yD * yD);
        if (distance > 0.3f) {
            rb.MovePosition(rb.position + new Vector2(dirX, dirY * (yD / xD)).normalized * speed * Time.deltaTime);
        } else {
            moveToShrimp = !moveToShrimp;
            startMoving = false;
            startTime = true;
            moving = false;
            animator.SetBool("Move", false);
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Shrimp") {
            animator.SetBool("ShrimpClose", true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Shrimp") {
            animator.SetBool("ShrimpClose", false);
        }
        
    }
}
