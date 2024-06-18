﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinTracker.AlternativeTelas;

namespace FinTracker.TelasPrincipais
{
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
        }
        private void pnlPesquisarCliente_Click(object sender, EventArgs e)
        {
            txbPesquisa.Focus();
        }

        private void btnNovoCliente_Click(object sender, EventArgs e)
        {
            ClienteFormManipulacao formManipulacao = new ClienteFormManipulacao();
            formManipulacao.Show();
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}
