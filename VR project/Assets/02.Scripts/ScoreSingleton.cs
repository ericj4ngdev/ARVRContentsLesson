using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSingleton : MonoBehaviour
{
    private static ScoreSingleton _instance;
    public int totalScore;
    public Text Score;
    
    public static ScoreSingleton Instance
    {
        get
        {
            if (null == _instance)
            {
                return null;
            }
            return _instance;       // private의 instance 반환
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void UpdateScore()
    {
        Score.text = totalScore.ToString();
    }
}
