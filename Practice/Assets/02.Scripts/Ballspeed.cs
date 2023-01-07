using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ballspeed : MonoBehaviour
{
    // public static Action SpeedText;
    // public BowlingBall ball;
    public Text m_MeterPerSecond;
    public Text m_KilometersPerHour;
    private float Speed;

    private void Awake()
    {
        // Speed = ball.GetComponent<Rigidbody>().velocity.magnitude;
        // SpeedText = () => { FixedUpdate(); };
    }

    void FixedUpdate()
    {
        // m_MeterPerSecond.text = string.Format("{0:00.00} m/s", Speed); 
        // m_KilometersPerHour.text = string.Format("{0:00.00} km/h", Speed * 3.6f); 
    }

    private void Update()
    {
    }
}
