using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBubble : MonoBehaviour
{
    public float timer;

    void Update()
    {
        
    }

    void FixedUpdate() {

        timer -= Time.deltaTime;

        if(timer < 0){

            Destroy(gameObject);

        }

        

    }
}
