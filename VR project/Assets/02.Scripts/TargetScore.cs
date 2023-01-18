using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetScore : MonoBehaviour
{
    private int yellow_target = 5;
    private int red_target = 4;
    private int blue_target = 1;
    // public ScoreSingleton Score_;

    private void Start()
    {
        // red_target = red_target - blue_target;
        // yellow_target = yellow_target - (red_target + blue_target);
        // Debug.Log("총 점수 : " + ScoreSingleton.Instance.totalScore);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            switch (this.transform.name)
            {
                case "Score_1":
                    Debug.Log("5점 획득");
                    ScoreSingleton.Instance.totalScore += yellow_target;
                    Destroy(other);
                    break;
                case "Score_2":
                    Debug.Log("3점 획득");
                    ScoreSingleton.Instance.totalScore += red_target;
                    Destroy(other);
                    break;
                case "Score_3":
                    Debug.Log("1점 획득");
                    ScoreSingleton.Instance.totalScore += blue_target;
                    Destroy(other);
                    break;
                default:
                    break;
            }
            Debug.Log("총 점수 : " + ScoreSingleton.Instance.totalScore);
        }
        ScoreSingleton.Instance.UpdateScore();
    }
}
