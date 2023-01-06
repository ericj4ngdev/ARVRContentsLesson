using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ballspeed : MonoBehaviour
{
    [Range(0, 10000)] public float mSpeed;
    [HideInInspector] public Transform tr;
    [HideInInspector] public Rigidbody rb;
    public Text mpersecond;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        mpersecond.text = string.Format("{0:00.00} m/s", rb.velocity.magnitude);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.forward * mSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
