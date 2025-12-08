namespace Asteroids.Objects
{
    public class SmallAsteroid : Asteroid
    {
        protected override float _speed => Config.SmallAsteroidsSpeed;
    }
}    
