using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EstudosApi.Models
{
    public class ProfessorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DtaNascimento { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

        public DateTime? DtaAcesso { get; set; }

        [NotMapped]
        public string? PassowordString { get; set; }
        
        [JsonIgnore]
        public List<MateriaViewModel> Materias { get; set; }

    }
}