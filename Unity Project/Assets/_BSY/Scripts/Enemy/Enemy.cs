using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //위에서 아래로 떨어지기만 한다.(똥피하기 느낌)
    //충돌처리(에너미랑 플레이어, 플레이어 총알과 에너미)

    private GameObject startZone;

    public float speed = 1.0f;

    public GameObject fxFactory;

    public GameObject coinFactory;

    public GameObject[] coinPool;

    public int coinSize = 5;

    public GameObject BulletFactory;

    public GameObject target;

    public GameObject[] bulletPool;
    
    public float fireTime = 2.0f;

    float curTime = 0.0f;

    public int bulletMax = 9;

    private int bulletIdx = 0;

    private void Awake()
    {
        InitCoin();
        InitBullet();
        target = GameObject.Find("Player");
        startZone = GameObject.Find("GameStartZone");
    }

    private void InitCoin()
    {
        coinPool = new GameObject[coinSize];
        for (int i = 0; i < coinSize; i++)
        {
            GameObject coin = Instantiate(coinFactory);
            coin.SetActive(false);
            coinPool[i] = coin;
        }
    }

    private void InitBullet()
    {
        bulletPool = new GameObject[bulletMax];
        for(int i = 0; i < bulletMax; i++)
        {
            GameObject bullet = Instantiate(BulletFactory);
            bullet.SetActive(false);
            bulletPool[i] = bullet;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(startZone.transform.position.y < transform.position.y)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        //아래로 이동해라
        transform.Translate(Vector3.down * speed * Time.deltaTime);       
        Fire();
    }
    
    private void Fire()
    {
        if (target != null)
        {
            curTime += Time.deltaTime;
            if (curTime > fireTime)
            {
                if (curTime >= fireTime + 0.1f && bulletIdx % 3 == 0)   //첫번째 공격
                {
                    //총알생성 위치
                    bulletPool[bulletIdx].SetActive(true);
                    bulletPool[bulletIdx].transform.position = transform.position;                                       
                    //플레이어의 방향 구하기(벡터의 뺄셈)
                    Vector3 dir = target.transform.position - transform.position;
                    dir.Normalize();
                    //총구의 방향도 맞춰준다
                    bulletPool[bulletIdx++].transform.up = dir;
                }
                else if (curTime >= fireTime + 0.2f && bulletIdx % 3 == 1)
                {
                    //총알생성 위치
                    bulletPool[bulletIdx].SetActive(true);
                    bulletPool[bulletIdx].transform.position = transform.position;
                    //플레이어의 방향 구하기(벡터의 뺄셈)
                    Vector3 dir = target.transform.position - transform.position;
                    dir.Normalize();
                    //총구의 방향도 맞춰준다
                    bulletPool[bulletIdx++].transform.up = dir;
                }
                else if (curTime >= fireTime + 0.3f && bulletIdx % 3 == 2)
                {
                    //총알생성 위치
                    bulletPool[bulletIdx].SetActive(true);
                    bulletPool[bulletIdx].transform.position = transform.position;
                    //플레이어의 방향 구하기(벡터의 뺄셈)
                    Vector3 dir = target.transform.position - transform.position;
                    dir.Normalize();
                    //총구의 방향도 맞춰준다
                    bulletPool[bulletIdx++].transform.up = dir;
                    curTime = 0.0f;
                }
                
                if (bulletIdx > 8)
                    bulletIdx = 0;
            }
        }
    }

    void ShowEffect()
    {
        GameObject fx = Instantiate(fxFactory);
        fx.transform.position = transform.position;
        Destroy(fx, 1.0f);
    }

    void spreadCoin()
    {
        for (int i = 0; i < coinSize; i++)
        {
            coinPool[i].SetActive(true);
            Vector3 position = this.transform.position;
            position.y += 2.0f;
            coinPool[i].transform.position = position;
        }
    }

    void KillEnemy()
    {
        //이펙트보여주기
        ShowEffect();
        //코인 뿌리기
        spreadCoin();
        //점수추가
        ScoreManager.instance.AddScore(50);
        Destroy(gameObject);//소문자 gameObject는 자기자신
    }
}


