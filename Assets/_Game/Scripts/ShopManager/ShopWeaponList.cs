using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeaponList : MonoBehaviour
{
    [SerializeField] EWeaponType weaponType;
    [SerializeField] List<GameObject> modelsList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupVisualModel()
    {
        foreach (var model in modelsList)
        {
            model.SetActive(false);
        }

        modelsList[(int)weaponType].SetActive(true);
    }

/*    public void ChangeNext()
    {
        UnDisplayWeapon(currentWeapIndext);
        UnDisplayAllWeaponMats();
        currentWeapIndext++;

        if (currentWeapIndext == weaponMats.Length)
        {
            currentWeapIndext = 0;
        }
        DisplayWeapon(currentWeapIndext);
        DisplayWeaponMats(currentWeapIndext);
    }

    public void ChangeBack()
    {
        UnDisplayWeapon(currentWeapIndext);
        UnDisplayAllWeaponMats();
        currentWeapIndext--;

        if (currentWeapIndext < 0)
        {
            currentWeapIndext = weapons.Length - 1;
        }
        DisplayWeapon(currentWeapIndext);
        DisplayWeaponMats(currentWeapIndext);
    }*/
}
