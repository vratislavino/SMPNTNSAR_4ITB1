using SMPNTNSAR;
using System.Drawing;

namespace MyNewShapes
{
    public class Triangle : Shape
    {
        public Triangle(ShapeDTO data) : base(data)
        {
        }

        public Triangle(int x, int y, bool filled, Color color) : base(x, y, filled, color)
        {
        }

        public override void DoYourThing()
        {
            throw new NotImplementedException();
        }

        public override bool IsMouseOver(int mx, int my)
        {
            return mx >= x && my >= y && mx <= x + width && my <= y + height;
        }

        public override void Draw(Graphics g)
        {
            Point[] points = new Point[3];
            points[0] = new Point(x, y + height);
            points[1] = new Point(x + width, y + height);
            points[2] = new Point(x + width / 2, y);

            if (filled)
            {
                g.FillPolygon(brush, points);
            }
            else
            {
                g.DrawPolygon(pen, points);
            }

            base.Draw(g);
        }
    }
}
