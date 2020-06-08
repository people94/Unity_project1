using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text highScoreTxt;
    public Text scoreTxt;
    public int highScore = 0;
    public int score = 0;

    //score가 highScore 넘으면 갱신해줘야함

    public static GameManager gameManager = null;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        else if( gameManager != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        ShowHighScore();
        ShowScore();
    }

    public void UpScore()
    {
        score++;
    }

    public void ShowHighScore()
    {
        highScoreTxt.text = "HighScore : " + highScore.ToString();
    }

    public void ShowScore()
    {
        scoreTxt.text = "Score : " + score.ToString();
    }

    private void OnDisable()
    {
        if(score > highScore)
        {
            highScore = score;
        }
        PlayerPrefs.SetInt("HIGHSCORE", highScore);
    }
}
