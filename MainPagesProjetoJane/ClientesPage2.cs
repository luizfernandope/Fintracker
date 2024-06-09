using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainPagesProjetoJane.Models;
using MainPagesProjetoJane;

namespace MainPagesProjetoJane
{
    public partial class ClientesPage2 : Form
    {
        public ClientesPage2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataBaseInterface dataBase = new DataBaseInterface();
            DataTable dataTable = dataBase.buscaClientesAuto(textBox1.Text);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridView1.Rows.Add(row.ItemArray);
                }
            }
        }

        private void btnAdicionarCliente_Click(object sender, EventArgs e)
        {
            NovoClienteForm2 novoCliente = new NovoClienteForm2();
            novoCliente.Show();
        }
    }
}
