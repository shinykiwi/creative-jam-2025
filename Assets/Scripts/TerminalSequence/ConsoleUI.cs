using System;
using TerminalSequence;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ConsoleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI tokenCountText;
    [SerializeField] private GameObject unlockedPage;
    [SerializeField] private GameObject tokensPage;
    [SerializeField] private GameObject idlePage;

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
        ToUnlockedPage();
    }

    public void ToUnlockedPage()
    {
        unlockedPage.SetActive(true);
        tokensPage.SetActive(false);
        idlePage.SetActive(false);

        // var items = ItemManager.Instance.GetItems();
        //
        // foreach (var item in items)
        // {
        //     
        // }
    }

    public void ToTokensPage()
    {
        unlockedPage.SetActive(false);
        idlePage.SetActive(false);
        tokensPage.SetActive(true);
        tokenCountText.text = ItemManager.Instance.GetItemCount().ToString();
    }

    public void ToStartUpPage()
    {
        unlockedPage.SetActive(false);
        idlePage.SetActive(true);
        tokensPage.SetActive(false);
    }

    public void StartUp()
    {
        ToTokensPage();
    }
    
}
