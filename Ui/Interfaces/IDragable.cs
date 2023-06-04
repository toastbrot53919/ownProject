using UnityEngine.EventSystems;
using UnityEngine;
public interface IDragable : IBeginDragHandler, IDragHandler, IEndDragHandler
{

    GameObject getDraggedObject();
}
