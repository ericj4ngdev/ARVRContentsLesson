using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PinManager : MonoBehaviour
{
    private static PinManager instance;
    public Transform[] PinPosition;
    private GameObject[] BowlingPin;
    public GameObject BowlingPinprefab;

    public static PinManager Instance
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
            // 소환하면서 배열에 등록 및 배치
            BowlingPin[i] = Instantiate(BowlingPinprefab,PinPosition[i]);         // 소환, 
            // BowlingPin[i].transform.position = BowlingPinprefab.transform.position; // 배치, 둥록
            Debug.Log(BowlingPin[i].transform.position);
        }
    }

    private void Update()
    {
        Debug.Log(BowlingPin[0].transform.position);
        //for (int i = 0; i < PinPosition.Length; i++)
        //    BowlingPin[i].transform.position = PinPosition[i].position;     // 실시간으로 위치값 받기
    }

    public void SetPinPosition()
    {
        // 리셋 -> 이미 소환된 핀 오브젝트를 원래 위치로 옮기기. 소환이 아니라 기존거 원래자리로 돌려놓기
        // 기존 핀이 있다면 제거하고 재생성. 여기서 Destroy가 아닌 오브젝트 풀링을 사용해보려 한다. 
        Debug.Log("호출완료");
        for( int i = 0; i<PinPosition.Length; i++)
        {
            // Debug.Log(BowlingPin[i].transform.position);
            BowlingPin[i].transform.position = PinPosition[i].position+new Vector3(0,0.2f,0);
            BowlingPin[i].transform.rotation = PinPosition[i].rotation;
        }        
    }
    
}
