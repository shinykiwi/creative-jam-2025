using System;
using System.Collections.Generic;
using TerminalSequence;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ConsoleUI : MonoBehaviour
{
 
    [SerializeField] private TextMeshProUGUI tokenCountText;
    [SerializeField] private GameObject unlockedPage;
    [SerializeField] private GameObject tokensPage;
    [SerializeField] private GameObject idlePage;

    [SerializeField] private GameObject unlockedPagePrefab;

    private List<UnlockedPage> unlockedPages;
    private int currentPageID = 0;

    private AudioSource audioSource;

    public void OnUnlockButton()
    {
        ToUnlockedPage();
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        unlockedPages = new List<UnlockedPage>();
    }

    public void ToUnlockedPage()
    {
        unlockedPage.SetActive(true);
        tokensPage.SetActive(false);
        idlePage.SetActive(false);
        audioSource.Play();
        
        var items = ItemManager.Instance.GetItems();
        
        foreach (var item in items)
        {
            GameObject page = Instantiate(unlockedPagePrefab, transform);
            UnlockedPage uPage = page.GetComponent<UnlockedPage>();
            uPage.SetItemDescription(item.description);
            uPage.SetItemName(item.itemName);
            page.SetActive(false);
            unlockedPages.Add(uPage);
        }

        currentPageID = 0;
        unlockedPages[currentPageID].gameObject.SetActive(true);
        unlockedPages[currentPageID].GenerateText();
        
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
