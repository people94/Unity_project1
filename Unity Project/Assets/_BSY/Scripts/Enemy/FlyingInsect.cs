using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingInsect : MonoBehaviour
{
    //위에서 아래로 떨어지기만 한다.(똥피하기 느낌)
    //충돌처리(에너미랑 플레이어, 플레이어 총알과 에너미)
    private GameObject startZone;

    public float speed = 10.0f;

    public GameObject fxFactory;

    public GameObject coinFactory;

    public GameObject[] coinPool;

    public int coinSize = 5;

    private void Awake()
    {
        coinPool = new GameObject[coinSize];
        for (int i = 0; i < coinSize; i++)
        {
            GameObject coin = Instantiate(coinFactory);
            coin.SetActive(false);
            coinPool[i] = coin;
        }
        startZone = GameObject.Find("GameStartZone");
    }

    // Update is called once per frame
    void Update()
    {
        if (startZone.transform.position.y < transform.position.y)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        //아래로 이동해라
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //자기자신도 없애고
        //충돌된 오브젝트도 없앤다.
        //Destroy(gameObject, 1.0f); //1초 후에 없앤다.        
        //Destroy(collision.gameObject);//collision은 충돌된 게임오브젝트

        //플레이어면 플레이어 폭발하고 나도 폭발
        if (collision.gameObject.name == "Player")
        {
            ShowEffect();
            Destroy(gameObject);
            Destroy(collision.gameObject);//소문자 gameObject는 자기자신
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
