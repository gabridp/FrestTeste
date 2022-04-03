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
    public class ResumoReceitaRepository : IResumoReceitaRepository

    {
        private readonly string _connectionString;
        //Construtor para entrada de argumentos
        public ResumoReceitaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<ResumoReceita> GetAll()
        {
            using (var connection = new FbConnection(_connectionString))
            {
                return connection.Query<ResumoReceita>(@"SELECT GARCON, NOME_GARC,
                    SENHA,DIREITOS
                FROM RST006").ToList();
            }
        }

        public List<ResumoReceita> GetResumo(string DT_INICIAL, string DT_FINAL)
        {
            using (var connection = new FbConnection(_connectionString))
            {
                var query = @"SELECT  FIN006.DESC_CLASS,
                    FIN007.DESC_CONTA,
                    /*SUBSTRING(FIN003.DATA_CREDI FROM 7 FOR 4)||SUBSTRING(FIN003.DATA_CREDI FROM 4 FOR 2)||SUBSTRING(FIN003.DATA_CREDI FROM 1 FOR 2) AS DATA_ORDENADA,*/
                    sum(CAST(FIN008P.VL_PAGO AS DOUBLE PRECISION)) AS VALOR
                    FROM FIN003
                    inner JOIN FIN007
                        on fin003.conta_desp = FIN007.conta_desp
                        inner join fin006
                        ON FIN006.cod_class = FIN007.cod_class
                        INNER join fin008p
                        on FIN003.cod_lancto = FIN008P.vinculo_cartao
                    WHERE FIN003.TIPO_LANCT = 'C'
                    AND FIN003.TP_CREDITO <> 'E'
                    AND SUBSTRING(FIN003.DATA_CREDI FROM 7 FOR 4)||SUBSTRING(FIN003.DATA_CREDI FROM 4 FOR 2)||SUBSTRING(FIN003.DATA_CREDI FROM 1 FOR 2) 
                        BETWEEN @DT_INICIAL AND @DT_FINAL
                    group by FIN006.DESC_CLASS,  FIN007.DESC_CONTA
                        UNION ALL
                    SELECT  B.DESC_CLASS,
                       C.DESC_CONTA,
                       /*SUBSTRING(A.DATA_CREDI FROM 7 FOR 4)||SUBSTRING(A.DATA_CREDI FROM 4 FOR 2)||SUBSTRING(A.DATA_CREDI FROM 1 FOR 2) AS DATA_ORDENADA,*/
                       sum(CAST(A.VL_CREDITO AS DOUBLE PRECISION)) AS VALOR
                        FROM FIN003 A, FIN006 B, FIN007 C
                        WHERE A.CONTA_DESP = C.CONTA_DESP
                        AND B.COD_CLASS = C.COD_CLASS
                        AND A.TIPO_LANCT = 'C'
                        AND A.TP_CREDITO <> 'E' AND A.TP_CREDITO <> 'T' /***AND A.TP_CREDITO <> 'O'***/
                        AND SUBSTRING(A.DATA_CREDI FROM 7 FOR 4)||SUBSTRING(A.DATA_CREDI FROM 4 FOR 2)||SUBSTRING(A.DATA_CREDI FROM 1 FOR 2) 
                        BETWEEN @DT_INICIAL AND @DT_FINAL
                        group by B.DESC_CLASS,
                               C.DESC_CONTA
                        ORDER BY  1, 2, 3
                ";
                return connection.Query<ResumoReceita>(query, new { DT_INICIAL,DT_FINAL }).ToList();
            }
        }

        public List<ResumoReceita> GetDetalheReceitas(string DT_INICIAL, string DT_FINAL)
        {
            using (var connection = new FbConnection(_connectionString))
            {
                var query = @"SELECT FIN003.DATA_CREDI AS DATA, FIN006.DESC_CLASS,
                   FIN007.DESC_CONTA,
                    SUBSTRING(FIN003.DATA_CREDI FROM 7 FOR 4)||SUBSTRING(FIN003.DATA_CREDI FROM 4 FOR 2)||SUBSTRING(FIN003.DATA_CREDI FROM 1 FOR 2) AS DATA_ORDENADA,
                   CAST(FIN008P.VL_PAGO AS DOUBLE PRECISION) AS VALOR, FIN008P.HISTORICO, FIN006.COD_TIPO
                    FROM FIN003
                        inner JOIN FIN007
                        on fin003.conta_desp = FIN007.conta_desp
                        inner join fin006
                        ON FIN006.cod_class = FIN007.cod_class
                        INNER join fin008p
                        on FIN003.cod_lancto = FIN008P.vinculo_cartao
                        WHERE FIN003.TIPO_LANCT = 'C'
                        AND FIN003.TP_CREDITO <> 'E'
                        AND SUBSTRING(FIN003.DATA_CREDI FROM 7 FOR 4)||SUBSTRING(FIN003.DATA_CREDI FROM 4 FOR 2)||SUBSTRING(FIN003.DATA_CREDI FROM 1 FOR 2) 
                        BETWEEN @DT_INICIAL AND @DT_FINAL
                    UNION ALL
                    SELECT A.DATA_CREDI AS DATA, B.DESC_CLASS,
                        C.DESC_CONTA,
                        SUBSTRING(A.DATA_CREDI FROM 7 FOR 4)||SUBSTRING(A.DATA_CREDI FROM 4 FOR 2)||SUBSTRING(A.DATA_CREDI FROM 1 FOR 2) AS DATA_ORDENADA,
                        CAST(A.VL_CREDITO AS DOUBLE PRECISION) AS VALOR, A.DES_LAN_CR AS HISTORICO, B.COD_TIPO
                    FROM FIN003 A, FIN006 B, FIN007 C
                        WHERE A.CONTA_DESP = C.CONTA_DESP
                        AND B.COD_CLASS = C.COD_CLASS
                        AND A.TIPO_LANCT = 'C'
                        AND A.TP_CREDITO <> 'E' AND A.TP_CREDITO <> 'T' 
                        AND SUBSTRING(A.DATA_CREDI FROM 7 FOR 4)||SUBSTRING(A.DATA_CREDI FROM 4 FOR 2)||SUBSTRING(A.DATA_CREDI FROM 1 FOR 2) 
                        BETWEEN @DT_INICIAL AND @DT_FINAL
                    ORDER BY 2, 3, 4
                ";
                return connection.Query<ResumoReceita>(query, new { DT_INICIAL, DT_FINAL }).ToList();
            }
        }
    }
}
