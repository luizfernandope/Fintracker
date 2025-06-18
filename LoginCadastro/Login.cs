using FinTracker.Interfaces;
using FinTracker.Models;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace FinTracker.LoginCadastro
{
    public partial class Login : Form
    {
        Principal telaMae;
        public Login()
        {
            InitializeComponent();
        }

        public Login(Principal principal)
        {
            InitializeComponent();
            telaMae = principal;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Botão direcionando para a primeira página depois do LOGIN fica aqui.  
        }


        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Link que leva o usuário para página de recuperar a senha. 
            telaMae.mudarTelaDoPanelPrincipal(new Password(telaMae));
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Link que leva o usuário para página de cadastramento caso não tenha conta.

            telaMae.mudarTelaDoPanelPrincipal(new Register(telaMae));
        }

        private void checkBoxSenha_CheckedChanged_1(object sender, EventArgs e)
        {
            // Visibilidade ou censura da senha ao clicar na checkbox. 
            if (checkBoxSenha.Checked)
                txtSenha.PasswordChar = '\0';
            else
                txtSenha.PasswordChar = '•';
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            string email, senha;
            email = txtUser.Text;
            senha = txtSenha.Text;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Admin admin = await MetodosDB.logar(email, senha);
                if (admin != null)
                {
                    // Se o login for bem-sucedido, fecha a tela de login e abre a tela principal.
                    Form1 telaPrincipal = new Form1(admin, telaMae);
                    telaPrincipal.Show();
                    //deixar a telaMae maximizada
                    telaMae.MinimumSize = new System.Drawing.Size(960, 400); // Definindo um tamanho mínimo para a tela principal
                    telaMae.MaximizeBox = true;
                    telaMae.WindowState = FormWindowState.Maximized; // Maximiza a tela principal
                    telaMae.FormBorderStyle = FormBorderStyle.Sizable;
                    telaMae.mudarTelaDoPanelPrincipal(telaPrincipal);
                    //telaMae.Close(); // essa linha ta fechando o programa... como fazer para fechar a telaMae de login e cadastro sem fechar o programa?
                    // Alternativamente, você pode esconder a tela de login em vez de fechá-la
                    // telaMae.Hide(); // Se você quiser esconder a tela de login em vez de fechá-la
                }
                else
                {
                    // Se o login falhar, exibe uma mensagem de erro.
                    MessageBox.Show("Email ou senha incorretos. Tente novamente.", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
