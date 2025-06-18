using FinTracker.AlternativeTelas;
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

namespace FinTracker.TelasPrincipais
{
    public partial class Contas : Form
    {
        Admin admin;
        public Contas()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
        }
        public Contas(Admin admin)
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
            this.admin = admin;
            nomeUsuario.Text = admin.GetNome();
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil(admin);
            perfil.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GerirNovosCadastros gerirNovosCadastros = new GerirNovosCadastros();
            gerirNovosCadastros.Show();
        }
    }
}
