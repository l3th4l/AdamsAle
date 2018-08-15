namespace Weapons
{
    using UnityEngine;
    using UnityEngine.UI;

    internal sealed class WeaponWheel : MonoBehaviour
    {
        private const string
            WeaponsInput = "Weapons",
            SwapInput = "Fire1";

        [SerializeField]
        private WeaponContainer container;

        [Header("Containers"), SerializeField]
        private Image fullMask;
        
        [SerializeField]
        private Transform dividerContainer;

        [SerializeField]
        private GameObject dividerPrefab;

        [SerializeField]
        private Transform iconContainer;

        [SerializeField]
        private Image iconPrefab;
        
        [Header("Selection"), SerializeField]
        private Image selectionImage;

        [SerializeField]
        private Image selectionMask;

        [SerializeField]
        private float selectCircleRadius;

        [Header("Swapping"), SerializeField]
        private RectTransform circle;

        [SerializeField]
        private RectTransform circleMask;

        [SerializeField]
        private Image swapSelection;

        [SerializeField]
        private RectTransform outerCircleMask;

        [SerializeField]
        private float outerPadding = 15f;

        private Image[] icons;

        private Vector3 lastMousePos, accumulatedMousePos;
        
        private int cachedSlotCount, maskedIndex = -1;

        private bool IsSwitching
        {
            get { return this.selectionMask.enabled; }
            set { this.selectionMask.enabled = value; }
        }

        private float SelectionFillAmount
        {
            set
            {
                this.selectionImage.fillAmount = this.selectionMask.fillAmount = this.swapSelection.fillAmount = value;
            }
        }

        private void Reset()
        {
            this.container = WeaponWheel.FindObjectOfType<WeaponContainer>();
        }

        private void Update()
        {
            // Make this an animation later
            this.fullMask.enabled = !Input.GetButton(WeaponsInput);
            Cursor.visible = !Input.GetButton(WeaponsInput);
            this.IsSwitching = Input.GetButton(SwapInput);

            if (Input.GetButtonDown(WeaponsInput))
                this.StartSelection();
            else if (Input.GetButtonUp(WeaponsInput))
                this.maskedIndex = -1;

            if (Input.GetButton(WeaponsInput))
                this.UpdateSelected();
        }

        private void StartSelection()
        {
            this.SetSelectionRotation((RectTransform)this.selectionImage.transform);
            this.SetSelectionRotation((RectTransform)this.selectionMask.transform);

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
                this.SetSelectionRotation((RectTransform)this.selectionImage.transform);
                this.SetSelectionRotation((RectTransform)this.selectionMask.transform, this.maskedIndex);

                this.outerCircleMask.gameObject.SetActive(this.IsSwitching);
                this.swapSelection.gameObject.SetActive(this.IsSwitching);

                this.SetSelectionRotation((RectTransform)this.swapSelection.transform);
            }

            if (Input.GetButtonDown(SwapInput))
            {
                this.maskedIndex = this.container.SelectedIndex;
            }
            else if (Input.GetButtonUp(SwapInput))
                this.maskedIndex = -1;

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

            this.SelectionFillAmount = 1f / this.container.Slots;
        }

        private void UpdateIconImages()
        {
            for (int i = 0; i < this.iconContainer.childCount; i++)
            {
                WeaponWheel.Destroy(this.iconContainer.GetChild(i).gameObject);
            }

            const float Tau = 2f * Mathf.PI;
            float radsPerSlot = Tau / this.container.Slots;
            float rads = 0.5f * Mathf.PI;
            this.icons = new Image[this.container.Slots];
            for (int i = 0; i < this.container.Slots; i++)
            {
                this.icons[i] = WeaponWheel.Instantiate(this.iconPrefab, this.iconContainer, false);
                var transform = (RectTransform)this.icons[i].transform;
                transform.anchoredPosition =
                    new Vector3(
                        Mathf.Cos(rads),
                        Mathf.Sin(rads))
                    * transform.anchoredPosition.y;
                rads -= radsPerSlot;
            }
        }

        private void UpdateIcons()
        {
            var enumerator = this.container.Icons.GetEnumerator();
            for (int i = 0; i < this.container.Slots && enumerator.MoveNext(); i++)
            {
                this.icons[i].sprite = enumerator.Current.Sprite;
            }
        }

        private void SetSelectionRotation(RectTransform transform, int index)
        {
            float rotationPerSlot = 360f / this.container.Slots;
            float halfRotationPerSlot = rotationPerSlot * 0.5f;
            transform.rotation = Quaternion.Euler(0f, 0f, -180f + halfRotationPerSlot - index * rotationPerSlot);
        }

        private void SetSelectionRotation(RectTransform transform)
        {
            this.SetSelectionRotation(transform, this.container.SelectedIndex);
        }
    }
}