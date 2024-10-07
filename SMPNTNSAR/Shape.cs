using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPNTNSAR
{
    public abstract class Shape
    {
        protected int x;
        public int X => x;

        protected int y;
        public int Y => y;

        protected int width;
        protected int height;

        protected bool filled;
        protected Color color;
        protected Pen pen;
        protected Brush brush;

        protected bool highlighted;
        protected static Pen highlightPen;

        public int moveOffsetX;
        public int moveOffsetY;

        public Shape(int x, int y, bool filled, Color color)
        {
            this.width = 100;
            this.height = 100;
            this.x = x - width / 2;
            this.y = y - height / 2;
            this.filled = filled;
            this.color = color;

            highlighted = false;
            pen = new Pen(color, 8f);
            brush = new SolidBrush(color);
            highlightPen = new Pen(Color.Black, 2f);
            highlightPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            highlightPen.DashPattern = new float[] { 5, 5 };
        }

        public virtual void Draw(Graphics g)
        {
            if(highlighted)
            {
                g.DrawRectangle(highlightPen, x, y, width, height);
            }
        }

        public void Highlight(bool highlight)
        {
            highlighted = highlight;
        }

        public abstract bool IsMouseOver(int mx, int my);
        public abstract void DoYourThing();

        public void SetLocation(int x, int y)
        {
            this.x = x - moveOffsetX;
            this.y = y - moveOffsetY;
        }
    }
}
