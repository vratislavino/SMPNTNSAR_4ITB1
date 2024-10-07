using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPNTNSAR
{
    public class Square : Shape
    {
        public Square(int x, int y, bool filled, Color color) : base(x, y, filled, color)
        {
        }

        public override void DoYourThing()
        {
            throw new NotImplementedException();
        }

        public override void Draw(Graphics g)
        {
            if(filled)
            {
                g.FillRectangle(brush, x, y, width, height);
            } else
            {
                g.DrawRectangle(pen, x, y, width, height);
            }

            base.Draw(g);
        }

        public override bool IsMouseOver(int mx, int my)
        {
            return mx >= x && my >= y && mx <= x + width && my <= y + height;
        }
    }
}
