using FRestTeste.infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRestTeste.infra.Data.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario Get(string GARCON);
        Usuario GetSenha(string SENHA);
    }
}
