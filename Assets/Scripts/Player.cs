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
    private CameraController cameraController;
    [SerializeField] private PlayerHand playerHand;
    
    public PlayerHand PlayerHand => playerHand;
    
    public delegate void OnPause();
    public static OnPause onPause;


    private void Awake()
    {
        fpc = GetComponent<FirstPersonController>();
        playerInputSwitcher = GetComponent<PlayerInputSwitcher>();
        playerHUD = GetComponentInChildren<PlayerHUD>();
        playerRaycast = GetComponentInChildren<PlayerRaycast>();
        cameraController = GetComponentInChildren<CameraController>();
        PauseMenu.onResume += ResumeGame;
    }

    private void OnDestroy()
    {
        PauseMenu.onResume -= ResumeGame;
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

    private void ResumeGame()
    {
        SetState(PlayerState.Exploring);
    }
    
    public void OnInteract()
    {
        if (state != PlayerState.Exploring) return;
        Debug.Log("Interact");
        if (playerHand.IsHoldingItem) playerHand.DropItem();
        if (!playerRaycast.LastItem) return;
        
        // If the player is looking at an object they can pick up
        playerRaycast.LastItem.GetComponent<Item>().OnInteract(this);
        playerRaycast.LastItem = null;
        playerHUD.HideHelperText();
    }

    public void OnExit()
    {
        switch (state)
        {
            case PlayerState.Exploring:
                onPause?.Invoke();
                SetState(PlayerState.InMenu);
                break;
            case PlayerState.InMenu:
                // Exit the current menu
                break;
            case PlayerState.InTerminal:
                // Exit the terminal
                cameraController.DisableTerminalCamera();
                SetState(PlayerState.Exploring);
                break;
        }
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
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                break;
            case PlayerState.InMenu:
                fpc.enabled = false;
                playerInputSwitcher.ActivateUIInput();
                playerHUD.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                break;
            case PlayerState.InTerminal:
                fpc.enabled = false;
                playerInputSwitcher.ActivateTerminalInput();
                playerHUD.gameObject.SetActive(false);
                cameraController.EnableTerminalCamera();
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 1;
                break;
        }
    }
    
}
