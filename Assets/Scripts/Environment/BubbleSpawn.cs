using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn : MonoBehaviour
{

    public float timer;
    private float intTimer;
    private bool spawnedBubble;
    public GameObject smallBubble;
    public GameObject bubbleShaker;
    public GameObject spawnPoint;
    private float value;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        intTimer = timer;
        value = Random.value * intTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawnedBubble){

            SpawnBubble();

        }
    }
    
    void FixedUpdate() {
        
        if(spawnedBubble == true){

            timer -= Time.deltaTime;

            if(timer < value){

                timer = intTimer;
                spawnedBubble = false;

            }

        }

    }

    void SpawnBubble(){

        value = Random.value * intTimer;
        GameObject pp = Instantiate(smallBubble, bubbleShaker.transform);
        pp.transform.position = spawnPoint.transform.position;
        pp.GetComponent<Rigidbody2D>().AddForce(pp.transform.up * force, ForceMode2D.Impulse);
        spawnedBubble = true;

    }
}
