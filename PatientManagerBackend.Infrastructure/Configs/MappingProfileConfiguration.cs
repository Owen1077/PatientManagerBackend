using AutoMapper;
using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Core.DTO.Response;
using PatientManagerBackend.Domain.Entities;

namespace PatientManagerBackend.Infrastructure.Configs
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<Patient, PatientResponse>(MemberList.None);
            CreateMap<PatientRecord, PatientRecordResponse>(MemberList.None);

        }
    }
}
