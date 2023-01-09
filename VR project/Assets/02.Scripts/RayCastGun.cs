using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastGun : MonoBehaviour
{
    public LayerMask wall;
    private void Awake()
    {
        
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;
        
        if (Physics.Raycast(ray, out hitData, 10f))
        {
            Debug.DrawRay(transform.position, transform.forward * hitData.distance,
                Color.red);
            Debug.Log("Hit Something");
            StartCoroutine(printHitData(hitData));
            // Debug.Log(hitData.point);                       // 레이캐스팅이 감지된 위치
            // Debug.Log(hitData.distance);                    // Ray의 원점에서 충돌지점까지의 거리
            // Debug.Log(hitData.collider.tag);                // 히트된 대상 객체의 Collider 세부 정보
            // Debug.Log(hitData.transform.gameObject);        // 충돌 객체의 Transform
        }
        else
        {
            Debug.Log("Hit Nothing");
            Debug.DrawRay(transform.position, transform.forward * 20f, Color.green);
        }
        
        
    }

    IEnumerator printHitData(RaycastHit hitData)
    {
        Debug.Log(hitData.point);                       // 레이캐스팅이 감지된 위치
        Debug.Log(hitData.distance);                    // Ray의 원점에서 충돌지점까지의 거리
        Debug.Log(hitData.collider.tag);                // 히트된 대상 객체의 Collider 세부 정보
        Debug.Log(hitData.transform.gameObject);        // 충돌 객체의 Transform

        yield return null;
        //new WaitForSeconds(5f);
    }
    
}
