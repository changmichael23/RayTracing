using System;

namespace RayTracer
{
    public class RayCaster
    {
        private float _fovY;
        private float _aspectRatio;
        private float _invWidth;
        private float _invHeight;
        private float _halfWidth;
        private float _halfHeight;
        private Vector3 _lightPosition = new Vector3(0f, 10f, -50f);

        public Vector3 DiffuseLighting(Vector3 N, Vector3 L)
        {
           return new Vector3(0f, 0f, 1f) * Math.Max(0f, Vector3.Dot(N, L));
        }

        public Vector3 Trace(Ray ray, Sphere sphere)
        {
            Vector3 ambient = new Vector3(0.2f,0.8f,0.5f);
            Hit hit = new Hit();
            if(sphere.Intersect(ray, ref hit,0.001f))
            {
                Vector3 LightDir = _lightPosition - hit._point;
                LightDir.Normalize();
                float attenuation = 1f / (LightDir.Length * LightDir.Length);
                LightDir.Normalize();
                return  attenuation * DiffuseLighting(hit._normal, LightDir);
            }
            return ambient;
        }
        public RayCaster(ref byte[] backBuffer, float fov, int width, int height, ref int totalRayCount )
        {
            _fovY = fov;
            _aspectRatio = (float)width/(float)height;
            _invWidth = 1f / (float)width;
            _invHeight = 1f / (float)height;
            _halfWidth = 0.5f * width;
            _halfHeight = 0.5f * height;

            // position and direction of the camera
            Vector3 position = new Vector3(0f, 0f, -1f);
            Vector3 direction = new Vector3(0f, 0f, 1f);

            // left bottom corner position of the camera
            Vector3 up = new Vector3(0f, 1f, 0f);
            Vector3 U = Vector3.Cross(up, direction); //  right
            Vector3 V = Vector3.Cross(direction, U);  //  up
            Vector3 bottomLeft = position - U * _halfWidth - V * _halfHeight;

            float radFox = (float)(Math.PI / 180f) * (_fovY / 2f);
            float angle = (float)Math.Tan(_fovY);

            Vector3 xInc = (U * 2f ) * _invWidth * angle * _aspectRatio;
            Vector3 yInc = (V * 2f ) * _invHeight * angle;

            Sphere sphere = new Sphere(new Vector3(0f,0f, 10f),1f);
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    // Generate ray

                    float s = (2f * x * _invWidth - 1f) * angle * _aspectRatio;
                    float t = (2f * y * _invHeight - 1f) * angle;
                    Vector3 origin = new Vector3(s,t,0f);
                    Vector3 dir = origin - position;
                    dir.z += 1f;
                    dir.Normalize();
                    Ray ray = new Ray(origin, dir);
                    Vector3 color = Trace(ray,sphere);
                    int k = y * width * 4 + x * 4;
                    backBuffer[k + 0] = (byte)(color.x * 255f);
                    backBuffer[k + 1] = (byte)(color.y * 255f);
                    backBuffer[k + 2] = (byte)(color.z * 255f);
                    //backBuffer[k + 3] = (byte)(255f);

                }
            }

        }
    }
}