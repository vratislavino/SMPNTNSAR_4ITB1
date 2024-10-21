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
        public event Action ShapesChanged;

        private List<Shape> shapes = new List<Shape>();
        public IReadOnlyList<Shape> Shapes => shapes;

        private Shape selectedShape = null;
        private bool dragging = false;

        public Canvas()
        {
            InitializeComponent();
        }

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
            Invalidate();
            ShapesChanged?.Invoke();
        }

        public void ClearShapes()
        {
            shapes.Clear();
            Invalidate();
            ShapesChanged?.Invoke();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            if (shapes.Count == 0) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            shapes.ForEach(s => s.Draw(e.Graphics));
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selectedShape != null)
                {
                    dragging = true;
                    selectedShape.moveOffsetX = e.X - selectedShape.X;
                    selectedShape.moveOffsetY = e.Y - selectedShape.Y;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, e.X, e.Y);
                DisableActions();
            }
        }

        private void DisableActions()
        {
            if (selectedShape == null) return;
            toFrontToolStripMenuItem.Enabled =
                toBackToolStripMenuItem.Enabled = 
                true;

            if (shapes.IndexOf(selectedShape) == shapes.Count - 1)
            {
                toFrontToolStripMenuItem.Enabled = false;
            }
            
            if (shapes.IndexOf(selectedShape) == 0)
            {
                toBackToolStripMenuItem.Enabled = false;
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;

            ShapesChanged?.Invoke();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                if (selectedShape != null)
                    selectedShape.SetLocation(e.X, e.Y);
            }
            else
            {
                var shape = shapes.FirstOrDefault(s => s.IsMouseOver(e.X, e.Y));
                if (shape != null)
                {
                    if (selectedShape != null)
                    {
                        selectedShape.Highlight(false);
                    }
                    selectedShape = shape;
                    selectedShape.Highlight(true);
                }
                else
                {
                    if (selectedShape != null)
                    {
                        selectedShape.Highlight(false);
                        selectedShape = null;
                    }
                }
            }

            Invalidate();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                shapes.Remove(selectedShape);
                selectedShape = null;
                Invalidate();
                ShapesChanged?.Invoke();
            }
        }

        private void toBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedShape == null) return;
            int index = shapes.IndexOf(selectedShape);
            index--;
            if (index < 0) return;

            shapes.Remove(selectedShape);
            shapes.Insert(index, selectedShape);

            ShapesChanged?.Invoke();
        }

        private void toFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedShape == null) return;

            int index = shapes.IndexOf(selectedShape);
            index++;
            if (index > shapes.Count) return;

            shapes.Remove(selectedShape);
            shapes.Insert(index, selectedShape);

            ShapesChanged?.Invoke();
        }
    }
}
