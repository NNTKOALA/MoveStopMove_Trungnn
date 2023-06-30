using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    public WeaponData getWeaponData() { return weaponData; }

    public void OnEquip(Character character)
    {
        character.ModifyStatsByWeapon(weaponData.attackRange, weaponData.damage);
    }
}
