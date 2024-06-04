using MainPagesProjetoJane.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MainPagesProjetoJane
{
    public partial class NovoClienteForm : Form
    {
        private ClientesPage clientesPageForm;
        private Cliente cliente;
        public NovoClienteForm(ClientesPage clientesPage, Cliente dadosCliente)
        {
            InitializeComponent();
            clientesPageForm = clientesPage;
            cliente = dadosCliente;
            if (dadosCliente != null)
            {
                lblTituloForm.Text = $"Editando cliente (nome,cpf): [{cliente.nome}][{cliente.CPF}]";
                guna2Button2.Text = "Atualizar";
                txb_cpf.Text = cliente.CPF;
                txb_nome.Text = cliente.nome;
                txb_telefone.Text = cliente.telefone;
                txb_email.Text = cliente.email;
                txb_endereco.Text = cliente.endereco;
            }
            else
                btnConsultarValores.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            clientesPageForm.atualizarTabela();
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DataBaseInterface dataBase = new DataBaseInterface();
            if(cliente != null)
            {
                Cliente clienteAtualizado = new Cliente(txb_cpf.Text, txb_nome.Text, txb_telefone.Text, txb_email.Text, txb_endereco.Text);
                dataBase.updateCliente(clienteAtualizado, cliente.CPF);
                clientesPageForm.atualizarTabela();
                this.Close();
                return;
            }
            bool funfou = dataBase.addNewCliente(txb_cpf.Text, txb_nome.Text, txb_telefone.Text, txb_email.Text, txb_endereco.Text);
            if (funfou)
                limparCampos();
            clientesPageForm.atualizarTabela();
        }
        private void limparCampos()
        {
            txb_cpf.Clear(); txb_nome.Clear(); txb_telefone.Clear(); txb_email.Clear(); txb_endereco.Clear();
            txb_cpf.Focus();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void btnConsultarValores_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nome: " + cliente.nome + "\nCPF: " + cliente.CPF + "\nEmail: " + cliente.email + "\nTelefone: " + cliente.telefone + "\nEndereço: " + cliente.endereco, "Dados do clientes antes da atualização");

        }
    }
}
