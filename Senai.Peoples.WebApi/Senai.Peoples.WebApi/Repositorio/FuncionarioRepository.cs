using Senai.Peoples.WebApi.Dominios;
using Senai.Peoples.WebApi.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositorio
{
    public class FuncionarioRepository : IFuncionariosRepository
    {

        private string stringConexao = "Data Source=DESKTOP-4A3IQMH\\SQLEXPRESS; initial catalog=M_Peoples; user id=sa; pwd=senai@132";
        public void Atualizar(int id, FuncionarioDomain novoFuncionario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateIdUrl = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE idFuncionario = @id";

                using (SqlCommand cmd = new SqlCommand(queryUpdateIdUrl, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Nome", novoFuncionario.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", novoFuncionario.Sobrenome);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public FuncionarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = "SELECT * FROM Funcionarios WHERE idFuncionario = @id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    rdr = cmd.ExecuteReader();

                    if(rdr.Read())
                    {
                        FuncionarioDomain Funcionario = new FuncionarioDomain
                        {
                            idFuncionario = Convert.ToInt32(rdr["idFuncionario"]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()

                        };

                        return Funcionario;
                    }

                    return null;

                }
            }
        }   

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Funcionarios WHERE idFuncionario = @id";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inserir(FuncionarioDomain novoFuncionario)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Funcionarios (Nome, Sobrenome) VALUES (@Nome, @Sobrenome)";

                using (SqlCommand cmd = new SqlCommand (queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", novoFuncionario.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", novoFuncionario.Sobrenome);

                    con.Open();

                    cmd.ExecuteNonQuery();  
                }
            }
        }

        public List<FuncionarioDomain> Listar()
        {
            List<FuncionarioDomain> listaFuncionarios = new List<FuncionarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT * FROM Funcionarios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand (querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        FuncionarioDomain Funcionario = new FuncionarioDomain
                        {
                            idFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr[1].ToString(),
                            Sobrenome = rdr[2].ToString()

                        };

                        listaFuncionarios.Add(Funcionario);

                    }
                }

                return listaFuncionarios;
            }
        }
    }
}
