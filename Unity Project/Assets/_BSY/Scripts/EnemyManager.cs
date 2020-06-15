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

    //public GameObject spawnPoint;                 //스폰위치
    public GameObject flyingFactory;                //플라잉 공장    
    public GameObject[] flyingPoints;               //플라잉 스폰위치 여러개    
    float flyingSpawnTime = 4.0f;                   //플라잉 스폰타임 (몇초에 한번씩 생성)
    float flyingCurTime = 0.0f;                     //플라잉 누적타임

    public GameObject[] enemyPoints;                //에너미 스폰위치 여러개
    public GameObject enemyFactory;                 //에너미 공장
    float enemySpawnTime = 4.0f;                    //에너미 스폰타임 (몇초에 한번씩 생성)
    float enemyCurTime = 0.0f;                      //에너미 누적타임

    private GameObject boss;
    private Boss bossScript;
    public GameObject bossPoint;                    //보스 스폰위치
    public GameObject bossFactory;                  //보스 공장
    private bool spawnBoss = false;

    //보스 프로그레스바
    public ProgressBar progressBar;

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.instance.GetScore() < 2000)
        {
            //플라잉 생성
            SpawnFlying();
            //에너미 생성
            SpawnEnemy();
        }
        else
        {
            SpawnBoss();
            progressBar.BarValue = bossScript.curHp / bossScript.maxHp * 100;
        }
    }

    private void SpawnFlying()
    {
        //몇초에 한번씩 이벤트 발동
        //시간 누적타임으로 계산한다
        //게임에서 정말 자주 사용함
        flyingCurTime += Time.deltaTime;
        if(flyingCurTime > flyingSpawnTime)
        {
            //누적된 현재시간을 0초로 초기화(반드시 해줘야 한다)
            flyingCurTime = 0.0f;
            flyingSpawnTime = Random.Range(2.0f, 4.0f);
            //에너미 생성
            GameObject enemy = Instantiate(flyingFactory);
            //enemy.transform.position = spawnPoint.transform.position;
            int index = Random.Range(0, flyingPoints.Length);
            enemy.transform.position = flyingPoints[index].transform.position;
        }
    }

    private void SpawnEnemy()
    {
        //몇초에 한번씩 이벤트 발동
        //시간 누적타임으로 계산한다
        //게임에서 정말 자주 사용함

        enemyCurTime += Time.deltaTime;
        if (enemyCurTime > enemySpawnTime)
        {
            //누적된 현재시간을 0초로 초기화(반드시 해줘야 한다)
            enemyCurTime = 0.0f;
            enemySpawnTime = Random.Range(2.0f, 4.0f);
            //에너미 생성
            GameObject enemy = Instantiate(enemyFactory);
            //enemy.transform.position = spawnPoint.transform.position;
            int index = Random.Range(0, enemyPoints.Length);
            enemy.transform.position = enemyPoints[index].transform.position;
        }
    }

    private void SpawnBoss()
    {
        if(!spawnBoss)
        {
            boss = Instantiate(bossFactory);
            boss.transform.position = bossPoint.transform.position;
            spawnBoss = true;
            progressBar.gameObject.SetActive(true);
            bossScript = boss.GetComponent<Boss>();
        }
    }
}
