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
    public partial class EditarPerfil : Form
    {
        Admin admin;
        Perfil telaMae;
        public EditarPerfil(Admin admin, Perfil telaMae)
        {
            InitializeComponent();
            this.admin = admin;
            this.telaMae = telaMae;
            txbEmail.Text = admin.GetEmail();
            txbNome.Text = admin.GetNome();
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txbNome.Text) || string.IsNullOrWhiteSpace(txbEmail.Text))
            {
                MessageBox.Show("Preencha todos os campos.");
                return;
            }
            if(admin.GetEmail() == txbEmail.Text && admin.GetNome() == txbNome.Text)
            {
                MessageBox.Show("Nenhum dado foi alterado.");
                return;
            }
            bool atualizado = await new AdminRepository().UpdateAdmin(admin.GetId_Admin(), txbNome.Text, txbEmail.Text, admin.GetSenha());
            if (atualizado)
            {
                admin.SetNome(txbNome.Text);
                admin.SetEmail(txbEmail.Text);
                telaMae.atualizarPerfil(admin);
            }
        }
    }
}
