using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cor : MonoBehaviour
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
        Debug.Log((i + 1) + "번째 프레임 시작");
        yield return new WaitForSeconds(2.0f);
        for (int j = 0; j < 2; j++)
        {
            Debug.Log((j + 1) + "번째 시도");
            yield return null;
        }
        // yield return new WaitForSeconds(2.0f);
        yield break;
    }

    IEnumerator ThrowBall(int j)
    {
        Debug.Log((j + 1) + "번째 시도");

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("스트라이크");
            yield break;            
        }
        if(Input.GetKeyDown("w"))
        {
            Debug.Log("9");
        }

        yield return new WaitForSeconds(1f);
    }
    
}
