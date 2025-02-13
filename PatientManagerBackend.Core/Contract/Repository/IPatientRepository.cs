using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Domain.Common;
using PatientManagerBackend.Domain.Entities;
using PatientManagerBackend.Domain.QueryParameters;

namespace PatientManagerBackend.Core.Contract.Repository
{
    public interface IPatientRepository
    {
        Task<PagedResponse<List<Patient>>> GetAllAsync(UrlQueryParameters request);
        Task<Patient> GetByIdAsync(string id);
        Task<Patient> AddAsync(Patient patient);
        Task<Patient> UpdateAsync(Patient patient);
        Task<bool> SoftDeleteAsync(string id);
    }
}
