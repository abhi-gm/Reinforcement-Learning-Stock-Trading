//Poylgon Planet - Contact. https://polygonplanet.com/contact/
//Copyright © 2016-2018 Polygon Planet. All rights reserved. https://polygonplanet.com/privacy-policy/
//This source file is subject to Unity Technologies Asset Store Terms of Service. https://unity3d.com/legal/as_terms

#pragma warning disable 0168 //Variable declared, but not used.
#pragma warning disable 0219 //Variable assigned, but not used.
#pragma warning disable 0414 //Private field assigned, but not used.
#pragma warning disable 0649 //Variable asisgned to, and will always have default value.

using UnityEngine;
using UnityEngine.Advertisements;

public class ADManager : MonoBehaviour 
{
    [Header("Variables")]
    public bool enableADS;
    public float howOftenToPlayAD;

    //Singleton
    public static ADManager instance;

    //Variables
    [HideInInspector]
    public float currentTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

#if UNITY_ADS
    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= howOftenToPlayAD)
            ShowAD();
    }

    public void ShowAD(string zone = "")
    {
        if (enableADS == true)
        {
            Time.timeScale = 0;
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            currentTime = 0;
            Time.timeScale = 1;
        }
        else if (result == ShowResult.Skipped)
        {
            currentTime = 0;
            Time.timeScale = 1;
        }
        else if (result == ShowResult.Failed)
        {
            currentTime = 0;
            Time.timeScale = 1;
        }
    }
#endif
}