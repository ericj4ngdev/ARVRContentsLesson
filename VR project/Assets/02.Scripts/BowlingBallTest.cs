using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBallTest : MonoBehaviour
{
    Transform tr;
    Rigidbody rd;
    public float speed;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rd = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rd.AddForce(Vector3.forward * speed);
    }

    void Update()
    {
        // tr.position = new Vector3(tr.position.x, 0, tr.position.z).normalized;
    }
}
