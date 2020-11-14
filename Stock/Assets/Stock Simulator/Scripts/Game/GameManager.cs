//Poylgon Planet - Contact. https://polygonplanet.com/contact/
//Copyright © 2016-2018 Polygon Planet. All rights reserved. https://polygonplanet.com/privacy-policy/
//This source file is subject to Unity Technologies Asset Store Terms of Service. https://unity3d.com/legal/as_terms

#pragma warning disable 0168 //Variable declared, but not used.
#pragma warning disable 0219 //Variable assigned, but not used.
#pragma warning disable 0414 //Private field assigned, but not used.
#pragma warning disable 0649 //Variable asisgned to, and will always have default value.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Text moneyText;
    public Text dayText;

    [Header("Stock UI")]
    public GameObject stockLayoutHolder;
    public GameObject stockPrefab;

    [Header("Colors")]
    public Color colorOne;
    public Color colorTwo;

    [Header("Variables")]
    public float dayLength;

    //Singleton
    public static GameManager instance;

    //Variables
    [HideInInspector]
    public bool gameStarted, dayOver;
    [HideInInspector]
    public float currentTime, currentMoney;
    [HideInInspector]
    public int currentDay;
    private List<GameObject> spawnedStocks = new List<GameObject>();
    private SortMode sortMode;
    public enum SortMode
    {
        name,
        shares,
        price,
        change
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        StartCoroutine(GetActualWidth());
        LoadStocks();
        gameStarted = true;
        dayOver = false;
    }

    private IEnumerator GetActualWidth()
    {
        yield return 0;
        stockLayoutHolder.GetComponent<GridLayoutGroup>().cellSize = new Vector2(stockLayoutHolder.GetComponent<RectTransform>().rect.width, 150);
    }

    private void OnGUI()
    {
        moneyText.text = "$" + currentMoney.ToString("F0");
        dayText.text = "D: " + currentDay;
    }

    private void Update()
    {
        if (dayOver == true)
        {
            currentTime = dayLength;
            currentDay++;
            NextDay();
        }
        Timer();
    }

    private void Timer()
    {
        if (dayOver == false && gameStarted == true)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                dayOver = true;
        }
    }

    private void LoadStocks()
    {
        foreach (Transform child in stockLayoutHolder.transform)
            Destroy(child.gameObject);

        bool firstColor = new bool();
        for (int i = 0; i < StockManager.instance.stockSaveData.Count; i++)
        {
            GameObject stock = Instantiate(stockPrefab);
            stock.transform.SetParent(stockLayoutHolder.transform, false);
            if (firstColor == true)
                stock.GetComponent<ThisStockUI>().stockBackground.color = colorOne;
            else
                stock.GetComponent<ThisStockUI>().stockBackground.color = colorTwo;

            stock.transform.GetComponent<ThisStockUI>().thisStock = i;
            firstColor = !firstColor;
            spawnedStocks.Add(stock);
        }

        SortByName();
    }

    private void SortByName()
    {
        AudioManager.instance.PlayAudioClip(AudioManager.instance.uiClickSound);
        sortMode = SortMode.name;
        StockManager.instance.stockSaveData = StockManager.instance.stockSaveData.OrderBy(g => g.stockName).ToList();
        for (int i = 0; i < StockManager.instance.stockSaveData.Count; i++)
            spawnedStocks[i].transform.SetSiblingIndex(i);
    }

    private void SortByShares()
    {
        AudioManager.instance.PlayAudioClip(AudioManager.instance.uiClickSound);
        sortMode = SortMode.shares;
        StockManager.instance.stockSaveData = StockManager.instance.stockSaveData.OrderByDescending(g => g.currentShares).ToList();
        for (int i = 0; i < StockManager.instance.stockSaveData.Count; i++)
            spawnedStocks[i].transform.SetSiblingIndex(i);
    }

    private void SortByPrice()
    {
        AudioManager.instance.PlayAudioClip(AudioManager.instance.uiClickSound);
        sortMode = SortMode.price;
        StockManager.instance.stockSaveData = StockManager.instance.stockSaveData.OrderByDescending(g => g.currentStockPrice).ToList();
        for (int i = 0; i < StockManager.instance.stockSaveData.Count; i++)
            spawnedStocks[i].transform.SetSiblingIndex(i);
    }

    private void SortByChange()
    {
        AudioManager.instance.PlayAudioClip(AudioManager.instance.uiClickSound);
        sortMode = SortMode.change;
        StockManager.instance.stockSaveData = StockManager.instance.stockSaveData.OrderByDescending(g => g.lastStockMoneyChange).ToList();
        for (int i = 0; i < StockManager.instance.stockSaveData.Count; i++)
            spawnedStocks[i].transform.SetSiblingIndex(i);
    }

    private void NextDay()
    {
        for (int i = 0; i < StockManager.instance.defaultStocks.Count; i++)
        {
            float newPrice = StockRandomizer.ReturnRandom();
            StockManager.instance.stockSaveData[i].currentStockPrice += newPrice;
            StockManager.instance.stockSaveData[i].lastStockMoneyChange = newPrice;

            if (StockManager.instance.stockSaveData[i].currentStockPrice < 0)
                StockManager.instance.stockSaveData[i].currentStockPrice = 0;
        }

        if (sortMode == SortMode.name)
            SortByName();
        else if (sortMode == SortMode.shares)
            SortByShares();
        else if (sortMode == SortMode.price)
            SortByPrice();
        else if (sortMode == SortMode.change)
            SortByChange();

        dayOver = false;

        SaveLoadManager.instance.Save();
    }
}