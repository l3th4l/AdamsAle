namespace Weapons
{
    using System;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    internal sealed class WeaponWheel : MonoBehaviour
    {
        [SerializeField]
        private WeaponContainer container;

        [Space, SerializeField]
        private Image fullMask;
        
        [SerializeField]
        private Transform dividerContainer;

        [SerializeField]
        private GameObject dividerPrefab;

        [SerializeField]
        private Transform iconContainer;

        [SerializeField]
        private Image iconPrefab;
        
        [Space, SerializeField]
        private Image selection;

        [SerializeField]
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
            this.DrawSelected();

            // Don't want to update too often
            if (this.cachedSlotCount != this.container.Slots)
                this.UpdateSlots();

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

        private void UpdateSlots()
        {
            this.UpdateDividers();
            this.UpdateIconImages();
            this.UpdateIcons();
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

        private void UpdateIconImages()
        {
            for (int i = 0; i < this.iconContainer.childCount; i++)
            {
                WeaponWheel.Destroy(this.iconContainer.GetChild(i).gameObject);
            }
            
            float degsPerSlot = 360f / this.container.Slots;
            float degs = 90f;
            this.icons = new Image[this.container.Slots];
            for (int i = 0; i < this.container.Slots; i++)
            {
                this.icons[i] = WeaponWheel.Instantiate(this.iconPrefab, this.iconContainer, false);
                var transform = (RectTransform)this.icons[i].transform;
                transform.rotation = Quaternion.Euler(0f, 0f, degs);
                degs -= degsPerSlot;
            }
        }

        private void UpdateIcons()
        {
            int i = 0;
            foreach (var icon in this.container.Icons)
            {
                this.icons[i++].sprite = icon.Sprite;
            }
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