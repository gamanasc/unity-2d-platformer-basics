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

    // Start is called before the first frame update
    private void Awake() {
        this.gravidade = GetComponent<Rigidbody2D>();
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
    }

    // Pulo
    void Jump(){
        if(Input.GetButtonDown("Jump")){
            gravidade.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
        }
    }
}
