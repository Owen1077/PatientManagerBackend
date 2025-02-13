using PatientManagerBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagerBackend.Core.DTO.Response
{
    public class PatientResponse
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsDeleted { get; set; } 
        public List<PatientRecordResponse> Records { get; set; } = new();
    }
}
