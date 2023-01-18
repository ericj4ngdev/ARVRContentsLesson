using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    
    [Range(0,1)]public float intensity;
    [Range(0,1)]public float duration;

    public void TriggerHaptic_(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs.interactableObject is XRBaseControllerInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor.xrController);
        }
    }
    
    public void TriggerHaptic(XRBaseController controller)
    {
        if (intensity > 0)
            controller.SendHapticImpulse(intensity, duration);
    }
}

public class HapticInteractable : MonoBehaviour
{
    // 각 상호작용에 따라 haptic효과를 다르게 부여할 수 있다. 
    public Haptic hapticOnActivated;
    public Haptic hapticHoverEntered;
    public Haptic hapticHoverExited;
    public Haptic hapticSelectEntered;
    public Haptic hapticSelectExited;
    
    // Start is called before the first frame update
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.activated.AddListener(hapticOnActivated.TriggerHaptic_);
        interactable.hoverEntered.AddListener(hapticHoverEntered.TriggerHaptic_);
        interactable.hoverExited.AddListener(hapticHoverExited.TriggerHaptic_);
        interactable.selectEntered.AddListener(hapticSelectEntered.TriggerHaptic_);
        interactable.selectExited.AddListener(hapticSelectExited.TriggerHaptic_);
    }

}
