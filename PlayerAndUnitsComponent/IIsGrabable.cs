using UnityEngine;
public interface IIsGrabable
{
    bool IsGrabbed { get; set; }
    Transform Grabber { get; set; }
    void Grab(Transform grabber);
    void Release();
}