using UnityEngine;

public class safeZone : MonoBehaviour
{
    [SerializeField] private StoryMessageUI storyMessageUI;
    [SerializeField] private Wallet wallet;

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        SalvageItem item = other.GetComponent<SalvageItem>();
        if (item == null)
            return;

        
        if (wallet != null)
        {
            wallet.AddGold(item.Value);
            Debug.Log($"Item salvo! Valor de {item.Value} adicionado à carteira.");
        }
        else
        {
            return;
        }

        
        if (storyMessageUI != null && !string.IsNullOrEmpty(item.storyMessage))
        {
            storyMessageUI.ShowMessage(item.storyMessage);
        }
        else if (storyMessageUI == null)
        {
            return;

        }

       
        Destroy(other.gameObject);
    }
}


