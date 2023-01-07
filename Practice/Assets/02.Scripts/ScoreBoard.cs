using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor.UIElements;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public GameObject Pin;
    public Transform[] HittedPinPosition;
    private int[] Score;        // 누적 프레임 총 스코어
    private int[] Fscore;       // 각 프레임의 총 점수
    private int[][] Frame;      // i : 프레임, j : 던진 횟수, 각 프레임의 첫 번째, 두 번째 점수 저장
    private bool[] isSpare;     
    private bool[] isStrike;    
    private bool[][] hit;       // shootNum : 던진 수, k : 핀 개수, 쓰러짐 여부 저장
    // 쓰러짐 여부를 검사할 높이
    private float height;
    public float Speed;

    
    private void Awake()
    {
        height = Pin.transform.localScale.y;
    }

    /// <summary>
    /// 쓰러진 핀 개수 계산
    /// </summary>
    /// <param name="shootNum">해당 프레임의 shootNum번째 시도</param>
    /// <returns>쓰러진 핀 개수( = 점수)</returns>
    int PinScore(int shootNum)
    {
        int count = 0;
        for (int k = 0; k < 10; k++)
        {
            // 몇 번째 shoot에서 k번째 핀이 쓰러졌는지 여부
            if (SpawnManager.Instance.BowlingPin[k].transform.position.y < height)
            {
                hit[shootNum][k] = true;        // 쓰러지면 k번째 핀의 bool을 true로 변환
                count++;                        // 쓰러진 개수 = 점수
            }    
        }
        return count;       // 점수로 연결
    }
    
    /// <summary>
    /// 이전 프레임이 스페어인 경우, 현재 프레임 첫번째 샷 이후이전 프레임 점수 계산
    /// </summary>
    /// <param name="currentFrame">현재 프레임</param>
    /// <param name="shoot">던진 번째 수 - 1</param>
    void SpareScoreCheck(int currentFrame,int shoot)
    {
        if (isSpare[currentFrame - 1])
            Fscore[currentFrame - 1] += Frame[currentFrame][shoot];
    }
    
    /// <summary>
    /// 이전 프레임이 스트라이크인 경우, 현재 프레임 두번째 샷 이후 이전 프레임 점수 계산
    /// </summary>
    /// <param name="currentFrame"></param>
    /// <param name="shoot"></param>
    /// if (isStrike[currentFrame - 1])
    void StrikeScoreCheck(int currentFrame,int shoot)
    {
        if (isStrike[currentFrame - 1])
            Fscore[currentFrame - 1] += Frame[currentFrame][shoot];
    }

    void CleanupHittedPin(int pinNum)
    {
        SpawnManager.Instance.BowlingPin[pinNum].transform.position =
            HittedPinPosition[pinNum].position + new Vector3(0, 0.2f, 0);
        SpawnManager.Instance.BowlingPin[pinNum].transform.rotation = HittedPinPosition[pinNum].rotation;
    }

    /*IEnumerator GetVelocity(GameObject prefab)
    {
        var currentPosition = prefab.transform.position;
        //var velo;
        //velo = (prefab.transform.position - prefab.transform.position)/Time.deltaTime;

        return Speed;
    }*/
    
    void GetPinsSpeed()
    {
        // 10개 핀 읽어오는 함수
        for (int i = 0; i < 10; i++)
        {
            // float speed = ;
            // SpawnManager.Instance.BowlingPin[i].transform.position;
        }
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnManager.Instance.SetPinPosition();         // 모든 핀 리셋
            // StartCoroutine(BowlingFlow(i));
        }
    }

    void ThrowBall(int j)
    {
        /*
                // 그랩하고 그랩을 풀면 다시 돌아옴
                // 던질때 가속하는 기능 구현
                // if(!grap) return;
                bool OnTriggerEnter(Collider other)
                    {
                        if (other.gameObject.tag == "Ball")
                        {
                            Debug.Log("공 감지");
                            Invoke("CalcScore", 5f);
                        }// && GetBallSpeed(other.gameObject) < 0.1f)
                    }
            }*/
    }



    /*IEnumerator CalcScore()
    {
            for (int j = 0; j < 2; j++)
            {
                // 첫번째 시도
                if (j == 0)
                {
                    if (PinScore(j) == 10)
                    {
                        isStrike[i] = true;
                        Frame[i][j] = PinScore(j);
                        Frame[i][j + 1] = 0;            // 이러면 터키 계산할 때 문제가 된다. 
                        if (i >= 1)      // 이전 프레임 체크용. 인덱스 에러 방지
                        {
                            SpareScoreCheck(i, j);
                            if (isStrike[i - 1]) // 2연속 스트라이크(더블)인 경우 점수계산
                            {
                                Fscore[i - 1] += Frame[i][j];
                                if (i >= 2)
                                {
                                    if (isStrike[i - 2]) // 3연속 스트라이크(터키)인 경우 점수계산
                                        Fscore[i - 2] += Frame[i][j];
                                }
                            }
                        }
                        break;
                    }
                    else
                    {
                        Frame[i][j] = PinScore(j);
                        if (i >= 1)
                        {
                            SpareScoreCheck(i, j);
                            StrikeScoreCheck(i, j);
                        }
                    }
                }
                
                // 쓰러진 핀 치우기
                for (int k = 0; k < 10; k++)
                    if (hit[j][k] == true)          // 쓰러졌는가?
                        CleanupHittedPin(k);        // 위쪽으로 치운다. 
                
                // 두번째 시도
                if (j == 1)
                {
                    if (PinScore(j) == 10)
                    {
                        isSpare[i] = true;
                        Frame[i][j] = PinScore(j)-Frame[i][j-1];
                        StrikeScoreCheck(i, j);
                    }
                    else
                    {
                        Frame[i][j] = PinScore(j);
                        StrikeScoreCheck(i, j);
                    }
                    // 마지막 프레임 두번째 슛이 스트라이크이거나 스페어인 경우 한번 더 던질 수 있다.
                    if (i == 9 && (isStrike[i] || isSpare[i]))
                    {
                        Frame[i][j] += PinScore(j);
                        j++;
                        SpawnManager.Instance.SetPinPosition();
                        // ThrowBall();
                    }
                }
            }
            if (!isSpare[i] && !isStrike[i])
                Fscore[i] = Frame[i][0] + Frame[i][1];
            else
                Fscore[i] = 10;         // 스페어거나 스트라이크이면 10점먹고 들어간다. 
        
        yield return new WaitForSeconds(1f);
    }*/
    
    IEnumerator ThrowBall()
    {
            // 그랩 조건으로 던짐 여부 확인
            // 핀과 공의 속도가 0.5 이하가 된지 5초 후에 점수 계산 부분으로 넘어감
            // 함수로 만든 뒤, Invoke로 해도 될듯
            //SpawnManager.Instance.BallPrefab
        
        
        yield return new WaitForSeconds(.1f);
        // StartCoroutine()
    }
    // float Get
    float GetBallSpeed(GameObject Ball)
    {
        Vector3 m_LastPosition = Ball.transform.position;
        float speed = (((Ball.transform.position - m_LastPosition).magnitude) / Time.deltaTime); 
        m_LastPosition = Ball.transform.position;            // 현재 벡터를 이전벡터로 저장

        return speed; 
    }

    private void FixedUpdate()
    {
        Speed = GetBallSpeed(SpawnManager.Instance.BallPrefab);
        // Debug.Log(Speed);
    }


    
}
