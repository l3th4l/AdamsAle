namespace Dialogue
{
    using UnityEngine;

    public abstract class DialogueDisplay : MonoBehaviour
    {
        public abstract string Text { get; set; }

        public abstract void Open();

        public abstract void Close();
    }

}