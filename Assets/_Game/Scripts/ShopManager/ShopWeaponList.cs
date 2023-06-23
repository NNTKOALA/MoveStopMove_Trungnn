using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopWeaponList : MonoBehaviour
{
    [SerializeField] EWeaponType weaponType;
    [SerializeField] List<GameObject> modelsList = new List<GameObject>();
    [SerializeField] GameObject player;
    [SerializeField] GameObject weapon;
    [SerializeField] TextMeshProUGUI weaponName;
    int currentWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(true);
        weapon.SetActive(false);
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

    public void ChangeNext()
    {
        modelsList[currentWeapon].SetActive(false);
        currentWeapon = ++currentWeapon % modelsList.Count;
        modelsList[currentWeapon].SetActive(true);

        weaponName.text = modelsList[currentWeapon].name.ToUpper();
    }

    public void ChangeBack()
    {
        modelsList[currentWeapon].SetActive(false);
        currentWeapon = (--currentWeapon + modelsList.Count) % modelsList.Count;
        modelsList[currentWeapon].SetActive(true);

        weaponName.text = modelsList[currentWeapon].name.ToUpper();
    }

    public void OnEnable()
    {
        player.SetActive(false);
        weapon.SetActive(true);
    }

    public void OnDisable()
    {
        player.SetActive(true);
        weapon.SetActive(false);
    }
}
