using FinTracker.Interfaces;
using FinTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.LoginCadastro
{
    public partial class Register : Form
    {
        Principal telaMae;
        public Register()
        {
            InitializeComponent();
        }
        public Register(Principal principal)
        {
            InitializeComponent();
            telaMae = principal;
        }

        private void button1_Click(object sender, EventArgs e)

            // Mensagem de erro caso algum dos campos não esteja preenchido durante o cadastramento. 
        {
            /*if (txtNome.Text == " " || txtEnd.Text == " " || txtCep.Text == " " || txtTel.Text == " " || txtCpf.Text == " " || txtEmail.Text == " " || txtSenha.Text == " ")
            {
                MessageBox.Show(" Erro! Preencha todos os campos para concluir o cadastro.", "Cadastramento não concluído.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
                 {

                MessageBox.Show("Cadastro feito com sucesso.");

                 }*/
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            telaMae.mudarTelaDoPanelPrincipal(new Login(telaMae));
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(txtEmail.Text == "" || txtNome.Text == "" || txtSenha.Text == "")
            {
                // Mensagem de erro caso algum dos campos não esteja preenchido durante o cadastramento.
                MessageBox.Show("Erro! Preencha todos os campos para concluir o cadastro.", "Cadastramento não concluído.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!checkBox1.Checked)
            {
                // Mensagem de erro caso o usuário não tenha concordado com os termos de uso.
                MessageBox.Show("Você deve concordar com os termos de uso para solicitar o cadastro.", "Cadastramento não concluído.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Admin admin = new Admin(txtNome.Text, txtEmail.Text, txtSenha.Text);
            MetodosDB.solicitarCadastro(admin).ContinueWith((task) =>
            {
                if (task.Result)
                {
                    MessageBox.Show("Cadastro solicitado com sucesso.\nAguarde até um dos administradores aceitarem sua solicitação.", "Sucesso",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Erro solicitar cadastro. Tente novamente.", "Cadastramento não concluído.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            // Visibilidade ou censura da senha ao clicar na checkbox. 
            if (checkBox3.Checked)
                txtSenha.PasswordChar = '\0';
            else
                txtSenha.PasswordChar = '•';
        }
    }
}
