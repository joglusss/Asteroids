using UnityEngine;

namespace Asteroids.Helpers
{
    public class BorderSetting
    {
        public BorderSetting()
        { 
            Size = GameMath.CalculateBorderSize();
            Points = GameMath.CalculateBorderPoints();
            Center = GameMath.BorderCenter();
        }
    
        public Vector2 Size { get; private set; }
        public Vector2[] Points { get; private set; }
        public Vector2 Center { get; private set; }
    }
}


