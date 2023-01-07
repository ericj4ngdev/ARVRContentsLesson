using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingManager : MonoBehaviour
{
    [System.Serializable]           // 클래스 멤버들 serialize 
    public class ScoreInfo
    {
        public int FirstShoothit;
        public int SecondShoothit;
        public bool isSpare;
        public bool isStrike;
        public int FrameScore;
        public int CumulativeScore;
    }
    [SerializeField] private ScoreInfo[] scoreInfos;
    
    public GameObject Pin;
    private GameObject Ball;
    
    private int totalFrame = 10;
    private int totalShoots = 2;
    private int totalPins = 10;
    [SerializeField]private int _currentFrame = 0;
    [SerializeField]private int _currentShoot = 0;
    private float height = 0.2f;           // 쓰러짐 여부를 검사할 높이
    public Transform[] HittedPinPosition;
    
    [SerializeField]private int[] Score_1;        // 첫 시도 스코어 => FirstShoothit;
    [SerializeField]private int[] Score_2;        // 첫 시도 스코어 => SecondShoothit;
    [SerializeField]private int[] Fscore;       // 각 프레임의 총 점수 => FrameScore
    [SerializeField]private int[] Score;        // 누적 프레임 총 스코어 => CumulativeScore;
    [SerializeField]private bool[] isSpare;     
    [SerializeField]private bool[] isStrike;
    [SerializeField]private bool[] isHitted;
    void Awake()
    {
        height = Pin.transform.localScale.y;
        Ball = SpawnManager.Instance.BallObject;
        scoreInfos = new ScoreInfo[totalFrame];
        isHitted = new bool[10];
        
        // 동적 할당
        Score_1 = new int[scoreInfos.Length];
        Score_2 = new int[scoreInfos.Length];
        Fscore = new int[scoreInfos.Length];
        Score = new int[scoreInfos.Length];
        isSpare = new bool[scoreInfos.Length];
        isStrike = new bool[scoreInfos.Length];
        
        // 초기화
        /*for (int i = 0; i < frameScoreInfos.Length; i++)
        {
            Score_1[i] = frameScoreInfos[i].FirstShoothit;
            Score_2[i] = frameScoreInfos[i].SecondShoothit;
            Fscore[i] = frameScoreInfos[i].FrameScore;
            Score[i] = frameScoreInfos[i].CumulativeScore;
            isSpare[i] = frameScoreInfos[i].isSpare;
            isStrike[i] = frameScoreInfos[i].isStrike;
        }*/
    }

    /// <summary>
    /// 이전 프레임이 스페어인 경우, 현재 프레임 첫번째 샷 이후이전 프레임 점수 계산
    /// </summary>
    /// <param name="currentFrame">현재 프레임</param>
    /// <param name="shoot">던진 번째 수 - 1</param>
    void SpareScoreCheck(int currentFrame)
    {
        if (isSpare[currentFrame - 1])
            Fscore[currentFrame - 1] += Score_1[currentFrame];
    }
    /// <summary>
    /// 이전 프레임이 스트라이크인 경우, 현재 프레임   이후 이전 프레임 점수 계산
    /// </summary>
    /// <param name="currentFrame"></param>
    /// <param name="shoot"></param>
    /// if (isStrike[currentFrame - 1])
    // void StrikeScoreCheck(int currentFrame)
    // {
    //     if (isStrike[currentFrame - 1])
    //         Fscore[currentFrame - 1] += Score_1[currentFrame];
    // }
    /// <summary>
    /// 쓰러진 핀 개수 계산 및 치우기
    /// </summary>
    /// <param name="pinNum">넘어진 핀 번호</param>
    /// <returns>쓰러진 핀 개수( = 점수)</returns>
    void CleanupHittedPin(int pinNum)
    {
        // print("CleanupHittedPin");
        SpawnManager.Instance.BowlingPin[pinNum].transform.position =
            HittedPinPosition[pinNum].position + new Vector3(0, 0.5f, 0);
        SpawnManager.Instance.BowlingPin[pinNum].transform.rotation = HittedPinPosition[pinNum].rotation * Quaternion.Euler(90,0,0);;
    }


    void Update()
    {
        // 게임 시작 버튼 개념
        // 나중에 UI에 연결. 업데이트에서 안해도 될듯
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            print("게임 시작");
            StartCoroutine(FrameNum(_currentFrame));
        }   
    }

    void ResetHitInfo(bool[] isHit)
    {
        for (int k = 0; k < totalPins; k++)
        {
            isHit[k] = false;
        }
    }
    
    IEnumerator FrameNum(int frame)
    {
        while(true)
        {
            ResetHitInfo(isHitted);
            print("SetPinPosition() 호출");
            SpawnManager.Instance.SetPinPosition();
            print($"{frame+1}번째 프레임");
            for (_currentShoot = 0; _currentShoot < 2; _currentShoot++)
            {
                yield return StartCoroutine(Shoot(Ball, _currentShoot));
                CalcScore(frame, _currentShoot);            // 점수 계산
                // 스트가 아니면 넘어진 공만 치우기
                if (frame < 9)
                {
                    if (!isStrike[frame])
                    {
                        for (int k = 0; k < totalPins; k++)
                        {
                            if (isHitted[k]) CleanupHittedPin(k);
                        }
                    }
                    else
                        break;
                }

                if (frame == 10 && _currentShoot == 1 && isStrike[frame])
                {
                    // 핀 소환
                    yield return StartCoroutine(Shoot(Ball, 2));
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
    IEnumerator Shoot(GameObject Ball, int currentShoot)
    {
        print($"{currentShoot+1}번째 shoot");
        yield return StartCoroutine(hitpin(Ball));
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
    int PinScore()
    {
        int count = 0;
        for (int k = 0; k < 10; k++)
        {
            // 몇 번째 shoot에서 k번째 핀이 쓰러졌는지 여부
            if (SpawnManager.Instance.BowlingPin[k].transform.GetChild(0).transform.position.y < 0.2f)
            {
                isHitted[k] = true;
                count++;                        // 쓰러진 개수 = 점수
            }    
        }
        Debug.Log(count + "개 쓰러뜨림");
        return count;       // 점수로 연결
    }
    
    void CalcScore(int frame_num, int shoot_num)
    {
        print("계산 시작!!");
        if (shoot_num == 0)
        {
            if (PinScore() == 10)
            {
                isStrike[frame_num] = true;
                Score_1[frame_num] = PinScore();          // 여기 널 레퍼런스
                if (frame_num >= 1) // 이전 프레임 체크용. 인덱스 에러 방지
                {
                    SpareScoreCheck(frame_num);
                    if (isStrike[frame_num - 1]) // 2연속 스트라이크(더블)인 경우 점수계산
                    {
                        Fscore[frame_num - 1] += Score_1[frame_num];
                        if (frame_num >= 2)
                        {
                            if (isStrike[frame_num - 2]) // 3연속 스트라이크(터키)인 경우 점수계산
                                Fscore[frame_num - 2] += Score_1[frame_num];
                        }
                    }
                }
            }
            else
            {
                Score_1[frame_num] = PinScore();
                if (frame_num >= 1)
                {
                    SpareScoreCheck(frame_num);
                    // StrikeScoreCheck(frame_num);
                    if (isStrike[frame_num - 1])
                        Fscore[frame_num - 1] += Score_1[frame_num];
                }
            }
        }

        if (shoot_num == 1)
        {
            if (PinScore() == 10) // 스페어
            {
                isSpare[frame_num] = true;
            }
            Score_2[frame_num] = PinScore();
            // StrikeScoreCheck(frame_num);
            if (frame_num >= 1)
            {
                if (isStrike[frame_num - 1])
                    Fscore[frame_num - 1] += Score_2[frame_num];
            }


            // 마지막 프레임 두번째 슛이 스트라이크이거나 스페어인 경우 한번 더 던질 수 있다.
            if (frame_num == 9 && (isStrike[frame_num] || isSpare[frame_num]))
            {
                // Score_2[frame_num] = PinScore(shoot_num);
                // 나중에 개선하기
                SpawnManager.Instance.SetPinPosition();
                // ThrowBall();
            }
        }
        if (!isSpare[frame_num] && !isStrike[frame_num])
            Fscore[frame_num] = Score_1[frame_num] + Score_2[frame_num];
        else
            Fscore[frame_num] = 10;         // 스페어거나 스트라이크이면 10점먹고 들어간다. 
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
