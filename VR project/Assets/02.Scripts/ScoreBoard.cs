using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private int [][] Frame;
    private int[] Fscore;
    private bool[] isSpare;
    private bool[] isStrike;
    private bool[][] hit;
    private int StrikeBonusCount = 2;

    private int PinScore(int i)
    {
        int count = 0;
        for (int k = 0; k < 10; k++)
        {
            if (hit[i][k] == true)
                count++;
        }
        return count;
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
    
    private void Update()
    {
        for(int i = 0; i < 10; i++)
        {
            // 핀 리셋
            
            for (int j = 0; j < 2; j++)
            {
                // 볼링공 소환 및 넘어진거 체크하는 알고리즘 넣기
                // 핀 바닥에 Line을 넣고 Line이 벽과 만나면 hit[] = true
                // Line이 바닥과 만나고 있으면 서있는 것으로 hit[] = false
                
                // 첫번째 시도
                if (j == 0)
                {
                    if (PinScore(i) == 10)
                    {
                        isStrike[i] = true;
                        Frame[i][j] = PinScore(i);
                        Frame[i][j + 1] = 0;
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
                        Frame[i][j] = PinScore(i);
                        if (i >= 1)
                        {
                            SpareScoreCheck(i, j);
                            StrikeScoreCheck(i, j);
                        }
                    }
                }
                
                // 두번째 시도
                if (j == 1)
                {
                    if (PinScore(i) == 10)
                    {
                        isStrike[i] = true;
                        Frame[i][j] = PinScore(i)-Frame[i][j-1];
                        StrikeScoreCheck(i, j);
                    }
                    else
                    {
                        Frame[i][j] = PinScore(i);
                        StrikeScoreCheck(i, j);
                    }
                    if(i==9 && isStrike[i])
                        Frame[i][j] += PinScore(i);
                    else
                        Frame[i][j] += PinScore(i);
                }
            }

            if (!isSpare[i] && !isStrike[i])
                Fscore[i] = Frame[i][0] + Frame[i][1];
            else
                Fscore[i] = 10;         // 스페어거나 스트라이크이면 10점먹고 들어간다. 
        }
    }

    
    
    
    
    
    
}
