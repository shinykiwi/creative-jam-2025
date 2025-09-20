using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private CinemachineVirtualCamera terminalCamera;

    private void Start()
    {
        terminalCamera.enabled = false;
    }

    public void EnableTerminalCamera()
    {
        terminalCamera.enabled = true;
    }

    public void DisableTerminalCamera()
    {
        terminalCamera.enabled = false;
    }
}
