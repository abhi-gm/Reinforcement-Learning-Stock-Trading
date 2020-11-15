//Poylgon Planet - Contact. https://polygonplanet.com/contact/
//Copyright © 2016-2018 Polygon Planet. All rights reserved. https://polygonplanet.com/privacy-policy/
//This source file is subject to Unity Technologies Asset Store Terms of Service. https://unity3d.com/legal/as_terms

#pragma warning disable 0168 //Variable declared, but not used.
#pragma warning disable 0219 //Variable assigned, but not used.
#pragma warning disable 0414 //Private field assigned, but not used.
#pragma warning disable 0649 //Variable asisgned to, and will always have default value.

using UnityEngine;
using UnityEngine.UI;

public class StockInfoUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject s_InfoUI;
    public Text s_NameText;
    public Text s_PriceText;
    public Text s_SharesText;
    public Text s_MarketValueText;
    public Text s_NetCostText;
    public Text s_GainLossText;
    public InputField s_AmountInputField;
    public Button s_SellButton;
    public Button s_BuyButton;

    //Singleton
    public static StockInfoUI instance;

    //Variables
    [HideInInspector]
    public int selectedStock;
    private int currentAmount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnGUI()
    {
        s_NameText.text = StockManager.instance.stockSaveData[selectedStock].stockName;
        s_PriceText.text = "Price: $" + StockManager.instance.stockSaveData[selectedStock].currentStockPrice.ToString("F0");
        s_SharesText.text = "Shares: " + StockManager.instance.stockSaveData[selectedStock].currentShares;
        s_MarketValueText.text = "Market Value: $" + (StockManager.instance.stockSaveData[selectedStock].currentShares * 
            StockManager.instance.stockSaveData[selectedStock].currentStockPrice).ToString("F0");
        s_NetCostText.text = "Net Cost: $" + StockManager.instance.stockSaveData[selectedStock].netCost.ToString("F0");
        s_GainLossText.text = "Gain/Loss: $" + ((StockManager.instance.stockSaveData[selectedStock].currentShares * 
            StockManager.instance.stockSaveData[selectedStock].currentStockPrice) - StockManager.instance.stockSaveData[selectedStock].netCost).ToString("F0");

        if (currentAmount == 0)
        {
            s_BuyButton.interactable = false;
            s_SellButton.interactable = false;
        }
        else
        {
            if (GameManager.instance.currentMoney >= StockManager.instance.stockSaveData[selectedStock].currentStockPrice * currentAmount)
                s_BuyButton.interactable = true;
            else
                s_BuyButton.interactable = false;

            if (StockManager.instance.stockSaveData[selectedStock].currentShares >= currentAmount)
                s_SellButton.interactable = true;
            else
                s_SellButton.interactable = false;
        }
    }

    public void UpdateAmountInputField(int value)
    {
        s_AmountInputField.text = currentAmount.ToString();
    }

    public void ShowStockInfoUI()
    {
        AudioManager.instance.PlayAudioClip(AudioManager.instance.uiClickSound);
        currentAmount = 0;
        UpdateAmountInputField(currentAmount);
        GameManager.instance.gameStarted = false;
        s_InfoUI.SetActive(true);
    }

    public void CloseStockInfoUI()
    {
        AudioManager.instance.PlayAudioClip(AudioManager.instance.uiClickSound);
        GameManager.instance.gameStarted = true;
        s_InfoUI.SetActive(false);
    }

    public void IncreaseAmount()
    {
        AudioManager.instance.PlayAudioClip(AudioManager.instance.uiClickSound);
        currentAmount++;
        UpdateAmountInputField(currentAmount);
    }

    public void DecreaseAmount()
    {
        AudioManager.instance.PlayAudioClip(AudioManager.instance.uiClickSound);
        currentAmount--;
        if (currentAmount <= 0)
            currentAmount = 0;
        UpdateAmountInputField(currentAmount);
    }

    public void SetAmount()
    {
        AudioManager.instance.PlayAudioClip(AudioManager.instance.uiClickSound);
        currentAmount = int.Parse(s_AmountInputField.text);
        UpdateAmountInputField(currentAmount);
    }

    public void BuyStock()
    {
        if (GameManager.instance.currentMoney >= StockManager.instance.stockSaveData[selectedStock].currentStockPrice * currentAmount)
        {
            AudioManager.instance.PlayAudioClip(AudioManager.instance.uiClickSound);
            GameManager.instance.currentMoney -= StockManager.instance.stockSaveData[selectedStock].currentStockPrice * currentAmount;
            StockManager.instance.stockSaveData[selectedStock].currentShares += currentAmount;
            StockManager.instance.stockSaveData[selectedStock].netCost += StockManager.instance.stockSaveData[selectedStock].currentStockPrice * currentAmount;
            SaveLoadManager.instance.Save();
        }
    }

    public void SellStock()
    {
        if (StockManager.instance.stockSaveData[selectedStock].currentShares >= currentAmount)
        {
            AudioManager.instance.PlayAudioClip(AudioManager.instance.uiClickSound);
            GameManager.instance.currentMoney += StockManager.instance.stockSaveData[selectedStock].currentStockPrice * currentAmount;
            StockManager.instance.stockSaveData[selectedStock].currentShares -= currentAmount;
            StockManager.instance.stockSaveData[selectedStock].netCost -= StockManager.instance.stockSaveData[selectedStock].currentStockPrice * currentAmount;
            if (StockManager.instance.stockSaveData[selectedStock].currentShares == 0)
                StockManager.instance.stockSaveData[selectedStock].netCost = 0;

            SaveLoadManager.instance.Save();
        }
    }
}