namespace Asteroids.Ads
{ 
    public static class AdConfig
    {
        public static string AppKey => GetAppKey();
        public static string InterstitalAdUnitId => GetInterstitialAdUnitId();
        public static string RewardedVideoAdUnitId => GetRewardedVideoAdUnitId();

        static string GetAppKey()
        {
            #if UNITY_ANDROID
                return "5943098";
            #elif UNITY_IPHONE
                return "5943099";
            #else
                return "unexpected_platform";
            #endif
        }

        static string GetInterstitialAdUnitId()
        {
            #if UNITY_ANDROID
                return "Interstitial_Android";
            #elif UNITY_IPHONE
                return "Interstitial_iOS";
            #else
                return "unexpected_platform";
            #endif
        }

        static string GetRewardedVideoAdUnitId()
        {
            #if UNITY_ANDROID
                return "Rewarded_Android";
            #elif UNITY_IPHONE
                return "Rewarded_iOS";
            #else
                return "unexpected_platform";
            #endif
        }
    }
}
