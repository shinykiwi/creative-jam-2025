using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Exploring,
    InMenu,
    InTerminal
}
public class Player : MonoBehaviour
{
    private FirstPersonController fpc;
    private PlayerState state = PlayerState.Exploring;
    private PlayerInputSwitcher playerInputSwitcher;
    private PlayerHUD playerHUD;
    private PlayerRaycast playerRaycast;
    

    private void Awake()
    {
        fpc = GetComponent<FirstPersonController>();
        playerInputSwitcher = GetComponent<PlayerInputSwitcher>();
        playerHUD = GetComponentInChildren<PlayerHUD>();
        playerRaycast = GetComponentInChildren<PlayerRaycast>();
    }

    public void EnableMovement()
    {
        fpc.canMove = true;
    }

    public void EnableJump()
    {
        fpc.canJump = true;
    }

    public void EnableLook()
    {
        fpc.canLook = true;
    }
    
    public void OnInteract()
    {
        if (state != PlayerState.Exploring) return;
        if (!playerRaycast.LastItem) return;
        
        // If the player is looking at an object they can pick up
        playerRaycast.LastItem.GetComponent<Item>().OnInteract();
        playerRaycast.LastItem = null;
    }

    public void SetState(PlayerState newState)
    {
        state = newState;

        switch (state)
        {
            case PlayerState.Exploring:
                fpc.enabled = true;
                playerInputSwitcher.ActivatePlayerInput();
                playerHUD.gameObject.SetActive(true);
                break;
            case PlayerState.InMenu:
                fpc.enabled = false;
                playerInputSwitcher.ActivateUIInput();
                playerHUD.gameObject.SetActive(false);
                break;
            case PlayerState.InTerminal:
                fpc.enabled = false;
                playerInputSwitcher.ActivateTerminalInput();
                playerHUD.gameObject.SetActive(false);
                break;
        }
    }
    
}
