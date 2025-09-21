using UnityEngine;

namespace TerminalSequence
{
    [RequireComponent(typeof(Collider))]
    public class Terminal : MonoBehaviour, Item
    {
        private ConsoleUI consoleUI;
        private AudioSource audioSource;

        private void Awake()
        {
            consoleUI = GetComponentInChildren<ConsoleUI>();
            audioSource = GetComponent<AudioSource>();
        }

        public void OnInteract(Player player)
        {
            player.SetState(PlayerState.InTerminal);
            consoleUI.StartUp();
            audioSource.Play();
        }

        public void OnLookAt()
        {
        
        }

        public string GetDisplayName()
        {
            return "[E] Use Terminal";
        }
    }
}
