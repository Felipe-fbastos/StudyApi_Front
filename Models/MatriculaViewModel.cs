using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using StudyApi_Front.Models;

namespace StudyApi_Front.Models
{
    public class MatriculaViewModel
    {
        
        public int AlunoId { get; set; }
        [JsonIgnore]
        public AlunoViewModel Aluno { get; set; }   
        public int MateriaId { get; set; }
        [JsonIgnore] 
        public MateriaViewModel Materia { get; set; } 

        public DateTime? DataMatricula { get; set; } 
        
    }
}