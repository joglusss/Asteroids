using Asteroids.Total;
using Cysharp.Threading.Tasks;
using Unity.Services.LevelPlay;
using UnityEngine;


namespace Asteroids.Ads
{
    public class AdsController : IAdsService
    {    
        private bool _isAdsEnabled = true;
        private LevelPlayRewardedAd _rewardedAd;
        private LevelPlayInterstitialAd _interstitialAd;
        private AdStat _rewardedAdStat = AdStat.Ended;
        
        public AdsController()
        {
            LevelPlay.ValidateIntegration();
            LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
            LevelPlay.OnInitFailed += SdkInitializationFailedEvent;
            
            LevelPlay.Init(AdConfig.AppKey);
            
            _rewardedAd = new LevelPlayRewardedAd(AdConfig.RewardedVideoAdUnitId);
            _interstitialAd = new LevelPlayInterstitialAd(AdConfig.InterstitalAdUnitId);

            _rewardedAd.OnAdLoadFailed += RewardedAdLoadFailedEvent;
            _rewardedAd.OnAdLoaded += RewardedAdLoadedEvent;
            _rewardedAd.OnAdClosed += OnAdClosedEvent;
            _rewardedAd.OnAdRewarded += OnAdRewardedEvent;
        }

        private void OnAdRewardedEvent(LevelPlayAdInfo info, LevelPlayReward reward) => _rewardedAdStat = AdStat.Ended;

        private void OnAdClosedEvent(LevelPlayAdInfo info) => _rewardedAdStat = AdStat.Closed;

        private void RewardedAdLoadedEvent(LevelPlayAdInfo info) => _rewardedAdStat = AdStat.Loaded;

        private void RewardedAdLoadFailedEvent(LevelPlayAdError error) => _rewardedAdStat = AdStat.FailedLoad;

        public async UniTask ShowInterstitialAd()
        {
            if (!_isAdsEnabled) return;
        
            _interstitialAd.LoadAd();
            
            await UniTask.WaitUntil(() => _interstitialAd.IsAdReady());
            
            _interstitialAd.ShowAd();
        }

        public async UniTask<AdStat> ShowRewardedAd()
        {
            if (!_isAdsEnabled) return AdStat.FailedLoad;
        
             _rewardedAd.LoadAd();

            await UniTask.WaitUntil(() => _rewardedAdStat == AdStat.Loaded || _rewardedAdStat == AdStat.FailedLoad);

            if (_rewardedAdStat == AdStat.FailedLoad) return _rewardedAdStat;
            
            _rewardedAd.ShowAd();

            await UniTask.WaitUntil(() => _rewardedAdStat == AdStat.Ended || _rewardedAdStat == AdStat.Closed);
            
            return _rewardedAdStat;
        }
        
        private void SdkInitializationFailedEvent(LevelPlayInitError error)
        {
            Debug.Log($"[LevelPlaySample] Received SdkInitializationFailedEvent with Error: {error}");
        }

        private void SdkInitializationCompletedEvent(LevelPlayConfiguration configuration)
        {
            Debug.Log($"[LevelPlaySample] Received SdkInitializationCompletedEvent with Config: {configuration}");
            _isAdsEnabled = true;
            
            _rewardedAd = new LevelPlayRewardedAd(AdConfig.RewardedVideoAdUnitId);
            _interstitialAd = new LevelPlayInterstitialAd(AdConfig.InterstitalAdUnitId);
        }
    }

}

