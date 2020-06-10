using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //유니티엔진의 GUI에 접근가능
using TMPro;            //텍스트메시프로 사용시 필요함
using System;

public class ScoreManager : MonoBehaviour
{
    //스코어매니저 싱글톤 만들기
    public static ScoreManager instance;

    //초간단하게 싱글톤만들기
    private void Awake() => instance = this;

    public Text scorTxt;                        //일반 UI 텍스트
    public Text highScoreTxt;                   //일반 UI 텍스트
    //public TextMeshProUGUI textTxt;             //텍스트메시프로 사용할때 TextMeshProUGUI 사용

    int score = 0;
    int highScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        //하이스코어 불러오기
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreTxt.text = "HighScore : " + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //하이스코어
        SaveHighScore();
    }

    private void SaveHighScore()
    {
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreTxt.text = "HighScore" + highScore;
        }
    }

    //점수 추가 및 텍스트 업데이트
    public void AddScore()
    {
        score++;
        scorTxt.text = "Score : " + score;

        //텍스트메시 프로
        //textTxt.text = "test.....";
    }
}
