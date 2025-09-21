using TerminalSequence;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BaseItem : MonoBehaviour, Item
{
    [SerializeField] private ItemSO itemData;
    public void OnInteract(Player player)
    {
        ItemManager.Instance.AddItem(itemData);
        
        // Hide the item but don't delete it yet cause i havent tested it
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    public void OnLookAt()
    {
        
    }

    public string GetDisplayName()
    {
        return "[E] Pick up";
    }
}
