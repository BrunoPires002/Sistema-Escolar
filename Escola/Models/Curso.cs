using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Escola.Models
{
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }

        [DisplayName("Curso")]
        public string NomeCurso { get; set; }

        [DisplayName("Duração do curso")]
        public string DuracaoCurso { get; set; }

        [DisplayName("Descrição")]
        public string DescCurso { get; set; }
    }
}
