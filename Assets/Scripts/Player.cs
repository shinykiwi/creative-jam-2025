using System;
using StarterAssets;
using UnityEngine;

public class Player : MonoBehaviour
{
    private FirstPersonController fpc;

    private void Awake()
    {
        fpc = GetComponentInChildren<FirstPersonController>();
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
}
