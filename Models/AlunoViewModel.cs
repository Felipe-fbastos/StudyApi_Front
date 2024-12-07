using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EstudosApi.Models;

namespace StudyApi_Front.Models
{
    public class AlunoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DtaNascimento { get; set; }
        public List<MatriculaViewModel> Matricula { get; set; } = new List<MatriculaViewModel>();
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Email { get; set; }

         public DateTime? DtaAcesso { get; set; }
        
        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }
        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }

        [NotMapped]
        public string PassowordString { get; set; }

    }
}