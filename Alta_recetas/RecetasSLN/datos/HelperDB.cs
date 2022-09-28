using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.datos
{
    internal class HelperDB
    {

        const string cadenadeconexion= @"Data Source=DESKTOP-EN5VFFB\SQLEXPRESS;Initial Catalog=recetas_db;Integrated Security=True";
        
        SqlConnection cnn = new SqlConnection(cadenadeconexion);
        SqlCommand cmd = new SqlCommand();
        SqlTransaction trs;
        int nroreceta= 0;
        public DataTable ConsultaSQL(string nombreSP)
        {

            
            DataTable tabla = new DataTable();

            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = nombreSP;
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            return tabla;
        }

        public bool ConfirmarReceta(Receta oReceta)
        {
            bool ok = true;

            SqlCommand cmd = new SqlCommand();
            try
            {
                cnn.Open();
                trs = cnn.BeginTransaction();
                cmd.Transaction = trs;//espacio en memoria virtual (buffer)
                cmd.Connection = cnn;
                cmd.CommandText = "SP_INSERTAR_RECETA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", oReceta.Nombre);
                cmd.Parameters.AddWithValue("@cheff", oReceta.cheff);
                cmd.Parameters.AddWithValue("@tipo_receta", oReceta.Tipo);
                cmd.ExecuteNonQuery();

                

                SqlCommand cmdDetalle;
                
          
                foreach (DetalleReceta item in oReceta.Detalles)
                {
                    cmdDetalle = new SqlCommand();
                    cmdDetalle.Transaction = trs;
                    cmdDetalle.Connection = cnn;
                    cmdDetalle.CommandText = "SP_INSERTAR_DETALLES";
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@id_receta", nroreceta );
                    cmdDetalle.Parameters.AddWithValue("@id_ingrediente", item.oingrediente.ingredienteid);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", item.cantidad);
                    cmd.ExecuteNonQuery();


                    
                }
                nroreceta++;
                trs.Commit();
                cnn.Close();



            }
            catch (Exception)
            {
                trs.Rollback();
                ok = false;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ok;


        }




    }
}
