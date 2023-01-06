using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private bool isDelay;
    public float delayTime = 2f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDelay)
            {
                isDelay = true;
                Debug.Log("Attack");
                StartCoroutine(CountAttackDelay());
            }
            else
            {
                Debug.Log("Delay");
            }
        }
    }

    IEnumerator CountAttackDelay()
    {
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }
}
