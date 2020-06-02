using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{    
    public GameObject bulletFactory;//불릿 프리팹 
    public GameObject firePoint;    //발사위치
    public GameObject firePoint1;    //발사위치
    public GameObject firePoint2;    //발사위치
    private Coroutine coroutine;    //코루틴
    private bool coru = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.GetChild(1).transform.gameObject.SetActive(true);
            StartCoroutine(corutineT1());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.GetChild(2).transform.gameObject.SetActive(true);
            StartCoroutine(corutineT2());
        }
        
    }

    private void Fire()
    {
        //마우스왼쪽버튼 or 왼쪽컨트롤 키
        if (Input.GetButtonDown("Fire1"))
        {
            //총알공장(총알프리팹)에서 총알을 무한대로 찍어낼 수 있다.
            //Instantiate() 함수로 프리팹 파일을 게임오브젝트로 만든다.

            //총알 게임오브젝트 생성
            GameObject bullet = Instantiate(bulletFactory);
            //총알 오브젝트의 위치 지정
            bullet.transform.position = firePoint.transform.position;
        }
    }   

    IEnumerator corutineT1()
    {
        yield return new WaitForSeconds(1.0f);
        
        
        WeaponFire1();
        
    }

    private void WeaponFire1()
    {
         //총알 게임오브젝트 생성
         GameObject bullet = Instantiate(bulletFactory);
         //총알 오브젝트의 위치 지정
         bullet.transform.position = firePoint1.transform.position;
    }

    IEnumerator corutineT2()
    {
        yield return new WaitForSeconds(1.0f);
        
        WeaponFire2();
        
    }

    private void WeaponFire2()
    {
        //총알 게임오브젝트 생성
        GameObject bullet = Instantiate(bulletFactory);
        //총알 오브젝트의 위치 지정
        bullet.transform.position = firePoint2.transform.position;
    }
}
