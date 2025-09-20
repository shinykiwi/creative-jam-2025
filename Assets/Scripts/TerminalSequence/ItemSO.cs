using UnityEngine;

namespace TerminalSequence
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        private static int internalID = 0;
        public string description = "This is a description.";
        public string itemName = "UnlockedItemName";
        public int id = internalID++;
    }
}
