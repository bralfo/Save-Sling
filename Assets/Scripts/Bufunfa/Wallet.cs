using UnityEngine;
using System;

public class Wallet : MonoBehaviour
{
    public event Action<int> OnGoldChanged;
    [SerializeField] private int currentGold = 0;

    private void Start()
    {
        currentGold = PlayerPrefs.GetInt("TotalGold", 0);
        OnGoldChanged?.Invoke(currentGold);
    }
    public int CurrentGold => currentGold;

    public void AddGold(int amount)
    {   
        if(amount <= 0)
        {
            return;
        }
        currentGold += amount;
        OnGoldChanged?.Invoke(currentGold);
    }
    
    public bool TrySpendGold(int amount)
    {
        if (amount <= 0) return false;
        
        if (currentGold >= amount)
        {
            currentGold -= amount;
            OnGoldChanged?.Invoke(currentGold);
            return false;
        }
        Debug.LogWarning("Ouro insuficiente para realizar a compra!");
        return false;
         
    }
    
}

    
