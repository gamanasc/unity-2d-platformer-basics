using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;

    [SerializeField]
    private float speed;
    
    [SerializeField]
    private Transform rightColl;
    
    [SerializeField]
    private Transform leftColl;
    
    [SerializeField]
    private Transform headPoint;

    private bool colliding;

    [SerializeField]
    private LayerMask layer;

    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    private bool playerDestroyed = false;


    // Start is called before the first frame update
    void Awake()
    {
        this.rig = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.rig.velocity = new Vector2(speed, this.rig.velocity.y);
        this.colliding = Physics2D.Linecast(rightColl.position, leftColl.position, layer);

        if(this.colliding){
            this.transform.localScale = new Vector2(this.transform.localScale.x * -1f, this.transform.localScale.y);
            this.speed *= -1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        
        if(col.gameObject.tag == "Player"){
            float height = col.contacts[0].point.y - this.headPoint.position.y;
            
            if(height > 0 && !playerDestroyed){
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                this.speed = 0;
                anim.SetTrigger("die");

                this.boxCollider2D.enabled = false;
                this.circleCollider2D.enabled = false;
                this.rig.bodyType = RigidbodyType2D.Kinematic;

                Destroy(this.gameObject, 0.33f);
            }else{
                this.playerDestroyed = true;
                GameController.instance.ShowGameOver();
                Destroy(col.gameObject);
            }
        }

    }
}
