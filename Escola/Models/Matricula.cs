using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Escola.Models
{
    public class Matricula
    {
        [DisplayName("ID")]
        [Key]
        public int IdMatricula { get; set; }

        [ForeignKey("IdAluno")]
        public virtual Aluno Aluno { get; set; }

        [ForeignKey("IdCurso")]
        public virtual Curso Curso { get; set; }
    }
}
