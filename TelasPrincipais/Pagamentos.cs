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
        private async void atualizarTabela()
        {
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
    }
}
