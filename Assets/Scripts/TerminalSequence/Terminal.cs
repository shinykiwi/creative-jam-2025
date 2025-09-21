using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Terminal : MonoBehaviour, Item
{
    private ConsoleUI consoleUI;

    private void Awake()
    {
        consoleUI = GetComponentInChildren<ConsoleUI>();
    }

    public void OnInteract(Player player)
    {
        player.SetState(PlayerState.InTerminal);
        consoleUI.StartUp();
        
    }

    public void OnLookAt()
    {
        
    }

    public string GetDisplayName()
    {
        return "[E] Use Terminal";
    }
}
