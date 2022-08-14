
using AlunosApi.Pagination;
using API_Alunos.Models;
using System.Threading.Tasks;

namespace AlunosApi.Repository.Interfaces
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task<PagedList<Aluno>> GetAlunos(AlunosParamers alunosParamers);
        Task<IEnumerable<Aluno>> GetAlunoByName(string nome);
    }
}
