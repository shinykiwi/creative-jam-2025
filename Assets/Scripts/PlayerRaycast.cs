using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycast : MonoBehaviour
{
    public GameObject LastItem { get; set; } = null;

    private PlayerHUD playerHUD;

    private void Awake()
    {
        playerHUD = transform.parent.GetComponentInChildren<PlayerHUD>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sending a raycast from the mouse position (which is already locked to middle of screen)
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        // If something was hit
        if (Physics.Raycast(ray, out RaycastHit hit, 4f))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.GetComponent<Item>() is { } item)
            {
                LastItem = hitObject;
                item.OnLookAt();
                playerHUD.ShowHelperText();
                playerHUD.HelperText = item.GetDisplayName();
            }
            else
            {
                LastItem = null;
                playerHUD.HideHelperText();
            }
        }
        else
        {
            LastItem = null;
            playerHUD.HideHelperText();
        }
    }
}
