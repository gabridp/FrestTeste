using FRestTeste.infra.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FRestTeste.Financeiro.Models
{
    public class ReceitaResumoModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string DataMax { get; set; }

        // tipo de consulta
        [Required(ErrorMessage = "Por favor, informe o tipo de consulta.")]
        public string tipo { get; set; }

        public decimal totalGeral { get; set; }


        //propriedade para armazenar o resultado da consulta
        public List<ResumoReceita>? ResumoReceitas { get; set; }
    }
}
