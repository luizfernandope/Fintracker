using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Models
{
    public class Cliente
    {
        public int id_Cliente { get; set; }
        public string Nome { get; set; }
        public string Data_de_Cadastro { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public string Bairro   { get; set; }
        public string Cidade   { get; set; }
        public string Estado   { get; set; }
        public string CEP      { get; set; }
        public string Telefone { get; set; }
        public string Email    { get; set; }
        public string Status { get; set; }

        public Cliente(String nome, string Data_de_Cadastro, String cnpj, String endereco, String bairro, String cidade, String estado, String cep, String telefone, String email, String status)
        {
            this.Nome = nome;
            this.CNPJ= cnpj;
            this.Data_de_Cadastro = Data_de_Cadastro;
            this.Endereco = endereco;
            this.Bairro = bairro;
            this.Cidade = cidade;
            this.Estado = estado;
            this.CEP = cep;
            this.Telefone = telefone;
            this.Email = email;
            this.Status= status;
        }
        public Cliente(int id, String nome)
        {
            this.id_Cliente = id;
            this.Nome = nome;
        }
        public Cliente()
        {
            this.Status = "ativo";
        }
    }
}
