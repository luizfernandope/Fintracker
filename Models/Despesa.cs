using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Metodo { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public int Parcelas { get; set; }
        public DateTime Data { get; set; }

        public Despesa() { }

        public Despesa(int id, DateTime data, String Metodo, String tipo,int Parcelas, decimal valor, string descricao)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.Valor = valor;
            this.Data = data;
            this.Metodo = Metodo;
            this.Tipo = tipo;
            this.Parcelas = Parcelas;
            this.Valor = valor;
        }
        public Despesa(DateTime data, String Metodo, String tipo,int Parcelas, decimal valor, string descricao)
        {
            this.Descricao = descricao;
            this.Valor = valor;
            this.Data = data;
            this.Metodo = Metodo;
            this.Tipo = tipo;
            this.Parcelas = Parcelas;
            this.Valor = valor;
        }

        public override string ToString()
        {
            return $"{Descricao} - {Valor:C} em {Data:dd/MM/yyyy}";
        }
    }
}
