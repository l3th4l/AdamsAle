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
            DialogueItem item;

            for (int i = 0; i < this.providers.Length; i++)
            {
                if (this.providers[i].TryGetNext(out item))
                {
                    this.display.Text = item.Text;
                    item.Event.Invoke();
                    return true;
                }
            }

            return false;
        }
    }
}