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
    public partial class ContactosGestion : Form
    {

        DataTable _Contactos = new DataTable();

        private void ConfigurarTabla()
        {
            _Contactos.TableName = "Contactos";
            _Contactos.Columns.Add("cNombres");
            _Contactos.Columns.Add("cApellidos");
            _Contactos.Columns.Add("cTipo");
            _Contactos.Columns.Add("cTelefono");
            dtgContactos.AutoGenerateColumns = false;
            dtgContactos.DataSource = _Contactos;
        }
        private void CargarDatos()
        {
            try
            {
                _Contactos.ReadXml("Registros.xml");
                lblNumeroRegistros.Text = dtgContactos.Rows.Count.ToString() + " Registros Encontrados";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
        private void EscribirDatos()
        {
            try
            {
                _Contactos.WriteXml("Registros.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FiltrarDatos()
        {
            if(txbFiltro.TextLength>0)
            {
                try
                {
                    DataView vista = new DataView(_Contactos, "cNombres LIKE '%" + txbFiltro.Text + "%' OR cApellidos LIKE '%" + txbFiltro.Text + "%'", "cApellidos ASC", DataViewRowState.CurrentRows);
                    dtgContactos.DataSource = vista;
                }
                catch
                {

                }

            }
            else
            {
                dtgContactos.DataSource = _Contactos;
            }
            lblNumeroRegistros.Text = dtgContactos.Rows.Count.ToString() + " Registros Encontrados";
        }
       










        public ContactosGestion()
        {
            InitializeComponent();
            ConfigurarTabla();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //CREANDO UNA INSTANCIA (OBJETO)
            ContactoEdicion f = new ContactoEdicion();
            f.Crear = true;
            f.ShowDialog();
            if (f.Crear == true)
            {
                DataRow NuevaFila = _Contactos.NewRow();
                NuevaFila["cNombres"] = f.txbNombres.Text;
                NuevaFila["cApellidos"] = f.txbApellidos.Text;
                NuevaFila["cTipo"] = f.cbbTipo.Text;
                NuevaFila["cTelefono"] = f.txbTelefono.Text;
                _Contactos.Rows.Add(NuevaFila);
                lblNumeroRegistros.Text = dtgContactos.Rows.Count.ToString() + " Registros Encontrados";
                EscribirDatos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Realmente desea ELIMINAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    dtgContactos.Rows.RemoveAt(dtgContactos.CurrentRow.Index);
                    EscribirDatos();
                    lblNumeroRegistros.Text = dtgContactos.Rows.Count.ToString() + " Registros Encontrados";
                    MessageBox.Show("Registro eliminado correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch {
                    MessageBox.Show("El regitro no pudo ser eliminado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                
                //ContactoEdicion f = new ContactoEdicion();
                //f.txbNombres.Text = dtgContactos.CurrentRow.Cells["Nombres"].Value.ToString();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente desea EDITAR el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ContactoEdicion f = new ContactoEdicion();
                    f.Editar = true;
                    f.txbNombres.Text = dtgContactos.CurrentRow.Cells["Nombres"].Value.ToString();
                    f.txbApellidos.Text = dtgContactos.CurrentRow.Cells["Apellidos"].Value.ToString();
                    f.txbTelefono.Text = dtgContactos.CurrentRow.Cells["Telefono"].Value.ToString();
                    f.cbbTipo.Text = dtgContactos.CurrentRow.Cells["Tipo"].Value.ToString();
                    f.ShowDialog();

                    if (f.Editar)
                    {
                        dtgContactos.CurrentRow.Cells["Nombres"].Value = f.txbNombres.Text;
                        dtgContactos.CurrentRow.Cells["Apellidos"].Value = f.txbApellidos.Text;
                        dtgContactos.CurrentRow.Cells["Telefono"].Value = f.txbTelefono.Text;
                        dtgContactos.CurrentRow.Cells["Tipo"].Value = f.cbbTipo.Text;
                        EscribirDatos();
                        MessageBox.Show("Registro editado correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch
                {
                    MessageBox.Show("El regitro no pudo ser eliminado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                //ContactoEdicion f = new ContactoEdicion();
                //f.txbNombres.Text = dtgContactos.CurrentRow.Cells["Nombres"].Value.ToString();
            }
        }

        private void ContactosGestion_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void txbFiltro_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txbFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarDatos();
        }
    }
}
