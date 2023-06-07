using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponModelsList : ScriptableObject
{
    [SerializeField] List<GameObject> weaponModelsList = new List<GameObject>();

    public GameObject GetModelsByType(EWeaponType weaponType)
    {
        return weaponModelsList[(int)weaponType];
    }
}
