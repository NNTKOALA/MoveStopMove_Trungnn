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

    [Space, Header("Player Currency")]
    [SerializeField] int money = 10000;
    public int Money => money;

    private List<WeaponData> purchasedWeaponList = new List<WeaponData>();
    [SerializeField] List<WeaponData> checkList;

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
        money = PlayerPrefs.GetInt(StringData.moneyKey, 100);

        for (int i = 0; i < 11; i++) 
        {
            int check = PlayerPrefs.GetInt(WeaponData.GetWeaponKey(checkList[i]), 0);
            if (check > 0)
            {
                purchasedWeaponList.Add(checkList[i]);
            }
        }
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
            UIManager.Instance.UpdateMoney(money);
            PlayerPrefs.SetInt(StringData.moneyKey, money);
            return true;
        }

        return false;
    }

    public void AddWeaponToPlayer(WeaponData weaponData)
    {
        if (purchasedWeaponList.Contains(weaponData)) return;

        purchasedWeaponList.Add(weaponData);
        PlayerPrefs.SetInt(WeaponData.GetWeaponKey(weaponData), 1);
    }

    public void EquipWeapon(WeaponData weaponData)
    {
        onEquipWeapon?.Invoke(this, new OnEquipWeaponArgs { weaponData = weaponData });
    }

    public bool CheckHasPurchasedWeapon(WeaponData data)
    {
        foreach (var value in purchasedWeaponList)
        {
            if (data == value)
                return true;
        }

        return false;
    }

}
