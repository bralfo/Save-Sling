using UnityEngine;

public class ItemOutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessarItem(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessarItem(collision.collider);
    }

    private void ProcessarItem(Collider2D collider)
    {
        SalvageItem item = collider.GetComponent<SalvageItem>();

        // Se o objeto que encostou for um SalvageItem
        if (item != null)
        {
            // 1. Avisa a Câmera para voltar o foco para o Estilingue
            CameraController cam = FindAnyObjectByType<CameraController>();
            if (cam != null)
            {
                cam.FocusOnEstilingue();
            }

            Debug.Log("Item errou a SafeZone e caiu no chão/fora de alcance!");

            // 2. Destrói o item (sem adicionar ouro à carteira)
            Destroy(collider.gameObject);
        }
    }
}
