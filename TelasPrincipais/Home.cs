using FinTracker.AlternativeTelas;
using FinTracker.Interfaces;
using FinTracker.TelasPrincipais;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
        private String[] tabelasConfirmadas = null;//variavel para identificar tabelas que foram confirmadas em informações por periodo de tempo

        public Home()
        {
            InitializeComponent();
            lblData.Text = DateTime.Now.ToString(@"ddddd, dd \de MMMMM \de yyyy.");
            
            anoDoPeriodo.MaxDate = DateTime.Today;
            cmbReceitaUltimo.SelectedIndex = 0;
            cmbTransacoesNeste.SelectedIndex = 0;
            tbData1.Text = DateTime.Today.AddMonths(-1).ToString("ddMMyyyy");
            tbData2.Text = DateTime.Today.ToString("ddMMyyyy");
            qtdClientes.Text = MetodosDB.executarQuerrySimples("select count(id_cliente) from cliente");
            qtdFornecedores.Text = MetodosDB.executarQuerrySimples("select count(id_fornecedor) from fornecedor");
            qtdVendas.Text = MetodosDB.executarQuerrySimples("select count(id_venda) from venda");
            buscarInfoPorPeriodo_Click(null, null);
            atualizaGrafico(null, null);
            
            addLabelTip(receitaAllTime, "Receita de todas as vendas + todas contas recebidas.");
            addLabelTip(despesasAllTime, "Soma de todas despesas (sem contar impostos).");
            atualizarReceitaDoUltimo(null,null);
            atualizaTransacoesPendentes(null, null);
        }

        private String somarResultDeMultiplasQuerrys(String[] querrys)
        {
            double resultado=0;
            foreach (String q in querrys)
            {
                String retornoQuery = (MetodosDB.executarQuerrySimples(q));
                if (retornoQuery == "")
                    continue;
                resultado += double.Parse(retornoQuery);
            }
            return resultado.ToString();
        }
        private void addLabelTip(object componente,String tip)
        {
            System.Windows.Forms.ToolTip dica = new System.Windows.Forms.ToolTip();
            dica.SetToolTip((Label)componente, tip);
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
            if (paginaAtual == 0)
            {
                limparPaginasSelecionadas();
                pagina1infoPeriodo.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + @"shapes_backgrounds\miniCircleBlack.png");
                pagina1infoPeriodo.SizeMode = PictureBoxSizeMode.Zoom;
            }
            
            String[][] tituloCard = new string[1][];
            
            if (tabelasAlvo[paginaAtual].ToLower() == "venda")
            {
                tituloCard[0] = new string[]{
                    "Valor total de vendas",
                    "Valor médio de venda",
                    "Vendas canceladas",
                    "Vendas concluídas",
                    "Vendas pendentes",
                    "Nº de produdos únicos vendidos",
                    "Nº Clientes com vendas registradas neste período",
                    "Quantidade de produtos vendidos" };

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
            else if (tabelasAlvo[paginaAtual].ToLower() == "pagamento")
            {
                tituloCard[0] = new string[]{
                    "Quantidade pagamentos feitos",
                    "Valor total dos pagamentos realizados",
                    "Valor médio de pagamento",
                    "Quantidade de pagamentos abertos nesse periodo e ainda pendentes",
                    "Valor total dos pagamentos ainda pendentes deste periodo",
                    "",
                    "",
                    "" };

            }
            else if (tabelasAlvo[paginaAtual].ToLower() == "cliente")
            {
                tituloCard[0] = new string[]{
                    "Total de cliente até limite superior da data escolhida",
                    "Nº de novos clientes cadastrados neste periodo",
                    "Quantidade de clientes ativos",
                    "Quantidade de clientes inativos",
                    "% de clientes ativos",
                    "% de clientes inativos",
                    "",
                    "" };

            }
            else if (tabelasAlvo[paginaAtual].ToLower() == "fornecedor")
            {
                tituloCard[0] = new string[]{
                    "Quantidade de fornecedores",
                    "Quantidade de fornecedores ativos",
                    "Quantidade de fornecedores inativos",
                    "% de fornecedores ativos",
                    "% de fornecedores inativos",
                    "Nº de novos fornecedores cadastrados neste periodo",
                    "",
                    "" };

            }

            tituloCard1.Text = tituloCard[0][0];
            tituloCard2.Text = tituloCard[0][1];
            tituloCard3.Text = tituloCard[0][2];
            tituloCard4.Text = tituloCard[0][3];
            tituloCard5.Text = tituloCard[0][4];
            tituloCard6.Text = tituloCard[0][5];
            tituloCard7.Text = tituloCard[0][6];
            tituloCard8.Text = tituloCard[0][7];
        }

        private void buscarInfoPorPeriodo_Click(object sender, EventArgs e)
        {
            int qtd = 0, atual = 0;
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
            if (chekClientes.Checked)
            {
                tabelasSelecionadas[atual] = ("cliente");
                atual++;
            }
            if (chekFornecedores.Checked)
            {
                tabelasSelecionadas[atual] = ("fornecedor");
            }
            tabelasConfirmadas = tabelasSelecionadas;
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
            //String[] tabelasSelecionadas = pegarTabelasSelecionadas();
            int pagina = 0;//começa na pagina 1
            if (bolinhaAlvo == pagina2infoPeriodo)
                pagina = 1;
            else if (bolinhaAlvo == pagina3infoPeriodo)
                pagina = 2;
            else if (bolinhaAlvo == pagina4infoPeriodo)
                pagina = 3;
            atualizarInfoPorPeriodo(tabelasConfirmadas, pagina);
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
            
            String ano = anoDoPeriodo.Value.ToString("yyyy");
            if (rdbMes.Checked)
            {
                for (int i = 0; i < 12; i++)
                {
                    String entradaAtual = somarResultDeMultiplasQuerrys(new string[]
                    {   //total vendas + total recebidos
                        "SELECT sum(p.Valor_Unitario * v.quantidade_vendida) FROM venda v " +
                        "inner join produto p on p.id_Produto = v.id_Produto " +
                        "inner join pagamento pag on pag.id_Pagamento = v.id_Pagamento " +
                        $"where date_format(pag.Data, '%m/%Y') = '{(i+1):D2}/{ano}';"
                        ,
                        $"SELECT sum(valor) FROM conta_a_receber where date_format(Data_de_Transacao,'%m/%Y') = '{(i+1):D2}/{ano}'" 
                    });

                    String saidaAtual = MetodosDB.executarQuerrySimples($"SELECT sum(valor) FROM conta_a_pagar where date_format(Data_de_Transacao, '%m/%Y') = '{(i + 1):D2}/{ano}'");

                    entrada.Points.AddXY(meses[i], double.Parse(entradaAtual));
                    if(saidaAtual != "")
                        saida.Points.AddXY(meses[i], double.Parse(saidaAtual));
                    else
                        saida.Points.AddXY(meses[i], 0.0);

                }
            }
            if(rdbTrimestre.Checked)
            {
                for (int i = 0; i < 4; i++)
                {
                    int volta = i + 1;
                    int mesFim = volta * 3;
                    String entradaAtual = somarResultDeMultiplasQuerrys(new string[]
                    {   //total vendas + total recebidos
                        "SELECT sum(p.Valor_Unitario * v.quantidade_vendida) FROM venda v " +
                        "inner join produto p on p.id_Produto = v.id_Produto " +
                        "inner join pagamento pag on pag.id_Pagamento = v.id_Pagamento " +
                        $"where YEAR(pag.Data) = {ano} and date_format(pag.Data, '%m')  between '{(mesFim-2):D2}' and '{(mesFim):D2}'"
                        ,
                        $"SELECT sum(valor) FROM conta_a_receber where YEAR(Data_de_Transacao) = {ano} and " +
                        $"date_format(Data_de_Transacao,'%m') between '{(mesFim-2):D2}' and '{(mesFim):D2}'"
                    });

                    String saidaAtual = MetodosDB.executarQuerrySimples($"SELECT sum(valor) FROM conta_a_pagar where YEAR(Data_de_Transacao) = {ano} " +
                        $"and date_format(Data_de_Transacao, '%m')  between '{(mesFim - 2):D2}' and '{(mesFim):D2}'");

                    entrada.Points.AddXY(trimestres[i], double.Parse(entradaAtual));
                    if (saidaAtual != "")
                        saida.Points.AddXY(trimestres[i], double.Parse(saidaAtual));
                    else
                        saida.Points.AddXY(meses[i], 0.0);
                }
            }
            else if(rdbSemestre.Checked)
            {
                for (int i = 0; i < 2; i++)
                {
                    int mesFim = (i+1) * 6;
                    String entradaAtual = somarResultDeMultiplasQuerrys(new string[]
                    {   //total vendas + total recebidos
                        "SELECT sum(p.Valor_Unitario * v.quantidade_vendida) FROM venda v " +
                        "inner join produto p on p.id_Produto = v.id_Produto " +
                        "inner join pagamento pag on pag.id_Pagamento = v.id_Pagamento " +
                        $"where YEAR(pag.Data) = {ano} and date_format(pag.Data, '%m')  between '{(mesFim-5):D2}' and '{(mesFim):D2}'"
                        ,
                        $"SELECT sum(valor) FROM conta_a_receber where YEAR(Data_de_Transacao) = {ano} and " +
                        $"date_format(Data_de_Transacao,'%m') between '{(mesFim-5):D2}' and '{(mesFim):D2}'"
                    });

                    String saidaAtual = MetodosDB.executarQuerrySimples($"SELECT sum(valor) FROM conta_a_pagar where YEAR(Data_de_Transacao) = {ano} " +
                        $"and date_format(Data_de_Transacao, '%m')  between '{(mesFim - 5):D2}' and '{(mesFim):D2}'");

                    entrada.Points.AddXY(semestre[i], double.Parse(entradaAtual));
                    if (saidaAtual != "")
                        saida.Points.AddXY(semestre[i], double.Parse(saidaAtual));
                    else
                        saida.Points.AddXY(meses[i], 0.0);
                }
            }

            //colocando os valores das legendas Y no formato de Moeda
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "C2";  // "C2" para exibir como moeda com 2 casas decimais
            //permite saber o valor de cada coluna ao repousar mouse sobre ela
            saida.ToolTip = "R$ #VALY{N2}"; //{N2} para duas casas decimais
            entrada.ToolTip = "R$ #VALY{N2}"; 
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


        /*
         * 
         * Bloco de codigo para seção dos 3 cards
         *
         */

        private void changeVisibityDadosTodosTempos(object sender, EventArgs e)
        {
            PictureBox queChamou = (PictureBox)sender;
            if (queChamou == verReceita)
            {
                if (receitaAllTime.Text == "-")
                {
                    receitaAllTime.Text = "R$ " + somarResultDeMultiplasQuerrys(new string[]
                    {   //total vendas + total recebidos
                        "SELECT sum(p.Valor_Unitario * v.quantidade_vendida) FROM venda v inner join produto p on p.id_Produto = v.id_Produto",
                        "select sum(valor) from conta_a_receber"
                    });
                    verReceita.BackgroundImage = Properties.Resources.icon_olhoFechadoBranco_1_;
                }
                else
                {
                    receitaAllTime.Text = "-";
                    verReceita.BackgroundImage = Properties.Resources.icon_olhoAberto;
                }
            }
            if (queChamou == verSaidas)
            {
                if (despesasAllTime.Text == "-")
                {
                    despesasAllTime.Text = "R$ " + somarResultDeMultiplasQuerrys(new string[]
                    {   //total pagos + total a pagar
                        "SELECT sum(Valor) FROM  pagamento",
                        "SELECT sum(c.Valor)FROM  conta_a_pagar c"
                    });
                    verSaidas.BackgroundImage = Properties.Resources.icon_olhoFechadoBranco_1_;
                }
                else
                {
                    despesasAllTime.Text = "-";
                    verSaidas.BackgroundImage = Properties.Resources.icon_olhoAberto;
                }
            }
        }

        private void atualizarReceitaDoUltimo(object sender, EventArgs e)
        {
            String data1 = "", data2 = DateTime.Today.ToString("yyyy-MM-dd");
            if (cmbReceitaUltimo.Text == "Mês")
                data1 = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            else if (cmbReceitaUltimo.Text == "Bimestre")
                data1 = DateTime.Today.AddMonths(-2).ToString("yyyy-MM-dd");
            else if (cmbReceitaUltimo.Text == "Trimestre")
                data1 = DateTime.Today.AddMonths(-3).ToString("yyyy-MM-dd");
            else if (cmbReceitaUltimo.Text == "Semestre")
                data1 = DateTime.Today.AddMonths(-6).ToString("yyyy-MM-dd");
            else if (cmbReceitaUltimo.Text == "Ano")
                data1 = DateTime.Today.AddYears(-1).ToString("yyyy-MM-dd");

            entradasPeriodo.Text = MetodosDB.getTotalEntradasEntre(data1, data2);
            saidasPeriodo.Text = MetodosDB.getTotalSaidasEntre(data1, data2);
            double entradas = double.Parse(entradasPeriodo.Text);
            double saidas = double.Parse(saidasPeriodo.Text);
            double lucro = entradas - saidas;
            lucroPeriodo.Text = lucro.ToString();
            entradasPeriodo.Text = "R$ " + entradasPeriodo.Text;
            saidasPeriodo.Text = "R$ " + saidasPeriodo.Text;
            lucroPeriodo.Text = "R$ " + lucroPeriodo.Text;
        }

        private void atualizaTransacoesPendentes(object sender, EventArgs e)
        {
            String dataInicio = "", dataFim = DateTime.Today.ToString("yyyy-MM");
            if (cmbTransacoesNeste.Text == "Mês")
                dataInicio = DateTime.Today.AddMonths(-1).ToString("yyyy-MM");
            else if (cmbTransacoesNeste.Text == "Bimestre")
                dataInicio = DateTime.Today.AddMonths(-2).ToString("yyyy-MM");
            else if (cmbTransacoesNeste.Text == "Trimestre")
                dataInicio = DateTime.Today.AddMonths(-3).ToString("yyyy-MM");
            else if (cmbTransacoesNeste.Text == "Semestre")
                dataInicio = DateTime.Today.AddMonths(-6).ToString("yyyy-MM");
            else if (cmbTransacoesNeste.Text == "Ano")
                dataInicio = DateTime.Today.AddMonths(-12).ToString("yyyy-MM");
            double aPagar = double.Parse(MetodosDB.executarQuerrySimples(
                $"SELECT sum(valor) FROM conta_a_pagar where date_format(Previsao_de_Termino, '%Y-%m')  <= '{dataFim}' " +
                $"and date_format(Previsao_de_Termino, '%Y-%m')  >= '{dataInicio}'"));
            double aReceber = double.Parse(MetodosDB.executarQuerrySimples(
                $"SELECT sum(valor) FROM conta_a_receber where date_format(Previsao_de_Termino, '%Y-%m')  <= '{dataFim}' " +
                $"and date_format(Previsao_de_Termino, '%Y-%m')  >= '{dataInicio}'"));
            circularProgressBar1.Value = (int)(aPagar / aReceber) * 10;
            aPagarNeste.Text = "R$ " + aPagar;
            aReceberNeste.Text = "R$ " + aReceber;
        }
    }
}
