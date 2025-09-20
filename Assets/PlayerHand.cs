using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    
    private PickableItem heldItem;
    [SerializeField] private Transform ItemDropZone;
    public bool IsHoldingItem => heldItem != null;

    public void HoldItem(PickableItem item)
    {
        
        if (heldItem != null) return;
        heldItem = item;
        item.rb.isKinematic = true;
        item.transform.SetParent(transform);
        item.transform.rotation = Quaternion.identity;
        item.transform.localPosition = Vector3.zero;
        
    }

    public void DropItem()
    {
        heldItem.transform.SetParent(null);
        heldItem.transform.position = ItemDropZone.transform.position;
        heldItem.rb.isKinematic = false;
        Debug.Log("Drop Item");
        heldItem = null;
        
    }
}
