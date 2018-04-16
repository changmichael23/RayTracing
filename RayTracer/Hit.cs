using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Hit
    {
        public Vector3 _point;
        public Vector3 _normal;
        public float _t;
        public int _Material;

        public Vector3 eval(float t)
        {
            return _point + _normal * t;
        }
    }
}
