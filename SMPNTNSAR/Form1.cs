using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SMPNTNSAR
{
    public partial class Form1 : Form
    {
        SaveLoadManager saveLoadManager = new SaveLoadManager();
        Dictionary<string, Assembly> assemblyDictionary
            = new Dictionary<string, Assembly>();

        public Form1()
        {
            InitializeComponent();
            canvas1.ShapesChanged += OnShapesChanged;
        }

        private void OnShapesChanged()
        {
            listBox1.Items.Clear();
            foreach (var shape in canvas1.Shapes)
            {
                listBox1.Items.Add(shape.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AddAssemblyTypesToComboBox(assembly);


            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void AddAssemblyTypesToComboBox(Assembly ass)
        {
            Type[] types;
            try
            {
                types = ass.GetTypes();
            }
            catch (Exception e)
            {
                Debug.Write("Unable to load assembly " + ass.FullName + " : " + e.Message);
                return;
            }

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
            openFileDialog.Multiselect = true;
            string errors = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                foreach (var path in openFileDialog.FileNames)
                {
                    if (saveLoadManager.CopyDllToAppData(path))
                    {
                        Assembly ass = Assembly.LoadFrom(path);
                        AddAssemblyTypesToComboBox(ass);
                    }
                    else
                    {
                        errors += $"Nepodaøilo se naèíst DLL na cestì {path}, zkontrolujte Log \n";

                    }
                }


            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var shape in canvas1.Shapes)
            {
                shape.ShowNames(checkBox2.Checked);
            }
        }

        private void loadShapesFromAppDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var assemblies = saveLoadManager.GetAssembliesFromAppData();
            assemblies.ForEach(ass => AddAssemblyTypesToComboBox(ass));
        }
    }
}


public class MyException : Exception
{
    public string text;
    public MyException(string message)
            : base(message)
    {
    }

    public MyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}