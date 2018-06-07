namespace Weapons
{
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    
    internal sealed class WeaponWheel : MonoBehaviour
    {
        [SerializeField]
        private Image fullMask;

        [Space, SerializeField]
        private WeaponContainer container;

        [Space, SerializeField]
        private GameObject dividerPrefab;

        [SerializeField]
        private Transform dividerContainer;

        [SerializeField]
        private Image selection;

        [Space, SerializeField]
        private float selectCircleRadius;

        private Image[] icons;

        private Vector3 lastMousePos, accumulatedMousePos;

        private int cachedSlotCount;

        private void Reset()
        {
            this.container = WeaponWheel.FindObjectOfType<WeaponContainer>();
        }

        private void Update()
        {
            const string WeaponsInput = "Weapons";

            // Make this an animation later
            this.fullMask.enabled = !Input.GetButton(WeaponsInput);
            Cursor.visible = !Input.GetButton(WeaponsInput);

            if (Input.GetButtonDown(WeaponsInput))
                this.StartSelection();

            if (Input.GetButton(WeaponsInput))
                this.UpdateSelected();
        }

        private void StartSelection()
        {
            this.FetchIconImages();
            this.DrawSelected();

            // Don't want to update too often
            if (this.cachedSlotCount != this.container.Slots)
                this.UpdateDividers();

            this.accumulatedMousePos = Vector2.zero;
            this.lastMousePos = Input.mousePosition;

            this.cachedSlotCount = this.container.Slots;
        }

        private void UpdateSelected()
        {
            this.accumulatedMousePos =
                Vector2.ClampMagnitude(
                    this.accumulatedMousePos + (Input.mousePosition - this.lastMousePos),
                    this.selectCircleRadius);

            if (this.accumulatedMousePos != Vector3.zero)
            {
                const float Tau = 2f * Mathf.PI;
                float rotationPerSlot = Tau / this.container.Slots;
                float halfRotationPerSlot = rotationPerSlot * 0.5f;
                float rads = Mathf.Atan2(this.accumulatedMousePos.x, this.accumulatedMousePos.y) + halfRotationPerSlot;

                this.container.SelectedIndex = (int)((((rads % Tau + Tau) % Tau) / Tau) * this.container.Slots);
                this.DrawSelected();
            }

            this.lastMousePos = Input.mousePosition;
        }

        private void FetchIconImages()
        {
            this.icons =
                this.GetComponentsInChildren<Image>()
                .Where(i => i.CompareTag("WeaponIcon"))
                .ToArray();
        }

        private void UpdateIcons()
        {
            int i = 0;
            foreach (var icon in this.container.Icons)
            {
                if (icon != null)
                    this.icons[i++].sprite = icon.Sprite;
            }
        }

        private void UpdateDividers()
        {
            for (int i = 0; i < this.dividerContainer.childCount; i++)
            {
                WeaponWheel.Destroy(this.dividerContainer.GetChild(i).gameObject);
            }

            float rotationPerSlot = 360f / this.container.Slots;
            float halfRotationPerSlot = rotationPerSlot * 0.5f;
            float rotation = 90f + halfRotationPerSlot;
            for (int i = 0; i < this.container.Slots; i++)
            {
                var divider = WeaponWheel.Instantiate(this.dividerPrefab, this.dividerContainer, false);
                var transform = (RectTransform)divider.transform;
                transform.rotation = Quaternion.Euler(0f, 0f, rotation -= rotationPerSlot);
            }

            this.selection.fillAmount = 1f / this.container.Slots;
        }

        private void DrawSelected()
        {
            float rotationPerSlot = 360f / this.container.Slots;
            float halfRotationPerSlot = rotationPerSlot * 0.5f;
            var transform = (RectTransform)this.selection.transform;
            transform.rotation = Quaternion.Euler(0f, 0f, -180f + halfRotationPerSlot - this.container.SelectedIndex * rotationPerSlot);
        }
    }

}