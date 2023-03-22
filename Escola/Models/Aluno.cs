using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Escola.Models
{
    public class Aluno
    {
        [Key]
        public int IdAluno { get; set; }

        [DisplayName("Aluno")]
        public string NomeAluno { get; set; }

        [DisplayName("RG")]
        public string RgAluno { get; set; }

        [DisplayName("Data de Nascimento")]
        public DateTime? DataNascAluno { get; set; }
    }
}
