using UnityEngine;

public class safeZone : MonoBehaviour
{
    [SerializeField] private StoryMessageUI storyMessageUI;
    [SerializeField] private Wallet wallet;

    public void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Verifica se o objeto que entrou é realmente um SalvageItem
        SalvageItem item = other.GetComponent<SalvageItem>();
        if (item == null)
            return;

        // 2. Notifica a Câmera para retornar o foco ao Estilingue
        CameraController cam = FindAnyObjectByType<CameraController>();
        if (cam != null)
        {
            cam.FocusOnEstilingue();
        }
        else
        {
            Debug.LogWarning("CameraController não encontrado na cena!");
        }

        // 3. Adiciona o valor do item à carteira (se atribuída)
        if (wallet != null)
        {
            wallet.AddGold(item.Value);
            Debug.Log($"Item salvo! Valor de {item.Value} adicionado à carteira.");
        }

        // 4. Exibe a mensagem de história no UI (se houver)
        if (storyMessageUI != null && !string.IsNullOrEmpty(item.storyMessage))
        {
            storyMessageUI.ShowMessage(item.storyMessage);
        }

        // 5. Destrói o item coletado
        Destroy(other.gameObject);
    }
}