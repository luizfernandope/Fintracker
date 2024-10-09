using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FinTracker.BD
{
    public class ClienteRepository
    {
        private string _connectionString;

        public ClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddCliente(string nome, DateTime dataCadastro, string cnpj, string endereco, string bairro, string cidade, string estado, string cep, string telefone, string email, string status)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Cliente (Nome, Data_de_Cadastro, CNPJ, Endereço, Bairro, Cidade, Estado, CEP, Telefone, Email, Status) " +
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

        public DataTable GetClientes()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Cliente", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void UpdateCliente(int idCliente, string nome, DateTime dataCadastro, string cnpj, string endereco, string bairro, string cidade, string estado, string cep, string telefone, string email, string status)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Cliente SET Nome = @Nome, Data_de_Cadastro = @DataCadastro, CNPJ = @CNPJ, Endereço = @Endereco, Bairro = @Bairro, Cidade = @Cidade, Estado = @Estado, CEP = @CEP, Telefone = @Telefone, Email = @Email, Status = @Status WHERE id_Cliente = @idCliente";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
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

        public void DeleteCliente(int idCliente)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Cliente WHERE id_Cliente = @idCliente";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
