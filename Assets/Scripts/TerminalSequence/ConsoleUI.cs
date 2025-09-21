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

    private List<GameObject> unlockedPageObjects;
    private int currentPageID = 0;

    private AudioSource audioSource;

    public void OnUnlockButton()
    {
        ToUnlockedPage();
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        unlockedPageObjects = new List<GameObject>();
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
            uPage.ItemDescription = item.description;
            uPage.ItemName = item.itemName;
            page.SetActive(false);
            unlockedPageObjects.Add(page);
        }

        currentPageID = 0;
        unlockedPageObjects[currentPageID].SetActive(true);
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
