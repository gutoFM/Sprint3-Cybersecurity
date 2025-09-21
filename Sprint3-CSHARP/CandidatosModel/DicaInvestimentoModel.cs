using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Aqui é a classe onde iremos colocar na aba "Dicas", dicas personalizadas a partir do nivel do cliente
namespace SprintModel
{
    public class DicaInvestimentoModel
    {
        [Key] public int idDica { get; set; }
        public required string titulo { get; set; }
        public required string descricao { get; set; }
        public required string categoria { get; set; } // Ex: Iniciante, Avançado
        public DateTime dataPublicacao { get; set; }
        public string? link { get; set; }
    }
}