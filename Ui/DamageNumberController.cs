using TMPro;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public float floatSpeed = 1f;
    public float duration = 1.5f;

    private float elapsedTime = 0f;
    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    public void SetDamageValue(float damage)
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component is not assigned in the DamageNumberController component.");
            return;
        }

        textMeshPro.text = damage.ToString();
    }

    private void Update()
    {
        if (textMeshPro == null)
        {
            Destroy(gameObject);
            return;
        }

        // Rotate towards player camera
        if (playerCamera != null)
        {
            FaceCamera();
        }

        // Float upwards
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Fade effect
        textMeshPro.alpha = Mathf.Clamp01(1f - (elapsedTime / duration));

        // Destroy the damage number after the duration
        if (elapsedTime >= duration)
        {
            Destroy(gameObject);
        }
    }

    private void FaceCamera()
    {
        Vector3 targetDirection = playerCamera.transform.position - transform.position;
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(-targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1);
    }
}
