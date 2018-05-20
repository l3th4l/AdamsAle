namespace Dialogue
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    [Serializable]
    public struct DialogueItem
    {
        [Multiline]
        public string Text;

        public UnityEvent Event;
    }
}