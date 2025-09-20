using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour, Item
{
    public Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    public void OnInteract(Player player)
    {
        
        player.PlayerHand.HoldItem(this);
    }

    public void OnLookAt()
    {
    
    }

    public string GetDisplayName()
    {
        return "[E] Pick up";
    }
}
