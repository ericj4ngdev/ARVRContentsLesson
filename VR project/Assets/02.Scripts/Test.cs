using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            StartCoroutine(frame(i));
        }
    }

    IEnumerator frame(int i)
    {
        Debug.Log((i+1) + "번째 프레임 시작");
        for (int j = 0; j < 2; j++)
            StartCoroutine(ThrowBall(j));

        yield return new WaitForSeconds(1f);
    }

    IEnumerator ThrowBall(int j)
    {
        Debug.Log((j + 1) + "번째 시도");
        if (Input.GetKeyDown("Space"))
        {
            Debug.Log("스트라이크");
            yield break;            
        }
        
    }

}
