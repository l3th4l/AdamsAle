namespace Dialogue
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Text))]
    public sealed class TextBubble : DialogueDisplay
    {
        private Text display;

        public override string Text
        {
            get { return this.display.text; }
            set { this.display.text = value; }
        }

        private void Awake()
        {
            this.display = this.GetComponent<Text>();
        }
    }
}