using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPNTNSAR
{
    /// <summary>
    /// Base class for actual shapes
    /// </summary>
    public abstract class Shape
    {
        protected int x;
        /// <summary>
        /// Top left corner - X of a shape
        /// </summary>
        public int X => x;

        /// <summary>
        /// Top left corner - Y of a shape
        /// </summary>
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

        /// <summary>
        /// X offset when dragging shape
        /// </summary>
        public int moveOffsetX;
        /// <summary>
        /// Y offset when dragging shape
        /// </summary>
        public int moveOffsetY;

        private bool showNames = false;
        private static Font assFont;
        private static Font typeFont;

        /// <summary>
        /// Constructor when creating new shape from button click
        /// </summary>
        /// <param name="x">Center X of a container</param>
        /// <param name="y">Center Y of a container</param>
        /// <param name="filled">Whether it should be only line or filled object</param>
        /// <param name="color">Color of the line or the fill</param>
        public Shape(int x, int y, bool filled, Color color)
        {
            this.width = 100;
            this.height = 100;
            this.x = x - width / 2;
            this.y = y - height / 2;
            this.filled = filled;
            this.color = color;
            InitRuntimeValues();
        }

        private void InitRuntimeValues()
        {
            highlighted = false;
            pen = new Pen(color, 8f);
            brush = new SolidBrush(color);
            highlightPen = new Pen(Color.Black, 2f);
            highlightPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            highlightPen.DashPattern = new float[] { 5, 5 };
            assFont = new Font("Arial", 10, FontStyle.Regular);
            typeFont = new Font("Arial", 11, FontStyle.Bold);
        }

        /// <summary>
        /// Constructor serves as a creation of a shape from file
        /// </summary>
        /// <param name="data">Data from file</param>
        public Shape(ShapeDTO data)
        {
            this.width = data.width;
            this.height = data.height;
            this.x = data.x;
            this.y = data.y;
            this.filled = data.filled;
            this.color = Color.FromArgb(data.R, data.G, data.B);

            InitRuntimeValues();
        }

        /// <summary>
        /// Serves as drawing actual shape
        /// </summary>
        /// <param name="g">Needed graphics from System.Drawing</param>
        public void Draw(Graphics g)
        {
            DrawShape(g);
            DrawOutline(g);
        }

        protected void DrawOutline(Graphics g)
        {
            if (highlighted)
            {
                g.DrawRectangle(highlightPen, x, y, width, height);
            }
            if (showNames)
                DrawName(g);
        }

        protected virtual void DrawShape(Graphics g)
        {
            
        }

        private void DrawName(Graphics g)
        {
            string assText = GetType().Assembly.GetName().Name;
            string typeText = GetType().Name;
            SizeF assSize = g.MeasureString(assText, assFont);
            SizeF typeSize = g.MeasureString(typeText, typeFont);


            g.DrawString(assText, assFont, Brushes.DarkGray, x + width / 2 - assSize.Width / 2, y + height);
            g.DrawString(typeText, assFont, Brushes.Black, x + width / 2 - typeSize.Width / 2, y + height + assSize.Height + 2);
        }

        /// <summary>
        /// Method sets highlight to true to show dashed outline
        /// </summary>
        /// <param name="highlight">highlight activation</param>
        public void Highlight(bool highlight)
        {
            highlighted = highlight;
        }

        /// <summary>
        /// Method that checks if mouse is over the shape
        /// </summary>
        /// <param name="mx">Mouse position X</param>
        /// <param name="my">Mouse position Y</param>
        /// <returns>True if mouse is over the shape</returns>
        public abstract bool IsMouseOver(int mx, int my);
        
        public abstract void DoYourThing();

        public void SetLocation(int x, int y)
        {
            this.x = x - moveOffsetX;
            this.y = y - moveOffsetY;
        }

        public ShapeDTO GetDTO()
        {
            return new ShapeDTO(this);
        }

        public void ShowNames(bool @checked)
        {
            showNames = @checked;
        }

        public override string ToString()
        {
            return $"{GetType().Name} [{x},{y}] {color}";
        }

        public class ShapeDTO
        {
            public int x;
            public int y;

            public int width;
            public int height;

            public bool filled;
            public int R;
            public int G;
            public int B;

            public string shapeType;

            public ShapeDTO(Shape shape)
            {
                x = shape.x;
                y = shape.y;
                width = shape.width;
                height = shape.height;
                filled = shape.filled;
                R = shape.color.R;
                G = shape.color.G;
                B = shape.color.B;
                shapeType = shape.GetType().ToString();
            }

            public ShapeDTO() { } 
        }
    }
}
