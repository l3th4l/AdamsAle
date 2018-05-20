namespace Dialogue
{
    using UnityEngine;

    public sealed class SequencedDialogueProvider : DialogueProvider
    {
        [SerializeField]
        private string[] text;

        private int lastIndex = -1;

        public override bool TryGetNext(out string text)
        {
            if (this.lastIndex < this.text.Length - 1)
            {
                text = this.text[++this.lastIndex];
                return true;
            }

            text = string.Empty;
            return false;
        }
    }
}