using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejemplo1
{
    public partial class ContactoEdicion : Form
    {
        Boolean _Crear = false;
        Boolean _Editar = false;
        public bool Crear
        {
            get
            {
                return _Crear;
            }

            set
            {
                _Crear = value;
            }
        }

        public bool Editar
        {
            get
            {
                return _Editar;
            }

            set
            {
                _Editar = value;
            }
        }

        public String TXBNOMBRE
        {
            get
            {
                return txbNombres.Text;
            }

            set
            {
                txbNombres.Text = value;
            }
        }

        //CONSTRUCTOR
        public ContactoEdicion()
        {
            InitializeComponent();
            cbbTipo.SelectedIndex = 0;
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ContactoEdicion_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _Crear = false;
            _Editar = false;
            Close();
        }
    }
}
