using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseConfigData : MonoBehaviour
{
    public HouseConfig config;
    public float thisConfigPrice;
    
    
    // Start is called before the first frame update
    void Start()
    {
        thisConfigPrice = config.price;
    }
}
