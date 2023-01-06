using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int frame = 1;
    public Ballspeed Ball;
    public Transform Spawnspot;
    private bool isStrike;
    void Update()
    {
        // 게임 시작 버튼 개념
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartCoroutine(FrameNum(frame));
        }   
    }

    IEnumerator FrameNum(int frame)
    {
        // 핀 소환
        while(true)
        {
            print($"{frame}번째 프레임");
            for (int i = 0; i < 2; i++)
            {
                yield return StartCoroutine(Shoot(Ball, i, frame));
                if (isStrike)
                    break;
                if (frame == 10 && i == 1 && (isStrike))
                {
                    // 핀 소환
                    yield return StartCoroutine(Shoot(Ball, 2, 10));
                    break;
                }
            }
            frame++;
            if(frame > 10)
                break;
        }
        print("종료");
        // 점수 공개
    }

    IEnumerator Shoot(Ballspeed Ball, int i, int frame)
    {
        print($"{i+1}번째 shoot");
        yield return StartCoroutine(hitpin(Ball));
        ResetBall(Ball);
    }
    
    IEnumerator hitpin(Ballspeed ball)
    {
        while (true)
        {
            if (ball.transform.position.z >= 20)
            {
                Debug.Log(ball.transform.position.z);
                print("공 감지");
                yield return new WaitForSeconds(5f);
                calc();         // 계산 호출
                print("==============================");
                break;
            }
            yield return null;
        }
    }

    void calc()
    {
        print("계산 완료!!");
    }

    void ResetBall(Ballspeed Ball)
    {
        print("공 제자리");
        Ball.rb.velocity = new Vector3(0,0,0); 
        Ball.transform.position = Spawnspot.position;
        Ball.transform.rotation = Spawnspot.rotation;
    }
    
}