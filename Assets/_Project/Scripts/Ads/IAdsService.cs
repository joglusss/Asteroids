using Asteroids.Ads;
using Cysharp.Threading.Tasks;

namespace Asteroids.Total
{ 
    public interface IAdsService 
    {
        UniTask ShowInterstitialAd();
        UniTask<AdStat> ShowRewardedAd();
    }
}

