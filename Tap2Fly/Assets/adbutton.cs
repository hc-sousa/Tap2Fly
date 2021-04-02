using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System.ComponentModel.Design;
using System;

public class adbutton : MonoBehaviour
{
    public RewardBasedVideoAd rewardBasedVideoAd;
    public gamemanager gameManager;
    public int moedas;
    // Start is called before the first frame update
    void Start()
    {   
        this.rewardBasedVideoAd = RewardBasedVideoAd.Instance;
        rewardBasedVideoAd.OnAdLoaded += HandleOnAdLoaded;
        rewardBasedVideoAd.OnAdOpening += HandleOnAdOpening;
        rewardBasedVideoAd.OnAdClosed += HandleOnAdClosed;
        rewardBasedVideoAd.OnAdRewarded += HandleOnAdRewarded;
        MobileAds.Initialize(initStatus => { });
        this.LoadRewardBasedAd();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadRewardBasedAd()
    {

        string adUnitId;
        #if UNITY_ANDROID
            adUnitId = "ca-app-pub-8246676797736648/2502717408";
        #elif UNITY_IPHONE
            adUnitId = "todo";
        #else
        adUnitId = "unexpected_platform";
        #endif
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardBasedVideoAd.LoadAd(request, adUnitId);

    }
    public void ShowRewardBasedAd()
    {
        moedas = PlayerPrefs.GetInt("moedas", 0);
        if (this.rewardBasedVideoAd.IsLoaded())
        {
            this.rewardBasedVideoAd.Show();
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }


    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        this.LoadRewardBasedAd();
    }


    public void HandleOnAdRewarded(object sender, Reward args)
    {
        PlayerPrefs.SetInt("moedas", moedas + 300);   

    }

}
