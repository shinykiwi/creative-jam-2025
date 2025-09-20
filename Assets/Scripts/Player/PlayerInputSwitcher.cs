using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSwitcher : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    private InputActionMap playerActionMap;
    private InputActionMap uiActionMap;
    private InputActionMap terminalActionMap;

    void Awake()
    {
        playerActionMap = inputActions.FindActionMap("Player");
        uiActionMap = inputActions.FindActionMap("UI");
        terminalActionMap = inputActions.FindActionMap("Terminal");
    }

    void OnEnable()
    {
        playerActionMap.Enable();
        uiActionMap.Disable();
    }

    void OnDisable()
    {
        playerActionMap.Disable();
        uiActionMap.Disable();
    }

    public void ActivateUIInput()
    {
        playerActionMap.Disable();
        terminalActionMap.Disable();
        uiActionMap.Enable();
    }

    public void ActivatePlayerInput()
    {
        uiActionMap.Disable();
        terminalActionMap.Disable();
        playerActionMap.Enable();
    }

    public void ActivateTerminalInput()
    {
        playerActionMap.Disable();
        uiActionMap.Disable();
        terminalActionMap.Enable();
    }
}