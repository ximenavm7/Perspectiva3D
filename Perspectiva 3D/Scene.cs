using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Perspectiva_3D
{
    public class Scene
    {
        private Figure figure;
        public Pen pen = new Pen(Color.FromArgb(148, 0, 70), 2);
        public int angle;

        public Scene(Figure figures)
        {
            figure = figures;
        }

        public void CartesianPlane(Graphics g, int width, int height)
        {
            g.DrawLine(new Pen(Color.Gray), 0, (height / 2), width, (height / 2));
            g.DrawLine(new Pen(Color.Gray), (width / 2), 0, (width / 2), height);
        }

        public void Draw(Graphics graphics, int viewWidth, int viewHeight)
        {
            graphics.Clear(Color.FromArgb(80, 80, 80));

            // Draw cartesian plane
            CartesianPlane(graphics, viewWidth, viewHeight);


            var projected = new Vertex[figure.Vertices.Length];
            for (var i = 0; i < figure.Vertices.Length; i++)
            {
                var vertex = figure.Vertices[i];

                var transformed = vertex.FixedZ(i);

                if (angle > 0 && angle < 90)
                {   transformed = vertex.FixedZ(angle);}

                else if (angle > 90 && angle < 180)
                {   transformed = vertex.FixedX(angle);}

                else if (angle > 180 && angle < 270)
                {   transformed = vertex.FixedY(angle);}
                else
                {   transformed = vertex.FixedX(angle).FixedY(angle).FixedZ(angle);}

                projected[i] = transformed.Project(viewWidth, viewHeight, 512, 6);
            }

            for (var j = 0; j < 6; j++)
            {
                graphics.DrawLine(pen,
                    (float)projected[figure.Faces[j, 0]].X,
                    (float)projected[figure.Faces[j, 0]].Y,
                    (float)projected[figure.Faces[j, 1]].X,
                    (float)projected[figure.Faces[j, 1]].Y);

                graphics.DrawLine(pen,
                    (float)projected[figure.Faces[j, 1]].X,
                    (float)projected[figure.Faces[j, 1]].Y,
                    (float)projected[figure.Faces[j, 2]].X,
                    (float)projected[figure.Faces[j, 2]].Y);

                graphics.DrawLine(pen,
                    (float)projected[figure.Faces[j, 2]].X,
                    (float)projected[figure.Faces[j, 2]].Y, 
                    (float)projected[figure.Faces[j, 3]].X, 
                    (float)projected[figure.Faces[j, 3]].Y);

                graphics.DrawLine(pen,
                    (float)projected[figure.Faces[j, 3]].X, 
                    (float)projected[figure.Faces[j, 3]].Y,
                    (float)projected[figure.Faces[j, 0]].X, 
                    (float)projected[figure.Faces[j, 0]].Y);
            }
            angle++;
        }
    }
}
