using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FinTracker.BD
{
    public class FornecedorRepository
    {
        private string _connectionString;

        public FornecedorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddFornecedor(string nome, DateTime dataCadastro, string cnpj, string endereco, string bairro, string cidade, string estado, string cep, string telefone, string email, string status)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Fornecedor (Nome, Data_de_Cadastro, CNPJ, Endereço, Bairro, Cidade, Estado, CEP, Telefone, Email, Status) " +
                               "VALUES (@Nome, @DataCadastro, @CNPJ, @Endereco, @Bairro, @Cidade, @Estado, @CEP, @Telefone, @Email, @Status)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@DataCadastro", dataCadastro);
                cmd.Parameters.AddWithValue("@CNPJ", cnpj);
                cmd.Parameters.AddWithValue("@Endereco", endereco);
                cmd.Parameters.AddWithValue("@Bairro", bairro);
                cmd.Parameters.AddWithValue("@Cidade", cidade);
                cmd.Parameters.AddWithValue("@Estado", estado);
                cmd.Parameters.AddWithValue("@CEP", cep);
                cmd.Parameters.AddWithValue("@Telefone", telefone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetFornecedores()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Fornecedor", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void UpdateFornecedor(int idFornecedor, string nome, DateTime dataCadastro, string cnpj, string endereco, string bairro, string cidade, string estado, string cep, string telefone, string email, string status)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Fornecedor SET Nome = @Nome, Data_de_Cadastro = @DataCadastro, CNPJ = @CNPJ, Endereço = @Endereco, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, CEP = @CEP, Telefone = @Telefone, Email = @Email, Status = @Status WHERE id_Fornecedor = @idFornecedor";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idFornecedor", idFornecedor);
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@DataCadastro", dataCadastro);
                cmd.Parameters.AddWithValue("@CNPJ", cnpj);
                cmd.Parameters.AddWithValue("@Endereco", endereco);
                cmd.Parameters.AddWithValue("@Bairro", bairro);
                cmd.Parameters.AddWithValue("@Cidade", cidade);
                cmd.Parameters.AddWithValue("@Estado", estado);
                cmd.Parameters.AddWithValue("@CEP", cep);
                cmd.Parameters.AddWithValue("@Telefone", telefone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteFornecedor(int idFornecedor)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Fornecedor WHERE id_Fornecedor = @idFornecedor";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idFornecedor", idFornecedor);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
