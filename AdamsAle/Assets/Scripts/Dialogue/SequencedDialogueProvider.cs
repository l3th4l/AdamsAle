namespace Dialogue
{
    using UnityEngine;

    public sealed class SequencedDialogueProvider : DialogueProvider
    {
        [SerializeField]
        private DialogueItem[] items;

        private int lastIndex = -1;

        public override bool TryGetNext(out DialogueItem item)
        {
            if (this.lastIndex < this.items.Length - 1)
            {
                item = this.items[++this.lastIndex];
                return true;
            }

            item = new DialogueItem();
            return false;
        }
    }
}