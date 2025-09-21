using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Aqui é a classe onde colocaremos os possiveis tipos de investimentos que o cliente vai poder escolher para simular
namespace SprintModel
{
    public class InvestimentoModel
    {
        [Key] public int idInvestimento { get; set; }
        public required string nome { get; set; }
        public required string tipo { get; set; } // Ex: Renda Fixa, Ações, FIIs
        public decimal rentabilidadeAnual { get; set; }
        public decimal risco { get; set; } // escala de 1-5
        public decimal valorMinimo { get; set; }
    }
}
