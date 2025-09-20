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
    

    private void Awake()
    {
        fpc = GetComponent<FirstPersonController>();
        playerInputSwitcher = GetComponent<PlayerInputSwitcher>();
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

    public void SetState(PlayerState newState)
    {
        this.state = newState;

        switch (state)
        {
            case PlayerState.Exploring:
                fpc.enabled = true;
                playerInputSwitcher.ActivatePlayerInput();
                break;
            case PlayerState.InMenu:
                fpc.enabled = false;
                playerInputSwitcher.ActivateUIInput();
                break;
            case PlayerState.InTerminal:
                fpc.enabled = false;
                playerInputSwitcher.ActivateTerminalInput();
                break;
        }
    }
    
}
