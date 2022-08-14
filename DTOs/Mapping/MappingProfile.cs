using API_Alunos.Models;
using AutoMapper;

namespace API_Alunos.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Aluno, AlunoDTO>().ReverseMap();
        }
    }
}
