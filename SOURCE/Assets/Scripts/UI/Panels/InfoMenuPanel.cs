using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoMenuPanel : SingletonMonoBehaviour<InfoMenuPanel>
{
    public Text coinCountText;

    public void Start()
    {
        InventoryManager.Instance.onCoinAmountChangedCallback += OnCoinsAmountChanged;
    }

    private void OnCoinsAmountChanged(int pTotal)
    {
        SetCointext(pTotal.ToString("0"));
    }

    public void SetCointext(string pCointAmount)
    {
        coinCountText.text = pCointAmount;
    }
}
