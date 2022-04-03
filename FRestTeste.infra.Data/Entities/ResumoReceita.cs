using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRestTeste.infra.Data.Entities
{
    public class ResumoReceita
    {
        public string DATA { get; set; }
        public string DESC_CLASS { get; set; }
        public string DESC_CONTA { get; set; }
        public string DATA_ORDENADA { get; set; }

        public decimal VALOR { get; set; }
        public string HISTORICO { get; set; }
    }
}
