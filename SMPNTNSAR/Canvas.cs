using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPNTNSAR
{
    public partial class Canvas : UserControl
    {
        private List<Shape> shapes = new List<Shape>();
        public IReadOnlyList<Shape> Shapes => shapes;

        private Shape selectedShape = null;

        public Canvas()
        {
            InitializeComponent();
        }

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
            Invalidate();
        }

        public void ClearShapes()
        {
            shapes.Clear();
            Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            if (shapes.Count == 0) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            shapes.ForEach(s => s.Draw(e.Graphics));
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var shape = shapes.FirstOrDefault(s => s.IsMouseOver(e.X, e.Y));
            if(shape != null)
            {
                if(selectedShape != null)
                {
                    selectedShape.Highlight(false);
                }
                selectedShape = shape;
                selectedShape.Highlight(true);
            } else
            {
                if(selectedShape != null)
                {
                    selectedShape.Highlight(false);
                    selectedShape = null;
                }
            }

            Invalidate();
        }
    }
}
