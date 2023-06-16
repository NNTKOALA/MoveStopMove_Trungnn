using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameStateObjects;
    [Header("GameMenu:")]
    [SerializeField] private GameObject gameMenuMain;
    [SerializeField] private GameObject weaponShop;
    [SerializeField] private GameObject skinShop;

    public GameObject GameMenuMain { get => gameMenuMain; set => gameMenuMain = value; }
    public GameObject WeaponShop { get => weaponShop; set => weaponShop = value; }
    public GameObject SkinShop { get => skinShop; set => skinShop = value; }
    public List<GameObject> GameStateObjects { get => gameStateObjects; set => gameStateObjects = value; }


    private void HideAll()
    {
        HideGameMenuMain();
        HideWeaponShop();
        HideSkinShop();
    }
    public void HideGameMenuMain()
    {
        gameMenuMain.SetActive(false);
    }
    public void HideWeaponShop()
    {
        weaponShop.SetActive(false);
    }
    public void ShowWeaponShop()
    {
        weaponShop.SetActive(true);
    }
    public void HideSkinShop()
    {
        skinShop.SetActive(false);
    }

    private void ShowSkinShop()
    {
        skinShop.SetActive(true);
    }

    public void OnClickWeaponShop()
    {
        HideAll();
        ShowWeaponShop();
    }

    public void OnClickSkinShop()
    {
        HideAll();
        ShowSkinShop();
    }
    public void OnClickPlay()
    {
        HideAll();

    }
}
