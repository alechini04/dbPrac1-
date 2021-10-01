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
        //la (o) se coloca por la nomenclatura de objeto 
        public List<Model.alumno> oalumnos;
        public List<Model.apoderado> oapoderado;
        public List<Model.curso> ocurso;
        public List<Model.inscrito> oinscritos;
        int indice = 0;

        public Form1()
        {
            InitializeComponent();
        }

        //se añaden a el form1 las tablas para que asi el usuario busque con mayor facilidad 
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("alumno");
            comboBox1.Items.Add("apoderado");
            comboBox1.Items.Add("curso");
            comboBox1.Items.Add("inscrito");

        }

        //programacion de el combobox1 para la seleccion de las 4 tablas de la base de datos 
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
            // se hace presente un metodo llenar, el cual busca en la base de datos las relaciones 
            llenar();
        }

        //programacion de los botones, boton 1 para atras
        private void button1_Click(object sender, EventArgs e)
        {
            indice--;
            llenar();
        }

        //programacion de los botones, boton 2 para adelante
        private void button2_Click(object sender, EventArgs e)
        {
            indice++;
            llenar();
        }


        //creacion de el metodo llenar 
        public void llenar()
        {
            if (indice < 0)
                indice = 0;
            string cadena = "";
            switch (comboBox1.SelectedIndex)
            {
                //el caso 0 solo busca en la tabla alumnos
                case 0:
                    if (indice >= oalumnos.Count)
                        indice = oalumnos.Count - 1;
                    cadena = oalumnos[indice].Id.ToString() + ". " + oalumnos[indice].nombre + " , de " + oalumnos[indice].ciudad + ", " + oalumnos[indice].edad + "años";
                    break;

                 // el caso 1 busca informacion de diferentes tablas para eso se utiliza el find y el uso de model.dbprac1Entities 
                 //dentro de la cadena esta lo que nos va a traer, que en este caso es el ID el nombre (eso de la tabla de apoderado), lo otro que nos busca es si el ID de alumno es igual al ID de el apoderado, para poder darle relacion, y por ultimo el nombre de el alumno 
                case 1:
                    if (indice >= oapoderado.Count)
                        indice = oapoderado.Count - 1;
                    using (Model.dbprac1Entities db = new Model.dbprac1Entities())
                    {
                        oalumnos = db.alumno.ToList();
                        cadena = oapoderado[indice].Id.ToString() + ". " + oapoderado[indice].nombre + ", es el / la apoderado/a de " + oalumnos.Find(a => a.Id == (int)oapoderado[indice].id_alumno).nombre; ;
                    }
                    break;

                // el caso 2 solo busca en la tabla curso
                case 2:
                    if (indice >= ocurso.Count)
                        indice = ocurso.Count - 1;
                    cadena = ocurso[indice].COD.ToString() + " . " + ocurso[indice].nombre_ + " -- Fecha de inicio:  " + ocurso[indice].fecha_inicio + " -- Duracion: " + ocurso[indice].duracion + " -- valor: " + ocurso[indice].valor_;
                    break;

                // en el caso 3 buscamos en la tabla de alumno y curso por que es de donde vamos a sacar la informacion 
                //por eso ponemos dos find, para buscar dos igualdades en las diferentes tablas 
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
            //y por ultimo que en la caja de texto nos muestre "cadena" que es dependiendo el caso de el switch
            textBox1.Text = cadena;
        }
    }
}