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
    }
}