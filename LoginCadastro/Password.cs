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
    public partial class Password : Form
    {
        Principal telaMae;
        public Password()
        {
            InitializeComponent();
        }
        public Password(Principal principal)
        {
            InitializeComponent();
            telaMae = principal;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            telaMae.mudarTelaDoPanelPrincipal(new Login(telaMae));
        }
    }
}
