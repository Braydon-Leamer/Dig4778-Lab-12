using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsTracker : MonoBehaviour
{
    private void Start()
    {
        GameAnalytics.Initialize();
        CreateDesignEvent();
    }

    public void CreateDesignEvent()
    {
        GameAnalytics.NewDesignEvent("TestDesignEvent", 1);
    }
}
