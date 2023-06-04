using UnityEngine;

public class GrabableController : MonoBehaviour, IIsGrabable
{
    public bool IsGrabbed { get; set; }
    public Transform Grabber { get; set; }

    public bool canBeGrabbedByAtivaton;
    public void Grab(Transform grabber)
    {
        IsGrabbed = true;
        Grabber = grabber;
        transform.SetParent(Grabber);
        transform.localPosition = Vector3.zero; // you may adjust this as needed
    }


    public void Release()
    {
        IsGrabbed = false;
        transform.SetParent(null);
        Grabber = null;
    }
}