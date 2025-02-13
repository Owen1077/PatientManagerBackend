using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Core.DTO.Response;
using PatientManagerBackend.Domain.Common;
using PatientManagerBackend.Domain.Entities;
using PatientManagerBackend.Domain.QueryParameters;

namespace PatientManagerBackend.Core.Contract
{
    public interface IPatientService
    {
        Task<PagedResponse<List<PatientResponse>>> GetAllPatients(UrlQueryParameters queryParameters);
        Task<Response<string>> AddPatient(AddPatientRequest request);

        Task<Response<string>> UpdatePatient(string patientId, UpdatePatientRequest request);
        Task<Response<PatientResponse>> GetById(string id);
        Task<Response<string>> DeletePatient(string id);
    }
}
