namespace Dialogue
{
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class TextBubble : DialogueDisplay
    {
        [SerializeField]
        private Text display;

        public override string Text
        {
            get { return this.display.text; }
            set { this.display.text = value; }
        }

        private void Reset()
        {
            this.display = this.GetComponentInChildren<Text>();
        }

        public override void Open()
        {
            this.gameObject.SetActive(true);
        }

        public override void Close()
        {
            this.gameObject.SetActive(false);
        }
    }
}