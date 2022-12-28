using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �߻�
    public Vector3 ShootVector;
    public float BulletSpeed = 1000f;
    public float LifeTime = 2f;

    
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * BulletSpeed);
    }

    // �ð��� ������ ������
    private void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            Destroy(this);
        }

    }

}