using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] GameObject ingameUI;
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject weaponShopUI;
    [SerializeField] GameObject clothesShopUI;
    [SerializeField] GameObject revivePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] TextMeshProUGUI aliveCount;
    [SerializeField] TextMeshProUGUI moneyCount;


    public void Awake()
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

    public void Start()
    {

        SwitchToMainMenuUI();
    }

    public void SetPlayerName(string playerName)
    {
        GameManager.Instance.SetPlayerName(playerName);
    }

    public void ResetGame()
    {
        GameManager.Instance.ReturnAllEnemy();
    }
     
    public void NewGame()
    {
        SwitchToIngameUI();
        GameManager.Instance.StartNewGame();
    }

    public void DeactiveAll()
    {
        ingameUI.SetActive(false);
        mainMenuUI.SetActive(false);
        revivePanel.SetActive(false);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        weaponShopUI.SetActive(false);
        clothesShopUI.SetActive(false);
    }

    public void SwitchTo(GameObject ui)
    {
        DeactiveAll();
        ui.gameObject.SetActive(true);
    }

    public void SwitchToMainMenuUI()
    {
        SwitchTo(mainMenuUI);
        UpdateMoney(ShopManager.Instance.Money);
    }
    public void UpdateMoney(int amt)
    {
        moneyCount.text = amt.ToString();
    }

    public void SwitchToIngameUI()
    {
        SwitchTo(ingameUI);
    }

    public void SwitchToRevivePanel()
    {
        SwitchTo(revivePanel);
    }

    public void SwitchToPausePanel()
    {
        SwitchTo(pausePanel);
    }

    public void SwitchToWinPanel()
    {
        SwitchTo(winPanel);
    }

    public void SwitchToLosePanel()
    {
        SwitchTo(losePanel);
    }

    public void OpenWeaponShopUI()
    {
        weaponShopUI.SetActive(true);
    }
}
