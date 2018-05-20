namespace Dialogue
{
    using UnityEngine;

    public abstract class DialogueProvider : MonoBehaviour
    {
        public abstract bool TryGetNext(out string text);
    }
}