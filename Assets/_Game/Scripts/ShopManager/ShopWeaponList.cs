using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeaponList : MonoBehaviour
{
    [SerializeField] EWeaponType weaponType;
    [SerializeField] List<GameObject> modelsList = new List<GameObject>();
    [SerializeField] List<WeaponData> weaponDataList = new List<WeaponData>();
    [SerializeField] GameObject player;
    [SerializeField] GameObject weapon;
    [SerializeField] TextMeshProUGUI weaponName;
    [SerializeField] Text weaponPrice;
    [SerializeField] Button buyButton;
    [SerializeField] Button useButton;
    int currentWeapon = 0;

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

    public void ChangeNext()
    {
        modelsList[currentWeapon].SetActive(false);
        currentWeapon = ++currentWeapon % modelsList.Count;
        modelsList[currentWeapon].SetActive(true);

        weaponName.text = modelsList[currentWeapon].name.ToUpper();
        weaponPrice.text = modelsList[currentWeapon].GetComponent<Weapon>().getWeaponData().price.ToString();

        TestBuyWeapon();
    }

    public void ChangeBack()
    {
        modelsList[currentWeapon].SetActive(false);
        currentWeapon = (--currentWeapon + modelsList.Count) % modelsList.Count;
        modelsList[currentWeapon].SetActive(true);

        weaponName.text = modelsList[currentWeapon].name.ToUpper();
        weaponPrice.text = modelsList[currentWeapon].GetComponent<Weapon>().getWeaponData().price.ToString();

        TestBuyWeapon();
    }

    private void TestBuyWeapon()
    {
        if (ShopManager.Instance.CheckHasPurchasedWeapon(weaponDataList[currentWeapon]))
        {
            useButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            useButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
        }
    }

    public void OnEnable()
    {
        player.SetActive(false);
        weapon.SetActive(true);
        modelsList[currentWeapon].SetActive(true);

        weaponName.text = modelsList[currentWeapon].name.ToUpper();
        weaponPrice.text = modelsList[currentWeapon].GetComponent<Weapon>().getWeaponData().price.ToString();
    }

    public void OnDisable()
    {
        player.SetActive(true);
        weapon.SetActive(false);
    }

    public void BuyWeapon()
    {
        if (ShopManager.Instance.TryPurchaseItem(weaponDataList[currentWeapon].price))
        {
            ShopManager.Instance.AddWeaponToPlayer(weaponDataList[currentWeapon]);
            TestBuyWeapon();
        }
        else
        {
            //TODO: lam panel 
        }
    }

    public void EquipWeaponInHand()
    {
        GameManager.Instance.MainPlayer.SetWeapon((EWeaponType)currentWeapon);
    }
}
