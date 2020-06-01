using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;             //플레이어 이동속도
    public Vector2 margin;          //뷰포트좌표는 0.0f ~ 1.0f 사이의 값

    // Start is called before the first frame update
    void Start()
    {
        margin = new Vector2(0.08f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //플레이어 이동
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //transform.Translate(h * speed * Time.deltaTime,  v * speed * Time.deltaTime, 0);
        //Vector3 dir = Vector3.right * h + Vector3.up * v;
        Vector3 dir = new Vector3(h, v, 0);
        //dir.Normalize();
        transform.Translate(dir * speed * Time.deltaTime);

        //위치 = 현재위치 + (방향 + 시간)
        //P = P0 + vt;
        //transform.position = transform.position + (dir * speed * Time.deltaTime);
        //transform.position += dir * speed * Time.deltaTime;

        //플레이어가 화면 밖으로 못나가게 하는 코드
        MoveInScreen();
    }

    //플레이어가 화면 밖으로 못나가게 하는 코드
    private void MoveInScreen()
    {
        //방법은 크게 3가지
        //첫번째 : 화면밖의 공간에 큐브 4개 만들어서 배치 - 건너뜀, 해보니까 잘안됨
        //리지드바디의 충돌체로 이동 못하게 막기

        //두번째 : 플레이어의 포지션으로 이동처리
        //transform.position.x = 100; 이런 식으로 처리가 안됨. position 자체는 되는데 x에만 넣는거는 안됨
        //Vector3 position = transform.position;
        //position.x = 10; 이렇게는 됨
        //transform.position = position;   포지션의 값을 벡터에 담고 값을 변경시킨 다음 다시 포지션에 담는 것을 캐스팅이라고한다.
        //Vector3 position = transform.position;
        //position.x = Mathf.Clamp(position.x, -2.3f, 2.3f);      //position.x의 최소값은 2.5f ~ 2.5f
        //position.y = Mathf.Clamp(position.y, -4.5f, 4.5f);      //position.y의 최소값은 2.5f ~ 2.5f
        //transform.position = position;

        //세번째 : 메인카메라의 뷰포트를 가져와서 처리한다 ( 우린 이걸 사용한다.)
        //스크린좌표 : 왼쪽하단(0, 0), 우측상단(maxX, maxY)
        //뷰포트좌표 : 왼쪽하단(0, 0), 우측상단(1,0f, 1,0f)
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        //position.x = Mathf.Clamp(position.x, 0.0f, 1.0f);
        //position.y = Mathf.Clamp(position.y, 0.0f, 1.0f);
        position.x = Mathf.Clamp(position.x, 0.0f + margin.x, 1.0f - margin.x);
        position.y = Mathf.Clamp(position.y, 0.0f + margin.y, 1.0f - margin.y);
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }   

}
