namespace SMPNTNSAR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog1.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            canvas1.AddShape(new Circle(
                canvas1.Width / 2,
                canvas1.Height / 2,
                checkBox1.Checked,
                button1.BackColor)
                );
        }

        private void button2_Click(object sender, EventArgs e)
        {
            canvas1.ClearShapes();
        }
    }
}
