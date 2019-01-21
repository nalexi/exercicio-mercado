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
    public class RepositorioFornecedor : IRepositorio<Fornecedor>
    {
        private SqlCommand comando;

        public void Alterar(Fornecedor fornecedor)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE fornecedores SET
                                    razao_social = @RAZAO_SOCIAL,
                                    nome_fantasia = @NOME_FANTASIA,
                                    inscricao_estadual = @INSCRICAO_ESTADUAL
                                    WHERE id = @ID";
            comando.Parameters.AddWithValue("@RAZAO_SOCIAL", fornecedor.RazaoSocial);
            comando.Parameters.AddWithValue("@NOME_FANTASIA", fornecedor.NomeFantasia);
            comando.Parameters.AddWithValue("@INSCRICAO_ESTADUAL", fornecedor.InscricaoEstadual);
            comando.Parameters.AddWithValue("@ID", fornecedor.Id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();

        }

        public void Apagar(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE fornecedores SET registro_ativo = 0 WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public int Inserir(Fornecedor fornecedor)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO fornecedores
                                    (razao_social, nome_fantasia, inscricao_estadual, registro_ativo)
                                    OUTPUT INSERTED.ID
                                    VALUES (@RAZAO_SOCIAL, @NOME_FANTASIA, @INSCRICAO_ESTADUAL, 1)";
            comando.Parameters.AddWithValue("@RAZAO_SOCIAL", fornecedor.RazaoSocial);
            comando.Parameters.AddWithValue("@NOME_FANTASIA", fornecedor.NomeFantasia);
            comando.Parameters.AddWithValue("@INSCRICAO_ESTADUAL", fornecedor.InscricaoEstadual);

            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());

            comando.Connection.Close();
            return id;
        }

        public Fornecedor ObterPeloId(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM fornecedores WHERE id = @ID AND registro_ativo =1";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            Fornecedor fornecedor= null;
            if (table.Rows.Count == 1)
            {
                fornecedor = new Fornecedor();
                DataRow row = table.Rows[0];

                fornecedor.Id = Convert.ToInt32(row["id"].ToString());
                fornecedor.RazaoSocial = row["razao_social"].ToString();
                fornecedor.NomeFantasia = row["nome_fantasia"].ToString();
                fornecedor.InscricaoEstadual = row["inscricao_estadual"].ToString();
            }
            comando.Connection.Close();
            return fornecedor != null ? fornecedor : null;
        }

        public List<Fornecedor> ObterTodos()
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM fornecedores WHERE registro_ativo = 1 ORDER BY nome_fantasia";

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            List<Fornecedor> fornecedores = new List<Fornecedor>();
            foreach (DataRow row in table.Rows)
            {

                Fornecedor fornecedor = new Fornecedor();

                fornecedor.Id = Convert.ToInt32(row["id"].ToString());
                fornecedor.RazaoSocial = row["razao_social"].ToString();
                fornecedor.NomeFantasia = row["nome_fantasia"].ToString();
                fornecedor.InscricaoEstadual = row["inscricao_estadual"].ToString();

                fornecedores.Add(fornecedor);
            }
            comando.Connection.Close();
            return fornecedores;
        }
    }
}
