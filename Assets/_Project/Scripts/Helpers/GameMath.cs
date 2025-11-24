using UnityEngine;

namespace Asteroids.Helpers {
    public static class GameMath
    {
        public static Vector2 CalculateBorderSize() 
        {
            Camera camera = Camera.main;
            float halfHeight = camera.orthographicSize;
            float halfWidth = camera.aspect * halfHeight;
            camera.transform.position = new Vector3(halfWidth, halfHeight, camera.transform.position.z);
            return new Vector2(halfWidth * 2.0f, halfHeight * 2.0f);
        }

        public static Vector2 BorderCenter() => Camera.main.transform.position;

        public static Vector2[] CalculateBorderPoints()
        {
            Vector2 halfBorderSize = CalculateBorderSize() / 2.0f;
            Vector2 center = BorderCenter();
            Vector2[] points = new Vector2[4];

            points[0] = new Vector2(-halfBorderSize.x, -halfBorderSize.y) + center;
            points[1] = new Vector2(-halfBorderSize.x, halfBorderSize.y) + center;
            points[2] = new Vector2(halfBorderSize.x, halfBorderSize.y) + center;
            points[3] = new Vector2(halfBorderSize.x, -halfBorderSize.y) + center;

            return points;
        }

        
        public static void TeleportToBorder(Rigidbody2D rb, BorderSetting borderSetting) => TeleportToBorder(rb, borderSetting.Center, borderSetting.Points);
    
        public static void TeleportToBorder(Rigidbody2D rb, Vector2 center, Vector2[] borderPoints)
        {
            if (LineIntersect(borderPoints[0], borderPoints[1], center, rb.position, out Vector2 IntersectPoint))
                rb.position = IntersectPoint + rb.linearVelocity *  Time.deltaTime;

            if (LineIntersect(borderPoints[1], borderPoints[2], center, rb.position, out IntersectPoint))
                rb.position = IntersectPoint + rb.linearVelocity * Time.deltaTime;

            if (LineIntersect(borderPoints[2], borderPoints[3], center, rb.position, out IntersectPoint))
                rb.position = IntersectPoint + rb.linearVelocity * Time.deltaTime;

            if (LineIntersect(borderPoints[3], borderPoints[0], center, rb.position, out IntersectPoint))
                rb.position = IntersectPoint + rb.linearVelocity * Time.deltaTime;
        }


        public static Vector2 TeleportToBorder(Vector2 position, Vector2 center, Vector2[] borderPoints)
        {
            if (LineIntersect(borderPoints[0], borderPoints[1], center, position, out Vector2 IntersectPoint))
                return IntersectPoint;

            if (LineIntersect(borderPoints[1], borderPoints[2], center, position, out IntersectPoint))
                return IntersectPoint;

            if (LineIntersect(borderPoints[2], borderPoints[3], center, position, out IntersectPoint))
                return IntersectPoint;

            if (LineIntersect(borderPoints[3], borderPoints[0], center, position, out IntersectPoint))
                return IntersectPoint;

            return IntersectPoint;
        }

        public static bool LineIntersect( Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, out Vector2 IntersectionPoint)
        {
            IntersectionPoint = Vector2.zero;
            double denominator = (p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y);

            if (denominator == 0)
            {
                return false;
            }

            double t = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denominator;
            double u = -((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denominator;

            if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
            {
                double x = p1.x + t * (p2.x - p1.x);
                double y = p1.y + t * (p2.y - p1.y);

                IntersectionPoint = new Vector2((float)x, (float)y);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

   

