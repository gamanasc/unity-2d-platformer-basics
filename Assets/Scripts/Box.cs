using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private bool isUp;

    [SerializeField]
    private int health = 5;

    [SerializeField]
    public Animator anim;

    [SerializeField]
    public GameObject effect;


    private void Start(){
        // this.anim = GetComponent<Animator>();
    }

    private void Update(){
        if(this.health <= 0){
            Instantiate(effect, this.transform.position, this.transform.rotation);
            Destroy(this.transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){


            if(isUp){
                this.anim.SetTrigger("hit");
                this.health--;
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }else{
                this.anim.SetTrigger("hit");
                this.health--;
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -jumpForce), ForceMode2D.Impulse);
            }

        }
    }
}
