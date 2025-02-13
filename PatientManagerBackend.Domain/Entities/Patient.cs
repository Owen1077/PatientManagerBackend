using PatientManagerBackend.Domain.Common;
using PatientManagerBackend.Domain.Entities.Base;
using PatientManagerBackend.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagerBackend.Domain.Entities
{
    public class Patient : EntityBase<string>
    {
        public Patient()
        {
            SetNewId();
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsDeleted { get; set; } = false; 
        public List<PatientRecord> Records { get; set; } = new();
        public override void SetNewId()
        {
            Id = $"PAT_{CoreHelpers.CreateUlid(DateTimeOffset.Now)}";
        }
    }
}
