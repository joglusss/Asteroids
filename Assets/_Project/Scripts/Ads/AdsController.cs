using System;
using System.Threading;
using Asteroids.Total;
using Cysharp.Threading.Tasks;
using Unity.Services.LevelPlay;
using UnityEngine;
using Zenject;


namespace Asteroids.Ads
{
    public class AdsController : IAdsService, IDisposable
    {    
        private bool _isAdsEnabled;
        private LevelPlayRewardedAd _rewardedAd;
        private LevelPlayInterstitialAd _interstitialAd;
        private AdStat _rewardedAdStat = AdStat.Ended;
        
        [Inject] PurchasesService _purchasesService;
        
        public AdsController()
        {
            LevelPlay.ValidateIntegration();
            LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
            LevelPlay.OnInitFailed += SdkInitializationFailedEvent;
            
            LevelPlay.Init(AdConfig.AppKey);
            
            _rewardedAd = new LevelPlayRewardedAd(AdConfig.RewardedVideoAdUnitId);
            _interstitialAd = new LevelPlayInterstitialAd(AdConfig.InterstitialAdUnitId);

            _rewardedAd.OnAdLoadFailed += RewardedAdLoadFailedEvent;
            _rewardedAd.OnAdLoaded += RewardedAdLoadedEvent;
            _rewardedAd.OnAdClosed += OnAdClosedEvent;
            _rewardedAd.OnAdRewarded += OnAdRewardedEvent;
        }

        public void Dispose()
        {
            LevelPlay.OnInitSuccess -= SdkInitializationCompletedEvent;
            LevelPlay.OnInitFailed -= SdkInitializationFailedEvent;
        
            _rewardedAd.OnAdLoadFailed -= RewardedAdLoadFailedEvent;
            _rewardedAd.OnAdLoaded -= RewardedAdLoadedEvent;
            _rewardedAd.OnAdClosed -= OnAdClosedEvent;
            _rewardedAd.OnAdRewarded -= OnAdRewardedEvent;
        }
        
        private void OnAdRewardedEvent(LevelPlayAdInfo info, LevelPlayReward reward) => _rewardedAdStat = AdStat.Ended;

        private void OnAdClosedEvent(LevelPlayAdInfo info) => _rewardedAdStat = AdStat.Closed;

        private void RewardedAdLoadedEvent(LevelPlayAdInfo info) => _rewardedAdStat = AdStat.Loaded;

        private void RewardedAdLoadFailedEvent(LevelPlayAdError error) => _rewardedAdStat = AdStat.FailedLoad;

        public async UniTask ShowInterstitialAd(CancellationToken token)
        {
            if (!_isAdsEnabled) return;

            if(_purchasesService.CheckProduct(ProductID.NoAds)) return;
        
            _interstitialAd.LoadAd();
            
            await UniTask.WaitUntil(() => _interstitialAd.IsAdReady(), cancellationToken: token);
            
            _interstitialAd.ShowAd();
        }

        public async UniTask<AdStat> ShowRewardedAd(CancellationToken token)
        {
            if (!_isAdsEnabled) return AdStat.FailedLoad;
        
             _rewardedAd.LoadAd();

            await UniTask.WaitUntil(() => _rewardedAdStat == AdStat.Loaded || _rewardedAdStat == AdStat.FailedLoad, cancellationToken: token);

            if (_rewardedAdStat == AdStat.FailedLoad) return _rewardedAdStat;
            
            _rewardedAd.ShowAd();

            await UniTask.WaitUntil(() => _rewardedAdStat == AdStat.Ended || _rewardedAdStat == AdStat.Closed, cancellationToken: token);
            
            return _rewardedAdStat;
        }
        
        private void SdkInitializationFailedEvent(LevelPlayInitError error)
        {
            Debug.Log($"AdsController initialization failed: {error}");
            _isAdsEnabled = false;
        }

        private void SdkInitializationCompletedEvent(LevelPlayConfiguration configuration)
        {
            Debug.Log($"AdsController Initialized: {configuration}");
            _isAdsEnabled = true;
            
            _rewardedAd = new LevelPlayRewardedAd(AdConfig.RewardedVideoAdUnitId);
            _interstitialAd = new LevelPlayInterstitialAd(AdConfig.InterstitialAdUnitId);
        }
    }

}

