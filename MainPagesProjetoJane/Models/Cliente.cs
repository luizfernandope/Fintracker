using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainPagesProjetoJane.Models
{
    public class Cliente
    {
        public string CPF;
        public string nome;
        public string telefone;
        public string email;
        public string endereco;

        public Cliente(string CPF, string nome, string telefone, string email, string endereco)
        {
            this.CPF = CPF;
            this.nome = nome;
            this.telefone = telefone;
            this.email = email;
            this.endereco = endereco;
        }
    }
}
