namespace Dialogue
{
    using UnityEngine;

    internal sealed class Dialogue : MonoBehaviour
    {
        [SerializeField]
        private DialogueDisplay display;

        [SerializeField]
        private DialogueProvider[] providers;

        public void ContinueDialogue()
        {
            this.TryContinueDialogue();
        }

        public bool TryContinueDialogue()
        {
            string text;

            for (int i = 0; i < this.providers.Length; i++)
            {
                if (this.providers[i].TryGetNext(out text))
                {
                    this.display.Text = text;
                    return true;
                }
            }

            return false;
        }
    }
}