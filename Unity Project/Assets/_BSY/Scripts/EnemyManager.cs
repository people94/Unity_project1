//using System; //이게 있으면 Random이 사용안됨. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //에너미매니저 역할?
    //에너미 프리팹을 공장에서 찍어낸다
    //에너미 스폰타임    
    //에너미 스폰위치

    public GameObject enemyFactory;     //에너미 공장
    //public GameObject spawnPoint;       //스폰위치
    public GameObject[] spawnPoints;       //스폰위치 여러개
    float spawnTime = 1.0f;                    //스폰타임 (몇초에 한번씩 생성)
    float curTime = 0.0f;                      //누적타임

    // Update is called once per frame
    void Update()
    {
        //에너미 생성
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        //몇초에 한번씩 이벤트 발동
        //시간 누적타임으로 계산한다
        //게임에서 정말 자주 사용함

        curTime += Time.deltaTime;
        if(curTime > spawnTime)
        {
            //누적된 현재시간을 0초로 초기화(반드시 해줘야 한다)
            curTime = 0.0f;
            spawnTime = Random.Range(0.5f, 2.0f);
            //에너미 생성
            GameObject enemy = Instantiate(enemyFactory);
            //enemy.transform.position = spawnPoint.transform.position;
            int index = Random.Range(0, spawnPoints.Length);
            enemy.transform.position = spawnPoints[index].transform.position;
        }
    }
}
