using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using dotnet__rpg.Data;

namespace dotnet__rpg.Services.SuperHeroService
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly IMapper _mapper;
        
        public readonly DapperContext _context ;
        public SuperHeroService(IMapper mapper,  DapperContext context )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetSuperHeroDto>>> GetAllSuperHeros()
        {
            var connection = _context.CreateConnection();
            var heroes = await connection.QueryAsync<GetSuperHeroDto>("select * from superheroes ");

            var serviceResponse = new ServiceResponse<List<GetSuperHeroDto>>
            {
                Data = heroes.Select(c => _mapper.Map<GetSuperHeroDto>(c)).ToList()
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSuperHeroDto>> GetSuperHero(int id)
        {
            var serviceResponse = new ServiceResponse<GetSuperHeroDto>();
            try
            {
                var connection = _context.CreateConnection();
                var hero = await connection.QueryFirstAsync<GetSuperHeroDto>("select * from superheroes where id = @Id ", new { Id = id });
          
                serviceResponse.Data = _mapper.Map<GetSuperHeroDto>(hero);
              
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSuperHeroDto>>> CreateHero(AddSuperHeroDto addSuperHero)
        {
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync("insert into superheroes (name, firstname, lastname, place) values(@Name, @FirstName, @LastName, @Place)", addSuperHero);
            var heroes = await connection.QueryAsync<GetSuperHeroDto>("select * from superheroes ");
            var serviceResponse = new ServiceResponse<List<GetSuperHeroDto>>
            {
                Data = heroes.Select(c => _mapper.Map<GetSuperHeroDto>(c)).ToList()
            };
            return serviceResponse;
            
        }

        public async Task<ServiceResponse<GetSuperHeroDto>> UpdateSuperHero(AddSuperHeroDto addSuperHero)
        {
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync("update superheroes set name = @Name, firstname = @FirstName, lastname = @LastName, place = @Place where id = @Id ", addSuperHero) ;
            var hero = await connection.QueryFirstAsync<GetSuperHeroDto>("select * from superheroes where id = @Id ", new { Id = addSuperHero.Id });
            var serviceResponse = new ServiceResponse<GetSuperHeroDto>
            {
                Data = _mapper.Map<GetSuperHeroDto>(hero)
            };
            return serviceResponse;
            
        }

        public async Task<ServiceResponse<List<GetSuperHeroDto>>> DeleteSuperHero(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetSuperHeroDto>>();
            try
            {
                var connection = _context.CreateConnection();
                await connection.ExecuteAsync("delete from superheroes where id = @Id ", new { Id = id });
                var heroes = await connection.QueryAsync<GetSuperHeroDto>("select * from superheroes ");
                serviceResponse.Data = heroes.Select(c => _mapper.Map<GetSuperHeroDto>(c)).ToList();
              
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }
    }
}