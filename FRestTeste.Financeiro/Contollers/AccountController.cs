using FRestTeste.Financeiro.Models;
using FRestTeste.infra.Data.Interfaces;
using FRestTeste.infra.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FRestTeste.Financeiro.Contollers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AccountLoginModel model,
            [FromServices] IUsuarioRepository usuarioRepository)
        {
            try
            {
                //consultar o usuário no banco de dados 
                //atraves do email e da senha
                var usuario = usuarioRepository.GetSenha(model.Senha);
                //verificar se o usuário foi encontrado!
                if (usuario != null)
                {
                    TempData["MensagemSucesso"] = $"Seja bem vindo { usuario.NOME_GARC}, seu acesso foi realizado com sucesso.";

                    //Ainda iremos implementar a permissão de acesso!
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MensagemErro"] = "Acesso negado,usuário não identificado.";
                    }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro: {e.Message}";
            }
            return View();
        }

    }
}
