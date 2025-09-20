using TMPro;
using UnityEngine;

public class ConsoleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;

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
    
    public void UnlockButton()
    {
        
    }

    public void ToUnlockedPage()
    {
        
    }

    public void ToTokensPage()
    {
        
    }
}
