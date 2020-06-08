using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    /*내코드
    MeshRenderer background;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset.y += Time.deltaTime / 2;
        background.material.mainTextureOffset = offset;
    }
    */
    Material mat;           //백그라운드 메터리얼
    public float scrollSpeed = 0.1f;    //스크롤 속도

    private void Start()
    {
        //메터리얼은 렌더러 컴포넌트안에 속성으로 붙어 있다.
        mat = GetComponent<Renderer>().material;    //MeshRenderer대신 Renderer써도 됨
        //mat = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        BackgroundScroll();
    }

    //백그라운드 스크롤
    private void BackgroundScroll()
    {
        //아래와 같은 작업을 캐스팅이라고 한다.
        //transform.position 조정할때 방법과 동일하다. 
        //메터리얼의 메인텍스처 오프셋은 Vector2로 만들어져 있다.
        Vector2 offset = mat.mainTextureOffset;
        //offset.y 의 값만 보정해주면 된다.
        offset.Set(0, offset.y + (scrollSpeed * Time.deltaTime));
        //다시 메터리얼 오프셋에 담는다.
        mat.mainTextureOffset = offset;
    }
}
