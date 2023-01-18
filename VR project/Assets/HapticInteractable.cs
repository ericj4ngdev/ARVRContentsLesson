using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    [Range(0, 1)] public float intensity;
    [Range(0, 1)] public float duration;

    public void SendHaptic(BaseInteractionEventArgs eventArgs)
    {
        // 상호작용 가능하면 haptic효과 함수 호출
        if (eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
        {
            SendHaptic(controllerInteractor.xrController);
        }
    }

    public void SendHaptic(XRBaseController controller)
    {
        if (intensity > 0)
        {
            controller.SendHapticImpulse(intensity, duration);
            Debug.Log("TriggerHaptic. 발사 & 진동");
        }
    }
}

public class HapticInteractable : MonoBehaviour
{
    // 각 상호작용에 따라 haptic효과를 다르게 부여할 수 있다. 
    public Haptic hapticOnActivated;            // 총쏘기
    public Haptic hapticHoverEntered;           // 닿았을 때 반응
    public Haptic hapticHoverExited;
    public Haptic hapticSelectEntered;          // 잡았을 때
    public Haptic hapticSelectExited;

    // Start is called before the first frame update
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.activated.AddListener(hapticOnActivated.SendHaptic);
        interactable.hoverEntered.AddListener(hapticHoverEntered.SendHaptic);
        interactable.hoverExited.AddListener(hapticHoverExited.SendHaptic);
        interactable.selectEntered.AddListener(hapticSelectEntered.SendHaptic);
        interactable.selectExited.AddListener(hapticSelectExited.SendHaptic);
    }
}