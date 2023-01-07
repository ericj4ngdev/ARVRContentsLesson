using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance;
    public Transform[] PinPosition;
    public GameObject[] BowlingPin;
    public GameObject BowlingPinprefab;
    
    public GameObject BallPrefab;
    public Transform spawnspot;
    // [HideInInspector] public Rigidbody rb;
    [HideInInspector] public GameObject BallObject;

    public static SpawnManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;       // private의 instance 반환
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
        
        // BallObject = Instantiate(BallPrefab,spawnspot);
        PinPosition = new Transform[transform.childCount];
        BowlingPin = new GameObject[PinPosition.Length];
        for (int i = 0; i < PinPosition.Length; i++)
        {
            PinPosition[i] = transform.GetChild(i);             // 위치 정보 등록
        }
    }
    
    void Start()
    {
        for (int i = 0; i < PinPosition.Length; i++)
        {
            BowlingPin[i] = Instantiate(BowlingPinprefab,PinPosition[i]);
            // Debug.Log(transform.GetChild(i).transform.position);
        }
    }

    public void SetPinPosition()
    {
        // 리셋 -> 이미 소환된 핀 오브젝트를 원래 위치로 옮기기. 소환이 아니라 기존거 원래자리로 돌려놓기
        // 기존 핀이 있다면 제거하고 재생성. 여기서 Destroy가 아닌 오브젝트 풀링을 사용해보려 한다. 
        
        for( int i = 0; i<PinPosition.Length; i++)
        {
            // Debug.Log(BowlingPin[i].transform.position);
            BowlingPin[i].transform.position = PinPosition[i].position+new Vector3(0,0.2f,0);
            BowlingPin[i].transform.rotation = PinPosition[i].rotation * Quaternion.Euler(90,0,0);
        }        
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void ResetBall(GameObject gameObject)
    {
        gameObject.transform.position = spawnspot.position;
        gameObject.transform.rotation = spawnspot.rotation;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            ResetBall(BallObject);
            // rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
