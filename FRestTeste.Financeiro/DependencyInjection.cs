using FRestTeste.infra.Data.Interfaces;
using FRestTeste.infra.Data.Repositories;

namespace FRestTeste.Financeiro
{
    public class DependencyInjection
    {
        /// <summary>
        /// Classe para configuração da injeção de dependência do projeto
        /// </summary>
        public static void Register(WebApplicationBuilder builder)
        {
                var connectionString = builder.Configuration
                .GetConnectionString("FRestTesteDev");
                // instancia a classe Usuario
                builder.Services.AddTransient<IUsuarioRepository>
                (map => new UsuarioRepository(connectionString));

                // instancia a classe para o relatorio resumo receta
                builder.Services.AddTransient<IResumoReceitaRepository>
                (map => new ResumoReceitaRepository(connectionString));
        }
    }
}
