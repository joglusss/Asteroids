using UnityEngine;

namespace Asteroids.Helpers {
    public static class GameMath
    {
        public static bool IsOutCanvas(Vector2 position, out Vector2 wall, Vector2 borderSize)
        {
            wall = Vector2.zero;
            float deltaDistance = 0;

            if (position.x < 0.0f)
            {
                deltaDistance = -position.x;
                wall = Vector2.left;
            }

            if (position.x > borderSize.x && deltaDistance < (position.x - borderSize.x))
            {
                deltaDistance = position.x - borderSize.x;
                wall = Vector2.right;
            }

            if (position.y < 0.0f && deltaDistance < -position.y)
            {
                deltaDistance = -position.y;
                wall = Vector2.down;
            }

            if (position.y > borderSize.y && deltaDistance < (position.y - borderSize.y))
            {
                wall = Vector2.up;
            }


            return wall != Vector2.zero;
        }

        public static void BorderTeleport(Rigidbody2D rb, Vector2 wall, Vector2 borderSize)
        {
            if (wall == Vector2.left)
            {
                rb.position = BorderTeleportPosition(Vector2.zero, Vector2.up * borderSize.y, rb.position, borderSize);
                return;
            }
            if (wall == Vector2.right)
            {
                rb.position = BorderTeleportPosition(Vector2.right * borderSize.x, borderSize, rb.position, borderSize);
                return;
            }
            if (wall == Vector2.down)
            {
                rb.position = BorderTeleportPosition(Vector2.zero, Vector2.right * borderSize.x, rb.position, borderSize);
                return;
            }
            if (wall == Vector2.up)
            {
                rb.position = BorderTeleportPosition(Vector2.up * borderSize.y, borderSize, rb.position, borderSize);
                return;
            }

        }

        public static Vector2 BorderTeleportPosition(Vector2 WallPoint1, Vector2 WallPoint2, Vector2 objectPoint, Vector2 borderSize)
        {

            Vector2 intersectPoint = Vector2.zero;
            Vector2 centerPoint = borderSize / 2.0f;

            intersectPoint = LineIntersect(centerPoint, objectPoint, WallPoint1, WallPoint2);

            Vector2 dirToShip = objectPoint - centerPoint;
            Vector2 dirToIntersect = intersectPoint - centerPoint;

            float distanceOutScreen = dirToShip.magnitude - 2.0f * (dirToShip.magnitude - dirToIntersect.magnitude);

            Vector2 newObjectPosition = centerPoint + dirToShip.normalized * -distanceOutScreen;

            return newObjectPosition;
        }

        public static Vector2 LineIntersect(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
        {
            float a1 = B.y - A.y;
            float b1 = A.x - B.x;
            float c1 = a1 * (A.x) + b1 * (A.y);

            float a2 = D.y - C.y;
            float b2 = C.x - D.x;
            float c2 = a2 * (C.x) + b2 * (C.y);

            float denominator = (D.y - C.y) * (B.x - A.x) - (D.x - C.x) * (B.y - A.y);

            if (denominator != 0f)
            {
                float x = (b2 * c1 - b1 * c2) / denominator;
                float y = (a1 * c2 - a2 * c1) / denominator;

                return new Vector2(x, y);
            }

            return Vector2.zero;

        }
    }

    
}

