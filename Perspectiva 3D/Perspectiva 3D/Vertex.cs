using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perspectiva_3D
{
    public class Vertex
    {
        public Vertex(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vertex FixedX(int angle)
        {
            var rad = angle * Math.PI / 180;
            var cosa = Math.Cos(rad);
            var sina = Math.Sin(rad);
            var yn = (float)((this.Y * cosa) - (this.Z * sina));
            var zn = (float)((this.Y * sina) + (this.Z * cosa));
            return new Vertex(this.X, yn, zn);
        }

        public Vertex FixedY(int angle)
        {
            var rad = angle * Math.PI / 180;
            var cosa = Math.Cos(rad);
            var sina = Math.Sin(rad);
            var Zn = (float)((this.Z * cosa) - (this.X * sina));
            var Xn = (float)((this.Z * sina) + (this.X * cosa));
            return new Vertex(Xn, this.Y, Zn);
        }

        public Vertex FixedZ(int angle)
        {
            var rad = angle * Math.PI / 180;
            var cosa = Math.Cos(rad);
            var sina = Math.Sin(rad);
            var Xn = (float)((this.X * cosa) - (this.Y * sina));
            var Yn = (float)((this.X * sina) + (this.Y * cosa));
            return new Vertex(Xn, Yn, this.Z);
        }

        public Vertex Project(int viewWidth, int viewHeight, int fov, int viewDistance)
        {
            var factor = fov / (viewDistance + this.Z);
            var Xn = this.X * factor + viewWidth / 2;
            var Yn = this.Y * factor + viewHeight / 2;
            return new Vertex(Xn, Yn, this.Z);
        }
    }
}
