using Dapper;
using FirebirdSql.Data.FirebirdClient;
using FRestTeste.infra.Data.Entities;
using FRestTeste.infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRestTeste.infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;
        //Construtor para entrada de argumentos
        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Usuario Get(string GARCON)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> GetAll()
        {
            using (var connection = new FbConnection(_connectionString))
            {
                return connection.Query<Usuario>(@"SELECT GARCON, NOME_GARC,
                    SENHA,DIREITOS
                FROM RST006").ToList();
                    

            }
        }

        public Usuario GetSenha(string SENHA)
        {
            using (var connection = new FbConnection(_connectionString))
            {
               /* var query = @"SELECT GARCON, NOME_GARC,
                    SENHA,DIREITOS 
                        FROM RST006 a 
                            WHERE a.SENHA = @SENHA";*/
                var query = @"SELECT GARCON, NOME_GARC,
                    SENHA,DIREITOS 
                        FROM RST006 a 
                            WHERE a.GARCON = @SENHA";
                return connection.Query<Usuario>(query, new { SENHA })
                    .FirstOrDefault();

            }
        }
    }
}
