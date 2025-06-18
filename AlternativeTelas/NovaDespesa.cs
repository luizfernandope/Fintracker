using FinTracker.Interfaces;
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
    public partial class NovaDespesa : Form
    {
        Despesa despesa;
        Pagamentos telaMae;
        public NovaDespesa(Pagamentos telaMae)
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = "dd/MM/yyyy 'às' HH:mm:ss";
            this.telaMae = telaMae;
        }
        public NovaDespesa(Despesa despesa, Pagamentos telaMae)
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = "dd/MM/yyyy 'às' HH:mm:ss";
            this.despesa = despesa;
            this.Text = "Editando despesa";
            txbDescricao.Text = despesa.Descricao;
            txbValor.Text = despesa.Valor.ToString("F2");
            cmbMetodoPag.SelectedItem = despesa.Metodo;
            cmbTipo.SelectedItem = despesa.Tipo;
            cmbParcelas.SelectedItem = despesa.Parcelas.ToString();
            dateTimePicker1.Value = despesa.Data; // Define a data do DateTimePicker para a data da despesa
            btnSalvar.Text = "Atualizar Despesa"; // Altera o texto do botão para indicar que é uma atualização
            this.telaMae = telaMae;
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if(btnSalvar.Text == "Atualizar Despesa")
            {
                // Atualiza a despesa existente
                Despesa despesaAtualizada = pegarCampos();
                despesaAtualizada.Id = this.despesa.Id; // Preserva o ID da despesa existente para atualização
                if (despesaAtualizada == null)
                    return;
                bool sucess = await MetodosDB.atualizarDespesa(despesaAtualizada);
                if (sucess)
                {
                    telaMae.atualizarTabela(); // Atualiza a tabela na tela principal
                    MessageBox.Show("Despesa atualizada com sucesso!");
                    Close(); // Fecha o formulário após a atualização
                }
                return;
            }
            Despesa despesa = pegarCampos();
            if(despesa == null)
                return;
            bool sucesso = await MetodosDB.cadastrarDespesa(despesa);
            if (sucesso)
            {
                telaMae.atualizarTabela(); // Atualiza a tabela na tela principal
                MessageBox.Show("Despesa cadastrada com sucesso!");
                limparCampos();
            }
        }

        public Despesa pegarCampos()
        {
            string descricao = txbDescricao.Text;
            String valor = txbValor.Text;
            String metodo = cmbMetodoPag.Text;
            String tipo = cmbTipo.Text;
            if (string.IsNullOrEmpty(cmbParcelas.Text) || cmbParcelas.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return null;
            }
            int parcelas = int.Parse(cmbParcelas.Text);
            DateTime data = dateTimePicker1.Value;
            if(string.IsNullOrWhiteSpace(descricao) || string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(metodo) || string.IsNullOrWhiteSpace(tipo))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return null;
            }
            if (!decimal.TryParse(valor, out decimal valorDecimal) || valorDecimal <= 0)
            {
                MessageBox.Show("Por favor, insira um valor válido ex 99,99 ou 11.11.");
                return null;
            }
            Despesa novaDespesa = new Despesa(data, metodo, tipo, parcelas, decimal.Parse(valor), descricao);
            return novaDespesa;
        }

        public void limparCampos()
        {
            // Limpa os campos do formulário
            txbDescricao.Clear();
            txbValor.Clear();
            cmbMetodoPag.SelectedIndex = -1;
            cmbTipo.SelectedIndex = -1;
            cmbParcelas.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now; // Reseta para a data atual
            // Opcional: Foca no primeiro campo para facilitar a entrada de dados
            txbValor.Focus();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }
    }


}
