using UnityEngine;

namespace Kutie.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ProjectXZ(this Vector3 v) => new(v.x, 0, v.z);
        public static Vector3 ProjectXY(this Vector3 v) => new(v.x, v.y, 0);
        public static Vector3 ProjectYZ(this Vector3 v) => new(0, v.y, v.z);

        public static Vector3 Abs(this Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }


        public static Vector2 XY(this Vector3 v) => new(v.x, v.y);
        public static Vector2 XZ(this Vector3 v) => new(v.x, v.z);
        public static Vector2 YZ(this Vector3 v) => new(v.y, v.z);


        public static Vector2 WithX(this Vector2 v, float x) => new(x, v.y);
        public static Vector2 WithY(this Vector2 v, float y) => new(v.x, y);

        public static Vector3 WithZ(this Vector2 v, float z) => new(v.x, v.y, z);

        public static Vector3 WithX(this Vector3 v, float x) => new(x, v.y, v.z);
        public static Vector3 WithY(this Vector3 v, float y) => new(v.x, y, v.z);
        public static Vector3 WithZ(this Vector3 v, float z) => new(v.x, v.y, z);

        public static Vector3 WithXY(this Vector3 v, Vector2 xy) => new(xy.x, xy.y, v.z);
        public static Vector3 WithXZ(this Vector3 v, Vector2 xz) => new(xz.x, v.y, xz.y);
        public static Vector3 WithYZ(this Vector3 v, Vector2 yz) => new(v.x, yz.x, yz.y);

        public static float Volume(this Vector3 length) => length.x * length.y * length.z;
        public static int Volume(this Vector3Int length) => length.x * length.y * length.z;

        public static Vector2 Hammard(this Vector2 a, Vector2 b) => new(a.x * b.x, a.y * b.y);
        public static Vector2 Hammard(this Vector2 a, Vector2Int b) => new(a.x * b.x, a.y * b.y);
        public static Vector2Int Hammard(this Vector2Int a, Vector2Int b) => new(a.x * b.x, a.y * b.y);
        public static Vector2 Hammard(this Vector2Int a, Vector2 b) => new(a.x * b.x, a.y * b.y);

        public static Vector3 Hammard(this Vector3 a, Vector3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
        public static Vector3 Hammard(this Vector3 a, Vector3Int b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
        public static Vector3Int Hammard(this Vector3Int a, Vector3Int b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
        public static Vector3 Hammard(this Vector3Int a, Vector3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);

        public static Vector2 Divide(this Vector2 a, Vector2 b) => new(a.x / b.x, a.y / b.y);
        public static Vector2 Divide(this Vector2 a, Vector2Int b) => new(a.x / b.x, a.y / b.y);
        public static Vector2Int Divide(this Vector2Int a, Vector2Int b) => new(a.x / b.x, a.y / b.y);
        public static Vector2 Divide(this Vector2Int a, Vector2 b) => new(a.x / b.x, a.y / b.y);

        public static Vector3 Divide(this Vector3 a, Vector3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
        public static Vector3 Divide(this Vector3 a, Vector3Int b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
        public static Vector3Int Divide(this Vector3Int a, Vector3Int b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
        public static Vector3 Divide(this Vector3Int a, Vector3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);

        public static Vector3Int Rem(this Vector3Int a, Vector3Int b) => new(a.x % b.x, a.y % b.y, a.z % b.z);
        public static Vector3Int Rem(this Vector3Int a, int b) => new(a.x % b, a.y % b, a.z % b);

        public static Vector3 Rem(this Vector3 a, Vector3 b) => new(a.x % b.x, a.y % b.y, a.z % b.z);
        public static Vector3 Rem(this Vector3 a, float b) => new(a.x % b, a.y % b, a.z % b);
    }
}
