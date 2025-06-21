using FinTracker.AlternativeTelas;
using FinTracker.BD;
using FinTracker.Models;
using FinTracker.TelasPrincipais;
using MiniExcelLibs;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.Telas
{
    public partial class Importar : Form
    {
        Admin admin;
        public Importar()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
        }
        public Importar(Admin admin)
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
            this.admin = admin;
            nomeUsuario.Text = admin.GetNome();
            // Definir o modo de ajuste de colunas para preencher todo o espaço
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 40;

            dataGridView1.ReadOnly = true; // Impede a edição direta das células
        }
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string b = "";
                foreach (string a in files)
                    b = b + a + "\n";
                MessageBox.Show(b);
            }
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //se tem mais de um arquivo, não permite o drop
                if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 1)
                {
                    MessageBox.Show("Por favor, arraste apenas um arquivo Excel (.xlsx).", "Múltiplos Arquivos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Effect = DragDropEffects.None;
                    return;
                }
                //se for um arquivo Excel, preenche o ataGridView1 com os dados do arquivo
                if (e.Data.GetData(DataFormats.FileDrop) is string[] files1 && files1.Length > 0)
                {
                    string filePath = files1[0];
                    if (filePath.EndsWith(".xlsx"))
                    {
                        // Lê o Excel como DataTable
                        var table = MiniExcel.QueryAsDataTable(filePath, useHeaderRow: true);
                        dataGridView1.DataSource = table;
                        limparLinhasBrancas();
                        formatarCelulasDgv();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, arraste um arquivo Excel (.xlsx).", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnFileToExport_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                // Lê o Excel como DataTable
                var table = MiniExcel.QueryAsDataTable(op.FileName, useHeaderRow: true);
                dataGridView1.DataSource = table;
                limparLinhasBrancas();
                formatarCelulasDgv();
            }
        }
        public void limparLinhasBrancas()
        {
            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                if (dataGridView1.Rows[i].IsNewRow) continue;
                if (dataGridView1.Rows[i].Cells[1].Value == null || string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[1].Value.ToString()))
                {
                    dataGridView1.Rows.RemoveAt(i);
                }
            }
        }
        public void formatarCelulasDgv()
        {
            // Formatar as células do DataGridView para exibir com os tamanhos dinamicos
            foreach (DataGridViewColumn coluna in dataGridView1.Columns)
            {
                coluna.MinimumWidth = coluna.Width;
                if(coluna.Width > 180)
                    coluna.MinimumWidth = 180; // Define uma largura mínima de 180 pixels para todas as colunas muito grandes
                coluna.FillWeight = 3;  // Ajusta o peso de preenchimento de todas as colunas de forma uniforme
            }


        }

        public async void salvarFornecedores()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            //pega os fornecedores do datagrid e salva no banco de dados
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Fornecedor fornecedor = new Fornecedor();
                    if (row.IsNewRow) continue; // Ignora a linha de novo registro
                    fornecedor.Nome = row.Cells["Nome"].Value?.ToString();
                    fornecedor.Data_de_Cadastro = row.Cells["Data_de_Cadastro"].Value?.ToString();
                    fornecedor.CNPJ = row.Cells["CNPJ"].Value?.ToString();
                    fornecedor.Endereco = row.Cells["Endereco"].Value?.ToString();
                    fornecedor.Bairro = row.Cells["Bairro"].Value?.ToString();
                    fornecedor.Cidade = row.Cells["Cidade"].Value?.ToString();
                    fornecedor.Estado = row.Cells["Estado"].Value?.ToString();
                    fornecedor.CEP = row.Cells["CEP"].Value?.ToString();
                    fornecedor.Telefone = row.Cells["Telefone"].Value?.ToString();
                    fornecedor.Email = row.Cells["Email"].Value?.ToString();
                    fornecedor.Status = row.Cells["Status"].Value?.ToString();
                    fornecedores.Add(fornecedor);
                }
                // Aqui você deve implementar a lógica para salvar os dados no banco de dados
                FornecedorRepository fornecedorRepository = new FornecedorRepository();
                foreach (Fornecedor fornecedor in fornecedores)
                {
                    fornecedorRepository.AddFornecedor2(fornecedor);
                }
                MessageBox.Show(fornecedores.Count + " novos fornecedore(s) salvos com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar fornecedore(s):\n " + ex.Message);
            }
        }
        public void salvarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            //pega os fornecedores do datagrid e salva no banco de dados
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Cliente cliente= new Cliente();
                    if (row.IsNewRow) continue; // Ignora a linha de novo registro
                    cliente.Nome = row.Cells["Nome"].Value?.ToString();
                    cliente.Data_de_Cadastro = row.Cells["Data_de_Cadastro"].Value?.ToString();
                    cliente.CNPJ = row.Cells["CNPJ"].Value?.ToString();
                    cliente.Endereco = row.Cells["Endereco"].Value?.ToString();
                    cliente.Bairro = row.Cells["Bairro"].Value?.ToString();
                    cliente.Cidade = row.Cells["Cidade"].Value?.ToString();
                    cliente.Estado = row.Cells["Estado"].Value?.ToString();
                    cliente.CEP = row.Cells["CEP"].Value?.ToString();
                    cliente.Telefone = row.Cells["Telefone"].Value?.ToString();
                    cliente.Email = row.Cells["Email"].Value?.ToString();
                    cliente.Status = row.Cells["Status"].Value?.ToString();
                    clientes.Add(cliente);
                }
                // Aqui você deve implementar a lógica para salvar os dados no banco de dados
                ClienteRepository clienteRepository = new ClienteRepository();
                foreach (Cliente cliente in clientes)
                {
                    clienteRepository.AddCliente2(cliente);
                }
                MessageBox.Show(clientes.Count + " novos cliente(s) salvos com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar cliente(s):\n " + ex.Message);
            }
        }

        public void salvarDespesas()
        {
            List<Despesa> despesas = new List<Despesa>();
            //pega as despesas do datagrid e salva no banco de dados
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Despesa despesa = new Despesa();
                    if (row.IsNewRow) continue; // Ignora a linha de novo registro
                    despesa.Descricao = row.Cells["Descricao"].Value?.ToString();
                    despesa.Metodo = row.Cells["Metodo"].Value?.ToString();
                    despesa.Tipo = row.Cells["Tipo"].Value?.ToString();
                    despesa.Valor = Convert.ToDecimal(row.Cells["Valor"].Value);
                    despesa.Parcelas = Convert.ToInt32(row.Cells["Parcelas"].Value);
                    despesa.Data = Convert.ToDateTime(row.Cells["Data"].Value);
                    despesas.Add(despesa);
                }
                // Aqui você deve implementar a lógica para salvar os dados no banco de dados
                PagamentoRepository despesaRepository = new PagamentoRepository();
                foreach (Despesa despesa in despesas)
                {
                    despesaRepository.AddPagamentoAsync(despesa);
                }
                MessageBox.Show(despesas.Count + " novas despesa(s) salvas com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar despesa(s):\n " + ex.Message);
            }
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil(admin);
            perfil.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            GerirNovosCadastros gerirNovosCadastros = new GerirNovosCadastros();
            gerirNovosCadastros.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(rdbFornecedores.Checked)
                salvarFornecedores();
            else if (rdbClientes.Checked)
                salvarClientes();
            else if(rdbPagamentos.Checked)
                salvarDespesas();
            else
                MessageBox.Show("Selecione um tipo de dados para salvar.");
        }

        private void btnAllowEdit_Click(object sender, EventArgs e)
        {
            if(dataGridView1.ReadOnly)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true; // Permite adicionar novas linhas
                dataGridView1.AllowUserToDeleteRows = true; // Permite excluir linhas
                btnAllowEdit.Text = "Desabilitar edição de dados na tabela";
            }
            else
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false; // bloqueia adicionar novas linhas
                dataGridView1.AllowUserToDeleteRows = false; // bloqueia excluir linhas
                btnAllowEdit.Text = "Habilitar edição de dados na tabela";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Instruções de uso:\n1. Selecione o tipo de dado.\n2. Selecione um arquivo Excel (.xlsx) contendo os dados que deseja importar.\n3. Certifique-se de que as colunas estejam corretamente nomeadas e formatadas de acordo com o tipo de dados que você está importando (Fornecedores, Clientes, Despesas ou Vendas).\n4. Após selecionar o arquivo, clique no botão 'Salvar' para armazenar os dados no banco de dados.", "Instruções de Importação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
