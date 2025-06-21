using FinTracker.BD;
using FinTracker.Models;
using FinTracker.TelasPrincipais;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.AlternativeTelas
{
    public partial class NovaConta : Form
    {
        List<Cliente> clientes = new List<Cliente>();
        List<Fornecedor> fornecedores = new List<Fornecedor>();
        Conta conta1;
        public NovaConta()
        {
            InitializeComponent();
            cmbTipoConta.SelectedIndex = 0;

            dateTimePicker2.MinDate = dateTimePicker1.Value.AddDays(1);
        }
        public NovaConta(Conta conta1)
        {
            InitializeComponent();
            cmbTipoConta.SelectedIndex = 0;
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddDays(1);
            this.conta1 = conta1;
            lblTitulo.Text = "Editar conta existente";
            this.Text = "Edição de Conta";
            if(conta1.nomeCliente != null && conta1.nomeCliente != "")
                cmbTipoConta.SelectedIndex = 0; // conta a receber Cliente
            else
                cmbTipoConta.SelectedIndex = 1; // conta a pagar Fornecedor
            cmbMetodoPag.Text = conta1.metodoPagamento;
            txbDescricao.Text = conta1.descricao;
            txbValor.Text = conta1.valor.ToString("F2");
            dateTimePicker1.Value = conta1.dataTransacao;
            dateTimePicker2.Value = conta1.previsaoTermino.AddMinutes(1);
            cmbClientes_fornecedores.Text = conta1.id_Cliente > 0 ? conta1.nomeCliente : conta1.nomeFornecedor;
            
        }

        private void cmbTipoConta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoConta.SelectedIndex == 0) // Conta a receber (Cliente)
            {
                lblNome.Text = "Cliente";
                preencherClientes();
            }
            else // Conta a pagar (Fornecedor)
            {
                lblNome.Text = "Fornecedor";
                preencherFornecedores();
            }
        }

        private async void preencherClientes()
        {
            cmbClientes_fornecedores.Items.Clear();
            cmbClientes_fornecedores.Text = "";
            ClienteRepository clienteRepository = new ClienteRepository();
            clientes = await clienteRepository.PegarTodosClientes();
            foreach (Cliente c in clientes)
            {
                cmbClientes_fornecedores.Items.Add(c.Nome);
                if(conta1 != null)
                {
                    if(c.Nome == conta1.nomeCliente)
                        cmbClientes_fornecedores.SelectedIndex = cmbClientes_fornecedores.FindStringExact(c.Nome);
                }
            }
        }
        private async void preencherFornecedores()
        {
            cmbClientes_fornecedores.Items.Clear();
            cmbClientes_fornecedores.Text = "";
            FornecedorRepository fornecedorRepository = new FornecedorRepository();
            fornecedores = await fornecedorRepository.PegarTodosFornecedores();
            foreach (Fornecedor f in fornecedores)
            {
                cmbClientes_fornecedores.Items.Add(f.Nome);
                if (conta1 != null)
                {
                    if (f.Nome == conta1.nomeFornecedor)
                        cmbClientes_fornecedores.SelectedIndex = cmbClientes_fornecedores.FindStringExact(f.Nome);
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddDays(1);
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txbDescricao.Text.Trim() == "" || cmbClientes_fornecedores.Text.Trim() == "" || txbValor.Text.Trim() == "" || cmbMetodoPag.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Conta conta = new Conta
            {
                valor = double.Parse(txbValor.Text),
                metodoPagamento = cmbMetodoPag.Text,
                dataTransacao = dateTimePicker1.Value,
                previsaoTermino = dateTimePicker2.Value,
                descricao = txbDescricao.Text
            };
            try
            {
                if (cmbTipoConta.Text.Contains("receber"))
                {
                    ContaReceberRepository contaReceberRepository = new ContaReceberRepository();
                    conta.id_Cliente = clientes[cmbClientes_fornecedores.SelectedIndex].id_Cliente;
                    if (conta1 != null)
                    {
                        conta.id_Conta_a_Receber = conta1.id_Conta_a_Receber; // Preservar o ID da conta existente
                        contaReceberRepository.UpdateContaReceber(conta);
                        MessageBox.Show("Conta atualizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    contaReceberRepository.AddContaReceber(conta);
                }
                else
                {
                    ContaPagarRepository contaPagarRepository = new ContaPagarRepository();
                    conta.id_Fornecedor = fornecedores[cmbClientes_fornecedores.SelectedIndex].id;
                    if (conta1 != null)
                    {
                        conta.id_Conta_a_Pagar = conta1.id_Conta_a_Pagar; // Preservar o ID da conta existente
                        contaPagarRepository.UpdateContaPagar(conta);
                        MessageBox.Show("Conta atualizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    contaPagarRepository.AddContaPagar(conta);
                }
                MessageBox.Show("Conta salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar conta: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void limpar()
        {
            cmbClientes_fornecedores.Items.Clear();
            cmbClientes_fornecedores.Text = "";
            cmbTipoConta.SelectedIndex = 0;
            txbDescricao.Text = "";
            txbValor.Text = "";
            cmbMetodoPag.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddDays(1);
        }
    }
}
