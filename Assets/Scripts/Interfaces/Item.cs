using UnityEngine;

public interface Item
{
    void OnInteract(Player player);

    void OnLookAt();
    
    string GetDisplayName();
}
