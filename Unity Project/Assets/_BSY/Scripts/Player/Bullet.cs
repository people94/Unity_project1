using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    //총알클래스 하는일
    //플레이어가 발사 버튼을 누르면
    //총알이 생성된 후 발사하고 싶은 방향위로만 움직인다.
    public float speed = 10.0f;    
   
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * 360 * Time.deltaTime);
    }

    //카메라 화면밖으로나가서 보이지 않게 되면
    //호출되는 이벤트 함수
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
        if(collision.collider.CompareTag("ENEMY"))
        {
            collision.collider.SendMessage("KillEnemy");
        }
        else if(collision.collider.CompareTag("BOSS"))
        {
            collision.collider.SendMessage("HitBoss", 2.0f);
        }
    }

    /*내가 짠코드
     * 
     * [HideInInspector] public bool isPlayer = false;   //플레이어가 쏜건지
    private void OnTriggerEnter(Collider other)
    {
        if(isPlayer && other.CompareTag("ENEMY"))
        {
            GameManager.gameManager.UpScore();
            GameManager.gameManager.ShowScore();
            GameManager.gameManager.ShowHighScore();
            Destroy(other.gameObject);
        }
    }
    */
}
