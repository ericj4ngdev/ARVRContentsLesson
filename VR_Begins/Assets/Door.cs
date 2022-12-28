using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 targetP;

    // Start is called before the first frame update
    void Start()
    {
        targetP = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetP, Time.deltaTime);
    }
}
