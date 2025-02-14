using AutoMapper;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using PatientManagerBackend.Core.Implementation;
using PatientManagerBackend.Core.Contract.Repository;
using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Core.DTO.Response;
using PatientManagerBackend.Domain.Entities;
using PatientManagerBackend.Domain.QueryParameters;
using PatientManagerBackend.Core.Exceptions;
using PatientManagerBackend.Domain.Common;

namespace PatientManagerBackend.Tests
{
    public class PatientRecordServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPatientRepository> _patientRepositoryMock;
        private readonly Mock<IPatientRecordRepository> _patientRecordRepositoryMock;
        private readonly PatientRecordService _patientRecordService;

        public PatientRecordServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _patientRecordRepositoryMock = new Mock<IPatientRecordRepository>();
            _patientRecordService = new PatientRecordService(_mapperMock.Object, _patientRecordRepositoryMock.Object, _patientRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllPatientRecords_ShouldReturnPagedResponse_WhenRecordsExist()
        {
            // Arrange
            var records = new List<PatientRecord>
            {
                new PatientRecord { Id = "1", Description = "Record 1", PatientId = "1" },
                new PatientRecord { Id = "2", Description = "Record 2", PatientId = "1" }
            };

            var pagedResponse = new PagedResponse<List<PatientRecord>>(records, 1, 10, 2, "Data retrieved successfully");
            _patientRecordRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<UrlQueryParameters>())).ReturnsAsync(pagedResponse);

            var recordResponses = new List<PatientRecordResponse>
            {
                new PatientRecordResponse { Id = "1", Description = "Record 1" },
                new PatientRecordResponse { Id = "2", Description = "Record 2" }
            };

            _mapperMock.Setup(x => x.Map<List<PatientRecordResponse>>(records)).Returns(recordResponses);

            var queryParameters = new UrlQueryParameters { PageNumber = 1, PageSize = 10 };

            // Act
            var result = await _patientRecordService.GetAllPatientRecords(queryParameters);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Data.Count);
        }

        [Fact]
        public async Task AddPatientRecord_ShouldAddRecord()
        {
            // Arrange
            var addPatientRecordRequest = new AddPatientRecordRequest { Description = "New Record" };
            var patient = new Patient { Id = "1", Name = "John Doe", Age = 30 };
            var record = new PatientRecord { Id = "1", Description = "New Record", PatientId = "1" };

            _patientRepositoryMock.Setup(x => x.GetByIdAsync("1")).ReturnsAsync(patient);
            _patientRecordRepositoryMock.Setup(x => x.AddAsync(It.IsAny<PatientRecord>())).ReturnsAsync(record);

            // Act
            var result = await _patientRecordService.AddPatientRecord("1", addPatientRecordRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Successfully added patient data", result.Message);
        }

        [Fact]
        public async Task UpdatePatientRecord_ShouldUpdateRecord()
        {
            // Arrange
            var updatePatientRecordRequest = new UpdatePatientRecordRequest { Description = "Updated Record" };
            var record = new PatientRecord { Id = "1", Description = "Old Record", PatientId = "1" };

            _patientRecordRepositoryMock.Setup(x => x.GetByIdAsync("1")).ReturnsAsync(record);
            _patientRecordRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<PatientRecord>())).ReturnsAsync(record);

            // Act
            var result = await _patientRecordService.UpdatePatientRecord("1", updatePatientRecordRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Successfully updated record data", result.Message);
        }

        [Fact]
        public async Task GetById_ShouldReturnRecordResponse_WhenRecordExists()
        {
            // Arrange
            var record = new PatientRecord { Id = "1", Description = "Record 1", PatientId = "1" };
            var recordResponse = new PatientRecordResponse { Id = "1", Description = "Record 1" };

            _patientRecordRepositoryMock.Setup(x => x.GetByIdAsync("1")).ReturnsAsync(record);
            _mapperMock.Setup(x => x.Map<PatientRecordResponse>(record)).Returns(recordResponse);

            // Act
            var result = await _patientRecordService.GetById("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Record 1", result.Data.Description);
        }
    }
}
