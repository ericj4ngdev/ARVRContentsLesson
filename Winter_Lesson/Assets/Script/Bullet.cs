using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 발사
    public Vector3 ShootVector;
    public float BulletSpeed = 1000f;
    public float LifeTime = 2f;

    
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * BulletSpeed);
    }

    // 시간이 지나면 없어짐
    private void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            Destroy(this);
        }

    }

}