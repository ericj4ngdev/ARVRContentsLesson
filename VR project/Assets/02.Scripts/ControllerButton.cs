using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerButton : MonoBehaviour
{
    public GameObject BallPrefab;
    public Transform spawnspot;
    public InputActionProperty PressX;
    public InputActionProperty PressY;
    public InputActionProperty PressA;
    public InputActionProperty PressB;
    public InputActionProperty Home;

    void SpawnBowlingBall()
    {
        GameObject Ball = Instantiate(BallPrefab);
        Ball.transform.position = spawnspot.position;
    }


    void Update()
    {
        if (PressX.action.WasPressedThisFrame())
        {
            Debug.Log("Pressed X");
        }

        if (PressY.action.WasPressedThisFrame())
        {
            Debug.Log("Pressed Y");
        }

        if (PressA.action.WasPressedThisFrame())
        {
            Debug.Log("Pressed A");
            SpawnBowlingBall();
        }

        if (PressB.action.WasPressedThisFrame())
        {
            Debug.Log("Pressed B");
            
        }

        if (Home.action.WasPressedThisFrame())
        {
            Debug.Log("Pressed Home");
        }


    }
}
