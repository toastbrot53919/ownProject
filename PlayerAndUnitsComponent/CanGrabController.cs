using UnityEngine;

public class CanGrabController : MonoBehaviour
{
    public IIsGrabable currentGrabbedObject;

    public Transform grabHoldPoint;

    public void Grab(IIsGrabable grabable)
    {
        if (currentGrabbedObject != null)
        {
            currentGrabbedObject.Release();
        }
        currentGrabbedObject = grabable;
        currentGrabbedObject.Grab(grabHoldPoint);

    }
    public void Release()
    {
        if (currentGrabbedObject != null)
        {
            currentGrabbedObject.Release();
        }
        currentGrabbedObject = null;
    }
    
}