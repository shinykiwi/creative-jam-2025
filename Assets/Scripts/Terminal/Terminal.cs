using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Terminal : MonoBehaviour, Item
{
    public void OnInteract(Player player)
    {
        player.SetState(PlayerState.InTerminal);
    }

    public void OnLookAt()
    {
        
    }

    public string GetDisplayName()
    {
        return "[E] Use Terminal";
    }
}
