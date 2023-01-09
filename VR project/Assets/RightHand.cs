using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RightHand : MonoBehaviour
{
    private Vector3 m_LastPosition; 
    public float m_Speed;
    public float m_Acceleration;
    public Vector3 m_Velocity;
    public Text m_MeterPerSecond, m_KilometersPerHour; 

    float GetSpeed()
    {
        // transform.position = 나중 위치
        // m_LastPosition 초기 위치 
        float speed = (((transform.position - m_LastPosition).magnitude) / Time.deltaTime);
        
        // 초기에 나중을 넣는다. 
        m_LastPosition = transform.position;

        return speed;
    }

    Vector3 GetVelocity()
    {
        Vector3 Velocity = ((transform.position - m_LastPosition) / Time.deltaTime);
        m_LastPosition = transform.position;
        return Velocity;
    }

    float GetAcceleration()
    {
        float speed = (((transform.position - m_LastPosition).magnitude) / Time.deltaTime);
        Vector3 Velocity = ((transform.position - m_LastPosition) / Time.deltaTime);
        m_LastPosition = transform.position;
        float Acc = (speed - speed);
        // float Acceleration = (m_Velocity)
        return Acc;
    }

    private void FixedUpdate()
    {
        m_Speed = GetSpeed();
        m_Velocity = GetVelocity();
        
        // m_MeterPerSecond.text = string.Format("{0:00.00} m/s", m_Speed); 
        // m_KilometersPerHour.text = string.Format("{0:00.00} km/h", m_Speed * 3.6f); 
    }
}
