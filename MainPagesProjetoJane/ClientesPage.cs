using Guna.UI2.WinForms;
using MainPagesProjetoJane.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainPagesProjetoJane
{
    public partial class ClientesPage : Form
    {
        public ClientesPage()
        {
            Guna2Button btnDel = new Guna2Button();
            btnDel.Image = Image.FromFile("D:\\Repositorios\\C#\\MainPagesProjetoJane\\img\\icon_trash.png");
            btnDel.Width = 36;
            btnDel.Height = 24;
            btnDel.Text = "";
            
            InitializeComponent();
            //guna2DataGridView1.Rows.Add("123", "Luiz", "1199999-9999", "meuemail.com@email.com", "este endereço 555, neste bairro, UC");
            atualizarTabela();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            NovoClienteForm novoClienteForm = new NovoClienteForm(this, null);
            novoClienteForm.Show();
        }
        public void atualizarTabela()
        {
            guna2DataGridView1.Rows.Clear();
            DataBaseInterface dataBase = new DataBaseInterface();
            DataTable dataTable = dataBase.selectClient();
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    guna2DataGridView1.Rows.Add(row.ItemArray);
                }
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Clear();
            atualizarTabela();
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            guna2DataGridView1.Rows[e.RowIndex].Cells["btnDelete"].ToolTipText = "Clique aqui para excluir";
            guna2DataGridView1.Rows[e.RowIndex].Cells["btnEdit"].ToolTipText = "Clique aqui para editar";
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string cpf, nome, telefone, email, endereco;
            cpf = guna2DataGridView1.Rows[e.RowIndex].Cells["cpf"].Value.ToString();
            nome = guna2DataGridView1.Rows[e.RowIndex].Cells["nome"].Value.ToString();
            telefone = guna2DataGridView1.Rows[e.RowIndex].Cells["telefone"].Value.ToString();
            email = guna2DataGridView1.Rows[e.RowIndex].Cells["email"].Value.ToString();
            endereco = guna2DataGridView1.Rows[e.RowIndex].Cells["endereco"].Value.ToString();

            Cliente cliente = new Cliente(cpf, nome, telefone, email, endereco);

            if(guna2DataGridView1.Columns[e.ColumnIndex] == guna2DataGridView1.Columns["btnDelete"])
            {
                if(MessageBox.Show("Nome: " + nome + "\nCPF: " + cpf+ "\nEmail: " + email + "\nTelefone: " + telefone + "\nEndereço: " + endereco, "Deseja excluir cliente permanentemente ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DataBaseInterface dataBaseInterface = new DataBaseInterface();
                    List<string> cpfList = new List<string>();
                    cpfList.Add(cpf);
                    dataBaseInterface.removeCliente(cpfList);
                }
            }
            if (guna2DataGridView1.Columns[e.ColumnIndex] == guna2DataGridView1.Columns["btnEdit"])
            {
                NovoClienteForm novoClienteForm = new NovoClienteForm(this, cliente);
                novoClienteForm.Show();
            }
            atualizarTabela();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            List<string> cpfList = new List<string>();

            int qtdLinhasSelecionadas = guna2DataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (MessageBox.Show($"{qtdLinhasSelecionadas} clientes selecionados.\nDeseja excluir todos cientes selecionados?", "Deseja excluir lista de clientes permanentemente ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                for (int i = 0; i < qtdLinhasSelecionadas; i++)
                {
                    cpfList.Add(guna2DataGridView1.SelectedRows[i].Cells["cpf"].Value.ToString());
                }
                DataBaseInterface dataBaseInterface = new DataBaseInterface();
                dataBaseInterface.removeCliente(cpfList);
                atualizarTabela();
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            DataBaseInterface dataBase = new DataBaseInterface();
            DataTable dataTable = dataBase.buscaClientesAuto(guna2TextBox1.Text);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    guna2DataGridView1.Rows.Add(row.ItemArray);
                }
            }
        }
    }
}
