using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    interface IRepositorio<T>

    {
        List<T> ObterTodos();

        T ObterPeloId(int id);

        int Inserir(T objeto);

        void Alterar(T objeto);

        void Apagar(int id);
    }
}
