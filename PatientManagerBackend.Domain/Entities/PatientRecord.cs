using PatientManagerBackend.Domain.Common;
using PatientManagerBackend.Domain.Entities.Base;
using PatientManagerBackend.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagerBackend.Domain.Entities
{
    public class PatientRecord : EntityBase<string>
    {
        public PatientRecord()
        {
            SetNewId();
        }
        public string PatientId { get; set; }
        public string Description { get; set; }
        public Patient Patient { get; set; }
        public override void SetNewId()
        {
            Id = $"RCD_{CoreHelpers.CreateUlid(DateTimeOffset.Now)}";
        }
    }
}
