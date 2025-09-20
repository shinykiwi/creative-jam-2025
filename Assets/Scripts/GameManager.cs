using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void OnAdvanceDialogueEvent();
    public static OnAdvanceDialogueEvent onAdvanceDialogue;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onAdvanceDialogue?.Invoke();
        }
    }
}
