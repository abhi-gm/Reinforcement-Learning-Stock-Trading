//Poylgon Planet - Contact. https://polygonplanet.com/contact/
//Copyright © 2016-2018 Polygon Planet. All rights reserved. https://polygonplanet.com/privacy-policy/
//This source file is subject to Unity Technologies Asset Store Terms of Service. https://unity3d.com/legal/as_terms

#pragma warning disable 0168 //Variable declared, but not used.
#pragma warning disable 0219 //Variable assigned, but not used.
#pragma warning disable 0414 //Private field assigned, but not used.
#pragma warning disable 0649 //Variable asisgned to, and will always have default value.

using System;
using System.Collections.Generic;
using UnityEngine;

public class StockManager : MonoBehaviour 
{
    //Singleton
    public static StockManager instance;

    //Data
    public List<StockObject> defaultStocks;
    public List<StockSaveData> stockSaveData;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}

[Serializable]
public class StockSaveData
{
    //Info
    public string stockName;

    //Variables
    public int currentShares;
    public float currentStockPrice;
    public float lastStockMoneyChange;
    public float netCost;
}