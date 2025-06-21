using FinTracker.Interfaces;
using FinTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FinTracker.BD
{
    public class ContaPagarRepository
    {
        private string _connectionString;

        public ContaPagarRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ContaPagarRepository()
        {
        }

        public async void AddContaPagar(Conta conta)
        {

            MySqlConnection conn = await MetodosDB.conexao();
            string query = "INSERT INTO Conta_a_Pagar (Valor, id_Fornecedor, Metodo_de_Pagamento, Data_de_Transacao, Previsao_de_Termino, descricao) VALUES (@Valor, @idFornecedor, @MetodoPagamento, @DataTransacao, @PrevisaoTermino, @descricao)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Valor", conta.valor);
            cmd.Parameters.AddWithValue("@idFornecedor", conta.id_Fornecedor);
            cmd.Parameters.AddWithValue("@MetodoPagamento", conta.metodoPagamento);
            cmd.Parameters.AddWithValue("@descricao", conta.descricao);
            cmd.Parameters.AddWithValue("@DataTransacao", conta.dataTransacao.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@PrevisaoTermino", conta.previsaoTermino.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
        }

        public DataTable GetContasPagar()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Conta_a_Pagar", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public async Task<List<Conta>> pegarContas_a_pagar()
        {
            List<Conta> contas = new List<Conta>();
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "SELECT * FROM conta_a_pagar";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = (MySqlDataReader) await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Conta conta = new Conta
                        {
                            id_Conta_a_Pagar = reader.GetInt32("id_Conta_a_Pagar"),
                            id_Fornecedor = reader.GetInt32("id_Fornecedor"),
                            valor = reader.GetDouble("Valor"),
                            metodoPagamento = reader.GetString("Metodo_de_Pagamento"),
                            dataTransacao = reader.GetDateTime("Data_de_Transacao"),
                            previsaoTermino = reader.GetDateTime("Previsao_de_Termino") ,
                            descricao = reader.GetString("Descricao")
                        };
                        conta.nomeFornecedor = await new FornecedorRepository().pegarNomeByid(conta.id_Fornecedor);
                        contas.Add(conta);
                    }
                }
            return contas;
        }

        public async void UpdateContaPagar(Conta c)
        {
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "UPDATE Conta_a_Pagar SET Valor = @Valor, descricao = @descricao, id_Fornecedor = @idFornecedor, Metodo_de_Pagamento = @MetodoPagamento, Data_de_Transacao = @DataTransacao, Previsao_de_Termino = @PrevisaoTermino WHERE id_Conta_a_Pagar = @idContaPagar";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idContaPagar", c.id_Conta_a_Pagar);
            cmd.Parameters.AddWithValue("@Valor", c.valor);
            cmd.Parameters.AddWithValue("@idFornecedor", c.id_Fornecedor);
            cmd.Parameters.AddWithValue("@MetodoPagamento", c.metodoPagamento);
            cmd.Parameters.AddWithValue("@descricao", c.descricao);
            cmd.Parameters.AddWithValue("@DataTransacao", c.dataTransacao.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@PrevisaoTermino", c.previsaoTermino.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
        }

        public async void DeleteContaPagar(int idContaPagar)
        {
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "DELETE FROM Conta_a_Pagar WHERE id_Conta_a_Pagar = @idContaPagar";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idContaPagar", idContaPagar);
            cmd.ExecuteNonQuery();
        }
    }
}
