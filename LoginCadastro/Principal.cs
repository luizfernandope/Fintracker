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
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            mudarTelaDoPanelPrincipal(new Login(this));
        }

        public void mudarTelaDoPanelPrincipal(object Tela)
        {
            if (this.panel1.Controls.Count > 0)
                this.panel1.Controls.RemoveAt(0);
            Form f = Tela as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.Text = f.Text; // Atualiza o título do formulário principal com o título da nova tela
            this.panel1.Controls.Add(f);
            this.panel1.Tag = f;
            f.Show();
        }
    }
}
