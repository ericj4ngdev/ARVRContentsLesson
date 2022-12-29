using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingManager : MonoBehaviour
{
    public Transform[] pin;
    public GameObject pinprefab;

    private void Awake()
    {
        pin = new Transform[transform.childCount];
        for (int i = 0; i < pin.Length; i++)
            pin[i] = transform.GetChild(i);
    }
    void SetBallPosition()
    {
        for( int i = 0; i<pin.Length; i++)
        {
            GameObject pinobj = Instantiate(pinprefab);
            pinobj.transform.position = pin[i].position;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        SetBallPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
