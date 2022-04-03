using FrestTeste.Infra.Reports.Interfaces;
using FRestTeste.infra.Data.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrestTeste.infra.Reports.Services
{
    public class ReceitaReportServiceExcell : IReceitaReportService
    {
        public byte[] GerarRelatorio(List<ResumoReceita> eventos, string dataMin, string dataMax)
        {
            //definindo o uso 'free' da biblioteca
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //inicializando o arquivo em memória
            using (var excelPackage = new ExcelPackage())
            {
                //criando a planilha..
                var planilha = excelPackage.Workbook.Worksheets.Add("Eventos");

                //escrevendo o conteudo da planilha
                planilha.Cells["A1"].Value = "Relatório das Receitas";

                planilha.Cells["A3"].Value = "Data de início:";
                planilha.Cells["B3"].Value = dataMin;//.ToString("dd/MM/yyyy");

                planilha.Cells["A4"].Value = "Data de Fim:";
                planilha.Cells["B4"].Value = dataMax;//.ToString("dd/MM/yyyy");

                planilha.Cells["A5"].Value = "Nome do usuário:";
                planilha.Cells["B5"].Value = "";

                planilha.Cells["A7"].Value = "Data";
                planilha.Cells["B7"].Value = "Classificação";
                planilha.Cells["C7"].Value = "Conta Receita";
                planilha.Cells["D7"].Value = "Valor";
                planilha.Cells["E7"].Value = "Historico";
                planilha.Cells["F7"].Value = "";

                var linha = 8;
                foreach (var item in eventos)
                {
                    try
                    {
                        planilha.Cells[$"A{linha}"].Value = item.DATA.ToString();
                    }
                    catch
                    {
                        planilha.Cells[$"A{linha}"].Value = "";
                    }
                    planilha.Cells[$"B{linha}"].Value = item.DESC_CLASS;
                    planilha.Cells[$"C{linha}"].Value = item.DESC_CONTA;
                    planilha.Cells[$"D{linha}"].Value = item.VALOR.ToString();
                    planilha.Cells[$"E{linha}"].Value = item.HISTORICO;
                    planilha.Cells[$"F{linha}"].Value = "";

                    linha++;
                }

                //ajustar a largura das colunas
                planilha.Cells["A:F"].AutoFitColumns();

                //retornando a planilha em formato de arquivo..
                return excelPackage.GetAsByteArray();
            }
        }
    }
}
