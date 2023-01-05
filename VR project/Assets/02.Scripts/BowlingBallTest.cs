using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlingBallTest : MonoBehaviour
{
    Transform tr;
    Rigidbody rd;
    public float speed;
    public float mSpeed;
    public Vector3 m_LastPosition;         // 초기 속도(=벡터)
    public Text m_MeterPerSecond, m_KilometersPerHour;

    float GetSpeed() 
    { 
        float speed = (((transform.position - m_LastPosition).magnitude) / Time.deltaTime); 
        //Debug.Log("초기 위치 : " + m_LastPosition.magnitude);                // 초기
        //Debug.Log("나중 위치 : " + transform.position.magnitude);            // 나중
        m_LastPosition = transform.position; 
   
        return speed; 
    } 
    
    private void Awake()
    {
        tr = GetComponent<Transform>();
        rd = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rd.AddForce(Vector3.forward * mSpeed);
    }
    void FixedUpdate()
    {
        // speed = rd.velocity.magnitude;
        speed = GetSpeed();
        // Debug.Log("속도 : " + speed);
        
        m_MeterPerSecond.text = string.Format("{0:00.00} m/s", speed); 
        m_KilometersPerHour.text = string.Format("{0:00.00} km/h", speed * 3.6f); 
    } 
    
    void Update()
    {
        // tr.position = new Vector3(tr.position.x, 0, tr.position.z).normalized;
    }
}
