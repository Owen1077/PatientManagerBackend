using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PatientManagerBackend.Core.Repository;
using PatientManagerBackend.Domain.Entities;
using PatientManagerBackend.Domain.QueryParameters;
using PatientManagerBackend.Core.Exceptions;
using PatientManagerBackend.Domain.Common;
using PatientManagerBackend.Persistence;
using System.Linq;
using System.Linq.Expressions;

namespace PatientManagerBackend.Tests
{
    public class PatientRepositoryTests
    {
        //private readonly Mock<IServiceScopeFactory> _serviceScopeFactoryMock;
        //private readonly Mock<IServiceScope> _serviceScopeMock;
        //private readonly Mock<ApplicationDbContext> _dbContextMock;
        //private readonly PatientRepository _patientRepository;

        //public PatientRepositoryTests()
        //{
        //    _serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
        //    _serviceScopeMock = new Mock<IServiceScope>();
        //    _dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

        //    _serviceScopeFactoryMock.Setup(x => x.CreateScope()).Returns(_serviceScopeMock.Object);
        //    _serviceScopeMock.Setup(x => x.ServiceProvider.GetService(typeof(ApplicationDbContext))).Returns(_dbContextMock.Object);

        //    _patientRepository = new PatientRepository(_serviceScopeFactoryMock.Object);
        //}

        //[Fact]
        //public async Task GetAllAsync_ShouldReturnPagedResponse()
        //{
        //    // Arrange
        //    var patients = new List<Patient> { new Patient { Id = "1", Name = "John Doe" } }.AsQueryable();
        //    var queryParameters = new UrlQueryParameters { PageNumber = 1, PageSize = 10 };

        //    var mockSet = new Mock<DbSet<Patient>>();
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.Provider);
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.Expression);
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

        //    _dbContextMock.Setup(x => x.Patients).Returns(mockSet.Object);

        //    // Act
        //    var result = await _patientRepository.GetAllAsync(queryParameters);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(1, result.Data.Count);
        //}

        //[Fact]
        //public async Task GetByIdAsync_ShouldReturnPatient()
        //{
        //    // Arrange
        //    var patient = new Patient { Id = "1", Name = "John Doe" };
        //    var patients = new List<Patient> { patient }.AsQueryable();

        //    var mockSet = new Mock<DbSet<Patient>>();
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.Provider);
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.Expression);
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

        //    _dbContextMock.Setup(x => x.Patients).Returns(mockSet.Object);

        //    // Act
        //    var result = await _patientRepository.GetByIdAsync("1");

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal("John Doe", result.Name);
        //}

        //[Fact]
        //public async Task AddAsync_ShouldAddPatient()
        //{
        //    // Arrange
        //    var patient = new Patient { Id = "1", Name = "John Doe" };

        //    var mockSet = new Mock<DbSet<Patient>>();
        //    _dbContextMock.Setup(x => x.Patients).Returns(mockSet.Object);
        //    _dbContextMock.Setup(x => x.Patients.AddAsync(patient, default)).ReturnsAsync((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Patient>)null);
        //    _dbContextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        //    // Act
        //    var result = await _patientRepository.AddAsync(patient);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal("John Doe", result.Name);
        //}

        //[Fact]
        //public async Task UpdateAsync_ShouldUpdatePatient()
        //{
        //    // Arrange
        //    var patient = new Patient { Id = "1", Name = "John Doe" };

        //    var mockSet = new Mock<DbSet<Patient>>();
        //    _dbContextMock.Setup(x => x.Patients).Returns(mockSet.Object);
        //    _dbContextMock.Setup(x => x.Patients.Update(patient)).Returns((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Patient>)null);
        //    _dbContextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        //    // Act
        //    var result = await _patientRepository.UpdateAsync(patient);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal("John Doe", result.Name);
        //}

        //[Fact]
        //public async Task SoftDeleteAsync_ShouldMarkPatientAsDeleted()
        //{
        //    // Arrange
        //    var patient = new Patient { Id = "1", Name = "John Doe", IsDeleted = false };
        //    var patients = new List<Patient> { patient }.AsQueryable();

        //    var mockSet = new Mock<DbSet<Patient>>();
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.Provider);
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.Expression);
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
        //    mockSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

        //    _dbContextMock.Setup(x => x.Patients).Returns(mockSet.Object);
        //    //_dbContextMock.Setup(x => x.Patients.Where(It.IsAny<Expression<Func<Patient, bool>>>()).FirstOrDefaultAsync()).ReturnsAsync(patient);
        //    _dbContextMock.Setup(x => x.Patients.Update(patient)).Returns((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Patient>)null);
        //    _dbContextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

        //    // Act
        //    var result = await _patientRepository.SoftDeleteAsync("1");

        //    // Assert
        //    Assert.True(result);
        //    Assert.True(patient.IsDeleted);
        //}
    }
}
