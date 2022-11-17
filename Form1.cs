using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registros_RRHH
{
    public partial class Form1 : Form
    {
        private readonly cContacto contacto = new cContacto();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refreshGrid();
        }
        private void refreshGrid()
        {
            dataGridView1.DataSource = contacto.GetContactos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            contacto.Nombre_completo = txtNombre.Text.Trim();
            contacto.NotificarAdicionEnContacto += Contacto_NotificarAdicionEnContacto;
            contacto.agregarContacto();
        }
        private void Contacto_NotificarAdicionEnContacto(object sender, EventArgs e)
        {
            MessageBox.Show("done");
            contacto.NotificarAdicionEnContacto -= Contacto_NotificarAdicionEnContacto;
            refreshGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Archivos jpg (*.jpg)|*.jpg|Archivos png (*.png)|*.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = "C:\\Users\\arago\\OneDrive\\Escritorio\\POE\\Registros_RRHH\\img";
            saveFileDialog1.Filter = "Images|*.png;*.bmp;*.jpg";
            saveFileDialog1.Title = $"Photo_{contacto.Nombre_completo}";
            ImageFormat format = ImageFormat.Png;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }

                string location = saveFileDialog1.FileName;
                contacto.Foto = location.Trim();
                contacto.NotificarAdicionEnContacto += Contacto_NotificarAdicionEnContacto;
                contacto.agregarContacto();
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new Form2();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }
    }
}
