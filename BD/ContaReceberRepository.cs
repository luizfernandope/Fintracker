using FinTracker.Interfaces;
using FinTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FinTracker.BD
{
    public class ContaReceberRepository
    {
        private string _connectionString;

        public ContaReceberRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ContaReceberRepository()
        {
        }

        public async void AddContaReceber(Conta c)
        {

            MySqlConnection conn = await MetodosDB.conexao();
            string query = "INSERT INTO Conta_a_Receber (Valor, id_Cliente, Metodo_de_Pagamento, Data_de_Transacao, Previsao_de_Termino, descricao) VALUES (@Valor, @idCliente, @MetodoPagamento, @DataTransacao, @PrevisaoTermino, @descricao)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Valor", c.valor);
            cmd.Parameters.AddWithValue("@idCliente", c.id_Cliente);
            cmd.Parameters.AddWithValue("@MetodoPagamento", c.metodoPagamento);
            cmd.Parameters.AddWithValue("@DataTransacao", c.dataTransacao.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@PrevisaoTermino", c.previsaoTermino.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@descricao", c.descricao);
            cmd.ExecuteNonQuery();
        }

        public DataTable GetContasReceber()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Conta_a_Receber", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public async Task<List<Conta>> pegarContas_a_receber()
        {
            List<Conta> contas = new List<Conta>();
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "SELECT * FROM conta_a_receber";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Conta conta = new Conta
                    {
                        id_Conta_a_Receber = reader.GetInt32("id_Conta_a_Receber"),
                        id_Fornecedor = reader.GetInt32("id_Cliente"),
                        valor = reader.GetDouble("Valor"),
                        metodoPagamento = reader.GetString("Metodo_de_Pagamento"),
                        dataTransacao = reader.GetDateTime("Data_de_Transacao"),
                        previsaoTermino = reader.GetDateTime("Previsao_de_Termino"),
                        descricao = reader.GetString("Descricao")

                    };
                    conta.nomeCliente = await new ClienteRepository().pegarNomeByid(conta.id_Fornecedor);
                    contas.Add(conta);
                }
            }
            return contas;
        }

        public async void UpdateContaReceber(Conta c)
        {

            MySqlConnection conn = await MetodosDB.conexao();
            string query = "UPDATE Conta_a_Receber SET Valor = @Valor, descricao = @descricao, id_Cliente = @idCliente, Metodo_de_Pagamento = @MetodoPagamento, Data_de_Transacao = @DataTransacao, Previsao_de_Termino = @PrevisaoTermino WHERE id_Conta_a_Receber = @idContaReceber";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idContaReceber", c.id_Conta_a_Receber);
            cmd.Parameters.AddWithValue("@Valor", c.valor);
            cmd.Parameters.AddWithValue("@idCliente", c.id_Cliente);
            cmd.Parameters.AddWithValue("@MetodoPagamento", c.metodoPagamento);
            cmd.Parameters.AddWithValue("@descricao", c.descricao);
            cmd.Parameters.AddWithValue("@DataTransacao", c.dataTransacao.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@PrevisaoTermino", c.previsaoTermino.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
        }

        public async void DeleteContaReceber(int idContaReceber)
        {
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "DELETE FROM Conta_a_Receber WHERE id_Conta_a_Receber = @idContaReceber";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idContaReceber", idContaReceber);
            cmd.ExecuteNonQuery();
        }
    }
}
