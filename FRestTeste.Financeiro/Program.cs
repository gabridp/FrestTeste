using FRestTeste.Financeiro;

var builder = WebApplication.CreateBuilder(args);

// Configurar o projeto para o padrão MVC
builder.Services.AddControllersWithViews();

//Configuração de injeção de dependência do projeto
DependencyInjection.Register(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//definir a página inicial do projeto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}"
);

app.Run();

