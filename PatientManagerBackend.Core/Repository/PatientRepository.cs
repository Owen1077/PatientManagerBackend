using PatientManagerBackend.Core.Contract.Repository;
using PatientManagerBackend.Core.DTO.Request;
using PatientManagerBackend.Core.Repository.Base;
using PatientManagerBackend.Domain.Common;
using PatientManagerBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using System;
using System.Threading;
using PatientManagerBackend.Core.Exceptions;
using PatientManagerBackend.Domain.QueryParameters;

namespace PatientManagerBackend.Core.Repository
{
    public class PatientRepository : RepositoryBase, IPatientRepository
    {
        public PatientRepository(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }

        public async Task<PagedResponse<List<Patient>>> GetAllAsync(UrlQueryParameters request)
        {

            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDatabaseContext(scope);

            int totalRecords = await dbContext.Patients.CountAsync();

            if (totalRecords <= 0)
            {
                throw new ApiException("No result.");
            }

            var result = await dbContext.Patients
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .Include(c => c.Records)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResponse<List<Patient>>(result, request.PageNumber, request.PageSize, totalRecords, "Data retrieved successfully.");


        }

        public async Task<Patient> GetByIdAsync(string id)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDatabaseContext(scope);

            var response = await dbContext.Patients
                .Where(x => x.Id == id)
                .Where(p => !p.IsDeleted)
                .Include(c => c.Records)
                .AsNoTracking()
                .FirstOrDefaultAsync() ?? throw new ApiException("Record not found");

            return response;
        }

        public async Task<Patient> AddAsync(Patient patient) 
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDatabaseContext(scope);

            var result = await dbContext.Patients.AddAsync(patient);

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                throw new ApiException("An error occured while saving");
            }
            

            return result.Entity;
        
        }
        public async Task<Patient> UpdateAsync(Patient patient) 
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDatabaseContext(scope);

            var result = dbContext.Patients.Update(patient);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException("An error occured while saving");
            }

            return result.Entity;
        
        
        }
        public async Task<bool> SoftDeleteAsync(string id)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDatabaseContext(scope);

            var patient = await dbContext.Patients
                .Where(x => x.Id == id)
                .Where(p => !p.IsDeleted)
                .FirstOrDefaultAsync() ?? throw new ApiException("Record not found");

            patient.IsDeleted = true;

            var result = dbContext.Patients.Update(patient);
            await dbContext.SaveChangesAsync();

            return true;
        }

       
    }
}
