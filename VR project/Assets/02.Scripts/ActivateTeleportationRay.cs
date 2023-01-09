using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel;

    // UI에 상호작용시 텔레포트 Ray와 인터렉션 Ray가 겹치는 문제
    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;

    void Update()
    {
        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumber, out bool leftValid);


        leftTeleportation.SetActive(!isLeftRayHovering && leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);

        bool isRightRayHovering = leftRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightValid);

        rightTeleportation.SetActive(!isRightRayHovering && rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);


    }
}
