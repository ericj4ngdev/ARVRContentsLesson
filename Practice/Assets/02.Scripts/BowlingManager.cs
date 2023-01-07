using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingManager : MonoBehaviour
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
    
    // ===========================================================
    private int frame = 1;
    public Transform Spawnspot;
    private GameObject Ball;

    
    private void Awake()
    {
        height = Pin.transform.localScale.y;
        Ball = SpawnManager.Instance.BallPrefab;
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
    /// <summary>
    /// 쓰러진 핀 개수 계산 및 치우기
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
                CleanupHittedPin(k);
                hit[shootNum][k] = true;        // 쓰러지면 k번째 핀의 bool을 true로 변환
                count++;                        // 쓰러진 개수 = 점수
            }    
        }
        return count;       // 점수로 연결
    }
    
    void CleanupHittedPin(int pinNum)
    {
        SpawnManager.Instance.BowlingPin[pinNum].transform.position =
            HittedPinPosition[pinNum].position + new Vector3(0, 0.2f, 0);
        SpawnManager.Instance.BowlingPin[pinNum].transform.rotation = HittedPinPosition[pinNum].rotation;
    }

    private void Start()
    {
        // for (int i = 0; i < 10; i++)
        // {
        //     SpawnManager.Instance.SetPinPosition();         // 모든 핀 리셋
        // }
    }
    
    void Update()
    {
        // 게임 시작 버튼 개념
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            StartCoroutine(FrameNum(frame));
        }   
    }

    void CalcScore(int frame_num, int shoot_num)
    {
        if (shoot_num == 0)
        {
            if (PinScore(shoot_num) == 10)
            {
                isStrike[frame_num] = true;
                Frame[frame_num][shoot_num] = PinScore(shoot_num);
                if (frame_num >= 1) // 이전 프레임 체크용. 인덱스 에러 방지
                {
                    SpareScoreCheck(frame_num, shoot_num);
                    if (isStrike[frame_num - 1]) // 2연속 스트라이크(더블)인 경우 점수계산
                    {
                        Fscore[frame_num - 1] += Frame[frame_num][shoot_num];
                        if (frame_num >= 2)
                        {
                            if (isStrike[frame_num - 2]) // 3연속 스트라이크(터키)인 경우 점수계산
                                Fscore[frame_num - 2] += Frame[frame_num][shoot_num];
                        }
                    }
                }
            }
            else
            {
                Frame[frame_num][shoot_num] = PinScore(shoot_num);
                if (frame_num >= 1)
                {
                    SpareScoreCheck(frame_num, shoot_num);
                    StrikeScoreCheck(frame_num, shoot_num);
                }
            }
        }

        if (shoot_num == 1)
        {
            if (PinScore(shoot_num) == 10)
            {
                isSpare[frame_num] = true;
                Frame[frame_num][shoot_num] = PinScore(shoot_num) - Frame[frame_num][shoot_num - 1];
                StrikeScoreCheck(frame_num, shoot_num);
            }
            else
            {
                Frame[frame_num][shoot_num] = PinScore(shoot_num);
                StrikeScoreCheck(frame_num, shoot_num);
            }

            // 마지막 프레임 두번째 슛이 스트라이크이거나 스페어인 경우 한번 더 던질 수 있다.
            if (frame_num == 9 && (isStrike[frame_num] || isSpare[frame_num]))
            {
                Frame[frame_num][shoot_num] += PinScore(shoot_num);
                shoot_num++;
                SpawnManager.Instance.SetPinPosition();
                // ThrowBall();
            }
        }
        if (!isSpare[frame_num] && !isStrike[frame_num])
            Fscore[frame_num] = Frame[frame_num][0] + Frame[frame_num][1];
        else
            Fscore[frame_num] = 10;         // 스페어거나 스트라이크이면 10점먹고 들어간다. 
    }

    IEnumerator FrameNum(int frame)
    {
        print("SetPinPosition() 호출");
        SpawnManager.Instance.SetPinPosition();
        while(true)
        {
            print($"{frame}번째 프레임");
            for (int i = 0; i < 2; i++)
            {
                yield return StartCoroutine(Shoot(Ball, i, frame));
                // 점수 계산
                CalcScore(frame, i);

                if (frame == 10 && i == 1 && isStrike[frame])
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
    
    // 던지는 시도 1, 2번
    IEnumerator Shoot(GameObject Ball, int i, int frame)
    {
        print($"{i+1}번째 shoot");
        yield return StartCoroutine(hitpin(Ball));
        // i==1? CleanupHittedPin() : 치우는 게 pinscore에 있어서 문제...  
        SpawnManager.Instance.ResetBall(Ball);
    }
    
    // 공 감지 후 5초 뒤
    IEnumerator hitpin(GameObject ball)
    {
        while (true)
        {
            if (ball.transform.position.z >= 20)
            {
                Debug.Log(ball.transform.position.z);
                print("공 감지");
                yield return new WaitForSeconds(5f);
                break;
            }
            yield return null;
        }
    }

    void calc()
    {
        print("계산 완료!!");
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


}
