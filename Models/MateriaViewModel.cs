using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudyApi_Front.Models.Enuns;

namespace StudyApi_Front.Models
{
    public class MateriaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int HorasDoCurso { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public StatusMateria StatusMateria { get; set; }
        public List<MatriculaViewModel> Matricula { get; set; } = new List<MatriculaViewModel>();
        public int IdProfessor { get; set; }
        public ProfessorViewModel Professor { get; set; } = null;
        
    }
}