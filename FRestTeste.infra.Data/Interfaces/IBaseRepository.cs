using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRestTeste.infra.Data.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        List<TEntity> GetAll();
    }
}
