using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Vector3
    {
        public float x, y, z;
        public float Length => (float)Math.Sqrt(x * x + y * y + z * z);

        public Vector3(float a, float b, float c)
        {
            this.x = a;
            this.y = b;
            this.z = c;
        }

        public void Normalize()
        {
            x /= Length;
            y /= Length;
            z /= Length;
        }
        public static float Dot(Vector3 u, Vector3 v)
        {
            return u.x * v.x + u.y * v.y + u.z * v.z;
        }
        public static Vector3 Cross(Vector3 u, Vector3 v)
        {
            return new Vector3(u.y * v.z - u.z * v.y,
                u.z * v.x - u.x * v.z,
                u.x * v.y - u.y * v.x);
        }
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }
        public static Vector3 operator *(float b, Vector3 a)
        {
            return a * b;
        }
    }


}
