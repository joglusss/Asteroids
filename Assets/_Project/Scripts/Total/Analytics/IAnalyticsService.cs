namespace Asteroids.Total
{
    public interface IAnalyticsService
    {
        public void SendGameStart();
        public void SendLaserUsed();
        public void SendGameStop(
            int shootsCount,
            int lasersCount,
            int destroyedAsteroidsCount,
            int destroyedAliensCount);
    }
}


