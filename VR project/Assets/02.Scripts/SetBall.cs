using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class SetBall : MonoBehaviour
{
    // A 버튼 눌렀을때 공 소환
    // Ball size와 Weight 조절
    // 공 색깔 선택

    public GameObject BallPrefab;
    public Transform spawnspot;

    private void Awake()
    {
        GameObject Ball = Instantiate(BallPrefab);
        Ball.transform.position = spawnspot.position;
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
