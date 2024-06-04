using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;
using MainPagesProjetoJane.Pages;

namespace MainPagesProjetoJane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.") ;
            if (btnHome.Checked)
                trocarTela(new homePage());


            DataBaseInterface a = new DataBaseInterface();
            //a.criarTabelas();
            
        }

        private void trocarTela(object Tela)
        {
            if (this.panelPrincipal.Controls.Count > 0)
                this.panelPrincipal.Controls.RemoveAt(0);
            Form f = Tela as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelPrincipal.Controls.Add(f);
            this.panelPrincipal.Tag = f;
            f.Show();
        }
        private void btnHome_CheckedChanged(object sender, EventArgs e)
        {
            if (btnHome.Checked)
                trocarTela(new homePage());
        }

        private void btnClientes_CheckedChanged(object sender, EventArgs e)
        {
            if (btnClientes.Checked)
                trocarTela(new ClientesPage());
        }

        private void btnImportar_CheckedChanged(object sender, EventArgs e)
        {
            if (btnImportar.Checked)
                trocarTela(new ImportPage());
        }
    }
}
