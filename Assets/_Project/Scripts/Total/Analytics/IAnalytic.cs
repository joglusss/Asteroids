namespace Asteroids.Total
{
    public interface IAnalytic
    {
        public void SendGameStart();
        public void SendLaserUsed();
        public void SendGameStop(
            int ShootsCount,
            int LasersCount,
            int DestroyedAsteroidsCount,
            int DestroyedAliensCount);
    }
}


