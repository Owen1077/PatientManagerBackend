using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Core.DTO.Response;
using PatientManagerBackend.Domain.Common;
using PatientManagerBackend.Domain.Entities;
using PatientManagerBackend.Domain.QueryParameters;

namespace PatientManagerBackend.Core.Contract
{
    public interface IPatientRecordService
    {
        Task<PagedResponse<List<PatientRecordResponse>>> GetAllPatientRecords(UrlQueryParameters queryParameters);
        Task<Response<string>> AddPatientRecord(string patientId, AddPatientRecordRequest request);
        Task<Response<string>> UpdatePatientRecord(string recordId, UpdatePatientRecordRequest request);
        Task<Response<PatientRecordResponse>> GetById(string id);
    }
}
