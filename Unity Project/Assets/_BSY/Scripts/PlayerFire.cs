using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{    
    public GameObject bulletFactory;//불릿 프리팹 
    public GameObject firePoint;    //발사위치
    /* 내코드
    private bool raser = false;
    private RaycastHit hit;
    private float distance = 20.0f;
    public GameObject target;
    
    private float fireTime = 1.0f;
    private float curTime = 0.0f;
    */

    //레이저를 발사하기 위해서는 라인렌더러가 필요하다
    //선은 최소 2개의 점이 필요하다( 시작점, 끝점 )
    LineRenderer lr;    //라인렌더러 컴포넌트
    //일정시간동안만 레이저 보여주기
    public float rayTime = 0.3f;
    float timer = 0.0f;

    //사운드 재생
    AudioSource audio;

    //오브젝트 풀링
    //오브젝트 풀링에 사용할 최대 총알 갯수
    int poolSize = 20;
    int fireIdx = 0;
    //1. 배열
    //GameObject[] bulletPool;
    //2. 리스트
    //public List<GameObject> bulletPool;
    //3.큐
    public Queue<GameObject> bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        //라인렌더러 컴포넌트 추가
        lr = GetComponent<LineRenderer>();
        //중요
        //게임오브젝트는 활성화 비활성화 => SetActive() 함수 사용
        //컴포넌트는 enabled 속성 사용

        //오디오소스 컴포넌트 캐스팅
        audio = GetComponent<AudioSource>();

        //오브젝트 풀링 초기화
        InitObjectPooling();
    }

    //오브젝트 풀링 초기화
    private void InitObjectPooling()
    {
        //1. 배열
        //bulletPool = new GameObject[poolSize];
        //for(int i = 0; i < poolSize; i++)
        //{
        //    GameObject bullet = Instantiate(bulletFactory);
        //    bullet.SetActive(false);
        //    bulletPool[i] = bullet;
        //}

        //2. 리스트
        //bulletPool = new List<GameObject>();
        //for (int i = 0; i < poolSize; i++)
        //{
        //    GameObject bullet = Instantiate(bulletFactory);
        //    bullet.SetActive(false);
        //    bulletPool.Add(bullet);
        //}

        //3. 큐
        bulletPool = new Queue<GameObject>();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Fire();
        FireRay();
        //레이저 보여주는 기능이 활성화 되어 있을때만
        //레이저를 보여준다
        //일정시간이 지나면 레이저 보여주는 기능 비활성화
        if (lr.enabled) ShowRay();
    }

    private void ShowRay()
    {
        timer += Time.deltaTime;
        if(timer > rayTime)
        {
            lr.enabled = false;
            timer = 0.0f;
        }
    }

    //총알발사
    private void Fire()
    {
        //마우스왼쪽버튼 or 왼쪽컨트롤 키
        if (Input.GetButtonDown("Fire1"))
        {
            //총알공장(총알프리팹)에서 총알을 무한대로 찍어낼 수 있다.
            //Instantiate() 함수로 프리팹 파일을 게임오브젝트로 만든다.

            ////총알 게임오브젝트 생성
            //GameObject bullet = Instantiate(bulletFactory);
            ////총알 오브젝트의 위치 지정
            //bullet.transform.position = firePoint.transform.position;

            //1. 배열 오브젝트풀링으로 총알발사
            //bulletPool[fireIdx].SetActive(true);
            //bulletPool[fireIdx].transform.position = firePoint.transform.position;
            //bulletPool[fireIdx].transform.up = firePoint.transform.up;
            //fireIdx++;
            //if (fireIdx >= poolSize) fireIdx = 0;

            //2. 리스트 오브젝트풀링으로 총알발사
            //bulletPool[fireIdx].SetActive(true);
            //bulletPool[fireIdx].transform.position = firePoint.transform.position;
            //bulletPool[fireIdx].transform.up = firePoint.transform.up;
            //fireIdx++;
            //if (fireIdx >= poolSize) fireIdx = 0;

            //3. 리스트 오브젝트풀링으로 촟알발사 (진짜 오브젝트 풀링)
            //if(bulletPool.Count > 0)
            //{
            //    GameObject bullet = bulletPool[0];
            //    bullet.SetActive(true);
            //    bullet.transform.position = firePoint.transform.position;
            //    bullet.transform.up = firePoint.transform.up;
            //    //오브젝트 풀에서 빼준다
            //    bulletPool.Remove(bullet);
            //}
            //else//오브젝트 풀이 비어서 남은 총알이 하나도 없으니 풀크기를 늘려준다.
            //{
            //    GameObject bullet = Instantiate(bulletFactory);
            //    bullet.SetActive(false);
            //    bulletPool.Add(bullet);                
            //}

            //4. 큐 오브젝트풀링 사용하기
            if (bulletPool.Count > 0)
            {
                GameObject bullet = bulletPool.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = firePoint.transform.position;
                bullet.transform.up = firePoint.transform.up;
            }
            else
            {
                //총알 오브젝트 생성한다.
                GameObject bullet = Instantiate(bulletFactory);
                bullet.SetActive(false);
                //생성된 총알 오브젝트를 풀에 담는다
                bulletPool.Enqueue(bullet);
            }
        }
    }

    //레이저 발사
    private void FireRay()
    {
        //마우스왼쪽버튼 or 왼쪽컨트롤 키
        if (Input.GetButtonDown("Fire1"))
        {
            //레이저 사운드 재생
            audio.Play();

            //raser = true;
            //라인렌더러 컴포넌트 활성화
            lr.enabled = true;
            //라인 시작점, 끝점
            lr.SetPosition(0, transform.position);                                  //인덱스 0번 - 시작위치
            //lr.SetPosition(1, transform.position + Vector3.up * distance);          //인덱스 1번 - 끝위치
            //라인의 끝점은 충돌됝 지점으로 변경한다.

            //Ray로 충돌처리
            Ray _ray = new Ray(transform.position, Vector3.up);
            RaycastHit hitInfo; //Ray와 충돌된 오브젝트의 정보를 담는다
            if(Physics.Raycast(_ray, out hitInfo))
            {
                //레이저의 끝점 지정
                lr.SetPosition(1, hitInfo.point);
                
                //디스트로이존의 탑과는 충돌처리 되지 않도록 한다
                if (hitInfo.collider.name != "Top")
                {
                    Destroy(hitInfo.collider.gameObject);
                }

                //충돌된 에너미 오브젝트 삭제
                //프리팹으로 만든 오브젝트 같은경우는 생성될때 클론으로 생성된다
                //Contains("Enemy") => Enemy(clone) 이런것도 포함함
                //Contains("Enemy")->Enemy 라는 이름을 포함하면 지운다.
                //if (hitInfo.collider.name.Contains("Enemy"))
                //{
                //    Destroy(hitInfo.collider.gameObject);
                //}

                //충돌된 오브젝트 삭제
                //Destroy(hitInfo.collider.gameObject);
            }
            else
            {
                //충돌된 오브젝트가 없으니 끝점을 정해준다
                lr.SetPosition(1, transform.position + Vector3.up * 10);
            }
        }

        //내가 한 코드
        ////레이캐스트 여부를 나타내는 raser가 true일때 레이캐스트를 쏘고 충돌처리한다.
        //if (raser)
        //{
        //    Debug.DrawRay(transform.position, Vector3.up * distance, Color.red);
        //    if (Physics.Raycast(transform.position, Vector3.up, out hit, distance))
        //    {
        //        raser = false;
        //        if (hit.collider.tag == "BOSS")
        //        {
        //            Debug.Log("Boss");
        //            lr.SetPosition(1, transform.position + Vector3.up * hit.distance);
        //        }
        //    }
        //}
        //
        ////라인 렌더러가 인에이블 상태일때 시간을 제서 1초가 지나면 라인 렌더러를 끈다.
        //if(lr.enabled)
        //{
        //    curTime += Time.deltaTime;
        //    if(curTime > fireTime)
        //    {
        //        curTime = 0.0f;
        //        raser = false;
        //        lr.enabled = false;
        //    }
        //}
    }
    

    //파이어버튼 클릭시
    public void OnFireButtonClick()
    {
        /*내가 한 코드
        //총알 게임오브젝트 생성
        GameObject bullet = Instantiate(bulletFactory);

        Bullet _bullet = bullet.GetComponent<Bullet>();
        //내가 추가한코드
        //_bullet.isPlayer = true;
        //총알 오브젝트의 위치 지정
        bullet.transform.position = firePoint.transform.position;
        */

        //총알 게임오브젝트 생성
        GameObject bullet = Instantiate(bulletFactory);
        //총알 오브젝트의 위치 지정
        //bullet.transform.position = transform.position;
        bullet.transform.position = firePoint.transform.position;

        SceneMgr.Instance.LoadScene("StartScene");
    }
}
