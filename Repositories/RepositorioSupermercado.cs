using Models;
using Repositories.Database;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    class RepositorioSupermercado : IRepositorio<Supermercado>

    {
        private SqlCommand comando;

        public void Alterar(Supermercado supermercado)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE supermercados SET
                                    cnpj = @CNPJ
                                    nome = @NOME
                                    faturamento = @FATURAMENTO
                                    WHERE id = @ID";
            comando.Parameters.AddWithValue("@CNPJ", supermercado.Cnpj);
            comando.Parameters.AddWithValue("@NOME", supermercado.Nome);
            comando.Parameters.AddWithValue("@FATURAMENTO", supermercado.Faturamento);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public void Apagar(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE supermercados SET registro_ativo = 0 WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public int Inserir(Supermercado supermercado)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO supermercados
                                    (cnpj, nome, faturamento, registro_ativo)
                                    OUTPUT INSERTED.ID
                                    VALUES (@CNPJ, @NOME, @FATURAMENTO, 1";
            comando.Parameters.AddWithValue("@CNPJ", supermercado.Cnpj);
            comando.Parameters.AddWithValue("@NOME", supermercado.Nome);
            comando.Parameters.AddWithValue("@FATURAMENTO", supermercado.Faturamento);

            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            comando.Connection.Close();

            return id;
        }

        public Supermercado ObterPeloId(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM supermercados WHERE id = @ID AND registro_ativo = 1";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            Supermercado supermercado = null;
            if (table.Rows.Count == 1)
            {
                supermercado = new Supermercado();
                DataRow row = table.Rows[0];

                supermercado.Id = Convert.ToInt32(row["id"].ToString());
                supermercado.Cnpj = row["cnpj"].ToString();
                supermercado.Nome = row["nome"].ToString();
                supermercado.Faturamento = Convert.ToDecimal(row["faturamento"].ToString());
            }
            comando.Connection.Close();
            return supermercado != null ? supermercado:null;
        }

        public List<Supermercado> ObterTodos()
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM supermercados WHERE registro_ativo = 1 ORDER BY nome";

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            List<Supermercado> supermercados = new List<Supermercado>();
            foreach (DataRow row in table.Rows)
            {
                Supermercado supermercado = new Supermercado();

                supermercado.Id = Convert.ToInt32(row["id"].ToString());
                supermercado.Cnpj = row["cnpj"].ToString();
                supermercado.Nome = row["nome"].ToString();
                supermercado.Faturamento = Convert.ToDecimal(row["faturamento"].ToString());
                supermercados.Add(supermercado);
            }
            comando.Connection.Close();
            return supermercados;
        }
    }
}
