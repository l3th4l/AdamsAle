namespace Testing
{
    using UnityEngine;

    [ExecuteInEditMode]
    internal sealed class ArbitraryMovement : MonoBehaviour
    {
        [SerializeField]
        private bool executeInEditMode;

        [SerializeField]
        private Transform[] transforms;

        [SerializeField]
        private float speed = 0.01f;

        private void Update()
        {
            if (this.executeInEditMode || Application.isPlaying)
            {
                var delta = this.GetDelta(Time.time) * this.speed;

                foreach (var t in this.transforms)
                {
                    t.Translate(delta);
                }
            }
        }

        private Vector3 GetDelta(float t)
        {
            return new Vector3(
                -Mathf.Sin(t),
                Mathf.Cos(t));
        }
    }
}