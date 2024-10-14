using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.Interfaces
{
    internal static class MetodosDB
    {
        public static MySqlConnection conexao()
        {
            try
            {
                MySqlConnection con = new MySqlConnection("Server=localhost;Database=BD_FinTracker;User Id=root;Password=admin");
                con.Open();
                return con;
            }
            catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            return null;
        }

        public static String executarQuerrySimples(String querry)
        {
            MySqlConnection con = conexao();
            if (con == null)
                return null;
            MySqlCommand command = new MySqlCommand(querry, con);
            var resultado = command.ExecuteScalar();
            String teste = resultado.ToString();
            if (resultado != null)
                return resultado.ToString();
            return null;
        }

        public static String getTotalEntradasEntre(String dataInicio, String dataFim)
        {

            MySqlConnection con = conexao();
            if (con == null)
                return null;
            String query = "SELECT sum(p.Valor_Unitario * v.quantidade_vendida) FROM venda v " +
                "inner join produto p on p.id_Produto = v.id_Produto " +
                "inner join pagamento pag on pag.id_Pagamento = v.id_Pagamento " +
                $"where pag.Data between '{dataInicio}' and '{dataFim}' " +
                "group by v.id_Venda";
            MySqlCommand command = new MySqlCommand(query, con);
            //pegando o resultado do comando sql
            MySqlDataReader reader = command.ExecuteReader();
            //declarando variavel para somatorio dos resultados
            double totalEntradas = 0;
            //se tem dados no reader (se consulta retornou dados)
            if (reader.HasRows)
            {
                //faz a leitura de cada linha
                while (reader.Read())
                    totalEntradas += reader.GetDouble(0);//pega o valor da coluna 0 (1ª coluna) e soma em totalEntradas
            }
            //adiciona em totalEntradas a soma total dos valores na tabela contas a receber se estiver entre data dos parametros deta funcao
            totalEntradas += double.Parse(executarQuerrySimples($"SELECT sum(valor) FROM conta_a_receber where Previsao_de_Termino >= '{dataFim}'"));

            return totalEntradas.ToString();
        }
        public static String getTotalSaidasEntre(String dataInicio, String dataFim)
        {
            MySqlConnection con = conexao();
            if (con == null)
                return null;
            //declarando variavel para somatorio dos resultados
            double totalSaidas = 0;
            //adiciona em totalEntradas a soma total dos valores na tabela contas a receber se estiver entre data dos parametros deta funcao
            String a = executarQuerrySimples(
                $"SELECT sum(valor) FROM conta_a_pagar where date_format(Previsao_de_Termino, '%Y-%m-%d')  <= '{dataFim}' " +
                $"or date_format(Data_de_Transacao, '%Y-%m-%d')  >= '{dataInicio}'");
            if(a!="")
                totalSaidas += double.Parse(a);

            return totalSaidas.ToString();
        }
    }
}
