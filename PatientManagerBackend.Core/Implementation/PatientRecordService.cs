using AutoMapper;
using PatientManagerBackend.Core.Contract;
using PatientManagerBackend.Core.Contract.Repository;
using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Core.DTO.Response;
using PatientManagerBackend.Core.Exceptions;
using PatientManagerBackend.Core.Repository;
using PatientManagerBackend.Domain.Common;
using PatientManagerBackend.Domain.Entities;
using PatientManagerBackend.Domain.QueryParameters;

namespace PatientManagerBackend.Core.Implementation
{
    public class PatientRecordService : IPatientRecordService
    {
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientRecordRepository _patientRecordRepository;

        public PatientRecordService(IMapper mapper,
            IPatientRecordRepository patientRecordRepository,
            IPatientRepository patientRepository)
        {
            _mapper = mapper;
            _patientRepository = patientRepository;
            _patientRecordRepository = patientRecordRepository;
        }

        public async Task<PagedResponse<List<PatientRecordResponse>>> GetAllPatientRecords(UrlQueryParameters queryParameters)
        {            
            var result = await _patientRecordRepository.GetAllAsync(queryParameters);

            var response = _mapper.Map<List<PatientRecordResponse>>(result.Data);

            return new PagedResponse<List<PatientRecordResponse>>(response, queryParameters.PageNumber, queryParameters.PageSize, result.PageMeta.TotalRecords, $"Successfully retrieved data");
        }

        public async Task<Response<string>> AddPatientRecord(string patientId, AddPatientRecordRequest request)
        {
            var patient = await _patientRepository.GetByIdAsync(patientId);

            var record = new PatientRecord
            {
                Description = request.Description,
                PatientId = patientId,
            };

            var response = await _patientRecordRepository.AddAsync(record);

            return new Response<string>("Success", "Successfully added patient data");
        }

        public async Task<Response<string>> UpdatePatientRecord(string recordId, UpdatePatientRecordRequest request)
        {
            var record = await _patientRecordRepository.GetByIdAsync(recordId);

            record.Description = string.IsNullOrEmpty(request.Description) ? record.Description : request.Description;

            var response = await _patientRecordRepository.UpdateAsync(record);

            return new Response<string>("Success", "Successfully updated record data");
        }

        public async Task<Response<PatientRecordResponse>> GetById(string id)
        {
            var result = await _patientRecordRepository.GetByIdAsync(id);

            var response = _mapper.Map<PatientRecordResponse>(result);

            return new Response<PatientRecordResponse>(response, "Successfully retrieved data");
        }

    }
}
