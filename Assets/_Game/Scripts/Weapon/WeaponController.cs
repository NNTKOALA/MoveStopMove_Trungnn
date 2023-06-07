using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon List", fileName = "WeaponList_")]
public class WeaponController : ScriptableObject
{
    public List<WeaponDataPair> listWeapon;

    public GameObject GetWeaponPrefab(EWeaponType weaponType)
    {
        foreach (var weapon in listWeapon)
        {
            if (weapon.data.weaponType == weaponType)
            {
                return weapon.prefab;
            }
        }

        return null;
    }
}

[System.Serializable]
public class WeaponDataPair
{
    public WeaponData data;
    public GameObject prefab;
}
