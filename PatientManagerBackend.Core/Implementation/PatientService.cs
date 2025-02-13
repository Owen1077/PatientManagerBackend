using AutoMapper;
using PatientManagerBackend.Core.Contract;
using PatientManagerBackend.Core.Contract.Repository;
using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Core.DTO.Response;
using PatientManagerBackend.Core.Exceptions;
using PatientManagerBackend.Domain.Common;
using PatientManagerBackend.Domain.Entities;
using PatientManagerBackend.Domain.QueryParameters;

namespace PatientManagerBackend.Core.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;

        public PatientService(IMapper mapper,
            IPatientRepository patientRepository)
        {
            _mapper = mapper;
            _patientRepository = patientRepository;
        }

        public async Task<PagedResponse<List<PatientResponse>>> GetAllPatients(UrlQueryParameters queryParameters)
        {            
            var result = await _patientRepository.GetAllAsync(queryParameters);

            var response = _mapper.Map<List<PatientResponse>>(result.Data);

            return new PagedResponse<List<PatientResponse>>(response, queryParameters.PageNumber, queryParameters.PageSize, result.PageMeta.TotalRecords, $"Successfully retrieved data");
        }

        public async Task<Response<string>> AddPatient(AddPatientRequest request)
        {
            if(request.Age <= 0)
            {
                throw new ApiException("Please select a valid age");
            }

            var patient = new Patient
            {
                Name = request.Name,
                Age = request.Age,
            };

            var response = await _patientRepository.AddAsync(patient);

            return new Response<string>("Success", "Successfully added patient data");
        }

        public async Task<Response<string>> UpdatePatient(string patientId, UpdatePatientRequest request)
        {
            if (request.Age.HasValue && request.Age <= 0)
            {
                throw new ApiException("Please select a valid age");
            }

            var patient = await _patientRepository.GetByIdAsync(patientId);

            patient.Name = string.IsNullOrEmpty(request.Name) ? patient.Name : request.Name;
            patient.Age = request.Age.HasValue ? request.Age.Value : patient.Age;

            var response = await _patientRepository.UpdateAsync(patient);

            return new Response<string>("Success", "Successfully updated patient data");
        }

        public async Task<Response<PatientResponse>> GetById(string id)
        {

            var result = await _patientRepository.GetByIdAsync(id);

            var response = _mapper.Map<PatientResponse>(result);

            return new Response<PatientResponse>(response, "Successfully retrieved data");
        }

        public async Task<Response<string>> DeletePatient(string id)
        {
            var result = await _patientRepository.SoftDeleteAsync(id);

            return new Response<string>("Success", "Successfully deleted patient");
        }
    }
}
