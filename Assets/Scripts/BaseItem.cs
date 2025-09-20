using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BaseItem : MonoBehaviour, Item
{
    public void OnInteract(Player player)
    {
        
    }

    public void OnLookAt()
    {
        
    }

    public string GetDisplayName()
    {
        return "[E] Pick up";
    }
}
