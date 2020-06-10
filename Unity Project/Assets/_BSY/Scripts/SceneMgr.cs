﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    //씬매니져 싱글톤 만들기
    //씬매니져는 시작, 게임, 종료씬 모두를 관리해야 한다
    //또한 씬매니져는 씬이 변경되도 삭제되면 안된다
    public static SceneMgr Instance;
    private void Awake()
    {
        //씬매니져가 존재한다면
        //새로생성되는 씬매니져는 삭제하고 바로 빠져나와라
        if(Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        //인스턴스가 없을때
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string value)
    {
        SceneManager.LoadScene(value);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
