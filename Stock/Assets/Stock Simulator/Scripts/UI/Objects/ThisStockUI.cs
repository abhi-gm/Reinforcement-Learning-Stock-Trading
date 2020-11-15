//Poylgon Planet - Contact. https://polygonplanet.com/contact/
//Copyright © 2016-2018 Polygon Planet. All rights reserved. https://polygonplanet.com/privacy-policy/
//This source file is subject to Unity Technologies Asset Store Terms of Service. https://unity3d.com/legal/as_terms

#pragma warning disable 0168 //Variable declared, but not used.
#pragma warning disable 0219 //Variable assigned, but not used.
#pragma warning disable 0414 //Private field assigned, but not used.
#pragma warning disable 0649 //Variable asisgned to, and will always have default value.

using UnityEngine;
using UnityEngine.UI;

public class ThisStockUI : MonoBehaviour 
{
    [Header("UI")]
    public Image stockBackground;
    public Text stockNameText;
    public Text stockSharesText;
    public Text stockPriceText;
    public Text stockMoneyChangeText;

    [Header("Colors")]
    public Color greenColor;
    public Color redColor;

    //Variables
    [HideInInspector]
    public int thisStock;

    private void OnGUI()
    {
        stockNameText.text = StockManager.instance.stockSaveData[thisStock].stockName;
        stockSharesText.text = StockManager.instance.stockSaveData[thisStock].currentShares.ToString();
        stockPriceText.text = StockManager.instance.stockSaveData[thisStock].currentStockPrice.ToString("F0");
        stockMoneyChangeText.text = StockManager.instance.stockSaveData[thisStock].lastStockMoneyChange.ToString("F0");
        if (StockManager.instance.stockSaveData[thisStock].lastStockMoneyChange > 0)
            stockMoneyChangeText.color = greenColor;
        else
            stockMoneyChangeText.color = redColor;
    }

    public void ShowStockUI()
    {
        StockInfoUI.instance.selectedStock = thisStock;
        StockInfoUI.instance.ShowStockInfoUI();
    }
}