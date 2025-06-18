using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using FinTracker.Properties;
using FinTracker.BD;
using FinTracker.Models;
using FinTracker.AlternativeTelas;

namespace FinTracker.TelasPrincipais
{
    public partial class Pagamentos : Form
    {
        PagamentoRepository pagamentoRepository = new PagamentoRepository();
        Admin admin;
        public void inicializar()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
            atualizarTabela();
            // Definir o modo de ajuste de colunas para preencher todo o espaço
            dgv_Vendas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Vendas.RowTemplate.Height = 40;
            dgv_Vendas.ReadOnly = true;
        }
        public Pagamentos()
        {
            inicializar();
        }
        public Pagamentos(Admin admin)
        {
            inicializar();
            this.admin = admin;
            nomeUsuario.Text = admin.GetNome();
        }
        public async void atualizarTabela()
        {
            // Limpar a tabela antes de atualizar
            dgv_Vendas.DataSource = null; // Limpa a fonte de dados atual
            dgv_Vendas.DataSource = await pagamentoRepository.GetPagamentos();
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

        private void dgv_Vendas_DataSourceChanged(object sender, EventArgs e)
        {
            if(dgv_Vendas.DataSource == null || dgv_Vendas.Rows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewColumn coluna in dgv_Vendas.Columns)
            {
                coluna.FillWeight = 3;  // Ajusta o peso de preenchimento de todas as colunas de forma uniforme
                //coluna.MinimumWidth = 70;
            }
            dgv_Vendas.Columns["id_Pagamento"].HeaderText = "id";//mudando titulo da coluna
            /*formatando tamanho das colunas para responsividade*/
            dgv_Vendas.Columns["id_Pagamento"].MinimumWidth = 20;
            dgv_Vendas.Columns["Data"].MinimumWidth = 100;
            dgv_Vendas.Columns["Metodo"].MinimumWidth = 100;
            dgv_Vendas.Columns["Tipo"].MinimumWidth = 70;
            dgv_Vendas.Columns["Parcelas"].MinimumWidth = 60;
            dgv_Vendas.Columns["Valor"].MinimumWidth = 100;
            dgv_Vendas.Columns["descricao"].MinimumWidth = 120;
            dgv_Vendas.Columns["descricao"].FillWeight = 10;
            dgv_Vendas.Columns["id_Pagamento"].FillWeight = 0.6f;
            
        }

        private void btn_AdVenda_Click(object sender, EventArgs e)
        {
            NovaDespesa novaDespesa = new NovaDespesa(this);
            novaDespesa.Show();
        }

        private void btn_DelVenda_Click(object sender, EventArgs e)
        {
            //pegar id's dos pagamentos feito e realizar a exclusão
            if (dgv_Vendas.SelectedRows.Count > 0)
            {
                //carregar os ids de todas linhas seleciondas em uma lista
                List<int> ids = new List<int>();
                foreach (DataGridViewRow row in dgv_Vendas.SelectedRows)
                {
                    ids.Add(Convert.ToInt32(row.Cells["id_Pagamento"].Value));
                }
                //confirmar exclusão
                DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja excluir os pagamentos selecionados?\nIDs: " + string.Join(", ", ids), "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    //excluir os pagamentos
                    foreach (int id in ids)
                    {
                        pagamentoRepository.DeletePagamento(id);
                    }
                    atualizarTabela();
                }
            }
            else
            {
                MessageBox.Show("Selecione um pagamento para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public Despesa pegarDespesaSelecionada()
        {
            Despesa despesa= null;
            //cria os atributos do model da Despesa e coloca os valores com base no pagamento selecionado
            if (dgv_Vendas.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dgv_Vendas.SelectedRows[0];
                despesa = new Despesa
                {
                    Id = Convert.ToInt32(selectedRow.Cells["id_Pagamento"].Value),
                    Data = Convert.ToDateTime(selectedRow.Cells["Data"].Value),
                    Metodo = selectedRow.Cells["Metodo"].Value.ToString(),
                    Tipo = selectedRow.Cells["Tipo"].Value.ToString(),
                    Parcelas = Convert.ToInt32(selectedRow.Cells["Parcelas"].Value),
                    Valor = Convert.ToDecimal(selectedRow.Cells["Valor"].Value),
                    Descricao = selectedRow.Cells["descricao"].Value.ToString()
                };
            }
            return despesa;
        }

        private void btn_EditVenda_Click(object sender, EventArgs e)
        {
            // Verifica se há uma linha selecionada na tabela
            if (dgv_Vendas.SelectedRows.Count == 1)
            {
                // Obtém o ID do pagamento selecionado
                int idPagamento = Convert.ToInt32(dgv_Vendas.SelectedRows[0].Cells["id_Pagamento"].Value);
                //cria uma Despesa com os dados do pagamento selecionado
                Despesa despesa = pegarDespesaSelecionada();

                if (despesa == null)
                {
                    MessageBox.Show("Nenhum pagamento selecionado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Cria uma nova instância de NovaDespesa passando o ID do pagamento
                NovaDespesa novaDespesa = new NovaDespesa(despesa, this);

                // Exibe o formulário de edição
                novaDespesa.Show();
            }
            else
            {
                MessageBox.Show("Selecione somente um pagamento para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }   
        }

        private async void txbPesquisa_TextChanged(object sender, EventArgs e)
        {
            if (txbPesquisa.Text == "")
                atualizarTabela();
            dgv_Vendas.DataSource = await pagamentoRepository.pesquisarPagamentos(txbPesquisa.Text);
        }
    }
}
