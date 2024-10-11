using FinTracker.AlternativeTelas;
using FinTracker.Interfaces;
using FinTracker.TelasPrincipais;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinTracker.Telas
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de MMMMM \de yyyy.");
            
            dateTimePicker1.MaxDate = DateTime.Today;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            tbData1.Text = DateTime.Today.AddMonths(-1).ToString("ddMMyyyy");
            tbData2.Text = DateTime.Today.ToString("ddMMyyyy");
            qtdClientes.Text = MetodosDB.executarQuerrySimples("select count(id_cliente) from cliente");
            qtdFornecedores.Text = MetodosDB.executarQuerrySimples("select count(id_fornecedor) from fornecedor");
            qtdVendas.Text = MetodosDB.executarQuerrySimples("select count(id_venda) from venda");
            buscarInfoPorPeriodo_Click(null, null);
            atualizaGrafico(null, null);
        }
        private void pnlVerPerfil_Click(object sender, MouseEventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        /*
         * 
         * Bloco de codigo para informações por periodo de tempo
         *
         */

        private void panelFiltros_SizeChanged(object sender, EventArgs e)
        {
            if(panelFiltros.Width < 800)
                btnFiltrar.Location = new Point(btnFiltrar.Location.X,15);
            else
                btnFiltrar.Location = new Point(btnFiltrar.Location.X, 69);
        }

        private void numberOnlyInTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*metodo para permitir que o usuario digite apenas numeros no textBox*/

            // Verifica se a tecla pressionada nao é um número ou backspace
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;//bloqueia a entrada
        }

        private void inputData_TextChanged(object sender, EventArgs e)
        {
            /*metodo para formatar o estilo da data no textBox*/

            //peegando qual textBox solicitou o metodo
            System.Windows.Forms.TextBox input = (System.Windows.Forms.TextBox)sender;
            //pegando texto do textBox sem '/'
            string texto = input.Text.Replace("/", "").Trim();

            //se o texto for menor igual a 2, exibi apenas os caracteres iniciais (exclui a barra automaticamente)
            //exemplo: se input.text == "21/5" e o usuario deu backspace o texto recebe apenas "21"
            if (texto.Length <= 2)
                input.Text = texto;
            //se o texto for maior que dois e menor igual a 4, pode por a / no terceiro elemento da string texto (que é o texto do input sem '/')
            else if (texto.Length <= 4)
            {
                // Exibir dia/mês: 0107 -> 01/07
                input.Text = texto.Insert(2, "/");
            }
            //se o texto for maior que 4, pode por a / no terceiro e quinto elemento da string texto (que é o texto do input sem '/')
            else if (texto.Length > 4)
            {
                // Exibir dia/mês/ano: 11042024 -> 11/04/2024
                input.Text = texto.Insert(2, "/").Insert(5, "/");
            }

            // Mover o cursor para o final do texto
            input.SelectionStart = input.Text.Length;
        }

        public void atualizarInfoPorPeriodo(String[] tabelasAlvo, int paginaAtual)
        {
            //atualiza as bolas de navegacao
            pnlPaginasPeriodoTempo.Width = tabelasAlvo.Length * 22;
            pnlPaginasPeriodoTempo.Location = new Point(panel4.Width - pnlPaginasPeriodoTempo.Width - 3, 0);
            /*selecionando a primeira pagina de navegacao*/
            limparPaginasSelecionadas();
            pagina1infoPeriodo.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"shapes_backgrounds\miniCircleBlack.png");
            pagina1infoPeriodo.SizeMode = PictureBoxSizeMode.Zoom;


            if (tabelasAlvo[paginaAtual].ToLower() == "venda")
            {
                valorCard1.Text = "R$ " + MetodosDB.executarQuerrySimples(
                    "SELECT sum(p.Valor_Unitario * v.quantidade_vendida) FROM venda v " +
                    "inner join produto p on p.id_Produto = v.id_Produto;");
                valorCard2.Text = "R$ " + MetodosDB.executarQuerrySimples(
                    "SELECT round(sum(p.Valor_Unitario * v.quantidade_vendida)/count(v.id_Venda),2) " +
                    "FROM venda v inner join produto p on p.id_Produto = v.id_Produto;");
                valorCard3.Text = MetodosDB.executarQuerrySimples("select count(id_venda) from venda where lower(status) like lower('cancelada');");
                valorCard4.Text = MetodosDB.executarQuerrySimples("select count(id_venda) from venda where lower(status) like lower('Concluída');");
                valorCard5.Text = MetodosDB.executarQuerrySimples("select count(id_venda) from venda where lower(status) like lower('Pendente');");
                valorCard6.Text = MetodosDB.executarQuerrySimples("SELECT COUNT(DISTINCT id_produto) FROM venda;");
                valorCard7.Text = MetodosDB.executarQuerrySimples("SELECT COUNT(DISTINCT id_Cliente) FROM venda;");
                valorCard8.Text = MetodosDB.executarQuerrySimples("select sum(quantidade_vendida) from venda");
            }

            
        }

        private void buscarInfoPorPeriodo_Click(object sender, EventArgs e)
        {
            int qtd = 0, atual=0;
            if (chekVendas.Checked)
                qtd++;
            if (chekPagamentos.Checked)
                qtd++;
            if (chekClientes.Checked)
                qtd++;
            if (chekFornecedores.Checked)
                qtd++;
            String[] tabelasSelecionadas = new string[qtd];
            if (chekVendas.Checked) 
            {
                tabelasSelecionadas[atual] = "venda";
                atual++;
            }
            if (chekPagamentos.Checked)
            {
                tabelasSelecionadas[atual] = ("pagamento");
                atual++;
            }
            if (chekClientes.Checked) {
                tabelasSelecionadas[atual] = ("cliente");
                atual++;
            }
            if (chekFornecedores.Checked) { 
                tabelasSelecionadas[atual] = ("fornecedor");
            }
            atualizarInfoPorPeriodo(tabelasSelecionadas, 0);
        }

        private void mudarPagina(object sender, EventArgs e)
        {
            PictureBox quemChamou = (PictureBox)sender;
            string imagem = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"shapes_backgrounds\miniCircleBlack.png");
            if (pnlPaginasPeriodoTempo.Width == 22)
                return;
            if (pagina1infoPeriodo.SizeMode == PictureBoxSizeMode.Zoom) // se esse for o selecionado (selecionado vai estar SizeMode Zoom)
            {
                if (quemChamou == proximaPagina)
                    selecionaPaginaEspecifica(pagina2infoPeriodo);
                else
                {
                    
                    if(pnlPaginasPeriodoTempo.Width == 22 * 2)
                    {
                        selecionaPaginaEspecifica(pagina2infoPeriodo);
                        return;
                    }
                    else if(pnlPaginasPeriodoTempo.Width == 22 * 3)
                    {
                        selecionaPaginaEspecifica(pagina3infoPeriodo);
                        return;
                    }
                    selecionaPaginaEspecifica(pagina4infoPeriodo);
                }
                return;
            }
            if (pagina2infoPeriodo.SizeMode == PictureBoxSizeMode.Zoom)// se esse for o selecionado (selecionado vai estar SizeMode Zoom)
            {

                if (quemChamou == proximaPagina)
                {
                    if(pnlPaginasPeriodoTempo.Width > 22*2)
                        selecionaPaginaEspecifica(pagina3infoPeriodo);
                    else
                        selecionaPaginaEspecifica(pagina1infoPeriodo);
                }
                else
                    selecionaPaginaEspecifica(pagina1infoPeriodo);
                return;
            }
            if (pagina3infoPeriodo.SizeMode == PictureBoxSizeMode.Zoom)// se esse for o selecionado (selecionado vai estar SizeMode Zoom)
            {
                if (quemChamou == proximaPagina)
                {
                    if (pnlPaginasPeriodoTempo.Width > 22 * 3)
                        selecionaPaginaEspecifica(pagina4infoPeriodo);
                    else
                        selecionaPaginaEspecifica(pagina1infoPeriodo);
                }
                else
                    selecionaPaginaEspecifica(pagina2infoPeriodo);
                return;
            }
            if (pagina4infoPeriodo.SizeMode == PictureBoxSizeMode.Zoom)// se esse for o selecionado (selecionado vai estar SizeMode Zoom)
            {
                if (quemChamou == proximaPagina)
                    selecionaPaginaEspecifica(pagina1infoPeriodo);
                else
                    selecionaPaginaEspecifica(pagina3infoPeriodo);
                return;
            }
        }
        private void limparPaginasSelecionadas()
        {
            string imagem = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"shapes_backgrounds\miniCircleGray.png");
            pagina1infoPeriodo.BackgroundImage = Image.FromFile(imagem);
            pagina2infoPeriodo.BackgroundImage = Image.FromFile(imagem);
            pagina3infoPeriodo.BackgroundImage = Image.FromFile(imagem);
            pagina4infoPeriodo.BackgroundImage = Image.FromFile(imagem);
            pagina1infoPeriodo.SizeMode = PictureBoxSizeMode.Normal;
            pagina2infoPeriodo.SizeMode = PictureBoxSizeMode.Normal;
            pagina3infoPeriodo.SizeMode = PictureBoxSizeMode.Normal;
            pagina4infoPeriodo.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void mudarPaginaNoClickBolinha(object sender, EventArgs e)
        {
            PictureBox objeto = (PictureBox)sender;
            if(objeto.SizeMode != PictureBoxSizeMode.Zoom)
            {
                limparPaginasSelecionadas();
                selecionaPaginaEspecifica(objeto);
            }
        }
        private void selecionaPaginaEspecifica(PictureBox bolinhaAlvo)
        {
            limparPaginasSelecionadas();
            bolinhaAlvo.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"shapes_backgrounds\miniCircleBlack.png");
            bolinhaAlvo.SizeMode = PictureBoxSizeMode.Zoom;
        }

        /*
         * 
         * Bloco de codigo para Relatório de entradas e saidas
         *
         */
        

        private void atualizaGrafico(Object sender, EventArgs e)
        {
            // Limpa séries e áreas do gráfico se já existirem
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Cria área do gráfico
            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            // Cria duas séries (duas barras por coluna)
            Series saida = new Series("Total de saídas");
            Series entrada = new Series("Total de entradas");
            // Define que as séries serão do tipo coluna (barras verticais)
            saida.ChartType = SeriesChartType.Column;
            entrada.ChartType = SeriesChartType.Column;
            //instanciando os possiveis dados de x
            String[] meses = { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez" };
            String[] trimestres = { "Jan Fev Mar", "Abr Mai Jun", "Jul Ago Set", "Out Nov Dez" };
            String[] semestre = { "1º semestre", "2º semestre" };

            if(rdbMes.Checked)
            {
                Random n = new Random();
                for (int i = 0; i < 12; i++)
                {
                    saida.Points.AddXY(meses[i], n.Next(100000));
                    entrada.Points.AddXY(meses[i], n.Next(100000));
                }
            }
            if(rdbTrimestre.Checked)
            {
                Random n = new Random();
                for (int i = 0; i < 4; i++)
                {
                    saida.Points.AddXY(trimestres[i], n.Next(100000));
                    entrada.Points.AddXY(trimestres[i], n.Next(100000));
                }
            }
            else if(rdbSemestre.Checked)
            {
                Random n = new Random();
                for (int i = 0; i < 2; i++)
                {
                    saida.Points.AddXY(semestre[i], n.Next(100000));
                    entrada.Points.AddXY(semestre[i], n.Next(100000));
                }
            }

            //permite saber o valor da coluna ao repousar mouse sobre ela
            saida.ToolTip = "R$ #VALY";
            entrada.ToolTip = "R$ #VALY";
            //mudando cor das colunas
            saida.Color = Color.FromArgb(182, 2, 2);//setando cor das colunas vermelhas
            entrada.Color = Color.FromArgb(17, 134, 50);//setando cor das colunas verdes
            //configura largura das colunas
            saida["PointWidth"] = "0.6";
            entrada["PointWidth"] = "0.6";
            chartArea.AxisX.MajorGrid.Enabled = false; // Desabilita gridlines no eixo X ()
            // Adiciona as séries no gráfico
            chart1.Series.Add(saida);
            chart1.Series.Add(entrada);
            //removendo legendas
            chart1.Legends.Clear();
            chartArea.AxisX.Interval = 1; // Define intervalo no eixo X para espaçar as categorias
        }

    }
}
