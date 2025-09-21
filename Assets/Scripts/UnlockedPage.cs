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
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }

    public void GenerateText()
    {
        StartCoroutine(textRenderer.TypewriterEffect(ItemName, itemNameText));
        StartCoroutine(textRenderer.TypewriterEffect(ItemDescription, itemDescriptionText));
    }
    
    
    
}
