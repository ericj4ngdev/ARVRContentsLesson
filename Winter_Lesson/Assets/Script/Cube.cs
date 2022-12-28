using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public float MoveSpeed = 3.0f;
    public float JumpPower = 25f;
    private bool JumpAvailable = false;
    public float RotateSpeed = 90f;


    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "Plane") // 부딪힌게 Plane인 경우
        {
            // Debug.Log("바닥!");
            JumpAvailable = true;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKey("right"))
        {
            // transform.position += Vector3.right * MoveSpeed * Time.deltaTime;
            transform.RotateAround(transform.position, Vector3.up, RotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey("left"))
        {
            // transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
            transform.RotateAround(transform.position, Vector3.up, -RotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey("up"))
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("down"))
        {
            transform.position -= transform.forward * MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown("space") && JumpAvailable)
        {
            // transform.position += Vector3.up * MoveSpeed * Time.deltaTime;
            JumpAvailable = false;      // 점프 못하는 상황으로 
            GetComponent<Rigidbody>().AddForce(0, JumpPower, 0);
        }
    }
}
