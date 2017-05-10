﻿using Microsoft.AspNetCore.Mvc;
using System;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.AutoMapper;

namespace Tnf.Architecture.Web.Controllers
{
    [Route(RouteConsts.Professional)]
    public class ProfessionalController : ArchitectureControllerBase
    {
        private readonly IProfessionalAppService _professionalAppService;

        public ProfessionalController(IProfessionalAppService professionalAppService)
        {
            _professionalAppService = professionalAppService;
        }

        [HttpGet("")]
        public IActionResult Get(GetAllProfessionalsDto requestDto)
        {
            if (requestDto == null)
                return BadRequest($"Invalid parameter: {nameof(requestDto)}");

            if (requestDto.PageSize <= 0)
                return BadRequest($"Invalid parameter: {nameof(requestDto.PageSize)}");

            var response = _professionalAppService.GetAllProfessionals(requestDto);

            return Ok(response);
        }

        [HttpGet("{professionalId}/{code}")]
        public IActionResult Get(decimal professionalId, Guid code)
        {
            if (professionalId <= 0)
                return BadRequest($"Invalid parameter: {nameof(professionalId)}");

            if (code == Guid.Empty)
                return BadRequest($"Invalid parameter: {nameof(code)}");

            var result = _professionalAppService.GetProfessional(new ProfessionalKeysDto(professionalId, code));
            if (result == null)
                return NotFound(L("CouldNotFindProfessional"));

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProfessionalCreateDto professional)
        {
            if (professional == null)
                return BadRequest($"Invalid parameter: {nameof(professional)}");

            var result = _professionalAppService.CreateProfessional(professional.MapTo<ProfessionalCreateDto>());
            return Ok(result);
        }

        [HttpPut("{professionalId}/{code}")]
        public IActionResult Put(decimal professionalId, Guid code, [FromBody]ProfessionalUpdateDto professional)
        {
            if (professionalId <= 0)
                return BadRequest($"Invalid parameter: {nameof(professionalId)}");

            if (code == Guid.Empty)
                return BadRequest($"Invalid parameter: {nameof(code)}");

            if (professional == null)
                return BadRequest($"Invalid parameter: {nameof(professional)}");

            var updateParam = new ProfessionalDto();
            updateParam.ProfessionalId = professionalId;
            updateParam.Code = code;

            updateParam = professional.MapTo(updateParam);

            var result = _professionalAppService.UpdateProfessional(updateParam);
            if (result.Data == null)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{professionalId}/{code}")]
        public IActionResult Delete(decimal professionalId, Guid code)
        {
            if (professionalId <= 0)
                return BadRequest($"Invalid parameter: {nameof(professionalId)}");

            if (code == Guid.Empty)
                return BadRequest($"Invalid parameter: {nameof(code)}");

            var result = _professionalAppService.DeleteProfessional(new ProfessionalKeysDto(professionalId, code));
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}
