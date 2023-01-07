using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ArrayTest : MonoBehaviour
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
    
    [SerializeField] private bool[] isHitted;
    // [SerializeField] private bool[] isStrike;
    [SerializeField] private int _currentFrame = 0;
    [SerializeField] private int _currentShoot = 0;
    
    [SerializeField]private int[] Score_1;        // 첫 시도 스코어 => FirstShoothit;
    [SerializeField]private int[] Score_2;        // 첫 시도 스코어 => SecondShoothit;
    [SerializeField]private int[] Fscore;       // 각 프레임의 총 점수 => FrameScore
    [SerializeField]private int[] Score;        // 누적 프레임 총 스코어 => CumulativeScore;
    [SerializeField]private bool[] isSpare;     
    [SerializeField]private bool[] isStrike;
    
    
    public GameObject Pin;
    public GameObject Ball;
    
    private int frame;
    

    void Awake()
    {
        // Ball = SpawnManager.Instance.BallObject;
        isHitted = new bool[10];
        isStrike = new bool[10];
        
        scoreInfos = new ScoreInfo[10];
        // 동적 할당
        Score_1 = new int[scoreInfos.Length];
        Score_2 = new int[scoreInfos.Length];
        Fscore = new int[scoreInfos.Length];
        Score = new int[scoreInfos.Length];
        isSpare = new bool[scoreInfos.Length];
        isStrike = new bool[scoreInfos.Length];
    }

    void Update()
    {
        // 게임 시작 버튼 개념
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            print("게임 시작");
            StartCoroutine(FrameNum(_currentFrame));
        }
    }

    IEnumerator FrameNum(int frame)
    {
        // 핀 소환
        while (true)
        {
            print($"{frame+1}번째 프레임");
            for (_currentShoot = 0; _currentShoot < 2; _currentShoot++)
            {
                yield return StartCoroutine(Shoot(Ball));
                CalcScore(frame, _currentShoot);
            }
            frame++;
            if (frame > 10)
                break;
        }

        print("종료");
        // 점수 공개
    }

    IEnumerator Shoot(GameObject ball)
    {
        // SpawnManager.Instance.BowlingPin[0].transform.GetChild(0).transform.position.y < 
        print($"{_currentShoot + 1}번째 shoot");
        yield return StartCoroutine(hitpin(ball));
        // 공 재소환
        // yield return new WaitForSeconds(2f);
    }
    IEnumerator hitpin(GameObject ball)
    {
        while (true)
        {
            if (ball.transform.position.z >= 20)
            {
                //Debug.Log(ball.transform.position.z);
                print("공 감지");
                yield return new WaitForSeconds(5f);
                break;
            }
            yield return null;
        }
    }
    int PinScore(int shootNum)
    {
        // print($"{shootNum}");
        int count = 0;
        for (int k = 0; k < 10; k++)
        {
            // Debug.Log(SpawnManager.Instance.BowlingPin[k].transform.GetChild(0).transform.position.y);
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
        if (PinScore(shoot_num) == 10)
        {
            print("스트라이크!!!");
            Score_1[frame_num] = 10;
            isStrike[frame_num] = true;
        }
    }
}
    