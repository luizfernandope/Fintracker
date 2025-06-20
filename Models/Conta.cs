using FinTracker.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Models
{
    public class Conta
    {
        public int id_Conta_a_Receber { get; set; }
        public int id_Conta_a_Pagar { get; set; }
        public int id_Fornecedor { get; set; }
        public int id_Cliente { get; set; }
        public int id_Pagamento { get; set; }
        public double valor { get; set; }
        public string descricao { get; set; }
        public DateTime dataTransacao { get; set; }
        public DateTime previsaoTermino { get; set; }
        public string metodoPagamento { get; set; }
        public string nomeFornecedor { get; set; }
        public string nomeCliente { get; set; }

        public Conta(int id_Conta_a_Receber, int id_Cliente, int id_Pagamento, double valor, string descricao, DateTime dataTransacao, DateTime previsaoTermino, string metodoPagamento)
        {
            this.id_Conta_a_Receber = id_Conta_a_Receber;
            this.id_Cliente = id_Cliente;
            this.id_Pagamento = id_Pagamento;
            this.valor = valor;
            this.descricao = descricao;
            this.dataTransacao = dataTransacao;
            this.previsaoTermino = previsaoTermino;
            this.metodoPagamento = metodoPagamento;
        }
        public Conta() { }
    }
}
