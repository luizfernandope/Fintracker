using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Models
{
    public class Admin
    {
        private int id_Admin;
        private string Nome;
        private string email, senha;

        public Admin()
        {
        }
        public Admin(int id_Admin, string nome, string email, string senha)
        {
            this.id_Admin = id_Admin;
            Nome = nome;
            this.email = email;
            this.senha = senha;
        }
        public Admin(string nome, string email, string senha)
        {
            this.id_Admin = id_Admin;
            Nome = nome;
            this.email = email;
            this.senha = senha;
        }

        //getters and setter
        public int GetId_Admin()
        {
            return id_Admin;
        }
        public void SetId_Admin(int id_Admin)
        {
            this.id_Admin = id_Admin;
        }
        public string GetNome()
        {
            return Nome;
        }
        public void SetNome(string nome)
        {
            Nome = nome;
        }
        public string GetEmail()
        {
            return email;
        }
        public void SetEmail(string email)
        {
            this.email = email;
        }
        public string GetSenha()
        {
            return senha;
        }
        public void SetSenha(string senha)
        {
            this.senha = senha;
        }
        public override string ToString()
        {
            return $"ID: {id_Admin}, Nome: {Nome}, Email: {email}";
        }

    }
}
