namespace Asteroids.Objects
{
    public class SmallAsteroid : Asteroid
    {
        protected override float Speed => Config.SmallAsteroidsSpeed;
    }
}    
