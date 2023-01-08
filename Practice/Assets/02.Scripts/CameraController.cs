using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] Cameras;
    private int CamNo = 0;

    void FocusCamera(int No)
    {
        for(int i = 0; i < Cameras.Length; i++)
        {
            Cameras[i].SetActive(i == No);
        }
    }

    void ChangeCamera(int direct)
    {
        CamNo += direct;

        if(CamNo >= Cameras.Length)
        {
            CamNo = 0;
        }
        if (CamNo < 0)
        {
            CamNo = Cameras.Length - 1;
        }
        FocusCamera(CamNo);
    }

    // Start is called before the first frame update
    void Start()
    {
        FocusCamera(CamNo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))        // 좌클릭
        {
            ChangeCamera(1);        // CamNo가 1 증가
        }
        if (Input.GetMouseButtonDown(1))        // 우클릭
        {
            ChangeCamera(-1);        // CamNo가 1 감소
        }
    }
}
