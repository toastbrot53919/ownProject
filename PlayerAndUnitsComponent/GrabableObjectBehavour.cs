using UnityEngine;

public class GrabableObjectBehavour : MonoBehaviour, IIsGrabable, IInteractable
{
    public bool IsGrabbed { get; set; }
    public Transform Grabber { get; set; }
    public void Grab(Transform grabber)
    {
        IsGrabbed = true;
        Grabber = grabber;
        transform.SetParent(grabber.GetComponent<CanGrabController>().grabHoldPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public void Release()
    {
        IsGrabbed = false;
        Grabber = null;
        transform.SetParent(null);
    }
    public void Interact(Transform interacter)
    {
        if (IsGrabbed)
        {
            Release();
        }
        else
        {
            Grab(interacter);
        }
    }
}