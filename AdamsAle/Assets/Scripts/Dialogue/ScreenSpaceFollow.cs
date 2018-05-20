namespace Dialogue
{
    using UnityEngine;

    [RequireComponent(typeof(RectTransform)), ExecuteInEditMode]
    internal sealed class ScreenSpaceFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField]
        private Vector3 offset;

        private new RectTransform transform;

        private void Awake()
        {
            this.transform = this.GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (this.target)
            {
                var targetPosition = Camera.main.WorldToScreenPoint(this.target.position);
                this.transform.position = targetPosition + this.offset;
            }
        }
    }
}