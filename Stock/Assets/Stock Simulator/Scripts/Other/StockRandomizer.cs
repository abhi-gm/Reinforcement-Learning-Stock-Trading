//Poylgon Planet - Contact. https://polygonplanet.com/contact/
//Copyright © 2016-2018 Polygon Planet. All rights reserved. https://polygonplanet.com/privacy-policy/
//This source file is subject to Unity Technologies Asset Store Terms of Service. https://unity3d.com/legal/as_terms

#pragma warning disable 0168 //Variable declared, but not used.
#pragma warning disable 0219 //Variable assigned, but not used.
#pragma warning disable 0414 //Private field assigned, but not used.
#pragma warning disable 0649 //Variable asisgned to, and will always have default value.

using System.Collections.Generic;
using UnityEngine;

public class StockRandomizer : MonoBehaviour
{
    public static float ReturnRandom()
    {
        List<float> values = new List<float>();

        int i = new int();
        while (i < 40)
        {
            values.Add(Random.Range(-2, 2));
            i++;
        }
        i = 0;
        while (i < 30)
        {
            values.Add(Random.Range(-4f, 4f));
            i++;
        }
        i = 0;
        while (i < 20)
        {
            values.Add(Random.Range(-7f, 7f));
            i++;
        }
        i = 0;
        while (i < 7)
        {
            values.Add(Random.Range(-10f, 10f));
            i++;
        }
        i = 0;
        while (i < 2)
        {
            values.Add(Random.Range(-50f, 50f));
            i++;
        }
        i = 0;
        while (i < 1)
        {
            values.Add(Random.Range(-100f, 100f));
            i++;
        }

        float returnPostive = Random.Range(0, 2);
        if (returnPostive == 0)
            return Mathf.Abs(values[Random.Range(0, values.Count)]);
        else
            return values[Random.Range(0, values.Count)];
    }
}