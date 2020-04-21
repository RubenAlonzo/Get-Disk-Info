using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace GetDiskInfo
{
    public partial class Form1 : Form
    {
        PC obj = new PC();
        string DirectoryPath = "DiskInfo";
        string FilePath = "DiskInfo\\Data.txt";


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarDatos();
            InicializarTxt();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var tw = new StreamWriter(FilePath, true))
                {
                    tw.WriteLine(txtServiceTag.Text +"       "+ txtSerialDisk.Text);
                }
                MessageBox.Show("Se exportaron los datos correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar datos. " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtServiceTag.ReadOnly = false;
            txtSerialDisk.ReadOnly = false;
        }


        private void CargarDatos()
        {
            try
            {
                txtServiceTag.Text = obj.ServiceTag;
                txtSerialDisk.Text = obj.SerialDisk;
                lbModel.Text = obj.Model;
                lbType.Text = obj.Type;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos. " + ex.Message);
            }
        }

        private void InicializarTxt()
        {
            try
            {
                if (Directory.Exists(DirectoryPath) == false)
                {
                    Directory.CreateDirectory(DirectoryPath);

                    using (var tw = new StreamWriter(FilePath, true))
                    {
                        tw.WriteLine("ServiceTag    SerialDisk");
                    }
                }  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error iniciar archivos. " + ex.Message);
            }
        }
    }
}
