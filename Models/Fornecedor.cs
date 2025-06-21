using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Models
{
    public class Fornecedor
    {
        public int id;
        public string Nome;
        public string Data_de_Cadastro;
        public string CNPJ;
        public string Endereco;
        public string Bairro;
        public string Cidade;
        public string Estado;
        public string CEP;
        public string Telefone;
        public string Email;
        public string Status;

        public Fornecedor()
        {
        }
        public Fornecedor(int id,string nome)
        {
            this.id = id;
            this.Nome = nome;
        }
    }
}
