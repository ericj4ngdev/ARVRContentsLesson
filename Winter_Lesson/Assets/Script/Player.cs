using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform tr;
    public GameObject bullet;
    public Vector3 shootingpoint;

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    void Start()
    {        
        
    }

    // Update is called once per frame
    void Update()
    {
        // 발사
        Instantiate(bullet);
        // 발사 위치?
        GetComponentInChildren


    }
}
