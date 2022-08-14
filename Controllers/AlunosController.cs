using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Alunos.Context;
using API_Alunos.Models;
using API_Alunos.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using API_Alunos.DTOs;
using AlunosApi.Pagination;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API_Alunos.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlunosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public AlunosController(IUnitOfWork contexto, IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;
        }


        // api/alunos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> Get([FromQuery] AlunosParamers alunosParamers)
        {
            var alunos = await _uof.AlunoRepository.GetAlunos(alunosParamers);

            var metadata = new
            {
                alunos.TotalCount,
                alunos.PageSize,
                alunos.CurrentPage,
                alunos.TotalPages,
                alunos.HasNext,
                alunos.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var alunosDto = _mapper.Map<List<AlunoDTO>>(alunos);
            return alunosDto;
        }

        // api/alunos/1
        [HttpGet("{id}", Name = "Obteraluno")]
        public async Task<ActionResult<AlunoDTO>> Get(int id)
        {
            var aluno = await _uof.AlunoRepository.GetById(p => p.Id == id);

            if (aluno == null)
            {
                return NotFound();
            }

            var alunoDto = _mapper.Map<AlunoDTO>(aluno);
            return alunoDto;
        }

        //  api/alunos
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Aluno aluno)
        {

            _uof.AlunoRepository.Add(aluno);
            await _uof.Commit();

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            return new CreatedAtRouteResult("Obteraluno",
                new { id = aluno.Id }, alunoDTO);
        }

        // api/alunos/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return BadRequest();
            }

            var Aluno = _mapper.Map<Aluno>(aluno);

            _uof.AlunoRepository.Update(aluno);
            await _uof.Commit();
            return Ok();
        }

        [HttpGet("AlunoPorNome")]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunoPorNome([FromQuery] string nome)
        {
            var alunos = await _uof.AlunoRepository.GetAlunoByName(nome);

            if (alunos == null)
                return NotFound($"Não existem alunos com nome = {nome}");

            return Ok(alunos);
        }
        //  api/alunos/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<AlunoDTO>> Delete(int id)
        {
            var aluno = await _uof.AlunoRepository.GetById(p => p.Id == id);

            if (aluno == null)
            {
                return NotFound();
            }

            _uof.AlunoRepository.Delete(aluno);
            await _uof.Commit();

            var alunoDto = _mapper.Map<AlunoDTO>(aluno);

            return alunoDto;
        }
    }
}
