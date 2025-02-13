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
    public class PatientRecordRepository : RepositoryBase, IPatientRecordRepository
    {
        public PatientRecordRepository(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }

        public async Task<PagedResponse<List<PatientRecord>>> GetAllAsync(UrlQueryParameters request)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDatabaseContext(scope);

            int totalRecords = await dbContext.PatientRecords.CountAsync();

            if (totalRecords <= 0)
            {
                throw new ApiException("No result.");
            }

            var result = await dbContext.PatientRecords
                .AsNoTracking()
                .Where(c => !c.Patient.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResponse<List<PatientRecord>>(result, request.PageNumber, request.PageSize, totalRecords, "Data retrieved successfully.");

        }

        public async Task<PatientRecord> GetByIdAsync(string id)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDatabaseContext(scope);

            var response = await dbContext.PatientRecords
                .Where(x => x.Id == id)
                .Where(c => !c.Patient.IsDeleted)
                .FirstOrDefaultAsync() ?? throw new ApiException("Record not found");

            return response;
        }

        public async Task<PatientRecord> AddAsync(PatientRecord record) 
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDatabaseContext(scope);

            var result = await dbContext.PatientRecords.AddAsync(record);
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
        public async Task<PatientRecord> UpdateAsync(PatientRecord record) 
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = GetDatabaseContext(scope);

            var result = dbContext.PatientRecords.Update(record);
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

       
    }
}
