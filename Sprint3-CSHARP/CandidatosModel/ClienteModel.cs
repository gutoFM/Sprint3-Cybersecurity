using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintModel
{
    public class ClienteModel
    {
        [Key] public int idCliente { get; set; }
        public required string nome { get; set; }
        public required string email { get; set; }
        public required string senha { get; set; }
        public DateTime dataNascimento { get; set; }
        public decimal saldo { get; set; }
    }
}
