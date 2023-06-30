using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponData : ScriptableObject
{
    public WeaponModelsList weaponModelList;

    [Space, Header("Weapon Info")]
    public EWeaponType weaponType;
    public GameObject weaponModel;
    public int damage;
    public float attackRange;

    [Space, Header("Info for Shop")]
    public int price;

    private void OnValidate()
    {
        weaponModel = weaponModelList.GetModelsByType(weaponType);
    }

    public static string GetWeaponKey(WeaponData weaponData)
    {
        return weaponData.weaponType.ToString();
    }
}
public enum EWeaponType
{
    Arrow,
    Axe_0,
    Axe_1,
    Boomerang,
    Candy_0,
    Candy_1,
    Candy_2,
    Candy_4,
    Hammer,
    Knife,
    Uzi
}
