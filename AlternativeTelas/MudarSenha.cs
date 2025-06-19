using FinTracker.BD;
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

namespace FinTracker.AlternativeTelas
{
    public partial class MudarSenha : Form
    {
        AdminRepository adminRepository = new AdminRepository();
        Admin admin;
        public MudarSenha(Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if(senhaAtual.Text == "" || novaSenha.Text == "" || novaSenha2.Text == "")
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }
            if (novaSenha.Text != novaSenha2.Text)
            {
                MessageBox.Show("As novas senhas não coincidem.");
                return;
            }
            if (senhaAtual.Text != admin.GetSenha())
            {
                MessageBox.Show("Senha atual incorreta.");
                return;
            }
            if (novaSenha.Text == admin.GetSenha())
            {
                MessageBox.Show("A nova senha não pode ser igual à senha atual.");
                return;
            }
            bool sucesso = await adminRepository.UpdateAdmin(admin.GetId_Admin(), admin.GetNome(), admin.GetEmail(), novaSenha.Text);
            if (sucesso)
            {
                novaSenha.Text = "";
                novaSenha2.Text = "";
                senhaAtual.Text = "";
            }
        }

        private void checkSenhaAtual_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSenhaAtual.Checked)
                senhaAtual.PasswordChar = '\0';
            else
                senhaAtual.PasswordChar = '•';
        }

        private void checkSenha1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSenha1.Checked)
                novaSenha.PasswordChar = '\0';
            else
                novaSenha.PasswordChar = '•';
        }

        private void checkSenha2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSenha2.Checked)
                novaSenha2.PasswordChar = '\0';
            else
                novaSenha2.PasswordChar = '•';
        }
    }
}
