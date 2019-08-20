using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Propuesto2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dgPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["kobu"].ConnectionString);

        public void ListaPedidos()
        {
            using (SqlDataAdapter df = new SqlDataAdapter("Usp_Pedidos", cn))
            {
                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                using (DataSet da = new DataSet())
                {
                    df.Fill(da, "Pedidos");
                    dgPedidos.DataSource = da.Tables["Pedidos"];

                }
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            ListaPedidos();

        }

        private void dgPedidos_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int Codigo;
            Codigo = Convert.ToInt32(dgPedidos.CurrentRow.Cells[0].Value);
            using (SqlCommand cmd = new SqlCommand("Usp_Detalle_Pedido", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idpedido", Codigo);
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "Detalles");
                        dgDetalles.DataSource = df.Tables["Detalles"];
                    }
                }
            }
        }
    }
}
