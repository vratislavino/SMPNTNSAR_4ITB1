using System.Reflection;

namespace SMPNTNSAR
{
    public partial class Form1 : Form
    {
        // TODO: Použít DP template na Draw, abychom zachovali poøadí obj->outline

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Type typ = typeof(Shape);
            Assembly assembly = Assembly.GetExecutingAssembly();
            
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typ))
                {
                    comboBox1.Items.Add(type);
                }
            }

            if(comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
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
            var typeOfShape = (Type)comboBox1.SelectedItem;
            if(typeOfShape == null)
            {
                MessageBox.Show(
                    "Choose vadlid type of shape!",
                    "Error!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }

            var newShape = Activator.CreateInstance(
                typeOfShape,
                canvas1.Width / 2,
                canvas1.Height / 2,
                checkBox1.Checked,
                button1.BackColor);
            
            canvas1.AddShape((Shape)newShape);
            
            /*
            canvas1.AddShape(new Square(
                canvas1.Width / 2,
                canvas1.Height / 2,
                checkBox1.Checked,
                button1.BackColor)
                );*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            canvas1.ClearShapes();
        }

    }
}
