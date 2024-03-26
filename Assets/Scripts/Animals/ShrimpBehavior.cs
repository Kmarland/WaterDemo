using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpBehavior : MonoBehaviour
{

//Variables to determine which grass to go to
    #region Variables
    private GameObject grass;
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

    private bool moveToGrass = true;

    public float timer;
    private float intTimer;
    private bool startTime = false;

    private bool moving = true;
    #endregion
    // Start is called before the first frame update
    void Start() {
        //Determines which seagrass is closer and stores it in grass gameobject
        findClosestGrass();
        startX = transform.position.x;
        startY = transform.position.y;
        intTimer = timer;
        Physics2D.IgnoreLayerCollision(6, 6, true);
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(6, 8, true);
    }

    // Update is called once per frame
    void Update() {
        if(startMoving && moveToGrass){
            findDir(grass.transform.position);
            moving = true;
        }
        if(startMoving && !moveToGrass){
            findDir(pos);
            moving = true;
        }
    }

    void FixedUpdate() {
        if(moving){
            if(moveToGrass){
                moveShrimp(grass.transform.position);
            }else{
                moveShrimp(pos);
            }
        }

        if (startTime) {
            timer -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            if(timer < 0f){
                if(moveToGrass){
                    findClosestGrass();
                }else{
                    findNewPos();
                }
                startTime = false;
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
        y += 2;
        pos = new Vector2(x, y);
        startMoving = true;
    }

    void findClosestGrass() {
        GameObject[] sG = GameObject.FindGameObjectsWithTag("Seagrass");
        dist = 1000f;
        foreach (GameObject seaGrass in sG) {
            if (Mathf.Abs(transform.position.x - seaGrass.transform.position.x) < dist) {
                dist = Mathf.Abs(transform.position.x - seaGrass.transform.position.x);
                grass = seaGrass;
            }
        }
        startX = transform.position.x;
        startY = transform.position.y;
        startMoving = true;
    }

    void findDir (Vector2 standIn) {
        if(standIn.x - startX < 0){
            dirX = -1;
            transform.localScale = new Vector2(-1,1);
        }else{
            dirX = 1;
            transform.localScale = new Vector2(1,1);
        }
        if(standIn.y - startY < 0){
            dirY = -1;
        }else{
            dirY = 1;
        }
    }

    void moveShrimp (Vector2 standIn) {
        xD = Mathf.Abs(transform.position.x - standIn.x); 
        yD = Mathf.Abs(transform.position.y - standIn.y);
        distance = Mathf.Sqrt(xD * xD + yD * yD);
        if(distance > 0.3f){
            rb.MovePosition(rb.position + new Vector2(dirX, dirY * (yD / xD)).normalized * speed * Time.deltaTime);
        }else{
            moveToGrass = !moveToGrass;
            startMoving = false;
            startTime = true;
            moving = false;
        }
    }
}
