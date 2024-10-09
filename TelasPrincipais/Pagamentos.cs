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

namespace FinTracker.TelasPrincipais
{
    public partial class Pagamentos : Form
    {
        PagamentoRepository pagamentoRepository = new PagamentoRepository("Server=localhost;Database=BD_FinTracker;User Id=root;Password=admin");
        public Pagamentos()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
            dgv_Vendas.DataSource = pagamentoRepository.GetPagamentos();
        }


        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}
