namespace Dialogue
{
    using UnityEngine;

    public sealed class RandomDialogueProvider : DialogueProvider
    {
        [SerializeField]
        private DialogueItem[] items;

        private int lastIndex = -1;

        public override bool TryGetNext(out DialogueItem item)
        {
            item = new DialogueItem();
            switch (this.items.Length)
            {
                case 0:
                    this.lastIndex = -1;
                    return false;

                case 1:
                    if (this.lastIndex == -1)
                    {
                        this.SetText(ref item, 0);
                        return true;
                    }

                    this.lastIndex = -1;
                    return false;

                default:
                    int index = this.lastIndex;

                    do
                    {
                        index = Random.Range(0, this.items.Length);
                    } while (index == this.lastIndex);

                    this.SetText(ref item, index);
                    return true;
            }
        }

        private void SetText(ref DialogueItem item, int index)
        {
            item = this.items[index];
            this.lastIndex = index;
        }
    }
}