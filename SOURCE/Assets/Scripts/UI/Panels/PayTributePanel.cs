using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PayTributePanel : MonoBehaviour 
{
    public Text tributesStatus;

    public int tributesNeeded = 10;
    public int tributesCollected = 0;

    public void Show()
    {
        gameObject.SetActive(true);
        UpdateTributesText();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void PayTribute(int pAmount)
    {
        tributesCollected += pAmount;

        UpdateTributesText();
    }

    public void UpdateTributesText()
    {
        tributesStatus.text = string.Format("{0} / {1}", tributesCollected, tributesNeeded);
    }
}
