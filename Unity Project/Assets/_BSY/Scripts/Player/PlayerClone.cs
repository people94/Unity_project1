using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    //아이템 먹어서 보조비행기가 생기도록 해야 한다
    //보조비행기는 일정시간마다 자동으로 총알발사 한다

    public GameObject clone;
    public GameObject firePos1;
    public GameObject firePos2;
    public ProgressBar progressBar;

    //선은 최소 2개의 점이 필요하다( 시작점, 끝점 )
    LineRenderer[] lr;    //라인렌더러 컴포넌트

    LayerMask enemyLayer;
    
    //일정시간동안만 레이저 보여주기
    public float rayTime = 0.3f;
    float timer = 0.0f;

    public float fireTime = 2.0f;
    float curTime;

    public float distance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //라인렌더러 컴포넌트 추가
        lr = clone.GetComponentsInChildren<LineRenderer>();
        
        //에너미 레이어 정보
        enemyLayer = LayerMask.NameToLayer("Enemy");      
    }

    // Update is called once per frame
    void Update()
    {
        CreateClone();
        AutoFire();
        if (lr[0].enabled && lr[1].enabled) ShowRay();
    }

    private void ShowRay()
    {
        timer += Time.deltaTime;
        //Ray로 충돌처리
        Ray ray1 = new Ray(firePos1.transform.position, Vector3.up);
        RaycastHit hitInfo1; //Ray와 충돌된 오브젝트의 정보를 담는다
        if (Physics.Raycast(ray1, out hitInfo1, distance))
        {
            //Debug.Log(hitInfo1.collider.name);
            if(hitInfo1.collider.CompareTag("ENEMY"))
            {
                hitInfo1.collider.SendMessage("KillEnemy");
            }
            else if(hitInfo1.collider.CompareTag("BOSS"))
            {
                hitInfo1.collider.SendMessage("HitBoss", 0.5f);
            }
        }
        Debug.DrawRay(ray1.origin, ray1.direction * distance, Color.red);

        //Ray로 충돌처리
        Ray ray2 = new Ray(firePos2.transform.position, Vector3.up);
        RaycastHit hitInfo2; //Ray와 충돌된 오브젝트의 정보를 담는다
        if (Physics.Raycast(ray2, out hitInfo2, distance))
        {
            //Debug.Log(hitInfo2.collider.name);
            if (hitInfo2.collider.CompareTag("ENEMY"))
            {
                hitInfo2.collider.SendMessage("KillEnemy");
            }
            else if(hitInfo2.collider.CompareTag("BOSS"))
            {
                hitInfo2.collider.SendMessage("HitBoss", 0.5f);
            }
        }
        //라인 시작점, 끝점
        lr[0].SetPosition(0, firePos1.transform.position);                                //인덱스 0번 - 시작위치
        lr[0].SetPosition(1, firePos1.transform.position + Vector3.up * distance);        //인덱스 1번 - 끝위치
        Debug.DrawRay(ray2.origin, ray2.direction * distance, Color.red);
        if (timer > rayTime)
        {
            lr[0].enabled = false;
            lr[1].enabled = false;
            timer = 0.0f;
        }
        //라인 시작점, 끝점
        lr[1].SetPosition(0, firePos2.transform.position);                                //인덱스 0번 - 시작위치
        lr[1].SetPosition(1, firePos2.transform.position + Vector3.up * distance);        //인덱스 1번 - 끝위치
    }

    private void AutoFire()
    {
        //클론이 액티브상태일때 총알 자동 발사하기
        if(clone.activeSelf)
        {
            progressBar.BarValue = curTime / fireTime * 100;
            curTime += Time.deltaTime;
            if(curTime >= fireTime)
            {
                //당연히 curTime 0으로 초기화
                curTime = 0.0f;

                //라인렌더러 컴포넌트 활성화
                lr[0].enabled = true;
                
                
                //라인렌더러 컴포넌트 활성화
                lr[1].enabled = true;                
            }
        }
    }    

    private void CreateClone()
    {
        if (ScoreManager.instance.GetScore() > 300)
        {
            clone.SetActive(true);
            progressBar.gameObject.SetActive(true);
        }
    }
}
