using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody myRigid;
    Vector3 spawnDir;
    float curTime = 0.0f;
    public float speed = 5.0f;
    public float flightTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        myRigid.useGravity = false;
        spawnDir = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(2.0f, 10.0f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        this.transform.Rotate(Vector3.up * Time.deltaTime * 90);
        if(curTime < flightTime)
        {           
            myRigid.AddForce(spawnDir * speed);
        }
        else
            myRigid.useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        ScoreManager.instance.AddScore(10);
    }
}
