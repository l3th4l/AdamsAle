namespace Weapons.Testing
{
    using UnityEngine;

    internal sealed class WeaponIcon : MonoBehaviour, IWeaponIcon
    {
        [SerializeField]
        private Sprite icon;

        Sprite IWeaponIcon.Sprite
        {
            get { return this.icon; }
        }
    }
}