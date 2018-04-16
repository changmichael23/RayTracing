using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RayTracer
{
    class Program
    {
        const int width = 1280;
        const int height = 720;

        static void Main(string[] args)
        {
            byte[] frontBuffer = new byte[width * height * 4];
            byte[] backBuffer = new byte[width * height * 4];

            int totalRayCount = 0;
            RayCaster caster = new RayCaster(ref backBuffer, 20f,width,height,ref totalRayCount);

            for ( int j=0;j<height;++j)
            {
                for(int i=0;i<width;++i)
                {
                    int k = j * width *4 + i*4;
                    frontBuffer[k + 0] = backBuffer[k + 2];
                    frontBuffer[k + 1] = backBuffer[k + 1];

                    frontBuffer[k + 2] = backBuffer[k + 0];

                    frontBuffer[k + 3] = 255;

                }
            }
            byte[] tgaHeader =
            {
                0,0,2, // true color
                0,0,0,0,
                0,
                0,0,0,0,
                (byte)(width&0xff),(byte)((width&0xff00)>>8),
                (byte)(height & 0xff),(byte)((height & 0xff00) >> 8),
                32,0
            };

            using (BinaryWriter writer =
                new BinaryWriter(new FileStream("output.tga", FileMode.Create)))
            {
                writer.Write(tgaHeader);
                writer.Write(frontBuffer);
            }

        }
    }
}
