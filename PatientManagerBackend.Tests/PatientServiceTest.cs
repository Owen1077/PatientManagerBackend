using AutoMapper;
using Moq;
using PatientManagerBackend.Core.Implementation;
using PatientManagerBackend.Core.Contract.Repository;
using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Core.DTO.Response;
using PatientManagerBackend.Domain.Entities;
using PatientManagerBackend.Domain.QueryParameters;
using PatientManagerBackend.Domain.Common;

namespace PatientManagerBackend.Tests
{
    public class PatientServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _patientService = new PatientService(_mapperMock.Object, _patientRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllPatients_ShouldReturnPagedResponse_WhenPatientsExist()
        {
            // Arrange
            var patients = new List<Patient>
            {
                new Patient { Id = "1", Name = "John Doe", IsDeleted = false },
                new Patient { Id = "2", Name = "Jane Doe", IsDeleted = false }
            };

            var pagedResponse = new PagedResponse<List<Patient>>(patients, 1, 10, 2, "Data retrieved successfully");
            _patientRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<UrlQueryParameters>())).ReturnsAsync(pagedResponse);

            var patientResponses = new List<PatientResponse>
            {
                new PatientResponse { Id = "1", Name = "John Doe" },
                new PatientResponse { Id = "2", Name = "Jane Doe" }
            };

            _mapperMock.Setup(x => x.Map<List<PatientResponse>>(patients)).Returns(patientResponses);

            var queryParameters = new UrlQueryParameters { PageNumber = 1, PageSize = 10 };

            // Act
            var result = await _patientService.GetAllPatients(queryParameters);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public async Task AddPatient_ShouldAddPatient()
        {
            // Arrange
            var addPatientRequest = new AddPatientRequest { Name = "John Doe", Age = 30 };
            var patient = new Patient { Id = "1", Name = "John Doe", Age = 30 };

            _patientRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Patient>())).ReturnsAsync(patient);

            // Act
            var result = await _patientService.AddPatient(addPatientRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Successfully added patient data", result.Message);
        }

        [Fact]
        public async Task UpdatePatient_ShouldUpdatePatient()
        {
            // Arrange
            var updatePatientRequest = new UpdatePatientRequest { Name = "John Doe", Age = 35 };
            var patient = new Patient { Id = "1", Name = "John Doe", Age = 30 };

            _patientRepositoryMock.Setup(x => x.GetByIdAsync("1")).ReturnsAsync(patient);
            _patientRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Patient>())).ReturnsAsync(patient);

            // Act
            var result = await _patientService.UpdatePatient("1", updatePatientRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Successfully updated patient data", result.Message);
        }

        [Fact]
        public async Task GetById_ShouldReturnPatientResponse_WhenPatientExists()
        {
            // Arrange
            var patient = new Patient { Id = "1", Name = "John Doe", Age = 30 };
            var patientResponse = new PatientResponse { Id = "1", Name = "John Doe", Age = 30 };

            _patientRepositoryMock.Setup(x => x.GetByIdAsync("1")).ReturnsAsync(patient);
            _mapperMock.Setup(x => x.Map<PatientResponse>(patient)).Returns(patientResponse);

            // Act
            var result = await _patientService.GetById("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Data.Name);
        }

        [Fact]
        public async Task DeletePatient_ShouldReturnSuccessResponse_WhenPatientIsDeleted()
        {
            // Arrange
            _patientRepositoryMock.Setup(x => x.SoftDeleteAsync("1")).ReturnsAsync(true);

            // Act
            var result = await _patientService.DeletePatient("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Successfully deleted patient", result.Message);
        }
    }
}