using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsMan : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener
{
    public const string GAME_ID = "5100715";
    private const string BANNER_PLACEMENT = "Banner_Android";
    private const string VIDEO_PLACEMENT = "Interstitial_Android";

    [SerializeField] private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    private bool testMode = false;
    private bool showBanner = false;

    private bool ready = false;

    private void Start()
    {
        Initialize();
    }

    void IUnityAdsInitializationListener.OnInitializationComplete()
    {
        if (!showBanner)
            ToggleBanner();
        if (!ready)
            LoadNonRewardedAd();
    }

    void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message)
    { }

    void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
    {
        ready = true;
    }

    void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) 
    {
        ready = false;
    }

    public void Initialize()
    {
        if (Advertisement.isSupported)
        {
            Debug.Log(Application.platform + " supported by Advertisement");
        }

        Advertisement.Initialize(GAME_ID, testMode, this);
    }

    public void ToggleBanner()
    {
        showBanner = !showBanner;

        if (showBanner)
        {
            Advertisement.Banner.SetPosition(bannerPosition);
            Advertisement.Banner.Show(BANNER_PLACEMENT);
        }
        else
        {
            Advertisement.Banner.Hide(false);
        }
    }

    public void LoadNonRewardedAd()
    {
        Advertisement.Load(VIDEO_PLACEMENT, this);
    }

    public void ShowNonRewardedAd()
    {
        Advertisement.Show(VIDEO_PLACEMENT);
        ready = false;
        LoadNonRewardedAd();
    }

    public bool isReady()
    {
        return ready;
    }
}
