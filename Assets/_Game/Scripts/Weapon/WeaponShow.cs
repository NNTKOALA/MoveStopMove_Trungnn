using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShow : MonoBehaviour
{
    [SerializeField] List<GameObject> weapons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        DeactiveAll();
    }

    private void DeactiveAll()
    {
        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }

    public void ShowWeapon(EWeaponType type)
    {
        DeactiveAll();
        weapons[(int)type].SetActive(true);
    }
}
