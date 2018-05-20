namespace Dialogue
{
    using UnityEngine;

    internal sealed class Dialogue : MonoBehaviour
    {
        [SerializeField]
        private DialogueDisplay display;

        [SerializeField]
        private DialogueProvider provider;

        public void ContinueDialogue()
        {
            this.TryContinueDialogue();
        }

        public bool TryContinueDialogue()
        {
            string text;
            if (this.provider.TryGetNext(out text))
            {
                this.display.Text = text;
                return true;
            }

            return false;
        }
    }
}