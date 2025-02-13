using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagerBackend.Core.DTO.Response
{
    public class PatientRecordResponse
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string Description { get; set; }
    }
}
