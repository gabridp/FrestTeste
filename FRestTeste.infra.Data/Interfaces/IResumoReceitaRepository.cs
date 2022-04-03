using FRestTeste.infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRestTeste.infra.Data.Interfaces
{
    public interface IResumoReceitaRepository : IBaseRepository<ResumoReceita>
    {
        // Consulta ao total das receitas totalizado por contas de receita
        List<ResumoReceita> GetResumo(string DT_INICIAL, string DT_FINAL);

        // Consulta ao total das receitas detalhado
        List<ResumoReceita> GetDetalheReceitas(string DT_INICIAL, string DT_FINAL);
    }
}
