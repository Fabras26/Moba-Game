using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextGameOver;
    public TextMeshProUGUI highscoreText;


    void Start()
    {
        instance = this;
        score = 0;
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddPoints()
    {
        score++;
        scoreText.text = score.ToString();
    }
    public void GameOver()
    {
        if (score > PlayerPrefs.GetInt("Highscore")) PlayerPrefs.SetInt("Highscore", score);
        scoreTextGameOver.text = score.ToString();
        highscoreText.text = PlayerPrefs.GetInt("Highscore").ToString();
    }
}
