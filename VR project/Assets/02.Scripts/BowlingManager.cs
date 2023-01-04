using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingManager : MonoBehaviour
{
    public GameObject BallPrefab;
    public Transform spawnspot;
    private GameObject BowlingBall;

    private void Awake()
    {
        BowlingBall = Instantiate(BallPrefab,spawnspot);
    }

    public void ResetBall()
    {
        BowlingBall.transform.position = spawnspot.position;
        BowlingBall.transform.rotation = spawnspot.rotation;
    }
}
