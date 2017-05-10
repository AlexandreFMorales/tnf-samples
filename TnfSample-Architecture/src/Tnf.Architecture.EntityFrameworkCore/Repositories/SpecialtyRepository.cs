﻿using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;
using Tnf.AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Tnf.Architecture.EntityFrameworkCore.Repositories
{
    public class SpecialtyRepository : EfCoreRepositoryBase<LegacyDbContext, SpecialtyPoco>, ISpecialtyRepository
    {
        public SpecialtyRepository(IDbContextProvider<LegacyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public SpecialtyDto CreateSpecialty(SpecialtyDto dto)
        {
            var dbEntity = dto.MapTo<SpecialtyPoco>();

            dto.Id = base.InsertAndGetId(dbEntity);

            return dto;
        }

        public bool DeleteSpecialty(int id)
        {
            var success = false;
            try
            {
                base.Delete(id);
            }
            catch (Exception)
            {
                //TODO: Estourar exceção quando não achar?
            }

            return success;
        }

        public PagingResponseDto<SpecialtyDto> GetAllSpecialties(GetAllSpecialtiesDto request)
        {
            var response = new PagingResponseDto<SpecialtyDto>();

            var dbQuery = GetAll()
                .Where(w => request.Description == null || w.Description.Contains(request.Description))
                .Skip(request.Offset)
                .Take(request.PageSize)
                .ToArray();

            response.Total = base.Count();
            response.Data = dbQuery.MapTo<List<SpecialtyDto>>();

            return response;
        }

        public SpecialtyDto GetSpecialty(int id)
        {
            SpecialtyDto specialty = null;
            try
            {
                var dbEntity = base.Get(id);
                specialty = dbEntity.MapTo<SpecialtyDto>();
            }
            catch (Exception)
            {
                //TODO: Estourar exceção quando não achar?
            }

            return specialty;
        }

        public SpecialtyDto UpdateSpecialty(SpecialtyDto dto)
        {
            var dbEntity = dto.MapTo<SpecialtyPoco>();

            base.Update(dbEntity);

            return dto;
        }
    }
}
