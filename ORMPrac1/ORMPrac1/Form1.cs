using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORMPrac1
{
    public partial class Form1 : Form
    {
        public List<Model.alumno> oalumnos;
        public List<Model.apoderado> oapoderado;
        public List<Model.curso> ocurso;
        public List<Model.inscrito> oinscritos;
        int indice = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("alumno");
            comboBox1.Items.Add("apoderado");
            comboBox1.Items.Add("curso");
            comboBox1.Items.Add("inscrito");

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (Model.dbprac1Entities db = new Model.dbprac1Entities())
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        oalumnos = db.alumno.ToList();
                        break;

                    case 1:
                        oapoderado = db.apoderado.ToList();
                        break;

                    case 2:
                        ocurso = db.curso.ToList();
                        break;

                    case 3:
                        oinscritos = db.inscrito.ToList();
                        break;
                }
            }
            indice = 0;
            llenar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            indice--;
            llenar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            indice++;
            llenar();
        }

        public void llenar()
        {
            if (indice < 0)
                indice = 0;
            string cadena = "";
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    if (indice >= oalumnos.Count)
                        indice = oalumnos.Count - 1;
                    cadena = oalumnos[indice].Id.ToString() + ". " + oalumnos[indice].nombre + " , de " + oalumnos[indice].ciudad + ", " + oalumnos[indice].edad + "años";
                    break;

                case 1:
                    if (indice >= oapoderado.Count)
                        indice = oapoderado.Count - 1;
                    using (Model.dbprac1Entities db = new Model.dbprac1Entities())
                    {
                        oalumnos = db.alumno.ToList();
                        cadena = oapoderado[indice].Id.ToString() + ". " + oapoderado[indice].nombre + ", es el / la apoderado/a de " + oalumnos.Find(a => a.Id == (int)oapoderado[indice].id_alumno).nombre; ;
                    }
                    break;
                case 2:
                    if (indice >= ocurso.Count)
                        indice = ocurso.Count - 1;
                    cadena = ocurso[indice].COD.ToString() + " . " + ocurso[indice].nombre_ + " -- Fecha de inicio:  " + ocurso[indice].fecha_inicio + " -- Duracion: " + ocurso[indice].duracion + " -- valor: " + ocurso[indice].valor_;
                    break;

                case 3:
                    if (indice >= oinscritos.Count)
                        indice = oinscritos.Count - 1;

                    using (Model.dbprac1Entities db = new Model.dbprac1Entities())
                    {
                       oalumnos = db.alumno.ToList();
                        ocurso = db.curso.ToList();

                        cadena = oinscritos[indice].id.ToString() + " ." + oalumnos.Find(a => a.Id == (int)oinscritos[indice].id_alumno).nombre + "  estudia " + ocurso.Find(a => a.COD == (int)oinscritos[indice].codigo).nombre_;
                    }
                    break;
            }
            textBox1.Text = cadena;
        }
    }
}