using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinTracker.AlternativeTelas;
using FinTracker.BD;
using FinTracker.LoginCadastro;
using FinTracker.Models;

namespace FinTracker.TelasPrincipais
{
    public partial class Perfil : Form
    {
        Admin admin;
        Form1 telaMae;
        public Perfil()
        {
            InitializeComponent();
        }
        public Perfil(Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
            nome.Text = admin.GetNome();
            email.Text = admin.GetEmail();
        }
        private void btnsHover(Object sender, EventArgs e)
        {

        }

        public void atualizarPerfil(Admin admin)
        {
            this.admin = admin;
            nome.Text = admin.GetNome();
            email.Text = admin.GetEmail();
            btnVoltar_Click(null,null);
        }

        private void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            pnlTelaClicada.Controls.Clear();
            EditarPerfil editarPerfilPage = new EditarPerfil(admin, this);
            Form f = editarPerfilPage as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            pnlTelaClicada.Controls.Add(f);
            pnlTelaClicada.Tag = f;
            f.Show();
            pnlTela2.Visible = true;
        }

        private void btnMudarSenha_Click(object sender, EventArgs e)
        {
            pnlTelaClicada.Controls.Clear();
            
            MudarSenha mudarSenhaPage = new MudarSenha(admin);
            Form f = mudarSenhaPage as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            pnlTelaClicada.Controls.Add(f);
            pnlTelaClicada.Tag = f;
            f.Show();
            pnlTela2.Visible = true;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            pnlTela2.Visible = false;
            pnlTelaClicada.Controls.Clear();
        }

        private void pnlTelaClicada_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void btnExcluirPerfil_Click(object sender, EventArgs e)
        {
            //mensagem de confirmação
            DialogResult dialogResult = MessageBox.Show("Tem certeza que deseja excluir o perfil?", "Confirmação", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes)
                return; // Se o usuário não confirmar, sai do método
            bool deletado = await  new AdminRepository().DeleteAdmin(admin.GetId_Admin());
            if (deletado)
                Application.Restart();
        }
    }
}
