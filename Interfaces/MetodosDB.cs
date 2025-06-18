using FinTracker.Models;
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
        public static async Task<MySqlConnection> conexao()
        {
            try
            {
                //MySqlConnection con = new MySqlConnection("Server=db4free.net;Database=bd_fintracker;User Id=manuelagadelho;Password=Ma14082002.");
                MySqlConnection con = new MySqlConnection("server=localhost;database=bd_fintracker;uid=root;pwd=admin;");
                await con.OpenAsync();
                return con;
            }
            catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            return null;
        }

        public static async Task<Admin> logar(String email, String senha)
        {
            Admin admin = null;
            MySqlConnection con = await conexao();
            if (con == null)
                return admin;
            String query = $"SELECT * FROM admin WHERE (email = '{email}') AND senha = '{senha}'";
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
            // Verifica se há linhas para serem lidas
            if (reader.HasRows)
            {
                // Se houver, significa que o login foi bem-sucedido
                while (await reader.ReadAsync())
                {
                    admin = new Admin();
                    admin.SetId_Admin(reader.GetInt32("id_Admin"));
                    admin.SetNome(reader.GetString("nome"));
                    admin.SetEmail(reader.GetString("email"));
                    admin.SetSenha(reader.GetString("senha"));
                }
                return admin;
            }
            return admin;
        }

        public static async Task<bool> solicitarCadastro(Admin admin)
        {
            MySqlConnection con = await conexao();
            if (con == null)
                return false;
            String query = $"INSERT INTO solicitacoes_cadastro (Nome, Email, Senha) VALUES ('{admin.GetNome()}', '{admin.GetEmail()}', '{admin.GetSenha()}')";
            MySqlCommand command = new MySqlCommand(query, con);
            try
            {
                await command.ExecuteNonQueryAsync();
                return true; // Cadastro solicitado com sucesso
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao solicitar cadastro: " + e.Message);
                return false; // Falha ao solicitar cadastro
            }
        }
        public static async Task<List<Admin>> listarSolicitacoesCadastro()
        {
            // Cria uma lista para armazenar os administradores
            List<Admin> admins = new List<Admin>();
            MySqlConnection con = await conexao();
            if (con == null)
                return admins;
            String query = "SELECT * FROM solicitacoes_cadastro";
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
            // Verifica se há linhas para serem lidas
            if (reader.HasRows)
            {
                // Se houver, lê cada linha e adiciona à lista de administradores
                while (await reader.ReadAsync())
                {
                    Admin admin = new Admin();
                    admin.SetId_Admin(reader.GetInt32("id"));
                    admin.SetNome(reader.GetString("Nome"));
                    admin.SetEmail(reader.GetString("Email"));
                    admin.SetSenha(reader.GetString("Senha"));
                    admins.Add(admin);
                }
            }
            // Retorna a lista de administradores
            return admins;
        }

        public static async Task<bool> aceitarCadastro(Admin admin)
        {
            // Cria uma conexão com o banco de dados
            MySqlConnection con = await conexao();
            if (con == null)
                return false;
            // Cria uma consulta SQL para inserir o novo administrador na tabela admin
            String query = $"INSERT INTO admin (nome, email, senha) VALUES ('{admin.GetNome()}', '{admin.GetEmail()}', '{admin.GetSenha()}')";
            MySqlCommand command = new MySqlCommand(query, con);
            try
            {
                // Executa a consulta SQL
                await command.ExecuteNonQueryAsync();
                // Se a inserção for bem-sucedida, deleta a solicitação de cadastro da tabela solicitacoes_cadastro
                String deleteQuery = $"DELETE FROM solicitacoes_cadastro WHERE id = {admin.GetId_Admin()}";
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, con);
                await deleteCommand.ExecuteNonQueryAsync();
                return true; // Cadastro aceito com sucesso
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao aceitar cadastro: " + e.Message);
                return false; // Falha ao aceitar cadastro
            }
        }

        public static async Task<bool> cadastrarDespesa(Despesa despesa)
        {
            // Cria uma conexão com o banco de dados
            MySqlConnection con = await conexao();
            if (con == null)
                return false;
            // Cria uma consulta SQL para inserir a nova despesa na tabela despesas
            String query = $"INSERT INTO pagamento (descricao, metodo, tipo, valor, parcelas, data) " +
                $"VALUES ('{despesa.Descricao}', '{despesa.Metodo}', '{despesa.Tipo}', {despesa.Valor}, {despesa.Parcelas}, '{despesa.Data:yyyy-MM-dd HH:mm:ss}')";
            MySqlCommand command = new MySqlCommand(query, con);
            try
            {
                // Executa a consulta SQL
                await command.ExecuteNonQueryAsync();
                return true; // Despesa cadastrada com sucesso
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao cadastrar despesa: " + e.Message);
                return false; // Falha ao cadastrar despesa
            }
        }
        public static async Task<bool> atualizarDespesa(Despesa despesa)
        {
            // Cria uma conexão com o banco de dados
            MySqlConnection con = await conexao();
            if (con == null)
                return false;
            // Cria uma consulta SQL para atualizar a despesa na tabela despesas
            String query = $"UPDATE pagamento SET descricao = '{despesa.Descricao}', metodo = '{despesa.Metodo}', " +
                $"tipo = '{despesa.Tipo}', valor = {despesa.Valor.ToString().Replace(',', '.')}, parcelas = {despesa.Parcelas}, data = '{despesa.Data:yyyy-MM-dd HH:mm:ss}' " +
                $"WHERE id_Pagamento = {despesa.Id}";
            MySqlCommand command = new MySqlCommand(query, con);
            try
            {
                // Executa a consulta SQL
                await command.ExecuteNonQueryAsync();
                return true; // Despesa atualizada com sucesso
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao atualizar despesa: " + e.Message);
                return false; // Falha ao atualizar despesa
            }
        }

        //metodo excluirDespesa
        public static async Task<bool> excluirDespesa(int idDespesa)
        {
            // Cria uma conexão com o banco de dados
            MySqlConnection con = await conexao();
            if (con == null)
                return false;
            // Cria uma consulta SQL para excluir a despesa da tabela despesas
            String query = $"DELETE FROM pagamento WHERE id_Pagamento = {idDespesa}";
            MySqlCommand command = new MySqlCommand(query, con);
            try
            {
                // Executa a consulta SQL
                await command.ExecuteNonQueryAsync();
                return true; // Despesa excluída com sucesso
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao excluir despesa: " + e.Message);
                return false; // Falha ao excluir despesa
            }
        }

        public static async Task<bool> rejeitarCadastro(int idSolicitacao)
        {
            MySqlConnection con = await conexao();
            if (con == null)
                return false;
            String query = $"DELETE FROM solicitacoes_cadastro WHERE id = {idSolicitacao}";
            MySqlCommand command = new MySqlCommand(query, con);
            try
            {
                await command.ExecuteNonQueryAsync();
                return true; // Cadastro rejeitado com sucesso
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao rejeitar cadastro: " + e.Message);
                return false; // Falha ao rejeitar cadastro
            }
        }

        public static async Task<String> executarQuerrySimples(String querry)
        {
            MySqlConnection con = await conexao();
            if (con == null)
                return null;
            MySqlCommand command = new MySqlCommand(querry, con);
            var resultado = await command.ExecuteScalarAsync();
            String teste = resultado.ToString();
            if (resultado != null)
                return resultado.ToString();
            return null;
        }

        public static async Task<String> getTotalEntradasEntre(String dataInicio, String dataFim)
        {

            MySqlConnection con = await conexao();
            if (con == null)
                return null;
            String query = "SELECT sum(p.Valor_Unitario * v.quantidade_vendida) FROM venda v " +
                "inner join produto p on p.id_Produto = v.id_Produto " +
                "inner join pagamento pag on pag.id_Pagamento = v.id_Pagamento " +
                $"where pag.Data between '{dataInicio}' and '{dataFim}' " +
                "group by v.id_Venda";
            MySqlCommand command = new MySqlCommand(query, con);
            //pegando o resultado do comando sql
            MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
            //declarando variavel para somatorio dos resultados
            double totalEntradas = 0;
            //se tem dados no reader (se consulta retornou dados)
            if (reader.HasRows)
            {
                //faz a leitura de cada linha
                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                        continue;
                    totalEntradas += reader.GetDouble(0);//pega o valor da coluna 0 (1ª coluna) e soma em totalEntradas
                }
            }
            //adiciona em totalEntradas a soma total dos valores na tabela contas a receber se estiver entre data dos parametros deta funcao
            totalEntradas += double.Parse(await executarQuerrySimples($"SELECT sum(valor) FROM conta_a_receber where Previsao_de_Termino >= '{dataFim}'"));

            return totalEntradas.ToString();
        }
        public static async Task<String> getTotalSaidasEntre(String dataInicio, String dataFim)
        {
            MySqlConnection con = await conexao();
            if (con == null)
                return null;
            //declarando variavel para somatorio dos resultados
            double totalSaidas = 0;
            //adiciona em totalEntradas a soma total dos valores na tabela contas a receber se estiver entre data dos parametros deta funcao
            String a = await executarQuerrySimples(
                $"SELECT sum(valor) FROM conta_a_pagar where date_format(Previsao_de_Termino, '%Y-%m-%d')  <= '{dataFim}' " +
                $"or date_format(Data_de_Transacao, '%Y-%m-%d')  >= '{dataInicio}'");
            if(a!="")
                totalSaidas += double.Parse(a);

            return totalSaidas.ToString();
        }

        public static async Task<String[]> getFirstAndLastOfQuery(String query)
        {
            MySqlConnection con = await conexao();
            if (con == null)
                return null;
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
            if (reader == null)
                return null;
            String primeiroValor = "", ultimoValor = "";
            // Verifica se há linhas para serem lidas
            if (await reader.ReadAsync())
                primeiroValor = reader[0].ToString(); // Pegar a primeira célula da primeira linha
            // Continuar a ler até a última linha
            while (await reader.ReadAsync())
                ultimoValor = reader[0].ToString(); // Pegar a primeira célula da linha atual até chegar na ultima
            return new String[] { primeiroValor, ultimoValor };
        }

        public static async Task<bool> deleteVendaById(List<int> idsVenda)
        {
            try
            {
                MySqlConnection conn = await MetodosDB.conexao();
                foreach (int id in idsVenda)
                {
                    MySqlCommand cmdDeletarItensVenda = new MySqlCommand($"delete from itensVenda where id_Venda = {id};", conn);
                    MySqlCommand cmdDeletarVenda = new MySqlCommand($"delete from venda where id_Venda = {id}", conn);
                    await cmdDeletarItensVenda.ExecuteNonQueryAsync();
                    await cmdDeletarVenda.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao deletar venda: " + e.Message);
                return false;
            }
        }

        public static async Task<String> somarResultDeMultiplasQuerrys(String[] querrys)
        {
            decimal resultado = 0;
            foreach (String q in querrys)
            {
                String retornoQuery = await (MetodosDB.executarQuerrySimples(q));
                if (retornoQuery == "" || retornoQuery == null)
                    continue;
                resultado += decimal.Parse(retornoQuery);
            }
            return resultado.ToString();
        }
    }
}
