using System;
using TerminalSequence;
using TMPro;
using UnityEngine;

public class ConsoleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI tokenCountText;
    [SerializeField] private GameObject unlockedPage;
    [SerializeField] private GameObject tokensPage;

    private string itemName;
    private string itemDescription;
    public string ItemName
    {
        get => itemName;

        set
        {
            itemName = value;
            itemNameText.text = value;
        }
    }

    public string ItemDescription
    {
        get => itemDescription;

        set
        {
            itemDescription = value;
            itemDescriptionText.text = value;
        }
    }

    public void OnUnlockButton()
    {
        
    }

    public void ToUnlockedPage()
    {
        unlockedPage.SetActive(true);
        tokensPage.SetActive(false);
    }

    public void ToTokensPage()
    {
        unlockedPage.SetActive(false);
        tokensPage.SetActive(true);
        tokenCountText.text = ItemManager.Instance.GetItemCount().ToString();
        
    }

    public void StartUp()
    {
        ToTokensPage();
    }
    
}
