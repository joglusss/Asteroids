using System.Threading;
using Asteroids.Ads;
using Cysharp.Threading.Tasks;

namespace Asteroids.Total
{ 
    public interface IAdsService 
    {
        UniTask ShowInterstitialAd(CancellationToken cancellationToken);
        UniTask<AdStat> ShowRewardedAd(CancellationToken cancellationToken);
    }
}

