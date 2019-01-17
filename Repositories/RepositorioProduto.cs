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
    class RepositorioProduto : IRepositorio<Produto>
    {
        private SqlCommand comando;

        public void Alterar(Produto produto)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE produtos SET
                                    id_supermercado = @ID_SUPERMERCADO,
                                    id_fornecedor = @ID_FORNECEDOR,
                                    nome = @NOME,
                                    peso = @PESO,
                                    preco = @PRECO
                                    WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID_SUPERMERCADO", produto.IdSupermercado);
            comando.Parameters.AddWithValue("@ID_FORNECEDOR", produto.IdFornecedor);
            comando.Parameters.AddWithValue("@NOME", produto.Nome);
            comando.Parameters.AddWithValue("@PESO", produto.Peso);
            comando.Parameters.AddWithValue("@PRECO", produto.Preco);

            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public void Apagar(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE produtos SET registro_ativo = 0 WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public int Inserir(Produto produto)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO produtos
                                    (id_supermercado, id_fornecedor, nome, peso, preco, registro_ativo)
                                    OUTPUT INSERTED.ID
                                    VALUES (@ID_SUPERMERCADO, @ID_FORNECEDOR, @NOME, @PESO, @PRECO, 1";
            comando.Parameters.AddWithValue("@ID_SUPERMERCADO", produto.IdSupermercado);
            comando.Parameters.AddWithValue("@ID_FORNECEDOR", produto.IdFornecedor);
            comando.Parameters.AddWithValue("@NOME", produto.Nome);
            comando.Parameters.AddWithValue("@PESO", produto.Peso);
            comando.Parameters.AddWithValue("@PRECO", produto.Preco);

            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            comando.Connection.Close();

            return id;
        }

        public Supermercado ObterPeloId(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM produtos WHERE id = @ID AND registro_ativo = 1";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            Produto produto = null;
            if (table.Rows.Count == 1)
            {
                produto = new Produto();
                DataRow row = table.Rows[0];

                produto.Id = Convert.ToInt32(row["id"].ToString());
                produto.IdSupermercado = Convert.ToInt32(row["id_supermercado"].ToString());
                produto.IdFornecedor = Convert.ToInt32(row["id_fornecedor"].ToString());
                produto.Nome = row["nome"].ToString();
                produto.Peso = Convert.ToDouble(row["peso"].ToString());
                produto.Preco= Convert.ToDecimal(row["preco"].ToString());
            }
            comando.Connection.Close();
            return produto != null ? produto : null;
        }

        public List<Produto> ObterTodos()
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT pro.id, pro.id_supermercado, pro.nome, pro.peso, pro.preco, 
                                    sup.cnpj, sup.nome, sup.faturamento
                                    FROM produtos pro
                                    INNER JOIN supermercados sup ON(pro.id_supermercado = sup.id)
                                    WHERE pro.registro_ativo = 1 ORDER BY pro.nome";

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            List<Produto> produtos = new List<Produto>();
            foreach (DataRow row in table.Rows)
            {
                Produto produto = new Produto();

                produto.IdSupermercado = Convert.ToInt32(row["id_supermercado"].ToString());
                produto.Supermercado = new Supermercado();

                produto.Supermercado.Id = Convert.ToInt32(row["id_supermercado"].ToString());
                produto.Supermercado.Cnpj = row["cnpj"].ToString();
                produto.Supermercado.Nome = row["nome"].ToString();
                produto.Supermercado.Faturamento = Convert.ToDecimal(row["faturamento"].ToString());

                produto.Id = Convert.ToInt32(row["id"].ToString());
                produto.Nome = row["nome"].ToString();
                produto.Peso = Convert.ToDouble(row["peso"].ToString());
                produto.Preco = Convert.ToDecimal(row["preco"].ToString());

                produtos.Add(produto);
            }
            comando.Connection.Close();
            return produtos;
        }
    }
}
