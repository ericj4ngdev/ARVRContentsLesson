using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.XR.Interaction;
// using UnityEngine.XR.Interaction.Toolkit;

public class SetBall : MonoBehaviour
{
    // A ��ư �������� �� ��ȯ
    // Ball size�� Weight ����
    // �� ���� ����

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
