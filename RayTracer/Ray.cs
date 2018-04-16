using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Ray
    {
        public Vector3 _origin;
        public Vector3 _direction;

        public Ray(Vector3 r, Vector3 d)
        {
            _origin = r;
            _direction = d;
        }
        public Vector3 eval(float t)
        {
            return _origin + _direction * t;
        }
    }
}
