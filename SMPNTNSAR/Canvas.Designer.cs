namespace SMPNTNSAR
{
    partial class Canvas
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            deleteToolStripMenuItem = new ToolStripMenuItem();
            moveToBackToolStripMenuItem = new ToolStripMenuItem();
            toBackToolStripMenuItem = new ToolStripMenuItem();
            toFrontToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem, moveToBackToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(211, 80);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(210, 24);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // moveToBackToolStripMenuItem
            // 
            moveToBackToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toBackToolStripMenuItem, toFrontToolStripMenuItem });
            moveToBackToolStripMenuItem.Name = "moveToBackToolStripMenuItem";
            moveToBackToolStripMenuItem.Size = new Size(210, 24);
            moveToBackToolStripMenuItem.Text = "Move";
            // 
            // toBackToolStripMenuItem
            // 
            toBackToolStripMenuItem.Name = "toBackToolStripMenuItem";
            toBackToolStripMenuItem.Size = new Size(224, 26);
            toBackToolStripMenuItem.Text = "To Back";
            toBackToolStripMenuItem.Click += toBackToolStripMenuItem_Click;
            // 
            // toFrontToolStripMenuItem
            // 
            toFrontToolStripMenuItem.Name = "toFrontToolStripMenuItem";
            toFrontToolStripMenuItem.Size = new Size(224, 26);
            toFrontToolStripMenuItem.Text = "To Front";
            toFrontToolStripMenuItem.Click += toFrontToolStripMenuItem_Click;
            // 
            // Canvas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            DoubleBuffered = true;
            Name = "Canvas";
            Size = new Size(1198, 653);
            Paint += Canvas_Paint;
            MouseDown += Canvas_MouseDown;
            MouseMove += Canvas_MouseMove;
            MouseUp += Canvas_MouseUp;
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem moveToBackToolStripMenuItem;
        private ToolStripMenuItem toBackToolStripMenuItem;
        private ToolStripMenuItem toFrontToolStripMenuItem;
    }
}
