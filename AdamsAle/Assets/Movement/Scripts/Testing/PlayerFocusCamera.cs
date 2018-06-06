namespace CameraFocus
{
    using UnityEngine;

    internal sealed class PlayerFocusCamera : MonoBehaviour
    {
        private Vector2 velocity;

        [SerializeField]
        private Transform player;

        [SerializeField]
        private float smoothTime = 1f;

        private void Reset()
        {
            var gobj = GameObject.FindGameObjectWithTag("Player");

            if (gobj)
            {
                this.player = gobj.transform;
            }
        }

        private void LateUpdate()
        {
            Vector3 target = Vector2.SmoothDamp(this.transform.position, this.player.position, ref this.velocity, this.smoothTime);
            target.z = this.transform.position.z;
            this.transform.position = target;
        }
    }
}