using System.Collections;
using System.Collections.Generic;
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
        DeactivateModelView();
    }

    public void SetPlayerName(string playerName)
    {
        GameManager.Instance.SetPlayerName(playerName);
    }

    public void ResetGame()
    {
        GameManager.Instance.ReturnAllEnemy();
        GameManager.Instance.CalculateStarByKillAmount();
        
        SwitchToMainMenuUI(); 
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
        UpdateInfoOnScreen();
    }

    public void UpdateInfoOnScreen()
    {
        //currencyText.text = ShopSystem.Instance.Money.ToString();
        //starAmtText.text = GameManager.Instance.StarCount.ToString();
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
        //SetUpLosePanel();
    }

/*    public void SetUpLosePanel()
    {
        int killCount = GameManager.Instance.PlayerKillCount;
        Sprite starSprite = oneStar;
        if (killCount > 30)
        {
            starSprite = twoStar;
        }
        if (killCount > 60)
        {
            starSprite = threeStar;
        }

        starImage.sprite = starSprite;
    }*/

    public void OpenWeaponShopUI()
    {
        weaponShopUI.SetActive(true);
        //weaponShopView.SetActive(true);
    }

    public void OpenClothesShopUI()
    {
        clothesShopUI.SetActive(true);
        //clothesShopView.SetActive(true);
    }

    public void DeactivateModelView()
    {
        //weaponShopView.SetActive(false);
        //clothesShopView.SetActive(false);
    }
}
