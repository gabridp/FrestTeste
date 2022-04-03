using FrestTeste.infra.Reports.Services;
using FrestTeste.Infra.Reports.Interfaces;
using FRestTeste.Financeiro.Models;
using FRestTeste.infra.Data.Entities;
using FRestTeste.infra.Data.Interfaces;

using Microsoft.AspNetCore.Mvc;


namespace FRestTeste.Financeiro.Contollers
{
    public class ReceitaController : Controller
    {
        public IActionResult ReceitaResumo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ReceitaResumo(ReceitaResumoModel model,
            [FromServices] IResumoReceitaRepository receitaRepository)
        {
            //verificar se a model passou nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //convertendo para o tipo DateTime
                    var DdataMin = DateTime.Parse(model.DataMin);
                    var DdataMax = DateTime.Parse(model.DataMax);
                    var dataMin = DdataMin.ToString("yyyyMMdd"); //DateTime.Parse(model.DataMin);
                    var dataMax = DdataMax.ToString("yyyyMMdd");  //DateTime.Parse(model.DataMax);
                    // zera total q será preencido no formulario
                    model.totalGeral = 0;

                    //consultando os eventos no banco de dados
                    if (model.tipo == "1")
                    {
                        model.ResumoReceitas = receitaRepository.GetResumo(dataMin, dataMax);
                    }
                    else
                    {
                        model.ResumoReceitas = receitaRepository.GetDetalheReceitas(dataMin, dataMax);
                    }

                    if (model.ResumoReceitas != null && model.ResumoReceitas.Count > 0)
                        TempData["MensagemSucesso"] = $"A pesquisa obteve { model.ResumoReceitas.Count} registro(s).";
                    else
                        TempData["MensagemAlerta"] = "Nenhum registro foi encontrado para a pesquisa realizada.";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = $"Ocorreu um erro:{ e.Message}";
                }
            }

            return View(model);
        }
        public IActionResult ExportarExcell(string dtMin, string dtMAx,string tipo,
            ReceitaResumoModel model,
                [FromServices] IResumoReceitaRepository receitaRepository)
        {
            try
            {
                var DdataMin = DateTime.Parse(dtMin);
                var DdataMax = DateTime.Parse(dtMAx);
                var dataMin = DdataMin.ToString("yyyyMMdd"); 
                var dataMax = DdataMax.ToString("yyyyMMdd");  //DateTime.Parse(model.DataMax);
                                                              //consultando os eventos no banco de dados
                if (tipo == "1")
                {
                    model.ResumoReceitas = receitaRepository.GetResumo(dataMin, dataMax);
                }
                else
                {
                    model.ResumoReceitas = receitaRepository.GetDetalheReceitas(dataMin, dataMax);
                }
                IReceitaReportService receitaReportService = null;
                var nomeArquivo = $"eventos_{DateTime.Now.ToString("ddMMyyyyHHmmss")}";
                var tipoArquivo = string.Empty;

                receitaReportService = new ReceitaReportServiceExcell();
                nomeArquivo = $"{nomeArquivo}.xlsx";
                tipoArquivo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                //gerar o relatório
                var relatorio = receitaReportService.GerarRelatorio(model.ResumoReceitas, dataMin, dataMax);

                //download
                return File(relatorio, tipoArquivo, nomeArquivo);

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro:{ e.Message}";
            }
            return RedirectToAction("ReceitaResumo");
        }
    }
}
