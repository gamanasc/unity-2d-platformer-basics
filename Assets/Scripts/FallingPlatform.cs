using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{   

    [SerializeField]
    private float fallingTime;
    private TargetJoint2D target;
    private BoxCollider2D boxColl;

    void Awake()
    {
         this.target = GetComponent<TargetJoint2D>();  
         this.boxColl = GetComponent<BoxCollider2D>();  
    }

    // Quando personagem toca algo
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player"){ // LAYER GROUND
            this.Invoke("Falling", this.fallingTime);
        }
    }

    // Quando personagem toca algo
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == 9){ // LAYER GROUND
            Destroy(this.gameObject);
        }
    }

    private void Falling(){
        target.enabled = false;
        boxColl.isTrigger = true;
    }
}
