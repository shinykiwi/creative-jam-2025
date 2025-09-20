using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemCountText;
    
    private float itemCount;
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
}
