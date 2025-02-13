using PatientManagerBackend.Core.Contract;
using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Core.DTO.Response;
using PatientManagerBackend.Domain.Common;
using PatientManagerBackend.Domain.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientManagerBackend.Core.Implementation;

namespace PatientManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IPatientRecordService _patientRecordService;

        public RecordController(IPatientRecordService patientRecordService)
        {
            _patientRecordService = patientRecordService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddRecord([FromQuery]string patientId, AddPatientRecordRequest request)
        {
            return Ok(await _patientRecordService.AddPatientRecord(patientId, request));
        }

        [HttpGet("record/id")]
        public async Task<IActionResult> GetRecordById([FromQuery] string id)
        {
            return Ok(await _patientRecordService.GetById(id));
        }

        [HttpGet("records")]
        public async Task<IActionResult> GetRecords([FromQuery] UrlQueryParameters request)
        {
            return Ok(await _patientRecordService.GetAllPatientRecords(request));
        }

        [HttpPut("update")]
        public async Task<IActionResult> EditRecord([FromQuery] string recordId, UpdatePatientRecordRequest request)
        {
            return Ok(await _patientRecordService.UpdatePatientRecord(recordId, request));
        }
    }
}