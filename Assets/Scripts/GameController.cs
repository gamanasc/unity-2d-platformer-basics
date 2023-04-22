using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;
    public TMP_Text scoreText;
    public GameObject gameOver;

    public static GameController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void UpdateScoreText()
    {
        scoreText.text = this.totalScore.ToString();
    }

    public void ShowGameOver(){
        gameOver.SetActive(true);
    }

    public void RestartGame(string lvlname){
        SceneManager.LoadScene(lvlname);
    }

}
