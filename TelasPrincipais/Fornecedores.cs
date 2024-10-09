using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinTracker.BD;

namespace FinTracker.TelasPrincipais
{
    public partial class Fornecedores : Form
    {
        private FornecedorRepository fornecedorRepository = new FornecedorRepository("Server=localhost;Database=BD_FinTracker;User Id=root;Password=admin");
        public Fornecedores()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
            dataGridView1.DataSource = fornecedorRepository.GetFornecedores();
            // Definir o modo de ajuste de colunas para preencher todo o espaço
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn coluna in dataGridView1.Columns)
            {
                coluna.FillWeight = 1;  // Ajusta o peso de preenchimento de todas as colunas de forma uniforme
                //coluna.MinimumWidth = 70;
            }
            dataGridView1.Columns["id_Fornecedor"].HeaderText = "id";
            dataGridView1.Columns["id_Fornecedor"].MinimumWidth = 20;
            dataGridView1.Columns["Nome"].MinimumWidth = 40;
            dataGridView1.Columns["Data_de_Cadastro"].MinimumWidth = 100;
            dataGridView1.Columns["CNPJ"].MinimumWidth = 92;
            dataGridView1.Columns["Endereco"].MinimumWidth = 56;
            dataGridView1.Columns["bairro"].MinimumWidth = 40;
            dataGridView1.Columns["cidade"].MinimumWidth = 46;
            dataGridView1.Columns["Estado"].MinimumWidth = 42;
            dataGridView1.Columns["cep"].MinimumWidth = 60;
            dataGridView1.Columns["telefone"].MinimumWidth = 70;
            dataGridView1.Columns["email"].MinimumWidth = 40;
            dataGridView1.Columns["status"].MinimumWidth = 40;
            dataGridView1.Columns["nome"].FillWeight = 4;
            dataGridView1.Columns["endereco"].FillWeight = 3;
            dataGridView1.Columns["email"].FillWeight = 3;
        }
    }
}
