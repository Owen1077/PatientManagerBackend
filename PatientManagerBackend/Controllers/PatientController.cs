using PatientManagerBackend.Core.Contract;
using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Domain.QueryParameters;
using Microsoft.AspNetCore.Mvc;

namespace PatientManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddPatient(AddPatientRequest request)
        {
            return Ok(await _patientService.AddPatient(request));
        }

        [HttpGet("patient/id")]
        public async Task<IActionResult> GetPatientById([FromQuery]string id)
        {
            return Ok(await _patientService.GetById(id));
        }

        [HttpGet("patients")]
        public async Task<IActionResult> GetPatients([FromQuery] UrlQueryParameters request)
        {
            return Ok(await _patientService.GetAllPatients(request));
        }

        [HttpPut("update")]
        public async Task<IActionResult> EditPatient([FromQuery] string patientId, UpdatePatientRequest request)
        {
            return Ok(await _patientService.UpdatePatient(patientId, request));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            return Ok(await _patientService.DeletePatient(id));
        }
    }
}