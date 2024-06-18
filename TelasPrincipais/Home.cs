using FinTracker.AlternativeTelas;
using FinTracker.TelasPrincipais;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.Telas
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de  MMMMM \de yyyy.");
            
            dateTimePicker1.MaxDate = DateTime.Today;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        /*
         * 
         * Bloco de codigo para informações por periodo de tempo
         *
         */
        private void lblData1_Click(object sender, EventArgs e)
        {
            if (txbData1.Text == "dd/mm/aaaa")// verificando se esta sem valor preenchido (apenas o placeholder, pois é impossivel estar assim "")
                txbData1.Text = "";//deixando textbox vazio para entrada do usuario
            txbData1.Focus();//dando foco para textbox desejado
        }
        private void txbData1_TextChanged(object sender, EventArgs e)
        {
            lblData1.Text = txbData1.Text.Trim();//quando textbox escondido muda texto, o label visivel com visual adequado recebe esse texto
        }
        private void lblData2_Click(object sender, EventArgs e)
        {
            if(txbData2.Text == "dd/mm/aaaa")// verificando se esta sem valor preenchido (apenas o placeholder, pois é impossivel estar assim "")
                txbData2.Text = "";//deixando textbox vazio para entrada do usuario
            txbData2.Focus();//dando foco para textbox desejado
        }
        private void txbData2_TextChanged(object sender, EventArgs e)
        {
            lblData2.Text = txbData2.Text.Trim();//quando textbox escondido muda texto, o label visivel com visual adequado recebe esse texto
        }
        private void txbData_Leave(object sender, EventArgs e)
        {
            TextBox txb = (TextBox)sender;//pegando o textBox da data
            if ( txb.Text == "" || txb.Text.Trim() == "")// se estiver vazio
            {
                txb.Text = "dd/mm/aaaa"; //reseta o texto com placeholder de data
            }
        }

        private void panel10_SizeChanged(object sender, EventArgs e)
        {
            if(panel10.Width < 800)
            {
                btnFiltrar.Location = new Point(btnFiltrar.Location.X,15);
            }
            else
                btnFiltrar.Location = new Point(btnFiltrar.Location.X, 69);
        }


        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);
        private const Int32 CB_SETITEMHEIGHT = 0x153;

        private void SetComboBoxHeight(IntPtr comboBoxHandle, Int32 comboBoxDesiredHeight)
        {
            SendMessage(comboBoxHandle, CB_SETITEMHEIGHT, -1, comboBoxDesiredHeight);
        }

        public void mudarAlturaComboBox(ComboBox combo, int altura)
        {
            SetComboBoxHeight(combo.Handle, altura);
            combo.Refresh();
        }

        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}
