using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Domain.Common;
using PatientManagerBackend.Domain.Entities;
using PatientManagerBackend.Domain.QueryParameters;

namespace PatientManagerBackend.Core.Contract.Repository
{
    public interface IPatientRecordRepository
    {
        Task<PagedResponse<List<PatientRecord>>> GetAllAsync(UrlQueryParameters request);
        Task<PatientRecord> GetByIdAsync(string id);
        Task<PatientRecord> AddAsync(PatientRecord record);
        Task<PatientRecord> UpdateAsync(PatientRecord record);
    }
}
