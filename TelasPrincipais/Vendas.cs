using FinTracker.BD;
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
    public partial class Vendas : Form
    {
        VendaRepository vendaRepository = new VendaRepository("Server=localhost;Database=BD_FinTracker;User Id=root;Password=admin");
        public Vendas()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
            dgv_Vendas.DataSource = vendaRepository.GetVendas();
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        private void btn_AdVenda_Click(object sender, EventArgs e)
        {

        }
    }
}
