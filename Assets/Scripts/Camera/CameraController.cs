using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Alvos")]
    [SerializeField] private Transform estilingueTarget; // Arraste o GameObject do Estilingue no Inspector
    private Transform currentTarget;

    [Header("Configurações")]
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

    private void Start()
    {
        // Começa focada no estilingue
        FocusOnEstilingue();
    }

    private void LateUpdate()
    {
        if (currentTarget == null) return;

        Vector3 desiredPosition = currentTarget.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    public void FollowItem(Transform itemTransform)
    {
        currentTarget = itemTransform;
    }

    public void FocusOnEstilingue()
    {
        currentTarget = estilingueTarget;
    }
}