namespace Weapons
{
    using System.Collections.Generic;
    using UnityEngine;

    internal sealed class WeaponContainer : MonoBehaviour
    {
        private const int MinSlots = 2, MaxSlots = 8;

        private int slots;

        [SerializeField, Range(0, MaxSlots - 1)]
        private int selectedIndex;

        private IWeaponIcon[] weaponIcons;
        
        public int Slots
        {
            get { return this.slots; }
        }

        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set { this.selectedIndex = value; }
        }

        public IEnumerable<IWeaponIcon> Icons
        {
            get { return this.weaponIcons; }
        }

        private void Awake()
        {
            this.UpdateWeapons();
        }

        private void OnValidate()
        {
            this.selectedIndex = Mathf.Clamp(this.selectedIndex, 0, this.slots - 1);
        }

        private void UpdateWeapons()
        {
            this.weaponIcons = this.GetComponents<IWeaponIcon>();
            this.slots = Mathf.Min(this.weaponIcons.Length, MaxSlots);
        }
    }
}