using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float velocidade;
    [SerializeField]
    private float forcaPulo;
    private Rigidbody2D gravidade;

    private bool pulando;
    private bool pulo_duplo;

    private Animator animator;

    // Start is called before the first frame update
    private void Awake() {
        this.gravidade = GetComponent<Rigidbody2D>();
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
        
        Vector3 movimento = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movimento * Time.deltaTime * velocidade;
        if(Input.GetAxis("Horizontal") != 0f){
            this.animator.SetBool("walk", true);
            if(Input.GetAxis("Horizontal") < 0){
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
        if(Input.GetButtonDown("Jump")){

            if(!pulando){
                gravidade.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
                pulo_duplo = true;
                this.animator.SetBool("jump", true);

            }else{
                if(pulo_duplo){
                    gravidade.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
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
    }

    // Quando personagem NÃO toca algo
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.layer == 8){ // LAYER GROUND
            pulando = true;
            this.animator.SetBool("jump", false);
        }
    }
}
