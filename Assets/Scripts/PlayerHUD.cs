using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemCountText;
    [SerializeField] TextMeshProUGUI helperTextObject;
    private string helperText;
    private float itemCount;

    public string HelperText
    {
        get => helperText;
        set 
        {
            helperText = value;
            SetHelperText(helperText);
        }
    }
    public float ItemCount
    {
        get => itemCount;

        set
        {
            itemCount = value;
            SetItemCountText(value);
        }
    }

    private void SetItemCountText(float count)
    {
        itemCountText.text = "x" + count;
    }

    private void SetHelperText(string text)
    {
        helperTextObject.text = text;
    }
}
