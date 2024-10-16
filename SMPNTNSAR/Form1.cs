using System;
using System.Reflection;
using System.Windows.Forms;

namespace SMPNTNSAR
{
    public partial class Form1 : Form
    {
        // TODO: Použít DP template na Draw, abychom zachovali poøadí obj->outline

        SaveLoadManager saveLoadManager = new SaveLoadManager();
        Dictionary<string, Assembly> assemblyDictionary 
            = new Dictionary<string, Assembly>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AddAssemblyTypesToComboBox(assembly);

            var assemblies = saveLoadManager.GetAssembliesFromAppData();
            assemblies.ForEach(ass => AddAssemblyTypesToComboBox(ass));

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void AddAssemblyTypesToComboBox(Assembly ass)
        {
            var types = ass.GetTypes();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(Shape)))
                {
                    assemblyDictionary.Add(type.ToString(), ass);
                    comboBox1.Items.Add(type);
                }
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
            if (typeOfShape == null)
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var path = saveFileDialog.FileName;
                saveLoadManager.SaveShapesNonAsync(path, canvas1.Shapes);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog.FileName;
                var loadedShapes = saveLoadManager.LoadShapesNonAsync(path, assemblyDictionary);

                canvas1.ClearShapes();
                loadedShapes.ForEach(s => canvas1.AddShape(s));
            }
        }

        private void addMoreShapesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Shapes DLL (*.dll)|*.dll";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog.FileName;
                if (saveLoadManager.CopyDllToAppData(path))
                {
                    Assembly ass = Assembly.LoadFrom(path);
                    AddAssemblyTypesToComboBox(ass);
                } else
                {
                    MessageBox.Show("Nepodaøilo se naèíst DLL, zkontrolujte Log");
                }
            }
        }
    }
}
