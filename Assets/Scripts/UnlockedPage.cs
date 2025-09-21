using System;
using IntroSequence;
using TMPro;
using UnityEngine;

public class UnlockedPage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    private TextRenderer textRenderer;

    private void Awake()
    {
        textRenderer = GetComponent<TextRenderer>();
    }

    private string itemName;
    private string itemDescription;

    public void SetItemName(string text)
    {
        itemName = text;
    }

    public void SetItemDescription(string text)
    {
        itemDescription = text;
    }
    
    public void GenerateText()
    {
        // StartCoroutine(textRenderer.TypewriterEffect(itemName, itemNameText));
        // StartCoroutine(textRenderer.TypewriterEffect(itemDescription, itemDescriptionText));
    }
}
