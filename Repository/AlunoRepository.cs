
using AlunosApi.Pagination;
using AlunosApi.Repository.Interfaces;
using API_Alunos.Context;
using API_Alunos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Repository
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public async Task<PagedList<Aluno>> GetAlunos(AlunosParamers alunosParamers)
        {
            return await PagedList<Aluno>.ToPagedList(Get().OrderBy(on => on.Id),
            alunosParamers.PageNumber);
        }

        public async Task<IEnumerable<Aluno>> GetAlunoByName(string nome)
        {
            if (!string.IsNullOrWhiteSpace(nome))
            {
                var aluno = await Get().Where(aluno => aluno.Name.Contains(nome)).ToListAsync();
                return aluno;
            }
            else
            {
                var aluno = await Get().ToListAsync();
                return aluno;
            }
        }
    }
}
