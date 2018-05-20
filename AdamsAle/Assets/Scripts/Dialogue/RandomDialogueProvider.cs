namespace Dialogue
{
    using UnityEngine;

    public sealed class RandomDialogueProvider : DialogueProvider
    {
        [SerializeField]
        private string[] text;

        private int lastIndex = -1;

        public override bool TryGetNext(out string text)
        {
            text = string.Empty;
            switch (this.text.Length)
            {
                case 0:
                    this.lastIndex = -1;
                    return false;

                case 1:
                    if (this.lastIndex == -1)
                    {
                        this.SetText(ref text, 0);
                        return true;
                    }

                    this.lastIndex = -1;
                    return false;

                default:
                    int index = this.lastIndex;

                    do
                    {
                        index = Random.Range(0, this.text.Length);
                    } while (index == this.lastIndex);

                    this.SetText(ref text, index);
                    return true;
            }
        }

        private void SetText(ref string text, int index)
        {
            text = this.text[index];
            this.lastIndex = index;
        }
    }
}