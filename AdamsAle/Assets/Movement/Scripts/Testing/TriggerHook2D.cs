namespace CollisionUtility
{
    using UnityEngine;

    internal sealed class TriggerHook2D : MonoBehaviour
    {
        public delegate void TriggerHandler2D(Collider2D other);
        
        public event TriggerHandler2D Entered2D, Stayed2D, Exit2D;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (this.Entered2D != null)
            {
                this.Entered2D(other);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (this.Stayed2D != null)
            {
                this.Stayed2D(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (this.Exit2D != null)
            {
                this.Exit2D(other);
            }
        }
    }
}