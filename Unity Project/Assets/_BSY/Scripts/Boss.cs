using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //보스 총알발사 (총알패턴)
    //1. 플레이어를 향해서 총알발사
    //2. 회전총알 발사

    public GameObject BulletFactory;
    public GameObject target;
    public float fireTime = 1.0f;
    public float fireTime1 = 50.0f;
    float curTime = 0.0f;
    float curTime1 = 0.0f;
    public int bulletMax = 30;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AutoFire1();
        AutoFire2();
    }

    //플레이어를 향해서 총알발사
    private void AutoFire1()
    {
        if (target != null)
        {
            curTime += Time.deltaTime;
            if (curTime > fireTime)
            {
                //총알공장에서 총알생성
                GameObject bullet = Instantiate(BulletFactory);
                //총알생성 위치
                bullet.transform.position = transform.position;
                //플레이어의 방향 구하기(벡터의 뺄셈)
                Vector3 dir = target.transform.position - transform.position;
                dir.Normalize();
                //총구의 방향도 맞춰준다
                bullet.transform.up = dir;
                //타이머 초기화
                curTime = 0.0f;
            }
        }
    }

    //회전총알 발사
    private void AutoFire2()
    {
        curTime1 += Time.deltaTime;
        if (curTime1 > fireTime1)
        {
            for (int i = 0; i < bulletMax; i++)
            {
                //총알공장에서 총알생성
                GameObject bullet = Instantiate(BulletFactory);
                //총알생성 위치
                bullet.transform.position = transform.position;
                //플레이어의 방향 구하기(벡터의 뺄셈)
                Vector3 dir = target.transform.position - transform.position;
                dir.Normalize();
                //360도 방향으로 총알발사
                float angle = 360.0f / bulletMax;
                //총구의 방향도 돌려준다
                bullet.transform.eulerAngles = new Vector3(0, 0, i * angle);
            }

            //타이머 초기화
            curTime1 = 0.0f;
        }
    }

}