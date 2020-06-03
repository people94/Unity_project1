using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //위에서 아래로 떨어지기만 한다.(똥피하기 느낌)
    //충돌처리(에너미랑 플레이어, 플레이어 총알과 에너미)

    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //자기자신도 없애고
        //충돌된 오브젝트도 없앤다.
        //Destroy(gameObject, 1.0f); //1초 후에 없앤다.
        Destroy(gameObject);//소문자 gameObject는 자기자신
        Destroy(collision.gameObject);//collision은 충돌된 게임오브젝트
    }
}
