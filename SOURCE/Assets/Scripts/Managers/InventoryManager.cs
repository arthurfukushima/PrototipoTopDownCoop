using UnityEngine;
using System.Collections;

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>
{
    public delegate void OnCoinAmountChanged(int pTotal);
    public OnCoinAmountChanged onCoinAmountChangedCallback;

    public int coinsCount;

    public void Start()
    {
        
    }

    public void AddCoins(int pAmount)
    {
        coinsCount += pAmount;

        if(onCoinAmountChangedCallback != null)
            onCoinAmountChangedCallback(coinsCount);
    }
}
