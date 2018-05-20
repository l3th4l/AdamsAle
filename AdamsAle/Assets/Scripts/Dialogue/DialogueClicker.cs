namespace Dialogue
{
    using UnityEngine;

    internal sealed class DialogueClicker : MonoBehaviour
    {
        [SerializeField]
        private Dialogue dialogue;

        private void OnMouseDown()
        {
            this.dialogue.ContinueDialogue();
        }
    }
}