using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet__rpg.Services.SuperHeroService
{
    public interface ISuperHeroService
    {
        Task<ServiceResponse<List<GetSuperHeroDto>>> GetAllSuperHeros();
        Task<ServiceResponse<GetSuperHeroDto>> GetSuperHero(int id);
        Task<ServiceResponse<List<GetSuperHeroDto>>> CreateHero(AddSuperHeroDto addSuperHero);
        Task<ServiceResponse<GetSuperHeroDto>> UpdateSuperHero(AddSuperHeroDto addSuperHero);
        Task<ServiceResponse<List<GetSuperHeroDto>>> DeleteSuperHero(int id);
    }
}