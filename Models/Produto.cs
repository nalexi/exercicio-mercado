using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Produto
    {
        public int Id { get; set; }

        public int IdSupermercado { get; set; }
        public int IdFornecedor { get; set; }

        public Supermercado Supermercado { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public string Nome { get; set; }
        public double Peso { get; set; }
        public decimal Preco { get; set; }
        public bool RegistroAtivo { get; set; }
    }
}
