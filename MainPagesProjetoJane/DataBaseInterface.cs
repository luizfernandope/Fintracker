using MainPagesProjetoJane.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MainPagesProjetoJane
{
    internal class DataBaseInterface
    {
        private SqlCeConnection ConectarBD()
        {
            string path = Application.StartupPath + @"\BD\bdSqlCeTeste.sdf";
            string strConection = @"DataSource=" + path + "; Password='123'";
            SqlCeEngine bd = new SqlCeEngine(strConection);
            if (!File.Exists(path))
                bd.CreateDatabase();
            bd.Dispose(); //libera recursos
            try
            {
                SqlCeConnection conexao = new SqlCeConnection(strConection);
                conexao.Open();
                return conexao;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            return null;
        }


        public void criarTabelas()
        {
            string query = "CREATE TABLE Clientes (CPF NVARCHAR(14) NOT NULL primary key, nome NVARCHAR(80), Telefone NVARCHAR(19), Email NVARCHAR(255), Endereço NVARCHAR(255), Senha NVARCHAR(32));";
            
            SqlCeConnection connection = ConectarBD();
            SqlCeCommand command = new SqlCeCommand(query, connection);
            if (connection == null)
                return;
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Table cliente criada com sucesso!");
                command.Dispose(); //Libera recursos
                connection.Close();
            }catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        public bool addNewCliente(string CPF, string nome, string telefone, string email, string endereco)
        {
            string query = $"insert into Clientes (CPF, nome, Telefone, Email, Endereço) values ('{CPF}','{nome}','{telefone}','{email}','{endereco}');";

            SqlCeConnection connection = ConectarBD();
            SqlCeCommand command = new SqlCeCommand(query, connection);
            if (connection == null)
                return false;
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Cliente adicionado com sucesso!");
                command.Dispose(); //Libera recursos
                connection.Close();
                return true;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: "+erro.Message);
            }
            return false;
        }

        public void updateCliente(Cliente cliente, string cpfOld)
        {
            string query = $"update Clientes set CPF ='{cliente.CPF}', nome = '{cliente.nome}', Telefone = '{cliente.telefone}', Email = '{cliente.email}', Endereço = '{cliente.endereco}' where CPF = {cpfOld};";

            SqlCeConnection connection = ConectarBD();
            SqlCeCommand command = new SqlCeCommand(query, connection);
            if (connection == null)
                return;
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Cliente atualizado com sucesso!");
                command.Dispose(); //Libera recursos
                connection.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
        }

        public void removeCliente(List<string> listaDeCpf)
        {
            int qtdExcluida = 0, qtdTotal=listaDeCpf.Count;
            foreach(string cpf in listaDeCpf)
            {
                string query = $"delete from Clientes where CPF = '{cpf}';";

                SqlCeConnection connection = ConectarBD();
                SqlCeCommand command = new SqlCeCommand(query, connection);
                if (connection == null)
                    return;
                try
                {
                    command.ExecuteNonQuery();
                    command.Dispose(); //Libera recursos
                    connection.Close();
                    qtdExcluida++;
                }
                catch (Exception erro)
                {
                    MessageBox.Show($"Erro ao excluir CPF {cpf}:\n" + erro.Message);
                }
            }
                    MessageBox.Show($"{qtdExcluida}/{qtdTotal} clientes apagados.");

        }

        public DataTable buscaClientesAuto(string strBusca)
        {
            string path = Application.StartupPath + @"\BD\bdSqlCeTeste.sdf";
            string strConection = @"DataSource=" + path + "; Password='123'";
            string query = "select CPF, nome, Telefone, Email, Endereço from Clientes " +
                $"where CPF LIKE '%{strBusca}%' OR nome LIKE '%{strBusca}%' OR Telefone LIKE '%{strBusca}%' OR Email LIKE '%{strBusca}%' OR Endereço LIKE '%{strBusca}%';";

            SqlCeConnection connection = ConectarBD();
            if (connection == null)
                return null;
            try
            {

                DataTable dataTable = new DataTable(); //tabela de dados 
                SqlCeDataAdapter sqlCeData = new SqlCeDataAdapter(query, strConection);//Adaptador com os dados do BD para preencher dataTable
                sqlCeData.Fill(dataTable);//preenchendo dataTabe com dados do adapter
                connection.Close();
                return dataTable;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
            return null;
        }


        public DataTable selectClient()
        {
            string path = Application.StartupPath + @"\BD\bdSqlCeTeste.sdf";
            string strConection = @"DataSource=" + path + "; Password='123'";
            string query= "select CPF, nome, Telefone, Email, Endereço from Clientes;";

            SqlCeConnection connection = ConectarBD();
            if (connection == null)
                return null;
            try
            {
                
                DataTable dataTable = new DataTable(); //tabela de dados 
                SqlCeDataAdapter sqlCeData = new SqlCeDataAdapter(query, strConection);//Adaptador com os dados do BD para preencher dataTable
                sqlCeData.Fill(dataTable);//preenchendo dataTabe com dados do adapter
                connection.Close();
                return dataTable;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
            return null;
        }
    }
}
