using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    public event EventHandler<OnEquipWeaponArgs> onEquipWeapon;
    public class OnEquipWeaponArgs : EventArgs
    {
        public WeaponData weaponData;
    }


/*    public event EventHandler<OnEquipSkinArgs> onEquipSkin;
    public class OnEquipSkinArgs : EventArgs
    {
        public SkinData skinData;
    }*/

    [Space, Header("Player Currency")]
    [SerializeField] int money = 10000;
    public int Money => money;

    //Weapon List
    [SerializeField] List<Weapon> sellWeaponList;
    public List<WeaponData> GetSellWeaponList()
    {
        List<WeaponData> sellList = new List<WeaponData>();

        foreach (var weapon in sellWeaponList)
        {
            if (!purchasedWeaponList.Contains(weapon.getWeaponData()))
            {
                sellList.Add(weapon.getWeaponData());
            }
        }

        return sellList;
    }

    //Skin List
/*    [SerializeField] List<SkinData> sellSkinDataList;
    public List<SkinData> GetSellSkinList() => sellSkinDataList;*/


    private List<WeaponData> purchasedWeaponList = new List<WeaponData>();
    //private List<SkinData> purchasedSkinList = new List<SkinData>();

    //for save system
    private List<EWeaponType> weaponSaveList = new List<EWeaponType>();
    private List<int> skinSaveList = new List<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Bot.onAnyEnemyDeath += Enemy_onAnyEnemyDeath;
    }

    private void Enemy_onAnyEnemyDeath(object sender, Bot.OnAnyEnemyDeathArgs e)
    {
        Player player = e.damageDealer as Player;
        if (player == null) return;

        (player).ChangeScale(player);
        GameManager.Instance.IncreaseKillCount();

        int amtMoney = UnityEngine.Random.Range(0, 50);
        AddMoney(amtMoney);
    }

    private void AddMoney(int amt)
    {
        money += amt;
    }

    public bool TryPurchaseItem(int price)
    {
        if (money >= price)
        {
            money -= price;
            UIManager.Instance.UpdateInfoOnScreen();
            return true;
        }

        return false;
    }

    public void AddWeaponToPlayer(WeaponData weaponData)
    {
        if (purchasedWeaponList.Contains(weaponData)) return;

        purchasedWeaponList.Add(weaponData);

    }

    public void EquipWeapon(WeaponData weaponData)
    {
        onEquipWeapon?.Invoke(this, new OnEquipWeaponArgs { weaponData = weaponData });
    }

/*    public void AddSkinToPlayer(SkinData skinData)
    {
        if (purchasedSkinList.Contains(skinData)) return;

        purchasedSkinList.Add(skinData);

    }

    public void EquipSkin(SkinData skinData)
    {
        onEquipSkin?.Invoke(this, new OnEquipSkinArgs { skinData = skinData });
    }*/


/*        data.skinIdList = new List<int>();
        foreach (SkinData skinData in purchasedSkinList)
        {
            data.skinIdList.Add(skinData.itemId);
        }*/
   

    public bool CheckHasPurchasedWeapon(WeaponData data)
    {
        foreach (var value in purchasedWeaponList)
        {
            if (data == value)
                return true;
        }

        return false;
    }

/*    public bool CheckHasPurchasedSkin(SkinData data)
    {
        foreach (var value in skinSaveList)
        {
            if (data.itemId == value)
                return true;
        }

        return false;
    }*/
}
