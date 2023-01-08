using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlingBall : MonoBehaviour
{
    [Range(0, 10000)] public float mSpeed;
    [HideInInspector] public Transform tr;
    [HideInInspector] public Rigidbody rb;
    
    public Vector3 shootVector;

    public float speed = 1f;
    float hAxis;
    float vAxis;
    Vector3 moveVec;
    
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * speed  * Time.deltaTime;
    }
    
    private void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        shootVector = new Vector3(0, 0, 1);
        // Ballspeed.SpeedText();
    }
    void FixedUpdate()
    {
        float Speed = rb.velocity.magnitude;
        
        
    } 
    private void Update()
    {
        //GetInput();
        //Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mSpeed = 1000;
            rb.AddForce(shootVector * mSpeed);
            // BowlingBall.GetComponent<Rigidbody>().AddForce(Vector3.forward * mSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddForce(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddForce(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddForce(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddForce(0, 0, -1);
        }
        // 프른트 영역에 오면 정지시키기
    }
}
