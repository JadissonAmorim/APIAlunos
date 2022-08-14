using AlunosApi.Repository.Interfaces;

namespace API_Alunos.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IAlunoRepository AlunoRepository { get; }
        Task Commit();

        void Dispose();
    }
}
