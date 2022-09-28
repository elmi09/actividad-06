using RecetasSLN.datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using RecetasSLN.dominio;

namespace RecetasSLN.presentación
{
    
    public partial class Frmalta : Form
    {
        Receta oReceta;
        HelperDB ogestor = new HelperDB();
        public Frmalta()
        {
            oReceta = new Receta();
            InitializeComponent();
        }

        private void Frmalta_Load(object sender, EventArgs e)
        {
            cargarIngredientes();
        }

        public void cargarIngredientes() 
        {
            DataTable tabla = ogestor.ConsultaSQL("SP_CONSULTAR_INGREDIENTES");
            cboIngredientes.DataSource = tabla;
            cboIngredientes.ValueMember = tabla.Columns[0].ColumnName;
            cboIngredientes.DisplayMember = tabla.Columns[1].ColumnName;

        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            if (cboIngredientes.Text.Equals(String.Empty))
            {
                MessageBox.Show("Debe seleccionar un INGREDIENTE!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtCantidad.Text == "" || !int.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad válida!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                if(row.Cells["colIngrediente"].Value != null)
                {
                    if (row.Cells["colIngrediente"].Value.ToString().Equals(cboIngredientes.Text))
                    {
                        MessageBox.Show("PRODUCTO: " + cboIngredientes.Text + " ya se encuentra como detalle!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                    }
                }
               
            }

            DataRowView item = (DataRowView)cboIngredientes.SelectedItem;

            int id = Convert.ToInt32(item.Row.ItemArray[0]);
            string nom = item.Row.ItemArray[1].ToString();
            string unidad = item.Row.ItemArray[2].ToString();
            Ingrediente I = new Ingrediente(id, nom, unidad);
            int cantidad = Convert.ToInt32(txtCantidad.Text);

            DetalleReceta detalle = new DetalleReceta(I, cantidad);
            oReceta.AgregarDetalle(detalle);
            dgvDetalle.Rows.Add(new object[] { item.Row.ItemArray[1], cantidad });

           
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalle.CurrentCell.ColumnIndex == 2)
            {
                oReceta.QuitarDetalle(dgvDetalle.CurrentRow.Index);
                //click button:
                dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
                //presupuesto.quitarDetalle();


            }



        }

        private void GuardarReceta()
        {
            
            Receta oReceta = new Receta();
            oReceta.Nombre = txtNombre.Text;
            oReceta.cheff = txtCheff.Text;
            oReceta.Tipo = cboTipo.DisplayMember;

            if (ogestor.ConfirmarReceta(oReceta))
            {
                MessageBox.Show("Presupuesto registrado", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("ERROR. No se pudo registrar el presupuesto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //FALTA SP:

        //private void ProximaReceta()
        //{
        //    int next = ogestor.ObtenerProximoRecetaId();
        //    if (next > 0)
        //        lblNroReceta.Text = "Presupuesto Nº: " + next.ToString();
        //    else
        //        MessageBox.Show("Error de datos. No se puede obtener Nº de presupuesto!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}

        private void btnaceptar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Debe ingresar un cliente!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtCheff.Text == "")
            {
                MessageBox.Show("Debe ingresar un cheff!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (cboTipo.SelectedIndex != 0)
            {
                MessageBox.Show("Debe Seleccionar un tipo!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvDetalle.Rows.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos detalle!", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            GuardarReceta();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
