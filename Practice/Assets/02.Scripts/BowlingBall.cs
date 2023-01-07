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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mSpeed = 1000;
            rb.AddForce(shootVector * mSpeed);
            // BowlingBall.GetComponent<Rigidbody>().AddForce(Vector3.forward * mSpeed);
        }
    }
}
