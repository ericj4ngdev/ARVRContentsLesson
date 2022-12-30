using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnBowlingBall : MonoBehaviour
{
    public GameObject BallPrefab;
    public Transform spawnspot;
    public InputActionProperty PressX;
    

    void Update()
    {
        if (PressX.action.WasPressedThisFrame())
        {
            Debug.Log("Press X");

            GameObject Ball = Instantiate(BallPrefab);
            Ball.transform.position = spawnspot.position;
        }    
    }
}
