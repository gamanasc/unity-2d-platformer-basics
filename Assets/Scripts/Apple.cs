using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;
    [SerializeField]
    private GameObject collected;
    [SerializeField]
    private int score;

    // Start is called before the first frame update
    void Awake()
    {
        this.sr = GetComponent<SpriteRenderer>();
        this.circle = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            this.sr.enabled = false;
            this.circle.enabled = false;
            collected.SetActive(true);

            GameController.instance.totalScore += score;
            GameController.instance.UpdateScoreText();

            Destroy(this.gameObject, 0.3f);
        }
    }
}
