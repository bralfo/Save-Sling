using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo da Câmera")]
    [SerializeField] private Transform target;

    [Header("Configurações do Movimento")]
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Sem parênteses em 'target.position'
        Vector3 desiredPosition = target.position + offset;

        // Vector3.Lerp precisa de (Vector3 inicio, Vector3 fim, float tempo)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}