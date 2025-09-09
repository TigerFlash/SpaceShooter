using UnityEngine;
public class WeaponManager : MonoBehaviour
{
    [SerializeField] private ActivatebleObject[] weapons;
    public void PickUpWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length)
        {
            Debug.LogWarning("Неверный индекс оружия");
            return;
        }
        ActivatebleObject obj = weapons[index];
        if (obj.weapon != null)
            obj.weapon.SetActive(true);
        if (obj.weaponImage != null)
            obj.weaponImage.SetActive(true);
    }
}

[System.Serializable]
public class ActivatebleObject
{
    public GameObject weapon;
    public GameObject weaponImage;
}