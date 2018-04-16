using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Sphere
    {
        public Vector3 _center;
        public float _radius;

        public Sphere(Vector3 center, float radius)
        {
            _center = center;
            _radius = radius;

        }
        public bool Intersect(Ray ray, ref Hit hit, float tmin = 0f, float tmax = float.MaxValue)
        {
            Vector3 oc = ray._origin - _center;
            float b = Vector3.Dot(oc, ray._direction);

            float c = Vector3.Dot(oc, oc) - _radius * _radius;
            float discriminant = b * b - c;

            if(discriminant > 0f)
            {
                float sqrD = (float)Math.Sqrt(discriminant);
                float t = (-b - sqrD);
                if(t<tmax && t> tmin)
                {
                    hit._point = ray.eval(t);
                    hit._normal = (hit._point - _center);
                    hit._normal.Normalize();
                    hit._t = t;
                    return true;
                }
                t = (-b + sqrD);
                if(t< tmax && t> tmin)
                {
                    hit._point = ray.eval(t);
                    hit._t = t;
                    return true;
                }
                return true;
            }
            return false;

        }
    }
}
