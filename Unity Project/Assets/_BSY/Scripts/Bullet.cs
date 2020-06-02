using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총알클래스 하는일
    //플레이어가 발사 버튼을 누르면
    //총알이 생성된 후 발사하고 싶은 방향위로만 움직인다.
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    //카메라 화면밖으로나가서 보이지 않게 되면
    //호출되는 이벤트 함수
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
