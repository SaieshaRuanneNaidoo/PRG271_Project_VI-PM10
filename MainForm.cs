using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_PRG271
{
    public partial class MainForm : Form
    {

        //animals that showing on grid
        private List<Animal> animals = new List<Animal>();
        public MainForm()
        {
            InitializeComponent();
            dgvAnimals.AutoGenerateColumns = false;
        }

        //run when main form has been opened
        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();

            //tells the user about the bad lines and its ONLY here so its gonna show once
            if (FileHandler.LinesSkipped > 0)
            {
                MessageBox.Show(FileHandler.LinesSkipped + " Line/s Skipped");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RefreshGrid()
        {
            animals = FileHandler.Load();

            //emptying grid so no double results
            dgvAnimals.DataSource = null;
            dgvAnimals.DataSource = animals;

           //grid highlights foirst row by itself so clear it otherwise delete might remove a row usedr never picked
            dgvAnimals.ClearSelection();
            dgvAnimals.CurrentCell = null;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dgvAnimals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

