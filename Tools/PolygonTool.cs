using UnityEngine;

namespace _Main._Core.Scripts.Tools
{
    public class PolygonTool
    {
        private static RaycastHit2D m_hitFrom, m_hitTo;

        public static Vector2 GetDirection(Vector2 @from, Vector2 to, string layerMask = "Ground")
        {
            m_hitFrom = GetRaycastHit2D(from, layerMask);
            m_hitTo = GetRaycastHit2D(to, layerMask);
            
            return m_hitTo.point - m_hitFrom.point;
        }

        public static RaycastHit2D GetRaycastHit2D(Vector2 point, string layerMask = "Ground", float distance = 2500f)
        {
            return Physics2D.Raycast(point, Vector2.down, distance, LayerMask.GetMask(layerMask));
        }
        
        public static Vector2 Rotate(Vector2 v, float deltaAngle, bool angleInRadians = false)
        {
            if (!angleInRadians)
                deltaAngle *= Mathf.Deg2Rad;

            return new Vector2(
                v.x * Mathf.Cos(deltaAngle) - v.y * Mathf.Sin(deltaAngle),
                v.x * Mathf.Sin(deltaAngle) + v.y * Mathf.Cos(deltaAngle)
            );
        }

        public static float GetGroundAngle(Vector3 pos, float deltaOffset = 0.25f)
        {
            Vector2 pos1 = GetRaycastHit2D(pos + Vector3.left * deltaOffset).point;
            Vector2 pos2 = GetRaycastHit2D(pos + Vector3.right * deltaOffset).point;
            
            Vector2 direction = pos2 - pos1;

            float angle = Vector2.Angle(Vector2.right, direction.normalized);
            if (direction.y < 0)
            {
                angle = 360 - angle;
            }

            return angle;
        }

        public static Vector2 GetGroundPoint(Vector2 point)
        {
            return GetRaycastHit2D(point).point;
        }
    }
}