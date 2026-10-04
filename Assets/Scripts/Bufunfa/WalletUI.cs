using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    [SerializeField] private Wallet wallet;
    [SerializeField] private TMP_Text goldText;

    
    private void OnEnable()
    {
        if (wallet != null)
        {
            
            wallet.OnGoldChanged += UpdateGoldUI;
           
            UpdateGoldUI(wallet.CurrentGold);
        }
    }

    private void OnDisable()
    {
        if (wallet != null)
        {
            wallet.OnGoldChanged -= UpdateGoldUI;
        }
    }

    private void UpdateGoldUI(int totalGold)
    {
        if (goldText != null)
        {
            goldText.text = $"Ouro: {totalGold}";
        }
    }
}
