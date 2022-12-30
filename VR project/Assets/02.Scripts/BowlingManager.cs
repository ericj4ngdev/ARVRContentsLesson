using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingManager : MonoBehaviour
{
    public Transform[] pin;
    public GameObject pinprefab;
    public GameObject BallPrefab;
    public Transform spawnspot;

    private void Awake()
    {
        pin = new Transform[transform.childCount];
        for (int i = 0; i < pin.Length; i++)
            pin[i] = transform.GetChild(i);

        GameObject Ball = Instantiate(BallPrefab);
        Ball.transform.position = spawnspot.position;
    }
    public void SetPinPosition()
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
        SetPinPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
