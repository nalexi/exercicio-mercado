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

        public float peso { get; set; }
        public decimal preco { get; set; }
    }
}
