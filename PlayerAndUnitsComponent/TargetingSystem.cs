using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public Camera playerCamera;
    public LayerMask targetLayerMask;
    public GameObject currentTarget;
    public GameObject crosshair;
    public float maxTargetingDistance = 100f;
    public Material highlightMaterial;
    private GameObject lastTarget;
    private Material originalMaterial;
    public OutlineHighlight outlineHighlightController;

    private void Update()
    {
        HandleCrosshairTargeting();
        HandleMouseClickTargeting();
        HighlightTarget();
    }
    private void Start()
    {

    }

    private void HandleCrosshairTargeting()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(crosshair.transform.position);

        if (Physics.Raycast(ray, out hit, maxTargetingDistance, targetLayerMask))
        {
            currentTarget = hit.collider.gameObject;
        }
        else
        {
            currentTarget = null;
        }
    }

    private void HandleMouseClickTargeting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, maxTargetingDistance, targetLayerMask))
            {
                currentTarget = hit.collider.gameObject;
            }
        }
    }

    public GameObject GetTarget()
    {
        return currentTarget;
    }
    private void HighlightTarget()
    {
        if (currentTarget != null)
        {
            outlineHighlightController.target = currentTarget.transform;
            lastTarget = currentTarget;
        }
        else
        {
            outlineHighlightController.target = null;
        }
    }

}
