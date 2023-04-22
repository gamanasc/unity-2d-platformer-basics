using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float forcaPulo;
    private Rigidbody2D rig;

    private bool pulando;
    private bool pulo_duplo;

    private Animator animator;

    private bool isBlowing;

    // Start is called before the first frame update
    private void Awake() {
        this.rig = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
        this.Jump();
    }

    // Movimento lateral
    void Move(){
        

        // move sem usar a fisica
        // Vector3 movimento = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        // transform.position += movimento * Time.deltaTime * speed;

        float movement = Input.GetAxis("Horizontal");

        this.rig.velocity = new Vector2(movement * this.speed, this.rig.velocity.y);

        if(movement != 0f){
            this.animator.SetBool("walk", true);
            
            if(movement < 0){
                this.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }else{
                this.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        }else{
            this.animator.SetBool("walk", false);
        }
    }

    // Pulo
    void Jump(){
        if(Input.GetButtonDown("Jump") && !isBlowing ){

            if(!pulando){
                rig.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
                pulo_duplo = true;
                this.animator.SetBool("jump", true);

            }else{
                if(pulo_duplo){
                    rig.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
                    pulo_duplo = false;
                    this.animator.SetBool("jump", true);
                }
            }

        }
    }

    // Quando personagem toca algo
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.layer == 8){ // LAYER GROUND
            pulando = false;
        }

        if(
            other.gameObject.tag == "Spike" || 
            other.gameObject.tag == "Saw"
        ){ // tag spike
            GameController.instance.ShowGameOver();
            Destroy(this.gameObject);
        }


    }

    // Quando personagem NÃO toca algo
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.layer == 8){ // LAYER GROUND
            pulando = true;
            this.animator.SetBool("jump", false);
        }
    }

    // enquanto estiver colidindo
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.layer == 11){
            isBlowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == 11){
            isBlowing = false;
        }
    }

}
