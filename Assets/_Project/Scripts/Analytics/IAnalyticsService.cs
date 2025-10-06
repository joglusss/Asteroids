namespace Asteroids.Total
{
    public interface IAnalyticsService
    {
        public void SendGameStart();
        public void SendLaserUsed();
        public void SendGameStop(StopGameLogParameters parameters);
    }
}


